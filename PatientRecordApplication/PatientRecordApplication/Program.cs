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
    /// This program uses files to store and retrieve data. It also utilizes different exception handling.
    /// </summary>
    /// <Student>Grace Cappella</Student>
    /// <Class>CIS297</Class>
    /// <Semester>Winter 2022</Semester>
    class Program
    {
        /// <summary>
        /// Writes to a file with user input, then reads all, and reads specific based on user input.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Takes input from console, creates patient, inserts patient into file until told otherwise.
            string fileName;
            Console.Write("Enter a valid file name: ");
            fileName = Console.ReadLine();
            PatientRecords inputRecords = new PatientRecords(fileName);

            Patient inputPatient = new Patient();
            int inputID = 0;
            string inputName;
            decimal inputBalance;

            while(inputID > -1)
            {
                Console.WriteLine("Entering a new Patient into the system. To end, input a negative number for ID.");
                inputID = checkValidID();
                if (inputID < 0)
                    break;
                Console.Write("Enter a Patient Name: ");
                inputName = Console.ReadLine();

                inputBalance = checkValidBalance();

                inputPatient.ID = inputID; 
                inputPatient.Name = inputName; 
                inputPatient.Balance = inputBalance;
                inputRecords.addPatient(inputPatient);
            }

            inputRecords.closeWriter();
            inputRecords.closeRecords();

            //Read entire file.
            inputRecords.readAll();

            //Reads for specific ID
            Console.Write("Enter a Patient ID to search for: ");
            int matchID = int.Parse(Console.ReadLine());
            inputRecords.readForPatientID(matchID);

            //Reads for specific Balance
            Console.Write("Enter a minimum balance to search for: ");
            decimal matchBalance = decimal.Parse(Console.ReadLine());
            inputRecords.readForGreaterBalance(matchBalance);
        }

        /// <summary>
        /// Checks once if the ID entered is an integer
        /// </summary>
        /// <returns>an integer ID</returns>
        static int checkValidID()
        {
            int inputID;
            try
            {
                Console.Write("Enter a Patient ID number: ");
                inputID = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.Write("Please enter a valid integer!: ");
                inputID = int.Parse(Console.ReadLine());
            }
            return inputID;
        }

        /// <summary>
        /// Checks once if the balance entered is a positive decimal.
        /// </summary>
        /// <returns>a positive decimal balance</returns>
        static decimal checkValidBalance()
        {
            decimal inputBalance;
            try
            {
                Console.Write("Enter a Patient Balance: ");
                inputBalance = decimal.Parse(Console.ReadLine());
                if(inputBalance < 0)
                {
                    throw new InvalidBalanceException();
                }
            }
            catch(FormatException)
            {
                Console.Write("Please enter a valid decimal!: ");
                inputBalance = decimal.Parse(Console.ReadLine());
            }
            catch (InvalidBalanceException)
            {
                inputBalance = decimal.Parse(Console.ReadLine());
            }

            return inputBalance;
        }
    }
}
