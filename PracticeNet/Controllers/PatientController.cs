using Microsoft.AspNetCore.Mvc;
using UPB.PracticeNet.Models;
using UPB.PracticeNet.Managers;

namespace UPB.PracticeNet.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private PatientManager _patientManager;

    public PatientController()
    {
        _patientManager = new PatientManager();
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
    public Patient Post()
    {
        return _patientManager.Create();
    }

    [HttpDelete]
    [Route("{ci}")]
    public Patient Delete([FromRoute] int ci)
    {
        return _patientManager.Delete();
    }
}
