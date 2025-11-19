using System.Text.Json.Serialization;

namespace patient_management_api.Models.Api;

public class OrderApiRequest
{
    [JsonPropertyName("patient_id")] public Guid PatientId { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }
}