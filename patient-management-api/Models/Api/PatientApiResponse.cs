using System.Text.Json.Serialization;

namespace patient_management_api.Models.Api;

public class PatientApiResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("order_id")] public Guid? OrderId { get; set; }

    [JsonPropertyName("message")] public string Message { get; set; }
}