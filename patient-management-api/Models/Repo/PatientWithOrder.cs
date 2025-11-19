using patient_management_api.Models.Entities;

namespace patient_management_api.Models.Repo;

public class PatientWithOrder
{
    public Patient Patient { get; set; }

    public PatientOrder? Order { get; set; }
}