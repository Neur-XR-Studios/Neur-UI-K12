using UnityEngine;
using TMPro;

namespace K12.UI
{
    public class LanguageText : MonoBehaviour
    {
        [SerializeField]
        private string EnglishText;
        [SerializeField]
        private string HindiText;
        // Start is called before the first frame update
        private void Awake()
        {
            UpdateLanguage();
        }
        void UpdateLanguage()
        {
            bool isEnglish = DataManager.StaticVariables.IS_ENGLISH;
            TMP_Text text = GetComponent<TMP_Text>();
            text.text = isEnglish ? EnglishText : HindiText;
        }

        #region Event Subscription Handler
        //subribed on SPAW event
        private void OnEnable()
        {
            UpdateLanguage();
            EventManager.AddHandler(EVENTS.CHANGE_LANGUAGE, UpdateLanguage);
        }
        //unsubribed from SPAW event
        private void OnDisable()
        {
            EventManager.RemoveHandler(EVENTS.CHANGE_LANGUAGE, UpdateLanguage);
        }
        #endregion
    }
}