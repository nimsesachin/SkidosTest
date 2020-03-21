using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MockAPIData
{
    public static List<MockAPIData> FromJson(string json)
    {
        return JsonConvert.DeserializeObject<List<MockAPIData>>(json);
    }

    public Dictionary<string, SubTopic[]> GetOperationGata(string operation)
    {
        Dictionary<string, SubTopic[]> result = null;
        switch (operation)
        {
            case "Addition":
                result = this.Addition;
                break;

            case "Geometry":
                result = this.Geometry;
                break;
            case "Mixed Operations":
                result = this.MixedOperations;
                break;
            case "Number sense":
                result = this.NumberSense;
                break;
            case "Subtraction":
                result = this.Subtraction;
                break;
            default:
                Debug.Log(operation + "Operation not found");
                break;
        }

        return result;
    }
}
