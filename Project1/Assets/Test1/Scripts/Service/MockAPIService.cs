using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SN.Service
{
    public class MockAPIService 
    {
        //Hardcoding here
        private const string MockAPIUrl = "https://5e6b24f90f70dd001643c248.mockapi.io/v1";// This should be configure and store in central config object read at the start of initialization
        private const string MockAPIype = "MockAPI";

        public static void FetchData(ServiceEventHandler callback, object userData)
        {
            ServiceRequest request = new ServiceRequest() { _Type = MockAPIype, _ServiceCallContentType = ServiceCallContentType.Json, _EventDelegate = callback, _UserData = userData };
            RequestHelper requestHelper = new RequestHelper
            {
                Uri = MockAPIUrl + "/demo/math/data",
                Params = new Dictionary<string, string>
                {
                   
                },
                EnableDebug = true
            };
            request._requestHelper = requestHelper;
            
            ServiceCall<MockAPIData> call = ServiceCall<MockAPIData>.Create(request);
            if (call != null)
                call.DoPost();
        }
    }
}