using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace K12.Assessment
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
        [Space(20)]
        [TextArea(0, 20)]
        [SerializeField]
        private string Json;
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
        [Space(20)]
        [Header("----------------MCQ Type-Object----------------")]
        [SerializeField]
        private GameObject[] BoundingBoxObj;
        [Space(20)]
        [SerializeField]
        private GameObject[] Options3DObj;
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
        private QuestionResponse questionResponse;
        #endregion

        #region GET Response Classes
        [Serializable]
        public class Option
        {
            public string uuid;
            public string option;
        }

        [Serializable]
        public class DataItem
        {
            public string uuid;
            public string text;
            public string type;
            public List<Option> options;
            public string correctAnswer;
            public string response;
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
            public List<Response> response;
        }
        #endregion

        private void Start()
        {
            ResetAll();
        }

        #region Initialization
        public void InitializeAssessment()
        {
            IsSameQuestion = false;
            IsSubmitted = false;
            CurrentQuestion = 0;
            TotalQuestion = 0;
            questionResponse = new();
            NextButton.onClick.AddListener(NextButtonHandler);
            PreviousButton.onClick.AddListener(PreviousButtonHandler);
            SubmitButton.onClick.AddListener(SubmitButtonHandler);

            TextAsset json = Resources.Load<TextAsset>("Get"); //Only for testing purpose
            Json = json.text;

            questionResponse = JsonUtility.FromJson<QuestionResponse>(Json);
            TotalQuestion = questionResponse.data.Count;
            if (questionResponse.status)
            {
                PrepareAssessment();
            }
            else
            {
                Debug.Log("Error :  Failed to fetch assessment questions");
            }
        }

        void PrepareAssessment()
        {
            if (!IsSameQuestion)
            {
                ResetAll();
            }
            QuestionText.text = questionResponse.data[CurrentQuestion].text;
            CountText.text = $"{CurrentQuestion + 1}/{TotalQuestion}";
            string type = questionResponse.data[CurrentQuestion].type;
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
                ButtonSpriteChanger(TypeText, type);
                MCQTypeObjectHandler(TypeText);
            }

            IsSameQuestion = false;
        }
        #endregion

        #region MCQ Image Type Handler
        void MCQTypeImageHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);

                    Transform childImage = obj.transform.Find("Image");
                    //childImage.gameObject.SetActive(true);
                    count++;
                }
                foreach (GameObject opt in OptionsObj)
                {
                    //opt.gameObject.SetActive(true);
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = true;
                }
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj, sprite);
                    Transform childImage = obj.transform.Find("Image");
                    //childImage.gameObject.SetActive(true);
                    count++;
                }
                foreach (GameObject opt in OptionsObj)
                {
                    //opt.gameObject.SetActive(true);
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = false;
                }
            }
        }
        #endregion

        #region MCQ Object Type Handler
        void MCQTypeObjectHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    //Option UI controls for changing BG to selected/Default/Wrong/Right
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);
                    //Find Option Object to enable and attach to bounding box script
                    foreach (GameObject Option in Options3DObj)
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
                    /*  Transform childText = obj.transform.Find("Text");
                      childText.gameObject.SetActive(true);
                      TMP_Text text = childText.GetComponent<TMP_Text>();
                      text.text = opt.option;
                      */
                }
               /* foreach (GameObject opt in OptionsObj)
                {
                    opt.gameObject.SetActive(true);
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = false;
                }*/
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj,sprite);
                    //Find Option Object to enable and attach to bounding box script
                    foreach (GameObject Option in Options3DObj)
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
                    /* Transform childText = obj.transform.Find("Text");
                     childText.gameObject.SetActive(true);
                     TMP_Text text = childText.GetComponent<TMP_Text>();
                     text.text = opt.option;
                     count++;*/
                }
               /* foreach (GameObject opt in OptionsObj)
                {
                    opt.gameObject.SetActive(true);
                    Button obj = opt.GetComponent<Button>();
                    obj.enabled = false;
                }*/
            }
        }
        #endregion

        #region MCQ Text Type Handler
        void MCQTypeTextHandler(SpriteHolder sprite)
        {
            string ans_uuid = questionResponse.data[CurrentQuestion].response;
            if (!IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSelect(ans_uuid, count, obj, sprite);
                   /* obj.SetActive(true);
                    Transform childText = obj.transform.Find("Text");
                    childText.gameObject.SetActive(true);
                    TMP_Text text = childText.GetComponent<TMP_Text>();
                    text.text = opt.option;
                    Button objB = obj.GetComponent<Button>();
                    objB.enabled = true;*/
                    count++;
                }
            }
            if (IsSubmitted)
            {
                int count = 0;
                foreach (Option opt in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    ButtonSpriteChangerSubmit(ans_uuid, count, obj,sprite);
                   /* obj.SetActive(true);
                    Transform childText = obj.transform.Find("Text");
                    childText.gameObject.SetActive(true);
                    TMP_Text text = childText.GetComponent<TMP_Text>();
                    text.text = opt.option;
                    Button objB = obj.GetComponent<Button>();
                    objB.enabled = false;*/
                    count++;
                }
            }
        }
        #endregion

        #region Private Functions

        void ButtonSpriteChangerSubmit(string ans_uuid,int count , GameObject obj, SpriteHolder sprite)
        {
            if (ans_uuid == questionResponse.data[CurrentQuestion].options[count].uuid)
            {
                if (ans_uuid == questionResponse.data[CurrentQuestion].correctAnswer)
                {
                    obj.GetComponent<Image>().sprite = sprite.CorrectAnsSprite;
                }
                else
                {
                    obj.GetComponent<Image>().sprite = sprite.WrongAnsSprite;
                }
            }
            else if (questionResponse.data[CurrentQuestion].correctAnswer == questionResponse.data[CurrentQuestion].options[count].uuid)
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
            if (ans_uuid == questionResponse.data[CurrentQuestion].options[count].uuid)
            {
                obj.GetComponent<Image>().sprite = sprite.selectedSprite;
            }
            else
            {
                obj.GetComponent<Image>().sprite = sprite.NormalSprite;
            }
        }

        void NextButtonHandler()
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
        }
        void Score()
        {
            int score = 0;
            foreach (DataItem data in questionResponse.data)
            {
                if(data.correctAnswer == data.response)
                {
                    score++;
                }
            }

            SubmitText.text = SubmitTextContent + ". Your score is " + score + " Out of 6";
            SubmitText.gameObject.SetActive(true);
        }
        void StopGame()
        {
            Slingshot.SetActive(false);
           /* foreach (GameObject boundingBox in BoundingBoxObj)
            {
                BoundingBox Script = boundingBox.GetComponent<BoundingBox>();
                Script.isRotating = false;
            }*/
        }
        void SubmitAssessmentSummary()
        {
            SummaryResponse summary = new();
            summary.response = new();
            foreach (DataItem data in questionResponse.data)
            {
                Response response = new();
                response.questionId = data.uuid;
                response.optionId = data.response;
                summary.response.Add(response);
                string res = JsonUtility.ToJson(summary);
                Debug.Log(res);
            }
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
              //  Transform childRawImage = obj.transform.Find("RawImage");
                if (childText != null && childImage != null)
                {
                    childText.gameObject.SetActive(false); // Disable the child object
                    childImage.gameObject.SetActive(false); // Disable the child object
                   // childRawImage.gameObject.SetActive(false); // Disable the child object
                }
            }
            foreach(GameObject obj in Options3DObj)
            {
                obj.SetActive(false);
            }
            foreach(GameObject obj in BoundingBoxObj)
            {
                obj.SetActive(false);
            }
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
                    //opt.gameObject.SetActive(true);
                    Button obj = opt.GetComponent<Button>();
                    // obj.transition = 
                    SpriteState spriteState = obj.spriteState;
                    // Update the sprite states
                    spriteState.highlightedSprite = sprite.HighlightSprite;
                    spriteState.pressedSprite = sprite.NormalSprite;
                    spriteState.selectedSprite = sprite.selectedSprite;
                    spriteState.disabledSprite = sprite.NormalSprite;
                    // Apply the updated SpriteState back to the button
                    obj.spriteState = spriteState;

                    opt.GetComponent<Image>().sprite = sprite.NormalSprite;
                  //  opt.GetComponent<Image>().SetNativeSize();
                    /*Transform ImageWhite = obj.transform.Find("Image");
                    ImageWhite.gameObject.SetActive(true);
                    Image imageWhite = ImageWhite.GetComponent<Image>();
                    imageWhite.sprite = sprite.ImageSprite;*/
                  //  imageWhite.SetNativeSize();
                }

                int sum = 0;
                foreach (Option option in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[sum];
                    obj.SetActive(true);
                    sum++;
                }

                int count = 0;
                foreach (Option option in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[count];
                    //obj.SetActive(true);
                    obj.GetComponent<Button>().enabled = true;
                    Transform childText = obj.transform.Find("Text");
                    TMP_Text text = childText.GetComponent<TMP_Text>();
                    if (MCQType == "IMAGE")
                    {

                        // image
                        Transform childImage = obj.transform.Find("Image");
                        //childImage.gameObject.SetActive(false);
                        Image image = childImage.GetComponent<Image>();
                        image.sprite = await DownloadImageAsSpriteAsync(option.option);
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
                foreach (Option option in questionResponse.data[CurrentQuestion].options)
                {
                    GameObject obj = OptionsObj[num];
                    //obj.SetActive(true);

                    if (MCQType == "IMAGE")
                    {
                        // image
                        Transform childImage = obj.transform.Find("Image");
                        childImage.gameObject.SetActive(true);
                    }
                    num++;
                }
            }
       
        }
        #endregion

        #region Common Functions
        private async Task<Sprite> DownloadImageAsSpriteAsync(string url)
        {
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield(); // Wait for the request to complete without blocking
                }

                if (request.result == UnityWebRequest.Result.Success)
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
        #endregion

        #region Public Functions
        public void AnswerSelectionHandler(int optionNumber)
        {

            questionResponse.data[CurrentQuestion].response = questionResponse.data[CurrentQuestion].options[optionNumber].uuid;
            Debug.Log(questionResponse.data[CurrentQuestion].options[optionNumber].uuid);
            Json = JsonUtility.ToJson(questionResponse);
            IsSameQuestion = true;
            PrepareAssessment();

        }
        #endregion
    }

}