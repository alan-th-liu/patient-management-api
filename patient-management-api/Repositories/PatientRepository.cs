using Microsoft.EntityFrameworkCore;
using patient_management_api.Data;
using patient_management_api.Models.Entities;
using patient_management_api.Models.Repo;

namespace patient_management_api.Repositories;

public class PatientRepository(ApplicationDbContext db) : IPatientRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Patient?> GetByIdAsync(Guid id)
    {
        return await _db.Patient.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Patient>> GetAllAsync()
    {
        return await _db.Patient.AsNoTracking().ToListAsync();
    }

    public async Task<List<PatientWithOrder>> GetAllWithOrderAsync()
    {
        return await _db.Patient
            .GroupJoin(
                _db.PatientOrder,
                p => p.OrderId,
                o => o.Id,
                (p, orders) => new { p, orders }
            )
            .SelectMany(
                x => x.orders.DefaultIfEmpty(),
                (x, o) => new PatientWithOrder
                {
                    Patient = x.p,
                    Order = o
                }
            )
            .ToListAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _db.Patient.Update(patient);
        await _db.SaveChangesAsync();
    }
}