using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Models;
using UPB.CoreLogic.Managers;

namespace UPB.PracticeNet.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientManager _patientManager;

    private readonly ILogger<PatientController> _logger;

    public PatientController(PatientManager patientManager, ILogger<PatientController> logger)
    {
        _patientManager = patientManager;
        _logger = logger;
    }

    [HttpGet]
    public List<Patient> Get()
    {
        _logger.LogInformation("Getting all patients");
        return _patientManager.GetAll();
    }

    [HttpGet]
    [Route("{ci}")]
    public Patient GetByCi([FromRoute] int ci)
    {
        _logger.LogInformation("Getting patient by CI {CI}", ci);
        return _patientManager.GetByCi(ci);
    }

    [HttpPut]
    [Route("{ci}")]
    public Patient Put([FromRoute] int ci, [FromBody]Patient patientToUpdate)
    {
        _logger.LogInformation("Updating patient with CI {CI}", ci);

        Patient updatedPatient = new Patient()
        {
            Name = patientToUpdate.Name,
            LastName = patientToUpdate.LastName,
        };
        return _patientManager.Update(ci, patientToUpdate.Name ?? "Unknown", patientToUpdate.LastName ?? "Unknown");
    }

    [HttpPost]
    public Patient Post([FromBody]Patient patientToCreate)
    {
        _logger.LogInformation("Creating patient with CI {CI}", patientToCreate.CI);
        return _patientManager.Create(patientToCreate.Name ?? "Unknown", patientToCreate.LastName ?? "Unknown", patientToCreate.CI);
    }

    [HttpDelete]
    [Route("{ci}")]
    public Patient Delete([FromRoute] int ci)
    {
        _logger.LogInformation("Deleting patient with CI {CI}", ci);
        return _patientManager.Delete(ci);
    }
}
