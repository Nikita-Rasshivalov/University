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
        private string orgName; 

        /// <summary>
        /// Type of organization
        /// </summary>
        private string orgType;
        /// <summary>
        /// Adress when organization situated
        /// </summary>
        private string orgAdress;

        /// <summary>
        /// Phon number of organization
        /// </summary>
        private string phnNumber;


        /// <summary>
        /// Number of employees in the organization
        /// </summary>
        private string emplNumbers;

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
            this.orgName = organizationName;
            this.orgType = organizationType;
            this.orgAdress = adress;
            this.phnNumber = phoneNumber;
            this.emplNumbers = emloyNumbers;
        }

        /// <summary>
        /// 
        /// </summary>
        private int MethodOne(int x, int y)
        {
            return x + y;
        }
        /// <summary>
        /// 
        /// </summary>
        private double MethodTwo(double x,double y)
        {
            return x * y;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string  MethodThree(string s)
        {
            var myString = $" {s} + nice job ";
            return myString;
        }
    }
}
