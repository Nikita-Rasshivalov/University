using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryOrganizations
{
    /// <summary>
    /// Directory class
    /// </summary>
    public class Directory
    {
        /// <summary>
        /// List of records
        /// </summary>
        public  List<Records> DirectoryList { get; set; } = new List<Records>();
    }
}
