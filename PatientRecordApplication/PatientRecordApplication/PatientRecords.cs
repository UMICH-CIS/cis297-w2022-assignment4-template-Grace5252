using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace PatientRecordApplication
{
    /// <summary>
    /// Holds the patient file system to write to and read from.
    /// </summary>
    class PatientRecords
    {
        private FileStream fstream;
        private StreamWriter writer;
        private string fileName;

        /// <summary>
        /// Constructor that requires a file name to operate.
        /// </summary>
        /// <param name="inputFileName">name of the file to write to and read from.</param>
        public PatientRecords(string inputFileName)
        {
            fstream = new FileStream(inputFileName, FileMode.Create, FileAccess.Write);
            writer = new StreamWriter(fstream);
            fileName = inputFileName;
        }

        /// <summary>
        /// Closes the main filestream.
        /// </summary>
        public void closeRecords()
        {
            fstream.Close();
        }

        /// <summary>
        /// Closes the writer.
        /// </summary>
        public void closeWriter()
        {
            writer.Close();
        }

        /// <summary>
        /// Adds a patient to the file using the writer opened in the constructor.
        /// </summary>
        /// <param name="inputPatient">Patient with valid information to be entered</param>
        public void addPatient(Patient inputPatient)
        {
            const string DELIM = ",";
            writer.WriteLine(inputPatient.ID + DELIM + inputPatient.Name + DELIM + inputPatient.Balance);
        }

        /// <summary>
        /// Reads the entire file back into the console.
        /// </summary>
        public void readAll()
        {
            const char DELIM = ',';
            FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string patientIn;
            string[] fields;
            patientIn = reader.ReadLine();
            Patient outputPatient = new Patient();
            Console.WriteLine("\n{0,-5}{1,-12}{2,8}\n",
              "ID", "Name", "Balance");

            //If anything goes wrong with the read, it catches the exception with an error message. It finally closes the file.
            try
            {
                while (patientIn != null)
                {
                    fields = patientIn.Split(DELIM);
                    outputPatient.ID = int.Parse(fields[0]);
                    outputPatient.Name = fields[1];
                    outputPatient.Balance = decimal.Parse(fields[2]);
                    Console.WriteLine("{0,-5}{1,-12}{2,8}", outputPatient.ID, outputPatient.Name, outputPatient.Balance.ToString("C"));
                    patientIn = reader.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("Invalid Read, Data corrputed");
            }
            finally
            {
                reader.Close();
                inFile.Close();
            }
            
        }

        /// <summary>
        /// Reads the entire file, but only returns the Patient that matches the ID entered, if it exists.
        /// </summary>
        /// <param name="matchID">ID to search for.</param>
        public void readForPatientID(int matchID)
        {
            bool found = false;

            const char DELIM = ',';
            FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string patientIn;
            string[] fields;
            patientIn = reader.ReadLine();
            Patient outputPatient = new Patient();

            while (patientIn != null)
            {
                fields = patientIn.Split(DELIM);
                outputPatient.ID = int.Parse(fields[0]);
                outputPatient.Name = fields[1];
                outputPatient.Balance = decimal.Parse(fields[2]);
                if(outputPatient.ID == matchID)
                {
                    found = true;
                    Console.WriteLine("{0,-5}{1,-12}{2,8}", outputPatient.ID, outputPatient.Name, outputPatient.Balance.ToString("C"));
                    break;
                }
                patientIn = reader.ReadLine();
            }

            if (!found)
                Console.WriteLine("Patient not found!");

            reader.Close();
            inFile.Close();
        }

        /// <summary>
        /// Reads the entire file and writes to the console the Patients that have a balance >= user input, if they exist.
        /// </summary>
        /// <param name="matchBalance">Lower bound balance to be greater than.</param>
        public void readForGreaterBalance(decimal matchBalance)
        {
            bool found = false;

            const char DELIM = ',';
            FileStream inFile = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            string patientIn;
            string[] fields;
            patientIn = reader.ReadLine();
            Patient outputPatient = new Patient();
            while (patientIn != null)
            {
                fields = patientIn.Split(DELIM);
                outputPatient.ID = int.Parse(fields[0]);
                outputPatient.Name = fields[1];
                outputPatient.Balance = decimal.Parse(fields[2]);
                if (outputPatient.Balance >= matchBalance)
                {
                    found = true;
                    Console.WriteLine("{0,-5}{1,-12}{2,8}", outputPatient.ID, outputPatient.Name, outputPatient.Balance.ToString("C"));
                }
                patientIn = reader.ReadLine();
            }

            if (!found)
                Console.WriteLine("No patients found with lower balance!");

            reader.Close();
            inFile.Close();
        }
    }
}
