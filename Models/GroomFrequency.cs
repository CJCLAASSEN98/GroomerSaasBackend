using System.Text.Json.Serialization;

namespace backend.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GroomFrequency
    {
        EveryWeek = 1,
        EveryOtherWeek = 2,
        FirstWeekOfTheMonth = 3,
    }
}