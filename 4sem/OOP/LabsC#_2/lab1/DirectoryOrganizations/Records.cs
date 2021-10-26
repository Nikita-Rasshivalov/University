using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryOrganizations
{

    /// <summary>
    /// Records class
    /// </summary>
    public class Records
    {
        /// <summary>
        /// Name of organization
        /// </summary>
        private string OrganizationName { get; set; }

        /// <summary>
        /// Type of organization
        /// </summary>
        private string OrganizationType { get; set; }
        /// <summary>
        /// Adress when organization situated
        /// </summary>
        private string Adress { get; set; }

        /// <summary>
        /// Phon number of organization
        /// </summary>
        private string PhoneNumber { get; set; }


        /// <summary>
        /// Number of employees in the organization
        /// </summary>
        private string EmployNumbers { get; set; }

        /// <summary>
        /// Create an instance of  record
        /// </summary>
        /// <param name="organizationName">Name of org</param>
        /// <param name="organizationType">Type of org</param>
        /// <param name="adress">Adress of org</param>
        /// <param name="phoneNumber">Phone number of org</param>
        /// <param name="emloyNumbers">Number of employees in the organization</param>
        public Records(string organizationName, string organizationType, string adress, string phoneNumber, string emloyNumbers)
        {
            this.OrganizationName = organizationName;
            this.OrganizationType = organizationType;
            this.Adress = adress;
            this.PhoneNumber = phoneNumber;
            this.EmployNumbers = emloyNumbers;
        }
    }
}
