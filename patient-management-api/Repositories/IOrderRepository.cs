using patient_management_api.Models.Entities;

namespace patient_management_api.Repositories;

public interface IOrderRepository
{
    Task<PatientOrder?> GetByPatientAsync(Guid? patientId);
    Task AddAsync(PatientOrder patientOrder);
}