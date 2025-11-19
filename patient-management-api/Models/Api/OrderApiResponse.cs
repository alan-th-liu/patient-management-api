using System.Text.Json.Serialization;

namespace patient_management_api.Models.Api;

public class OrderApiResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("patient_id")] public Guid PatientId { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }
}