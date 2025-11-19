using Microsoft.EntityFrameworkCore;
using patient_management_api.Data;
using patient_management_api.Models.Entities;

namespace patient_management_api.Repositories;

public class OrderRepository(ApplicationDbContext db) : IOrderRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<PatientOrder?> GetByPatientAsync(Guid? patientId)
    {
        return await _db.Patient.Where(p => p.Id == patientId)
            .Join(_db.PatientOrder,
                p => p.OrderId,
                o => o.Id,
                (p, o) => new PatientOrder
                {
                    Id = o.Id,
                    Message = o.Message,
                    CreatedAt = o.CreatedAt,
                }
            )
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(PatientOrder patientOrder)
    {
        await _db.PatientOrder.AddAsync(patientOrder);
    }
}
