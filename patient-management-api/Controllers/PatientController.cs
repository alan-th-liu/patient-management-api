using Microsoft.AspNetCore.Mvc;
using patient_management_api.Models.Api;
using patient_management_api.Services;

namespace patient_management_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    [ProducesResponseType<List<PatientApiResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPatients()
    {
        var results = await _patientService.GetPatientsAsync();
        return Ok(results);
    }
}