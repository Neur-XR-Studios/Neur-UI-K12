using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Simulanis.ContentSDK.K12.Assessment
{
    public class SlingshotGame : MonoBehaviour
    {
        [Header("SlingShot Transform")]
        public GameObject SlingShot;
        [Header("Audio Settings")]
        public AudioSource effectAudioSource;
        public AudioClip pullSoundEffect;
        [Header("Projectile Settings")]
        public GameObject projectilePrefab;
        public GameObject pullObject;
        public Transform projectileSpawnPoint;
        public Transform pullLimitTransform;
        [Header("Target Settings")]
        public Transform[] targets;
        public Button[] optionButtons;
        public Button[] ClickableButtons;
        [Header("Particle Effect Settings")]
        public ParticleSystem targetEffectPrefab;
        public int pooledEffectsCount = 10;
        public float launchForce = 10f;
        public float pullSpeed = 5f;
        [Range(0.1f, 3f)]
        public float Lifetime = 0.5f;
        [Range(0.1f, 3f)]
        public float ReleaseDelay = 0.5f;
        private List<ParticleSystem> particlePool;
        private GameObject movingProjectile;
        private GameObject loadedProjectile;
        public bool isProjectileMoving = false;
        public bool isPulling = false;
        private Vector3 pullStartPosition;
        private Vector3 pullEndPosition;
        Vector3 direction;
        [Range(0.1f, 5f)]
        public float QuestionDelay = 0.5f;
        public AssessmentManager assessmentManager;
        private void Awake()
        {
            if(effectAudioSource is null)
            {
                effectAudioSource = GameObject.Find("Effect Sound").GetComponent<AudioSource>();
            }

            if(assessmentManager is null)
            {
                assessmentManager = FindObjectOfType<AssessmentManager>();
            }
        }
        private void Start()
        {
            pullStartPosition = pullObject.transform.position;
            pullEndPosition = pullLimitTransform.position;
            InitializeParticlePool();
            InitializeButtons();
            // Spawn the initial loaded projectile
            SpawnLoadedProjectile();
        }

        // Spawn a new loaded projectile (stationary) at the slingshot position
        private void SpawnLoadedProjectile()
        {
            // If a projectile is already moving, do not spawn another
            if (isProjectileMoving) return;
            // Create the loaded projectile at the slingshot's position
            loadedProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        }
        private void InitializeParticlePool()
        {
            particlePool = new List<ParticleSystem>();
            for (int i = 0; i < pooledEffectsCount; i++)
            {
                ParticleSystem particle = Instantiate(targetEffectPrefab);
                particle.gameObject.SetActive(false);
                particlePool.Add(particle);
            }
        }

        private void InitializeButtons()
        {
            for (int i = 0; i < optionButtons.Length; i++)
            {
                int targetIndex = i; // Local copy for lambda
                optionButtons[i].onClick.AddListener(() => StartPull(targetIndex));
            }
        }
        // Launch the loaded projectile to the selected target
        private void LaunchProjectile(int targetIndex)
        {

            if (isProjectileMoving || loadedProjectile == null) return;

            isProjectileMoving = true;

            movingProjectile = Instantiate(projectilePrefab, loadedProjectile.transform.position, Quaternion.identity);
            Rigidbody rb = movingProjectile.GetComponent<Rigidbody>();
            direction = (targets[targetIndex].position - projectileSpawnPoint.position).normalized;
            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            StartCoroutine(HandleProjectileHit(targetIndex, movingProjectile));
            Destroy(loadedProjectile);
        }

        private IEnumerator HandleProjectileHit(int targetIndex, GameObject projectile)
        {
            while (projectile != null && Vector3.Distance(projectile.transform.position, targets[targetIndex].position) > 1f)
            {
                yield return null;
            }

            if (projectile != null)
            {
                PlayParticleEffectAt(targets[targetIndex].position);
                Destroy(projectile);
                yield return new WaitForSeconds(QuestionDelay);
               assessmentManager.NextButtonHandler();
                DisableButtons(true);
            }
            isProjectileMoving = false;
            SpawnLoadedProjectile();
        }


        private void PlayParticleEffectAt(Vector3 position)
        {
            ParticleSystem particle = GetAvailableParticle();
            if (particle != null)
            {
                particle.transform.position = position;
                particle.gameObject.SetActive(true);
                particle.Play();
                StartCoroutine(DeactivateParticleAfterLifetime(particle));
            }
        }

        private ParticleSystem GetAvailableParticle()
        {
            foreach (var particle in particlePool)
            {
                if (!particle.isPlaying)
                {
                    return particle;
                }
            }
            return null;
        }

        private IEnumerator DeactivateParticleAfterLifetime(ParticleSystem particle)
        {
            yield return new WaitForSeconds(Lifetime);
            particle.Stop();
            particle.gameObject.SetActive(false);
        }

        // Begin the pull action
        public void StartPull(int targetIndex)
        {
           if (loadedProjectile == null && !isPulling) return;

             isPulling = true;
            DisableButtons(false);
            pullObject.transform.position = pullStartPosition;
            loadedProjectile.transform.position = projectileSpawnPoint.position;
            RotateSlingshotTowards(targetIndex);
            StartCoroutine(PullProjectile(targetIndex));
        }
        // Coroutine to handle the pull and release sequence
        private IEnumerator PullProjectile(int targetIndex)
        {
            yield return AnimatePull();
            yield return AnimateRelease();
            LaunchProjectile(targetIndex);
        }
        private void RotateSlingshotTowards(int targetIndex)
        {
            Vector3 direction = (targets[targetIndex].position - projectileSpawnPoint.position).normalized;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            SlingShot.transform.rotation = Quaternion.Euler(0f, angle + 180f, 0f);



            pullStartPosition = pullObject.transform.position;
            pullEndPosition = pullLimitTransform.position;
        }

        void PullAudio(AudioClip audioClip)
        {
            if (audioClip == null) return;
            effectAudioSource.PlayOneShot(audioClip);
        }

        private IEnumerator AnimatePull()
        {

            PullAudio(pullSoundEffect);
            // Smoothly move the projectile to the pull limit
            float pullProgress = 0f;
            while (pullProgress < ReleaseDelay)
            {
                pullProgress += Time.deltaTime * pullSpeed;
                pullObject.transform.position = Vector3.Lerp(pullStartPosition, pullEndPosition, pullProgress);
                loadedProjectile.transform.position = Vector3.Lerp(loadedProjectile.transform.position, projectileSpawnPoint.position, pullProgress);
                yield return null;
            }

        }

        private IEnumerator AnimateRelease()
        {
            // Smoothly move the projectile to the pull limit
            float pullProgress = 0f;
            while (pullProgress < ReleaseDelay)
            {
                pullProgress += Time.deltaTime * pullSpeed;
                pullObject.transform.position = Vector3.Lerp(pullEndPosition, pullStartPosition, pullProgress);
                loadedProjectile.transform.position = Vector3.Lerp(loadedProjectile.transform.position, projectileSpawnPoint.position, pullProgress*2.5f);
                yield return null;
            }
            isPulling = false;            
        }

        private void DisableButtons(bool value)
        {
            foreach(Button button in optionButtons)
            {
                button.enabled = value; 
            }
            foreach (Button button in ClickableButtons)
            {
                button.enabled = value;
            }

        }

        private void OnDisable()
        {
            foreach (ParticleSystem obj in particlePool)
            {
                Destroy(obj);
            }
            Destroy(movingProjectile);
            Destroy(loadedProjectile);

        }

    }
}
