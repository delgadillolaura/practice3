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
        return _patientManager.GetByCi();
    }

    [HttpPut]
    [Route("{ci}")]
    public Patient Put([FromRoute] int ci)
    {
        return _patientManager.Update();
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
        return _patientManager.Delete();
    }
}
