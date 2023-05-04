using UPB.CoreLogic.Models;

namespace UPB.CoreLogic.Managers;

public class PatientManager 
{
    private string _filePath;

    public PatientManager(string filePath)
    {
        _filePath = filePath;
    }

    public List<Patient> GetAll()
    {
        return ReadPatientsFromFile();
    }

    public Patient GetByCi(int ci)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }

        Patient? patientToGet = ReadPatientFromFile(ci);
       
        if (patientToGet == null)
        {
            throw new Exception("Patient not found");
        }

        return patientToGet;
    }

    public Patient Update(int ci, string name, string lastName)
    {
        if (ci < 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
        {
            throw new Exception("Invalid input parameters");
        }

        Patient? patientToUpdate = ReadPatientFromFile(ci);
        
        if (patientToUpdate == null)
        {
            throw new Exception("Patient not found");
        }

        patientToUpdate.Name = name;
        patientToUpdate.LastName = lastName;

        DeletePatientFromFile(ci);
        WritePatientToFile(patientToUpdate);

        return patientToUpdate;
    }

    public Patient Create(string name, string lastName, int ci)
    {
        if (ci < 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
        {
            throw new Exception("Invalid input parameters");
        }

        if (ReadPatientFromFile(ci) != null)
        {
            throw new Exception("Patient with the same CI already exists");
        }

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
        
        WritePatientToFile(createdPatient);

        return createdPatient;
    }

    public Patient Delete(int ci)
    {
        if (ci < 0)
        {
            throw new Exception("Invalid CI");
        }

        Patient? patientToDelete = ReadPatientFromFile(ci);

        if (patientToDelete == null)
        {
            throw new Exception("Patient not found");
        }

        DeletePatientFromFile(ci);

        return patientToDelete;
    }

    public Patient? ReadPatientFromFile(int ci)
    {
        if (!File.Exists(_filePath))
        {
            return null;
        }

        StreamReader reader = new StreamReader(_filePath);

        string? line = reader.ReadLine();
        while (line != null)
        {
            string[] patientInfo = line.Split(',');
            int patientCi = int.Parse(patientInfo[0]);
            string patientName = patientInfo[1];
            string patientLastName = patientInfo[2];
            string patientBloodType = patientInfo[3];

            if (patientCi == ci)
            {
                reader.Close();
                Patient patient = new Patient()
                {
                    CI = patientCi,
                    Name = patientName,
                    LastName = patientLastName,
                    BloodType = patientBloodType
                };
                return patient;
            }

            line = reader.ReadLine();
        }

        reader.Close();
        return null;
    }

    public List<Patient> ReadPatientsFromFile ()
    {
        List<Patient> patients = new List<Patient>();

        if (!File.Exists(_filePath))
        {
            return patients;
        }

        StreamReader reader = new StreamReader(_filePath);
        string? line = reader.ReadLine();

        while (line != null)
        {
            string[] patientInfo = line.Split(',');
            Patient patient = new Patient()
            {
                CI = int.Parse(patientInfo[0]),
                Name = patientInfo[1],
                LastName = patientInfo[2],
                BloodType = patientInfo[3]
            };
            
            patients.Add(patient);
            line = reader.ReadLine();
        }
        reader.Close();

        return patients;
    }

    public void WritePatientToFile(Patient patient)
    {
        StreamWriter writer = new StreamWriter(_filePath, true);
        string line = $"{patient.CI},{patient.Name},{patient.LastName},{patient.BloodType}";
        writer.WriteLine(line);
        writer.Close();
    }

    public void DeletePatientFromFile(int ci)
    {
        string tempPath = "temp.txt";

        StreamReader reader = new StreamReader(_filePath);
        StreamWriter writer = new StreamWriter(tempPath);

        string? line = reader.ReadLine();
        while (line != null)
        {
            string[] patientInfo = line.Split(',');
            int patientCi = int.Parse(patientInfo[0]);

            if (patientCi != ci)
            {
                writer.WriteLine(line);
            }

            line = reader.ReadLine();
        }

        reader.Close();
        writer.Close();

        File.Delete(_filePath);
        File.Move(tempPath, _filePath);
    }
}