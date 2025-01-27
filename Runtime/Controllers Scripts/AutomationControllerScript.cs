using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Simulanis.ContentSDK;
using Simulanis.ContentSDK.K12.HindiFontReplacer;
//using UnityEditor.PackageManager.Requests;
namespace Simulanis.ContentSDK.K12.UI
{

    public class AutomationControllerScript : MonoBehaviour
    {
        public CharReplacerHindi CharReplacerHindi;
        CsvTable table;
        //readonly string HindiCsv_url = Application.streamingAssetsPath + "/csvData_Hi.csv";
        //readonly string EnglishCsv_url = Application.streamingAssetsPath + "/csvData_Eng.csv";
        private int CurrentCount = 1;
        //private int TotalStep = 0;
        private bool IsConclusionStep = false;
        private string CsvName = string.Empty;

        #region Initialization
        void Start()
        {
            Initialization();
        }

        void Initialization()
        {
            string url = string.Empty;
            CsvHelper.Init();
            string lan = LanguageSelectionManager.CurrentLanguage;
            //lan = "Hindi";
            if (lan == "English")
            {
                DataManager.StaticVariables.IS_ENGLISH = true;
                //url = EnglishCsv_url;
                CsvName = "csvData_Eng";
                EventManager.Broadcast(EVENTS.CHANGE_LANGUAGE);
            }
            else if (lan == "Hindi")
            {
                DataManager.StaticVariables.IS_ENGLISH = false;
                //url = HindiCsv_url;
                CsvName = "csvData_Hi";
                EventManager.Broadcast(EVENTS.CHANGE_LANGUAGE);
            }
            LoadCsvFromResources();
            //StartCoroutine(LoadStreamingAsset(url));

        }
        #endregion

        #region Load CSV
        void LoadCsvFromResources()
        {
            TextAsset text = Resources.Load<TextAsset>(CsvName);
            table = CsvHelper.Create(text.name, text.text);
            //TotalStep = table.RowCount;

            parseColumnData();
            ParseRowData();
        }

        /*IEnumerator LoadStreamingAsset(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string csvData = request.downloadHandler.text;
                TextAsset text = new TextAsset(csvData);
                table = CsvHelper.Create(text.name, text.text);
                //TotalStep = table.RowCount;
            }
            else
            {
                Debug.LogError("Failed to load file: " + request.error);
            }
            parseColumnData();
            ParseRowData();
        }*/
        #endregion

        #region CSV Reader
        void parseColumnData()
        {
            int rowCount = 0;
            foreach (var row in table.RawDataList)
            {
                if (rowCount > 1)
                {
                    int count = 0;
                    foreach (var value in row)
                    {
                        switch (count)
                        {
                            case 3: UIAutomationController.Colunm_03.Add(value); break;
                            case 4: UIAutomationController.Colunm_04.Add(value); break;
                            default: break;
                        }
                        count++;
                    }
                }
                rowCount++;
            }
        }

        void ParseRowData()
        {
            int Row = 0;
            foreach (var row in table.RawDataList)
            {
                if (Row == CurrentCount)
                {
                    int Column = 0;
                    foreach (var value in row)
                    {
                        switch (Column)
                        {
                            case 0: DataManager.StaticVariables.COLUMN_00 = value; break;
                            case 1: DataManager.StaticVariables.COLUMN_01 = value; break;
                            case 2: DataManager.StaticVariables.COLUMN_02 = value; break;
                            case 3: DataManager.StaticVariables.COLUMN_03 = value; break;
                            case 4: DataManager.StaticVariables.COLUMN_04 = value; break;
                            case 5: DataManager.StaticVariables.COLUMN_05 = value; break;
                            case 6: DataManager.StaticVariables.COLUMN_06 = value; break;
                            case 7: DataManager.StaticVariables.COLUMN_07 = value; break;
                            case 8: DataManager.StaticVariables.COLUMN_08 = value; break;
                            case 9: DataManager.StaticVariables.COLUMN_09 = value; break;
                            case 10: DataManager.StaticVariables.COLUMN_10 = value; break;
                            case 11: DataManager.StaticVariables.COLUMN_11 = value; break;
                            default: break;
                        }
                        Column++;
                    }
                }
                Row++;
            }
            DataManager.StaticVariables.STEP_COUNT = CurrentCount - 1;
            //Debug.Log(DataManager.StaticVariables.STEP_COUNT + "Current Step");
            CurrentCount++;
            StepInitialisation();
        }
        #endregion

        #region private functions
        void PromptHandler()
        {
            string prompt = DataManager.StaticVariables.COLUMN_02;
            if (prompt == string.Empty || prompt == "")
            {
                EventManager.Broadcast(EVENTS.STEP);
                //EventManager.Broadcast(EVENTS.UPDATE_UI);
            }
            else
            {
                EventManager.Broadcast(EVENTS.PROMPT);
            }
        }

        void StepInitialisation()
        {
            if (!IsConclusionStep)
            {
                string stepType = DataManager.StaticVariables.COLUMN_01;
                EventManager.Broadcast(EVENTS.UPDATE_UI);
                switch (stepType.ToLower())
                {
                    case "intro": EventManager.Broadcast(EVENTS.SPAWN); return;
                    case "con":
                        string prompt = DataManager.StaticVariables.COLUMN_02;
                        if (!(prompt == string.Empty || prompt == ""))
                        {
                            UIAutomationController.IsConlusionStepHasPrompt = true;
                            EventManager.Broadcast(EVENTS.ENABLE_FINAL_UI);
                        }
                        else
                        {
                            EventManager.Broadcast(EVENTS.ENABLE_FINAL_UI);
                            EventManager.Broadcast(EVENTS.STEP);
                        }
                        IsConclusionStep = true;
                        return;
                    default: PromptHandler(); return;
                }
            }

        }
        void MoveNextStepHandler()
        {
            AudioSource audio = VoiceoverControllerScript.VoiceOverAudioSource;
            if (audio)
            {
                audio.Stop();
            }
            ParseRowData();
        }
        #endregion
        public void MoveToNextStep()
        {
            //Trigger next step in Automation script
            EventManager.Broadcast(EVENTS.NEXT_STEP);
        }

        public void PlayButtonClicked()
        {
            ParseRowData();
        }

        #region Event Subscription Handler
        //subribed on SPAW event
        private void OnEnable()
        {
            EventManager.AddHandler(EVENTS.NEXT_STEP, MoveNextStepHandler);
        }
        //unsubribed from SPAW event
        private void OnDisable()
        {
            EventManager.RemoveHandler(EVENTS.NEXT_STEP, MoveNextStepHandler);
        }
        #endregion

    }

}
