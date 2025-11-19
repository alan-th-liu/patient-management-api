using patient_management_api.Models.Entities;
using patient_management_api.Models.Repo;

namespace patient_management_api.Repositories;

public interface IPatientRepository
{
    Task<Patient?> GetByIdAsync(Guid id);

    Task<List<Patient>> GetAllAsync();

    Task<List<PatientWithOrder>> GetAllWithOrderAsync();

    Task UpdateAsync(Patient patient);
}