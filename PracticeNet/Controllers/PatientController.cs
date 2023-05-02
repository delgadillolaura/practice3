using Microsoft.AspNetCore.Mvc;
using UPB.PracticeNet.Models;

namespace UPB.PracticeNet.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    public PatientController()
    {
    }

    [HttpGet]
    public List<Patient> Get()
    {
        return new List<Patient>();
    }

    [HttpGet]
    [Route("{ci}")]
    public Patient GetByCi([FromRoute] int ci)
    {
        return new Patient();
    }

    [HttpPut]
    [Route("{ci}")]
    public Patient Put([FromRoute] int ci)
    {
        return new Patient();
    }

    [HttpPost]
    public Patient Post()
    {
        return new Patient();
    }

    [HttpDelete]
    [Route("{ci}")]
    public Patient Delete([FromRoute] int ci)
    {
        return new Patient();
    }
}
