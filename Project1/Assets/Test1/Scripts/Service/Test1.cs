using Proyecto26;
using SN.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Test1 : MonoBehaviour
{
    public string url = "https://5e6b24f90f70dd001643c248.mockapi.io/v1/demo/math/data";

    public string response;
    public MockAPIData a1 = new MockAPIData();
    private readonly string basePathMockAPI = "https://5e6b24f90f70dd001643c248.mockapi.io/v1";
    private RequestHelper currentRequest;
    // Start is called before the first frame update
    void Start()
    {
        TestMakeMockAPICall();
        //MakeMockAPICall();
        //StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                response = www.downloadHandler.text;
                a1 = Newtonsoft.Json.JsonConvert.DeserializeObject<MockAPIData>(response);
            }
        }
    }

    public MockAPIData MakeMockAPICall()
    {

        currentRequest = new RequestHelper
        {
            Uri = basePathMockAPI + "/demo/math/data",
            Params = new Dictionary<string, string>
                {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
            EnableDebug = true
        };

        RestClient.Post(currentRequest)
         .Then(res =>
         {

             // And later we can clear the default query string params for all requests
             RestClient.ClearDefaultParams();
             a1 = Newtonsoft.Json.JsonConvert.DeserializeObject<MockAPIData>(res.Text);
         })
        .Catch(err => Debug.Log("Error" + err.Message));

        return null;
    }

    public void TestMakeMockAPICall()
    {
        MockAPIService.FetchData(new ServiceEventHandler(FetchEventHandler), null);
    }
    protected virtual void FetchEventHandler(string type, ServiceEvent inEvent, float inProgress, object inObject, object inUserData)
    {
        if (inEvent == ServiceEvent.COMPLETE)
        {
            a1 = inObject as MockAPIData;
        }
        else
            Debug.LogError("Mock PAI Data fetch");
    }
}
