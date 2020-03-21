using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SN.NativeShare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeTest : MonoBehaviour
{
    public List<StoreData> storeDataList = new List<StoreData>();
    // Start is called before the first frame update
    void Start()
    {
        //Test();

       /* NativeSharedData nativeSharedData = new NativeSharedData();

        nativeSharedData.SaveData("Test1", "TestData1");
        nativeSharedData.SaveData("Test2", "TestData2");
        string data = nativeSharedData.GetData();

        Debug.LogError(data);
        storeDataList = JsonConvert.DeserializeObject<List<StoreData>>(data);

        foreach (StoreData storeData in storeDataList)
            Debug.Log(storeData.key + "  " + storeData.value);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test()
    {
        string testString = "[{\"key\":\"Test1\",\"value\":\"TestData1\"},{\"key\":\"Test2\",\"value\":\"TestData2\"}]";

        /*StoreData data = new StoreData();
        data.key = "Test1";
        data.value = "TestData1";
        storeDataList.Add(data);

        data = new StoreData();
        data.key = "Test2";
        data.value = "TestData2";
        storeDataList.Add(data);


        Debug.Log(JsonConvert.SerializeObject(storeDataList));*/


        Debug.Log("Test String : "+testString);
        //List<StoreData> storeDataList1 = new List<StoreData>();
        StoreData[] storeDataList1 = JsonConvert.DeserializeObject<StoreData[]>(testString);

        /*List<StoreData> storeDataList1 = JsonConvert.DeserializeObject<List<StoreData>>(testString);*/
        foreach (StoreData storeData in storeDataList1)
            Debug.Log("Test String : " + storeData.key + "  " + storeData.value);
    }

}
