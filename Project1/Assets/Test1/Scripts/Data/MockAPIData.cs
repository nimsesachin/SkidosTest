using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public partial class MockAPIData
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("avatar")]
    public string Avatar { get; set; }

    [JsonProperty("Addition")]
    public Dictionary<string, SubTopic[]> Addition { get; set; }

    [JsonProperty("Geometry")]
    public Dictionary<string, SubTopic[]> Geometry { get; set; }

    [JsonProperty("Mixed Operations")]
    public Dictionary<string, SubTopic[]> MixedOperations { get; set; }

    [JsonProperty("Number sense")]
    public Dictionary<string, SubTopic[]> NumberSense { get; set; }

    [JsonProperty("Subtraction")]
    public Dictionary<string, SubTopic[]> Subtraction { get; set; }
    
}

[System.Serializable]
public class SubTopic
{
    [JsonProperty("subtopic_id")]
    public Guid SubtopicId { get; set; }

    [JsonProperty("subtopic_name")]
    public string SubtopicName { get; set; }
}
