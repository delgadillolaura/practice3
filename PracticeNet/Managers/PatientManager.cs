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

    public Patient Create(string name, string lastName, int ci)
    {
        Random random = new Random();
        string[] bloodTypes = {"A", "B", "AB", "O"};
        int index = random.Next(bloodTypes.Length);

        Patient createdPatient = new Patient()
        {
            CI = ci,
            Name = name,
            LastName = lastName,
            BloodType = bloodTypes[index]
        };
        _patients.Add(createdPatient);
        return createdPatient;
    }

    public Patient Delete()
    {
        return new Patient();
    }

}