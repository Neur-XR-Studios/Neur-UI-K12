using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;
using Simulanis.ContentSDK;
using System.Threading;

namespace Simulanis.ContentSDK.K12.Assessment
{
    public class AssessmentManager : MonoBehaviour
    {
        #region Header
        [Space(20)]
        [Header("-----------------Button Images------------------")]
        [Space(20)]
        public SpriteHolder TypeImage;
        public SpriteHolder TypeText;
        [Header("-----------------SubmitText------------------")]
        [Space(20)]
        public TMP_Text SubmitText;
        public string SubmitTextContent;
        public string none;
        [SerializeField]
        GameObject scoreCard;
        [Space(20)]
        [SerializeField]
        private string AppID;
        [Space(20)]
        [Header("----------------Assessment----------------")]
        [Space(20)]
        [SerializeField]
        private TMP_Text QuestionText;
        [SerializeField]
        private TMP_Text CountText;
        [Space(20)]
        [SerializeField]
        private GameObject[] OptionsObj;
        [Space(20)]
        [SerializeField]
        private GameObject CF_ParentObj;
        [SerializeField]
        private GameObject NoneObject;
        [Space(20)]
        [Space(20)]
        [Header("----------------MCQ Type-Object----------------")]
        [SerializeField]
        private GameObject[] BoundingBoxObj;
        [Space(20)]
        [Header("-----------------Buttons------------------")]
        [Space(20)]
        [SerializeField]
        private Button NextButton;
        [SerializeField]
        private Button PreviousButton;
        [SerializeField]
        private Button SubmitButton;
        [Space(20)]
        [Header("-----------------Sprites------------------")]
        [Space(20)]
        [SerializeField]
        private Sprite DefaultOptionBG;
        [SerializeField]
        private Sprite SelectedOptionBG;
        [SerializeField]
        private Sprite CorrectAnswerBG;
        [SerializeField]
        private Sprite WrongAnswerBG;
        [Space(20)]
        [Header("-----------------Gamification------------------")]
        [Space(20)]
        [SerializeField]
        private GameObject Slingshot;
        private bool IsSameQuestion;
        private bool IsSubmitted;
        private int TotalQuestion = 0;
        private int CurrentQuestion = 0;
        private int LanguageIndex = 0;
        private QuestionResponse questionResponse;
        private SummaryResponse summaryResponse;

        [SerializeField]
        private GameObject AssessmentComplete;
        public string currentMCQ;
        private List<TransformData> originalTransforms;
        private List<GameObject> Options3DObject_List;
        public AssessmentTimer timer;
        private CancellationTokenSource cancellationTokenSource;
        #endregion

        #region GET Response Classes
        [Serializable]
        public class Option
        {
            public string uuid;
            public string option;
        }

        [Serializable]
        public class Questions
        {
            public string uuid;
            public string text;
            public string type;
            public List<Option> options;
            public string correctAnswer;
            public string response;
        }

        [Serializable]
        public class DataItem
        {
            public string language;
            public List<Questions> questions;
        }

        [Serializable]
        public class QuestionResponse
        {
            public bool status;
            public List<DataItem> data;
        }
        #endregion

        #region Post Response Classes

        [Serializable]
        public class Response
        {
            public string questionId;
            public string optionId;
        }

        [Serializable]
        public class SummaryResponse
        {
            public string SessionStartAt;
            public string SessionEndAt;
            public List<Response> response;
        }
        #endregion

        [System.Serializable]
        public class TransformData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;

            public TransformData(Vector3 pos, Quaternion rot, Vector3 scl)
            {
                position = pos;
                rotation = rot;
                scale = scl;
            }
        }

        private void Start()
        {
            Options3DObject_List = new();
            originalTransforms = new();
            Options3DObject_List.Clear();
            originalTransforms.Clear();
            ResetAll();
            if (timer is null)
            {
                timer = FindObjectOfType<AssessmentTimer>();
            }
        }

        #region Initialization
        public void InitializeAssessment()
        {            
            IsSameQuestion = false;
            IsSubmitted = false;
            CurrentQuestion = 0;
            TotalQuestion = 0;
            questionResponse = new();
            summaryResponse = new();
            summaryResponse.SessionStartAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            NextButton.onClick.AddListener(NextButtonHandler);
            PreviousButton.onClick.AddListener(PreviousButtonHandler);
            SubmitButton.onClick.AddListener(SubmitButtonHandler);
            scoreCard.SetActive(false);
            ModuleManager.Instance.GetAssessment(AppID,
            (data) =>
            {
                Debug.Log($"Received Data: {data}");
                questionResponse = JsonUtility.FromJson<QuestionResponse>(data);
                string lan = LanguageSelectionManager.CurrentLanguage;
                LanguageIndex = (lan == questionResponse.data[0].language ? 0 : 1);

                TotalQuestion = questionResponse.data[LanguageIndex].questions.Count;
                if (questionResponse.status)
                {
                    PrepareAssessment();
                }
                else
                {
                    Debug.Log("Error :  Failed to fetch assessment questions");
                }
            },
            (error) =>
            {
                Debug.Log($"Error: {error}");
            });            
        }

        void PrepareAssessment()
        {
            if (!IsSameQuestion)
            {
                ResetAll();
            }
            QuestionText.text = questionResponse.data[LanguageIndex].questions[CurrentQuestion].text;
            CountText.text = $"{CurrentQuestion + 1}/{TotalQuestion}";
            string type = questionResponse.data[LanguageIndex].questions[CurrentQuestion].type;
            if (type == "IMAGE")
            {
                ButtonSpriteChanger(TypeImage,type);
                MCQTypeImageHandler(TypeImage);
            }
            else if (type == "TEXT")
            {
                ButtonSpriteChanger(TypeText, type);
                MCQTypeTextHandler(TypeText);
            }
            else if (type == "OBJECT")
            {
                CollectOptionObjects();
                ButtonSpriteChanger(TypeText, type);
                MCQTypeObjectHandler(TypeText);
            }
            currentMCQ = type;
            IsSameQuestion = false;
        }
        #endregion

        #region MCQ Image Type Handler
        void MCQTypeImageHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[LanguageIndex].questions[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);

                    Transform childImage = obj.transform.Find("Image");
                    count++;
                }
                foreach (GameObject opt in OptionsObj)
                {
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = true;
                }
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj, sprite);
                    Transform childImage = obj.transform.Find("Image");
                    count++;
                }
                foreach (GameObject opt in OptionsObj)
                {
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = false;
                }
            }
        }
        #endregion

        #region MCQ Object Type Handler

        void CollectOptionObjects()
        {
            foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
            {
                //Option UI controls for changing BG to selected/Default/Wrong/Right
                Transform child = FindInChildrenRecursive(CF_ParentObj.transform, opt.option);                
                if (child != null)
                {
                    GameObject obj = child.gameObject;
                    Debug.Log(obj.name);
                    Options3DObject_List.Add(obj);
                    originalTransforms.Add(new TransformData(child.position, child.transform.rotation, child.transform.localScale));
                }
            }
        }

        void MCQTypeObjectHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[LanguageIndex].questions[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;                

                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    //Option UI controls for changing BG to selected/Default/Wrong/Right
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);
                    //Find Option Object to enable and attach to bounding box script
                    foreach (GameObject Option in Options3DObject_List)
                    {
                        if(Option.name == opt.option)
                        {
                            Option.SetActive(true);
                            BoundingBox boundingBox = BoundingBoxObj[count].GetComponent<BoundingBox>();
                            if (Option.name == none)
                            {
                                boundingBox.isRotating = false;
                            }
                            else
                            {
                                boundingBox.isRotating = true;
                            }
                            BoundingBoxObj[count].SetActive(true);
                            if (!IsSameQuestion)
                            {
                                boundingBox.FitAndCenterObject(Option);
                            }
                           
                        }
                    }
                    count++;
                }
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj,sprite);
                    //Find Option Object to enable and attach to bounding box script
                    foreach (GameObject Option in Options3DObject_List)
                    {
                        if (Option.name == opt.option)
                        {
                            Option.SetActive(true);
                            BoundingBox boundingBox = BoundingBoxObj[count].GetComponent<BoundingBox>();
                            BoundingBoxObj[count].SetActive(true);
                            if(!IsSameQuestion)
                            {
                                boundingBox.FitAndCenterObject(Option);
                            }
                        }
                    }
                    count++;
                }
            }
        }
        #endregion

        #region MCQ Text Type Handler
        void MCQTypeTextHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[LanguageIndex].questions[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);
                    count++;
                }
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj,sprite);
                    count++;
                }
            }
        }
        #endregion

        #region Private Functions

        void ButtonSpriteChangerSubmit(string ans_uuid,int count , GameObject obj, SpriteHolder sprite)
        {
            if (ans_uuid == questionResponse.data[LanguageIndex].questions[CurrentQuestion].options[count].uuid)
            {
                if (ans_uuid == questionResponse.data[LanguageIndex].questions[CurrentQuestion].correctAnswer)
                {
                    obj.GetComponent<Image>().sprite = sprite.CorrectAnsSprite;
                }
                else
                {
                    obj.GetComponent<Image>().sprite = sprite.WrongAnsSprite;
                }
            }
            else if (questionResponse.data[LanguageIndex].questions[CurrentQuestion].correctAnswer == questionResponse.data[LanguageIndex].questions[CurrentQuestion].options[count].uuid)
            {
                obj.GetComponent<Image>().sprite = sprite.CorrectAnsSprite;
            }
            else
            {
                obj.GetComponent<Image>().sprite = sprite.NormalSprite;
            }
        }
        void ButtonSpriteChangerSelect(string ans_uuid,int count , GameObject obj, SpriteHolder sprite)
        {
            if (ans_uuid == questionResponse.data[LanguageIndex].questions[CurrentQuestion].options[count].uuid)
            {
                obj.GetComponent<Image>().sprite = sprite.selectedSprite;
            }
            else
            {
                obj.GetComponent<Image>().sprite = sprite.NormalSprite;
            }
        }

        public void NextButtonHandler()
        {
            if ((CurrentQuestion + 1) < TotalQuestion)
            {
                CurrentQuestion++;
                PrepareAssessment();
            }
        }
        void PreviousButtonHandler()
        {
            if (CurrentQuestion > 0)
            {
                CurrentQuestion--;
                PrepareAssessment();
            }
        }
        void SubmitButtonHandler()
        {
            ButtonDisable();
            IsSubmitted = true;
            IsSameQuestion = true;
            PrepareAssessment();
            SubmitAssessmentSummary();
            StopGame();
            SubmitText.gameObject.SetActive(false);
            Score();
            GameObject buttonS = SubmitButton.gameObject;
            buttonS.SetActive(false);
            timer.StopTimer();
        }
        void Score()
        {
            int score = 0;
            foreach (Questions questions in questionResponse.data[LanguageIndex].questions)
            {
                if(questions.correctAnswer == questions.response)
                {
                    score++;
                }
            }

            SubmitText.text = SubmitTextContent + $". Your score is <color=green>{score}</color> out of <color=red>{TotalQuestion}</color>";
            SubmitText.gameObject.SetActive(true);
            scoreCard.SetActive(true);
            AssessmentComplete.SetActive(true);
        }
        void StopGame()
        {
            Slingshot.SetActive(false);
        }
        void SubmitAssessmentSummary()
        {
            summaryResponse.SessionEndAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            summaryResponse.response = new();
            foreach (Questions questions in questionResponse.data[LanguageIndex].questions)
            {
                Response response = new();
                response.questionId = questions.uuid;
                response.optionId = questions.response;
                summaryResponse.response.Add(response);
            }
            string res = JsonUtility.ToJson(summaryResponse);
            Debug.Log(res);
            ModuleManager.Instance.SendAssessment(res);
        }
        void ResetAll()
        {
            if(!IsSubmitted)
            {
                SubmitText.gameObject.SetActive(false);
            }
            foreach (GameObject obj in OptionsObj)
            {
                obj.SetActive(false);
                Transform childText = obj.transform.Find("Text");
                Transform childImage = obj.transform.Find("Image");
                if (childText != null && childImage != null)
                {
                    childText.gameObject.SetActive(false); // Disable the child object
                    childImage.gameObject.SetActive(false); // Disable the child object
                }
            }
            if(Options3DObject_List.Count > 0 && originalTransforms.Count > 0 && 
               Options3DObject_List.Count == originalTransforms.Count)
            {
                int Count = 0;
                foreach(GameObject obj in Options3DObject_List)
                {
                    TransformData targetTransform = originalTransforms[Count];
                    obj.transform.position = targetTransform.position;
                    obj.transform.rotation = targetTransform.rotation;
                    obj.transform.localScale = targetTransform.scale;
                    Count++;
                }
                Options3DObject_List.Clear();
                originalTransforms.Clear();
            }
            foreach(GameObject obj in BoundingBoxObj)
            {
                obj.SetActive(false);
            }
            NoneObject.SetActive(false);
        }
        void ButtonDisable()
        {
            foreach (GameObject obj in OptionsObj)
            {
                obj.GetComponent<Button>().enabled = false;
            }
            SubmitButton.onClick.RemoveListener(SubmitButtonHandler);
        }

       async void ButtonSpriteChanger(SpriteHolder sprite , string MCQType)
        {
            if (!IsSameQuestion)
            {
                foreach (GameObject opt in OptionsObj)
                {
                    Button obj = opt.GetComponent<Button>();
                    SpriteState spriteState = obj.spriteState;
                    // Update the sprite states
                    spriteState.highlightedSprite = sprite.HighlightSprite;
                    spriteState.pressedSprite = sprite.NormalSprite;
                    spriteState.selectedSprite = sprite.selectedSprite;
                    spriteState.disabledSprite = sprite.NormalSprite;
                    // Apply the updated SpriteState back to the button
                    obj.spriteState = spriteState;

                    opt.GetComponent<Image>().sprite = sprite.NormalSprite;
                }

                int sum = 0;
                foreach (Option option in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[sum];
                    obj.SetActive(true);
                    sum++;
                }

                int count = 0;
                foreach (Option option in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    obj.GetComponent<Button>().enabled = true;
                    Transform childText = obj.transform.Find("Text");
                    TMP_Text text = childText.GetComponent<TMP_Text>();
                    if (MCQType == "IMAGE")
                    {
                        // image
                        Transform childImage = obj.transform.Find("Image");
                        Image image = childImage.GetComponent<Image>();
                        image.sprite = await DownloadImageAsSpriteAsync(option.option,MCQType);
                        childText.gameObject.SetActive(false);
                    }
                    // text
                    else if (MCQType == "TEXT" || MCQType == "OBJECT")
                    {
                        Transform ImageWhite = obj.transform.Find("Image");
                        ImageWhite.gameObject.SetActive(true);
                        Image imageWhite = ImageWhite.GetComponent<Image>();
                        imageWhite.sprite = sprite.ImageSprite;

                        childText.gameObject.SetActive(true);
                        text.text = option.option;
                        text.color = Color.black;                      
                    }
                    count++;
                }
                //All options Image turned ON immediately
                int num = 0;
                foreach (Option option in questionResponse.data[LanguageIndex].questions[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[num];
                    // image
                    Transform childImage = obj.transform.Find("Image");

                    Transform childText = obj.transform.Find("Text");
                    TMP_Text text = childText.GetComponent<TMP_Text>();
                    if (currentMCQ == "IMAGE")
                    {
                       
                        childImage.gameObject.SetActive(true);
                    }
                    else if (currentMCQ == "TEXT" || currentMCQ == "OBJECT")
                    {
                        Transform ImageWhite = obj.transform.Find("Image");
                        Image imageWhite = ImageWhite.GetComponent<Image>();
                        imageWhite.sprite = TypeText.ImageSprite;
                        ImageWhite.gameObject.SetActive(true);
                        childText.gameObject.SetActive(true);
                        text.text = option.option;
                        text.color = Color.black;
                    }

                    num++;
                }
            }
       
        }
        #endregion

        #region Common Functions
        private async Task<Sprite> DownloadImageAsSpriteAsync(string url , string MCQ)
        {
            // Cancel any ongoing operations if the question changes
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    if (token.IsCancellationRequested)
                    {
                        Debug.Log("Download cancelled.");
                        return null; // Exit if the operation is canceled
                    }
                    await Task.Yield(); // Wait for the request to complete without blocking
                }
                if (token.IsCancellationRequested)
                {
                    Debug.Log("Download cancelled after completion.");
                    return null;
                }
                if (request.result == UnityWebRequest.Result.Success && currentMCQ == "IMAGE")
                {

                    // Convert the downloaded texture to a Sprite
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);
                    return Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(0.5f, 0.5f) // Pivot point at the center
                    );
                }
                else
                {
                    Debug.LogError($"Error downloading image: {request.error}");

                   
                    return null;
                }
               
            }
           
        }

        Transform FindInChildrenRecursive(Transform parent, string name)
        {
            // First, check if the current transform matches the name
            if (parent.name == name)
                return parent;
            if (name == none)
                return NoneObject.transform;
            // Then, recursively check all child transforms
            foreach (Transform child in parent)
            {
                Transform result = FindInChildrenRecursive(child, name);
                if (result != null)
                    return result;
            }

            // If nothing found, return null
            return null;
        }
        #endregion

        #region Public Functions
        public void AnswerSelectionHandler(int optionNumber)
        {
            questionResponse.data[LanguageIndex].questions[CurrentQuestion].response = questionResponse.data[LanguageIndex].questions[CurrentQuestion].options[optionNumber].uuid;
            Debug.Log(questionResponse.data[LanguageIndex].questions[CurrentQuestion].options[optionNumber].uuid);
            IsSameQuestion = true;
            PrepareAssessment();
        }
        #endregion
    }

}