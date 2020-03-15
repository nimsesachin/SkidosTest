#define USE_NEWTONSOFT_JSON

using System;
using UnityEngine;

#if USE_NEWTONSOFT_JSON
using Newtonsoft.Json;
#endif

namespace Proyecto26
{
    public static class JsonHelper
    {
        /// <summary>
        /// Get an array of objects when the response is an array "[]" instead of a valid JSON "{}"
        /// </summary>
        /// <returns>An array of objects.</returns>
        /// <param name="json">An array returned from the server.</param>
        /// <typeparam name="T">The element type of the array.</typeparam>
        public static T[] ArrayFromJson<T>(string json)
        {
#if USE_NEWTONSOFT_JSON
            string newJson = "{ \"Items\": " + json + "}";
            var wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(newJson);
            return wrapper.Items;
#else
            string newJson = "{ \"Items\": " + json + "}";
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
#endif
        }

        public static T[] FromJsonString<T>(string json)
        {
#if USE_NEWTONSOFT_JSON
            var wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(json);
            return wrapper.Items;
#else
            var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
#endif
        }

        public static string ArrayToJsonString<T>(T[] array)
        {
#if USE_NEWTONSOFT_JSON
            /*var wrapper = new Wrapper<T>();
            wrapper.Items = array;*/
            return JsonConvert.SerializeObject(array);
#else
            var wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
#endif
        }

        public static string ArrayToJsonString<T>(T[] array, bool prettyPrint)
        {
#if USE_NEWTONSOFT_JSON
            /*var wrapper = new Wrapper<T>();
            wrapper.Items = array;*/
            return JsonUtility.ToJson(array, prettyPrint);
#else
            var wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
#endif
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
