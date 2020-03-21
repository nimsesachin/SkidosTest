using SN.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SN.NativeShare
{
    public class NativeSharedData : Singleton<NativeSharedData>
    {
#if UNITY_ANDROID
        public string nativeClassName = "com.sn.skidosnative.NativeShare";

        private AndroidJavaObject nativeShareObject = null;

        void Initialize()
        {
            if (nativeShareObject == null)
            {
                nativeShareObject = new AndroidJavaObject(nativeClassName);

                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivityObject = jc.GetStatic<AndroidJavaObject>("currentActivity");

                if (nativeShareObject != null && unityActivityObject != null)
                {
                    nativeShareObject.Call("setActivity", unityActivityObject);
                }
            }
        }
#endif
        public bool SaveData(string key, string value)
        {
#if UNITY_ANDROID
            Initialize();
            return nativeShareObject != null ? nativeShareObject.Call<bool>("SaveData", key, value) : false;
#else
            Debug.LogError("Platform not supported");
            return false;
#endif
        }

        public bool SaveData(StoreData data)
        {
#if UNITY_ANDROID
            Initialize();
            return SaveData(data.key, data.value);
#else
            Debug.LogError("Platform not supported");
            return false;
#endif
        }

        public string GetData()
        {
#if UNITY_ANDROID
            Initialize();
            return nativeShareObject != null ? nativeShareObject.Call<string>("GetAllData") : string.Empty;
#else
            Debug.LogError("Platform not supported");
            return string.Empty;
#endif
        }

    }
}