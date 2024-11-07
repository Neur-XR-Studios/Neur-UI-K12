using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PrefabSpawnControllerScript : MonoBehaviour
{
    #region Headers
    [Space(10)]
    [Header("---------------CF OBJECT INSTANCE---------------")]
    [Space(10)]
    public GameObject[] CF_Objects; // Array of GameObjects where prefabs should spawn    
    [Space(10)]
    public GameObject DevParent_Object; // Parent of all development objects in scene
    public GameObject CFParent_Object; // Object refernce for making prefab as child of it
    public GameObject CfUi_Object; // Turn off it when CF button clicked
    [Space(10)]
    public Button CF_Button;

    private bool IsActivity = true; // To find activity scene or CF scene     
    #endregion

    #region Initialization
    void Start()
    {
        Initialisation();
    }

    void Initialisation()
    {
        CfUi_Object.SetActive(false);
        CF_Button.onClick.AddListener(CFButtonClickHandler);
        DevParent_Object.SetActive(false);
        CFParent_Object.SetActive(false);
    }
    #endregion

    #region CF Menu Preparation
    void InitialiseCF()
    {
        StartCoroutine(PrepareCF());
    }

    IEnumerator PrepareCF()
    {
        string objectNameCSV =     DataManager.StaticVariables.COLUMN_06; //Object names combined with commas string
        string objectInfoCSV =     DataManager.StaticVariables.COLUMN_07; //ObjectInfo names combined with commas string
        string objectIconNameCSV = DataManager.StaticVariables.COLUMN_08; //Object icon names combined with commas string

        List<string> objectNames = new List<string>();
        List<string> objectInfos = new List<string>();
        List<string> objectIconNames = new List<string>();        

        objectNames.AddRange(objectNameCSV.Split(','));
        objectInfos.AddRange(objectInfoCSV.Split('*'));
        objectIconNames.AddRange(objectIconNameCSV.Split(','));

        UIAutomationController.objectNames = objectNames;
        UIAutomationController.objectInfos = objectInfos;
        UIAutomationController.objectIconNames = objectIconNames;

        for (int i = 0; i < CF_Objects.Length; i++)
        {
            GameObject cfObject = CF_Objects[i];
            cfObject.AddComponent<CFManager>();
            TMP_Text textComponents = cfObject.GetComponentInChildren<TMP_Text>();
            textComponents.text = objectNames[i];
        }
        yield return null; // Wait for the next frame to reduce load
    }
    #endregion
    #region CF Handler
    public void CFButtonClickHandler()
    {        
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();// Find all AudioSource components in the scene
        
        foreach (AudioSource audioSource in allAudioSources) // Loop through and stop each AudioSource
        {
            audioSource.Stop();
        }

        if (IsActivity)
        {
            DevParent_Object.SetActive(false);
            CFParent_Object.SetActive(true);
            IsActivity = false;
            VoiceoverControllerScript.isActivity = false;
        }
        else
        {
            DevParent_Object.SetActive(true);
            CFParent_Object.SetActive(false);
            CfUi_Object.SetActive(false);
            IsActivity = true;
            VoiceoverControllerScript.isActivity = true;
        }
    }
    #endregion

    private void OnEnable()
    {
        EventManager.AddHandler(EVENTS.SPAWN, InitialiseCF);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(EVENTS.SPAWN, InitialiseCF);
    }
}
