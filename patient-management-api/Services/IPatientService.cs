using patient_management_api.Models.Api;

namespace patient_management_api.Services;

public interface IPatientService
{
    Task<List<PatientApiResponse>> GetPatientsAsync();
}
