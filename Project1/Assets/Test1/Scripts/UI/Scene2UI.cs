using SN.NativeShare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SN.UIBase
{
    public class Scene2UI : MonoBehaviour
    {
        public InputField keyInput = null;
        public InputField valueInput = null;

        public Text statusText = null;
        public string Status
        {
            set { statusText.text = value; }
            get { return statusText.text; }
        }



        // Start is called before the first frame update
        void Start()
        {
            Status = "Initialized";
        }

       
        public void OnSaveBtnClicked()
        {
            string keyText = keyInput.text;
            string valueText = valueInput.text;

            if(string.IsNullOrEmpty(keyText) || string.IsNullOrEmpty(valueText))
            {
                Status = "Key and Value cannot be empty";
            }
            else
            {
#if UNITY_ANDROID
                Status = NativeSharedData.pInstance.SaveData(keyText, valueText) ? "Save successful" : "Save Failed";
#else
                Status = "Platform not supported";
#endif
            }
        }

        public void OnBackBtnClicked()
        {
            SceneManager.LoadScene("Scene1");
        }

    }
}