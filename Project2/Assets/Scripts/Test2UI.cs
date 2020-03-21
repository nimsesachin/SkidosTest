using SN.NativeShare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SN.UIBase
{
    public class Test2UI : MonoBehaviour
    {
        [SerializeField] private Text retrivedText = default;

        // Start is called before the first frame update
        void Start()
        {
            FetchData();
        }

        
        void FetchData()
        {
            retrivedText.text = NativeSharedData.pInstance.GetData();
        }
    }
}