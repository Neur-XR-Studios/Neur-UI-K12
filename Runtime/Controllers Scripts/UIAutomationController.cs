using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
namespace K12.UI
{
    public class UIAutomationController : MonoBehaviour
    {
        #region Headers
        [Space(10)]
        [Header("----------------------CAMERA REFERENCEs--------------------")]
        [Space(10)]
        private Camera MainCamera;
        [Space(10)]
        private Camera CFCamera;
        [Space(10)]
        [Header("----------------------UI REFERENCES--------------------")]
        [Space(10)]
        [Header("Landing Menu")]
        [Space(10)]
        public GameObject LandingMenu_Obj;
        [Space(10)]
        public TMP_Text MainInfo_Text;
        [Space(10)]
        public Image Icon_Img;
        [Space(20)]
        [Header("Activity Page")]
        [Space(10)]
        public GameObject ActivityMenu_Obj;
        [Space(10)]
        public TMP_Text ActivityHeading_Text;
        [Space(20)]
        [Header("Welcome Page")]
        [Space(10)]
        public GameObject WelcomeMenu_Obj;
        [Space(10)]
        public TMP_Text WelcomeHeading_Text;
        [Space(10)]
        public TMP_Text Welcome_Text;
        [Space(10)]
        public Image WelcomePageIcon_Img;
        [Space(20)]
        [Header("CF Page")]
        [Space(10)]
        public GameObject CfMenu_Obj;
        [Space(10)]
        public TMP_Text ObjectInfo_Text;
        [Space(10)]
        public Image ObjectIcon_Img;
        [Space(20)]
        [Header("Prompt Menu")]
        [Space(10)]
        public GameObject PromptMenu_Obj;
        [Space(10)]
        public TMP_Text PromptInfo_Text;
        [Space(10)]
        public Button Ok_button;
        [Space(20)]
        [Header("Step Content Scroll")]
        [Space(10)]
        public GameObject mainStepContainer;
        [Space(10)]
        public GameObject subStepContainer;
        [Space(10)]
        public Transform StepContainer;
        [Space(10)]
        public TMP_Text StepInfo_Text;
        [Space(10)]
        public TMP_Text MainStepTittle;
        [Space(10)]
        public TMP_Text subStepTittle;
        [Space(20)]
        [Header("Conclusion panel")]
        [Space(10)]
        public GameObject Conclusion_Panel;
        [Space(10)]
        public TMP_Text Conclusion_Text;
        [Space(20)]
        [Header("Step Content Scroll")]
        [Space(10)]
        private List<GameObject> subSteps;
        [Space(10)]
        [Header("---------------CF OBJECT INSTANCE---------------")]
        [Space(10)]
        public GameObject[] CF_Objects; // Array of GameObjects where prefabs should spawn    
        [Space(10)]
        public GameObject DevParent_Object; // Parent of all development objects in scene
        [Space(10)]
        public GameObject CFParent_Object; // Object refernce for making prefab as child of it
        [Space(10)]
        public Button CF_Button;


        private bool IsActivity = true; // To find activity scene or CF scene 
        public static bool IsPromptEnabled = false; // To find activity scene or CF scene 
        private bool Is_CF_Holded = false; // To freeze CF during animations and camera movements
        [HideInInspector]
        public static bool IsConlusionStepHasPrompt = false; // To know this is the prompt of conclution step to prevent moving to next step.
                                                             //private string ModuleName = string.Empty;

        public static List<string> Colunm_03 = new(); // main step content in the CSV
        public static List<string> Colunm_04 = new();// sub step content in the CSV

        public static List<string> objectNames = new();
        public static List<string> objectInfos = new();
        public static List<string> objectIconNames = new();
        #endregion

        private enum PANEL_TYPE
        {
            LANDING,
            WELCOME,
            ACTIVITY
        }
        private PANEL_TYPE PanelType;

        #region Initialization
        private void Start()
        {
            Initialisation();
        }
        void Initialisation()
        {
            IsActivity = true;
            IsPromptEnabled = false;
            Is_CF_Holded = false;
            IsConlusionStepHasPrompt = false;

            Time.timeScale = 1; // Resume the game
            // Ensure only one camera is active at the start
            CFCamera = GameObject.FindWithTag("CFCamera").GetComponent<Camera>();
            MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            MainCamera.enabled = IsActivity;
            CFCamera.enabled = !IsActivity;

            PanelType = PANEL_TYPE.LANDING;
            subSteps = new();
            subSteps.Clear();
            EnableLandingMenu();
            Colunm_03.Clear();
            Colunm_04.Clear();

            objectNames.Clear();
            objectInfos.Clear();
            objectIconNames.Clear();


            DevParent_Object.SetActive(false);
            CFParent_Object.SetActive(false);

            Ok_button.onClick.AddListener(OkButtonClickHandler);
            CF_Button.onClick.AddListener(CFButtonClickHandler);
            MainInfo_Text.text = string.Empty;
            ActivityHeading_Text.text = string.Empty;
        }
        #endregion

        #region UI Controller
        void EnableLandingMenu()
        {
            ResetAllMenu();
            LandingMenu_Obj.SetActive(true);
        }
        void EnableActivityMenu()
        {
            ResetAllMenu();
            ActivityMenu_Obj.SetActive(true);
        }
        void EnableWelcomeMenu()
        {
            ResetAllMenu();
            WelcomeMenu_Obj.SetActive(true);
        }
        void EnableCFMenu()
        {
            ResetAllMenu();
            CfMenu_Obj.SetActive(true);
        }
        void EnablePromptMenu()
        {
            IsPromptEnabled = true;
            PromptMenu_Obj.SetActive(true);
            PromptInfo_Text.text = DataManager.StaticVariables.COLUMN_02;
        }
        void EnableConclusionPanel()
        {
            if (IsConlusionStepHasPrompt)
            {
                EnablePromptMenu();
            }
            else
            {
                ResetAllMenu();
                Conclusion_Panel.SetActive(true);
                Conclusion_Text.text = DataManager.StaticVariables.COLUMN_04;
            }
        }
        void ResetAllMenu()
        {
            WelcomeMenu_Obj.SetActive(false);
            LandingMenu_Obj.SetActive(false);
            ActivityMenu_Obj.SetActive(false);
            CfMenu_Obj.SetActive(false);
            LandingMenu_Obj.SetActive(false);
        }
        #endregion

        #region UI Content Updation
        void UpdateText()
        {
            MainStepContentChanger();
            switch (PanelType)
            {
                case PANEL_TYPE.LANDING:
                    {
                        UpdateMainMenuText();
                        UpdateStepDataMenuText();
                        PanelType = PANEL_TYPE.WELCOME;
                        return;
                    }
                case PANEL_TYPE.WELCOME:
                    {
                        UpdateWelcomeMenu();
                        UpdateActivityMenuText();
                        PanelType = PANEL_TYPE.ACTIVITY;
                        return;
                    }
                case PANEL_TYPE.ACTIVITY:
                    {
                        UpdateActivityMenuText();
                        return;
                    }
            }
        }
        void UpdateMainMenuText()
        {
            //Update text
            string content = DataManager.StaticVariables.COLUMN_04;
            MainInfo_Text.text = content;
            ActivityHeading_Text.text = DataManager.StaticVariables.COLUMN_02;

            //Update icon image
            Icon_Img.sprite = Resources.Load<Sprite>("Images/Landing_Image");
        }
        void UpdateWelcomeMenu()
        {
            string headContent = DataManager.StaticVariables.COLUMN_03;
            string content = DataManager.StaticVariables.COLUMN_04;
            WelcomeHeading_Text.text = headContent;
            Welcome_Text.text = content;
            //WelcomePageIcon_Img.sprite = Resources.Load<Sprite>("Images/Welcome_Icon");
            EnableWelcomeMenu();
        }
        void UpdateActivityMenuText()
        {
            string mainstep = DataManager.StaticVariables.COLUMN_03;
            if (mainstep != string.Empty)
            {
                MainStepTittle.text = mainstep.ToUpper();

            }
            int stepcount = DataManager.StaticVariables.STEP_COUNT - 2;
            StepInfo_Text.text = (stepcount + 2) + "/" + subSteps.Count;
            if (stepcount >= 0)
            {
                GameObject substepCont = subSteps[stepcount];
                Image tickImg = substepCont.transform.GetChild(2).GetComponent<Image>();
                tickImg.enabled = true;
            }
        }
        void MainStepContentChanger()
        {
            int stepcount = DataManager.StaticVariables.STEP_COUNT - 1;
            if (stepcount < 0) { stepcount = 0; }
            string mainstep = Colunm_03[stepcount];
            if (mainstep != string.Empty)
            {
                MainStepTittle.text = mainstep.ToUpper();
            }
            string substep = Colunm_04[stepcount];
            if (substep != string.Empty)
            {
                subStepTittle.text = substep;
            }
        }
        void UpdateCFObjectInfo_UI()
        {
            CfMenu_Obj.SetActive(true);
            string objectName = DataManager.StaticVariables.CF_OBJECT_NAME;
            int index = objectNames.IndexOf(objectName);
            ObjectInfo_Text.text = objectInfos[index];
            ObjectIcon_Img.sprite = Resources.Load<Sprite>($"Images/{objectIconNames[index].Trim()}"); //Update icon image
        }
        void UpdateCFContent()
        {
            StartCoroutine(PrepareCF());
        }
        IEnumerator PrepareCF()
        {
            string objectNameCSV = DataManager.StaticVariables.COLUMN_06; //Object names combined with commas string
            string objectInfoCSV = DataManager.StaticVariables.COLUMN_07; //ObjectInfo names combined with commas string
            string objectIconNameCSV = DataManager.StaticVariables.COLUMN_08; //Object icon names combined with commas string

            objectNames.AddRange(objectNameCSV.Split(','));
            objectInfos.AddRange(objectInfoCSV.Split('*'));
            objectIconNames.AddRange(objectIconNameCSV.Split(','));

            for (int i = 0; i < CF_Objects.Length; i++)
            {
                GameObject cfObject = CF_Objects[i];
                cfObject.AddComponent<CFManager>();
                TMP_Text textComponents = cfObject.GetComponentInChildren<TMP_Text>();
                textComponents.text = objectNames[i];
            }
            yield return null; // Wait for the next frame to reduce load
        }
        void UpdateStepDataMenuText()
        {
            // Ensure both lists have the same count
            if (Colunm_03.Count == Colunm_04.Count)
            {
                for (int i = 0; i < Colunm_03.Count; i++)
                {
                    // Instantiate Prefab03 if Colunm_03 entry is not empty
                    if (!string.IsNullOrEmpty(Colunm_03[i]))
                    {
                        GameObject mainStepObj = Instantiate(mainStepContainer, Vector3.zero, Quaternion.identity, StepContainer);
                        mainStepObj.name = "mainStep_" + i;  // Optional: Assign a name to the instance
                                                             // Additional customization or setup for obj03 can be done here
                        Transform contenttextObj = mainStepObj.transform.GetChild(0); // Use parentheses, not brackets

                        // Optionally set the text of the textObj if it's a TMP_Text or Text component
                        TMP_Text textComponent = contenttextObj.GetComponent<TMP_Text>();
                        if (textComponent != null)
                        {
                            textComponent.text = Colunm_03[i].ToUpper(); // Set the text to the current step
                        }
                        else
                        {
                            Debug.LogWarning("Text component not found on the child object.");
                        }
                    }

                    // Instantiate Prefab04 if Colunm_04 entry is not empty
                    if (!string.IsNullOrEmpty(Colunm_04[i]))
                    {
                        GameObject subStepObj = Instantiate(subStepContainer, Vector3.zero, Quaternion.identity, StepContainer);
                        subStepObj.name = "subStep_" + i;  // Optional: Assign a name to the instance
                        subSteps.Add(subStepObj);
                        Transform stepCont = subStepObj.transform.GetChild(1); // Use parentheses, not brackets
                        TMP_Text stepContText = stepCont.GetComponent<TMP_Text>();
                        Transform stepcount = subStepObj.transform.GetChild(0).GetChild(0); // Use parentheses, not brackets
                        TMP_Text countText = stepcount.GetComponent<TMP_Text>();
                        if (stepContText != null)
                        {
                            stepContText.text = Colunm_04[i]; // Set the text to the current step
                            countText.text = (1 + i).ToString() + ".";
                        }
                        else
                        {
                            Debug.LogWarning("Text component not found on the child object.");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Colunm_03 and Colunm_04 do not have the same count!");
            }
        }
        #endregion

        #region Others
        void CFButtonClickHandler()
        {
            if (!Is_CF_Holded && !IsPromptEnabled)
            {
                AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();// Find all AudioSource components in the scene
                if (IsActivity)
                {

                    foreach (AudioSource audioSource in allAudioSources) // Loop through and stop each AudioSource
                    {
                        audioSource.Pause();
                    }

                    //DevParent_Object.SetActive(false);
                    CFParent_Object.SetActive(true);

                    IsActivity = false;
                    VoiceoverControllerScript.isActivity = false;

                    MainCamera.enabled = IsActivity; //disable main camera
                    CFCamera.enabled = !IsActivity; //enable CF camera
                    Time.timeScale = 0; // Pauses the game
                }
                else
                {

                    foreach (AudioSource audioSource in allAudioSources) // Loop through and stop each AudioSource
                    {
                        audioSource.Play();
                    }

                    //DevParent_Object.SetActive(true);
                    CFParent_Object.SetActive(false);
                    CfMenu_Obj.SetActive(false);
                    IsActivity = true;
                    VoiceoverControllerScript.isActivity = true;

                    MainCamera.enabled = IsActivity;
                    CFCamera.enabled = !IsActivity;
                    Time.timeScale = 1; // Resume the game
                }
            }


        }
        void OkButtonClickHandler()
        {
            IsPromptEnabled = false;
            if (IsConlusionStepHasPrompt)
            {
                IsConlusionStepHasPrompt = false;
                EnableConclusionPanel();
                EventManager.Broadcast(EVENTS.STEP);
            }
            else
            {
                PromptMenu_Obj.SetActive(false);
                EventManager.Broadcast(EVENTS.STEP);
            }

        }
        #endregion

        #region Public Function
        public void Quit()
        {
#if UNITY_EDITOR
            // Quit play mode in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Quit the application on all other platforms
            Application.Quit();
#endif
        }
        public void ChangeScene()
        {
            SceneManager.LoadScene(0);
        }

        public void Hold_CF()
        {
            Is_CF_Holded = true;
        }
        public void UnHold_CF()
        {
            Is_CF_Holded = false;
        }
        #endregion

        #region Event Subscription Handler

        private void OnEnable() //subribed on SPAW event
        {
            EventManager.AddHandler(EVENTS.UPDATE_UI, UpdateText);
            EventManager.AddHandler(EVENTS.UPDATE_CF_UI, UpdateCFObjectInfo_UI);
            EventManager.AddHandler(EVENTS.PROMPT, EnablePromptMenu);
            EventManager.AddHandler(EVENTS.ENABLE_FINAL_UI, EnableConclusionPanel);
            EventManager.AddHandler(EVENTS.SPAWN, UpdateCFContent);
        }

        private void OnDisable() //unsubribed from SPAW event
        {
            EventManager.RemoveHandler(EVENTS.UPDATE_UI, UpdateText);
            EventManager.RemoveHandler(EVENTS.UPDATE_CF_UI, UpdateCFObjectInfo_UI);
            EventManager.RemoveHandler(EVENTS.PROMPT, EnablePromptMenu);
            EventManager.RemoveHandler(EVENTS.ENABLE_FINAL_UI, EnableConclusionPanel);
            EventManager.RemoveHandler(EVENTS.SPAWN, UpdateCFContent);

            Time.timeScale = 1; // Resume the game
        }
        #endregion
    }

}
