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
        private List<Records> directoryList;
        /// <summary>
        /// Create an istance of Directory
        /// </summary>
        public Directory()
        {
            this.directoryList = new List<Records>();
        } 
        /// <summary>
        /// Get records
        /// </summary>
        /// <param name="record"></param>
        /// <returns>list of records</returns>
        public List<Records> MakeRecordList(Records record)
        {
            directoryList.Add(record);
            return directoryList;
        }
    }
}
