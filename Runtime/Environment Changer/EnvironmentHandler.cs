using Simulanis.ContentSDK.K12.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public enum Subject
    {
        Science,
        SocialScience,
        Mathematic
    }
    [Space(20)]
    [Header("----------------- Change Rooms------------------")]
    [Space(20)]
    public Subject currentSubject = Subject.Science;
    [Space(20)]
    [Header("----------------- Activity Rooms------------------")]
    [Space(20)]
    public GameObject MathEnv;
    public GameObject SocialEnv;
    public GameObject SciEnv;
    private List<GameObject> Environments = new List<GameObject>();
    [Space(20)]
    [Header("----------------- Activity Rooms Board------------------")]
    [Space(20)]
    public List<GameObject> MathEnvBoard;
    public List<GameObject> SocialEnvBoard;
    public List<GameObject> SciEnvBoard;
    public List<GameObject> AssessEnvBoard;
    [Space(20)]
    [Header("----------------- CF Board Object------------------")]
    [Space(20)]
    public GameObject BoardBorder;
    public GameObject BoardScreen;
    public GameObject Duster;
    private List<GameObject> CFEnvironments = new List<GameObject>();
    [Space(20)]
    [Header("----------------- Board Materials English------------------")]
    [Space(20)]
    public Material MathEnvMat;
    public Material SocialEnvMat;
    public Material SciEnvMat;
    public Material AssessEnvMat;
    [Space(20)]
    [Header("----------------- Board Materials Hindi------------------")]
    [Space(20)]
    public Material MathEnvMatHin;
    public Material SocialEnvMatHin;
    public Material SciEnvMatHin;
    public Material AssessMatHin;
    [Space(20)]
    [Header("-----------------Background sounds------------------")]
    [Space(20)]
    public AudioClip MathClip;
    public AudioClip SciClip;
    public AudioClip SocialClip;
    [Space(20)]
    [Header("-----------------Audio Source------------------")]
    [Space(20)]
    public AudioSource BGSource;

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {
        ChangeEnvironment(currentSubject);
    }
    private void Initialize()
    {
        // Environment setup

        if (MathEnv != null)
        {
            Environments.Add(MathEnv);
        }
        if (SciEnv != null)
        {
            Environments.Add(SciEnv);
        }
        if (SocialEnv != null)
        {
            Environments.Add(SocialEnv);
        }

        // CF Environment setup
        if (BoardBorder != null)
        {
            CFEnvironments.Add(BoardBorder);
        }
        if (Duster != null)
        {
            CFEnvironments.Add(Duster);
        }
        if (BoardScreen != null)
        {
            CFEnvironments.Add(BoardScreen);
        }
    }
    public void ChangeEnvironment(Subject sub)
    {
        switch (sub)
        {
            case Subject.Science:
                SetEnv(SciEnv, SciEnvMat, SciEnvMatHin, SciEnvBoard);
                SetCFEnv(SciEnvMat,SciEnvMatHin);
                SetAudioClip(SciClip);
                break;

            case Subject.Mathematic:
                SetEnv(MathEnv, MathEnvMat, MathEnvMatHin,MathEnvBoard);
                SetCFEnv(MathEnvMat,MathEnvMatHin);
                SetAudioClip(MathClip);
                break;

            case Subject.SocialScience:
                SetEnv(SocialEnv, SocialEnvMat, SocialEnvMatHin, SocialEnvBoard);
                SetCFEnv(SocialEnvMat,SocialEnvMatHin);
                SetAudioClip(SocialClip);
                break;

            default:
                Debug.LogWarning("Invalid Subject Selected!");
                break;
        }
    }

    public void SetAudioClip(AudioClip clip)
    {
        if (clip != null)
        {
            BGSource.clip = clip;
            BGSource.Play();
            BGSource.loop = true;
        }
        else
        {
            Debug.LogWarning("No AudioClip assigned for this environment!");
        }
    }

    public void SetEnv(GameObject env, Material MatEng, Material MatHind,List<GameObject> gameObjects)
    {
        foreach (GameObject room in Environments)
        {
            room.SetActive(room == env);
        }
        bool isEnglish = DataManager.StaticVariables.IS_ENGLISH;
        Material board = isEnglish ? MatEng : MatHind;
        foreach (GameObject room in gameObjects)
        {
            room.GetComponent<Renderer>().material = board;
        }
    }
    public void SetCFEnv(Material MatEng, Material MatHind)
    {

        bool isEnglish = DataManager.StaticVariables.IS_ENGLISH;
        Material board = isEnglish ? MatEng : MatHind;
        foreach (GameObject room in CFEnvironments)
        {
            room.GetComponent<Renderer>().material = board;
        }
        SetAssessEnv();
    }

    public void SetAssessEnv()
    {

        bool isEnglish = DataManager.StaticVariables.IS_ENGLISH;
        Material board = isEnglish ? AssessEnvMat : AssessMatHin;
        foreach (GameObject room in AssessEnvBoard)
        {
            room.GetComponent<Renderer>().material = board;
        }
    }
}
