using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecordApplication
{
    /// <summary>
    /// User-created exception for a negative balance.
    /// </summary>
    class InvalidBalanceException :Exception
    {
        public InvalidBalanceException()
        {
            Console.Write("Please enter a non-negative balance!: ");
        }
    }
}
