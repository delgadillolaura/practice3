using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class PatientManager 
{
    private List<Patient> _patients;
    private string _filePath;

    public PatientManager(string filePath)
    {
        _patients = new List<Patient>();
        _filePath = filePath;
    }

    public List<Patient> GetAll()
    {
        return _patients;
    }

    public Patient GetByCi(int ci)
    {
        Patient? patientToGet = _patients.Find(patient => patient.CI == ci);
       
        if (patientToGet == null)
        {
            throw new Exception("Patient not found");
        }

        return patientToGet;
    }

    public Patient Update(int ci, string name, string lastName)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }

        Patient? patientToUpdate = _patients.Find(patient => patient.CI == ci);
        
        if (patientToUpdate == null)
        {
            throw new Exception("Patient not found");
        }

        patientToUpdate.Name = name;
        patientToUpdate.LastName = lastName;
        return patientToUpdate;
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

    public Patient Delete(int ci)
    {
        int patientToDeleteIndex = _patients.FindIndex(patient => patient.CI == ci);

        if (patientToDeleteIndex < 0)
        {
            throw new Exception("Patient not found"); 
        }

        Patient patientToDelete = _patients[patientToDeleteIndex];
        _patients.RemoveAt(patientToDeleteIndex);

        return patientToDelete;
    }
}