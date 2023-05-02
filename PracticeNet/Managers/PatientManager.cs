using UPB.PracticeNet.Models;

namespace UPB.PracticeNet.Managers;

public class PatientManager 
{
    private List<Patient> _patients;

    public PatientManager()
    {
        _patients = new List<Patient>();
    }

     public List<Patient> GetAll()
    {
        return new List<Patient>();
    }

    public Patient GetByCi()
    {
        return new Patient();
    }

    public Patient Update()
    {
        return new Patient();
    }

    public Patient Create()
    {
        return new Patient();
    }

    public Patient Delete()
    {
        return new Patient();
    }

}