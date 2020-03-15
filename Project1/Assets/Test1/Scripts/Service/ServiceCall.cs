using Proyecto26;
using Newtonsoft.Json;

namespace SN.Service
{
    public delegate void ServiceEventHandler(string serviceType, ServiceEvent serviceEvent, float progress, object obj, object userData);
    public enum ServiceEvent
    {
        NONE,
        PROGRESS,
        COMPLETE,
        ERROR
    }

    public enum ServiceCallContentType
    {
        Xml = 0,
        Json,
    }

    public enum ServiceCallRequestType
    {
        POST = 0,
        GET,
        DELETE,
        PUT
    }

    public class ServiceRequest
    {
        public string _Type;
        public ServiceEventHandler _EventDelegate = null;
        public object _UserData = null;
        public RequestHelper _requestHelper;

        public ServiceCallContentType _ServiceCallContentType = ServiceCallContentType.Xml;
        public ServiceCallRequestType _ServiceCallRequestType = ServiceCallRequestType.GET;
    }
    public class ServiceCall<DATA_TYPE>
    {
        public string _Type;
        private ServiceRequest request;

        public static ServiceCall<DATA_TYPE> Create(ServiceRequest serviceRequest)
        {
            ServiceCall<DATA_TYPE> call = new ServiceCall<DATA_TYPE>();
            call.request = serviceRequest;
            return call;
        }

        public virtual void DoGet()
        {

        }

        public virtual void DoPost()
        {
            request._ServiceCallRequestType = ServiceCallRequestType.POST;
            RestClient.Post(request._requestHelper, ProcessResponse);
        }

        public virtual void DoDelete()
        {
            //TODO
        }

        public virtual void DoPut()
        {
            //TODO
        }
        protected virtual T Deserialize<T>(string data)
        {

            if (request._ServiceCallContentType == ServiceCallContentType.Json)
            {
                //TODO USE_NEWTONSOFT_JSON
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
                return (T)Utility.DeserializeFromXml(data, typeof(T));
        }

        void ProcessResponse(RequestException requestException, ResponseHelper responseHelper)
        {
            request._EventDelegate(request._Type, requestException == null ? ServiceEvent.COMPLETE : ServiceEvent.ERROR, 1, Deserialize<DATA_TYPE>(responseHelper.Text), request._UserData);
        }
    }

}