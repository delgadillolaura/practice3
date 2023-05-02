using Microsoft.AspNetCore.Mvc;
using UPB.PracticeNet.Models;
using UPB.PracticeNet.Managers;

namespace UPB.PracticeNet.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientManager _patientManager;

    public PatientController(PatientManager patientManager)
    {
        _patientManager = patientManager;
    }

    [HttpGet]
    public List<Patient> Get()
    {
        return _patientManager.GetAll();
    }

    [HttpGet]
    [Route("{ci}")]
    public Patient GetByCi([FromRoute] int ci)
    {
        return _patientManager.GetByCi(ci);
    }

    [HttpPut]
    [Route("{ci}")]
    public Patient Put([FromRoute] int ci, [FromBody]Patient patientToUpdate)
    {
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
        return _patientManager.Create(patientToCreate.Name ?? "Unknown", patientToCreate.LastName ?? "Unknown", patientToCreate.CI);
    }

    [HttpDelete]
    [Route("{ci}")]
    public Patient Delete([FromRoute] int ci)
    {
        return _patientManager.Delete(ci);
    }
}
