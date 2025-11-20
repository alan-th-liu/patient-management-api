using patient_management_api.Models.Api;
using patient_management_api.Repositories;

namespace patient_management_api.Services;

public class PatientService(IPatientRepository patientRepository) : IPatientService
{
    private readonly IPatientRepository _patients = patientRepository;

    public async Task<List<PatientApiResponse>> GetPatientsAsync()
    {
        var patients = await _patients.GetAllWithOrderAsync();

        return patients.Select(p => new PatientApiResponse
            {
                Id = p.Patient.Id,
                Name = p.Patient.Name,
                OrderId = p.Patient.OrderId,
                Message = p.Order?.Message ?? string.Empty
            })
            .OrderBy(p => p.Name)
            .ToList();
    }
}