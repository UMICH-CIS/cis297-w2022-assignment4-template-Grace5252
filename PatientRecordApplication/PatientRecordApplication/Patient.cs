using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientRecordApplication
{
    /// <summary>
    /// Patient class that contains information on ID, Name, and Balance.
    /// </summary>
    class Patient
    {
        /// <summary>
        /// ID number property with setter and getter.
        /// </summary>
        public int ID { get; set; }
        
        /// <summary>
        /// Name string property with setter and getter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Balance decimal property with setter and getter.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
