using UnityEngine;
using TMPro;
using Simulanis.ContentSDK.K12.HindiFontConverter;
using Simulanis.ContentSDK.K12.UI;
namespace Simulanis.ContentSDK.K12.HindiFontReplacer
{
    public class CharReplacerHindi : MonoBehaviour
    {
        private TMP_Text _Text;
        private TMP_InputField _InputField;
        private TMP_Dropdown _DropDown;
        public string Convertedvalue = "";
        public string OriginalText = "";
        private TMP_FontAsset HindiBold;

        void Start()
        {

            HindiBold = (TMP_FontAsset)Resources.Load("HindiFonts/NotoSansDevanagari-Bold SDF");
            _Text = GetComponent<TMP_Text>();
            _InputField = GetComponent<TMP_InputField>();
            _DropDown = GetComponent<TMP_Dropdown>();
             LoadBoldFontAssets();
            UpdateMe();
        }

      void  LoadBoldFontAssets()
       {
          
            if (_DropDown != null)
            {
                if (_DropDown.itemText.fontStyle == FontStyles.Bold || _DropDown.captionText.fontStyle == FontStyles.Bold)
                {

                    _DropDown.captionText.font = HindiBold;
                    _DropDown.itemText.font = HindiBold;
                    _DropDown.itemText.fontStyle = FontStyles.Normal;
                    _DropDown.captionText.fontStyle = FontStyles.Normal;
                }
            }
        }

        void OnEnable()
        {
            EventManager.AddHandler(EVENTS.CORRECT_HINDI, UpdateMe);
            if (_Text == null) _Text = GetComponent<TMP_Text>();
            if (_InputField == null) _InputField = GetComponent<TMP_InputField>();
            if (_DropDown == null) _DropDown = GetComponent<TMP_Dropdown>();
            if (_Text != null)
            {
                if (_Text.isUsingBold)
                {
                    Debug.Log("Text is using Bold");
                    _Text.font = HindiBold;
                }
            }
            if (_InputField != null)
            {
                if (_InputField.textComponent.isUsingBold)
                {
                    _InputField.fontAsset = HindiBold;
                }
            }
            if (_DropDown != null)
            {
                if (_DropDown.itemText.isUsingBold || _DropDown.captionText.isUsingBold)
                {
                    _DropDown.captionText.font = HindiBold;
                    _DropDown.itemText.font = HindiBold;
                }
            }
        }

        /// <summary>
        /// Updates the dynamic text and converts broken letters from the dynamic texts either from local json file or from server.
        /// </summary>

        public void UpdateTextRuntime(string text)
        {
            if (_Text != null)
            {
                _Text.text = text;

            }
            else if (_InputField != null)
            {
                _InputField.text = text;
            }
            else if (_DropDown != null)
            {
                _DropDown.captionText.text = text;
            }
            UpdateMe();
        }


        public void UpdateMe()
        {
            string Value = "";

            if (_Text != null)
            {
                OriginalText = _Text.text;
                Value = _Text.text;
            }
            else if (_InputField != null)
            {
                Value = _InputField.text;
            }
            else if (_DropDown != null)
            {
                Value = _DropDown.captionText.text;
            }

            Value = HindiConvert.Convert(Value);

            if (_Text != null)
            {
                _Text.text = Value;
                Convertedvalue = Value;
            }
            if (_InputField != null)
            {
                _InputField.text = Value;
                Convertedvalue = Value;
            }
            if (_DropDown != null)
            {
                _DropDown.captionText.text = Value;
                Convertedvalue = Value;
            }

        }
        #region Event Subscription Handler
        private void OnDisable() //unsubribed from SPAW event
        {
            EventManager.RemoveHandler(EVENTS.CORRECT_HINDI, UpdateMe);

            Time.timeScale = 1; // Resume the game
        }
        #endregion
    }

}



