using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        /// <summary>
        /// Name of organization
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Number of employees in the organization
        /// </summary>
        public int EmployesNumber { get; set; }

        /// <summary>
        /// Adress when organization situated
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// Phon number of organization
        /// </summary>
        public long PhoneNumber { get; set; }
        public int TypeId { get; set; }
    }
}
