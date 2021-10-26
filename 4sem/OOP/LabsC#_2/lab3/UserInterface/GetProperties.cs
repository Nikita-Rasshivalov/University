using DirectoryOrganizations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace UserInterface
{
    /// <summary>
    /// Class GetProperties
    /// </summary>
    public class GetProperties
    {
        /// <summary>
        /// GetProperties
        /// </summary>
        public object OrganizationName { get; set; }
        /// <summary>
        /// OrganizationName
        /// </summary>
        public object OrganizationType { get; set; }
        /// <summary>
        /// OrganizationType
        /// </summary>
        public object Adress { get; set; }
        /// <summary>
        /// Adress
        /// </summary>
        public object PhoneNumber { get; set; }
        /// <summary>
        /// PhoneNumber
        /// </summary>
        public object EmployNumbers { get; set; }
        /// <summary>
        /// EmployNumbers
        /// </summary>
        public static List<object> DirList { get; set; } = new List<object>();
         /// <summary>
         /// Create an istance of GetPropertiesClass
         /// </summary>
         /// <param name="organizationName"></param>
         /// <param name="organizationType"></param>
         /// <param name="adress"></param>
         /// <param name="phoneNumber"></param>
         /// <param name="employNumbers"></param>
        public GetProperties(object organizationName, object organizationType, object adress, object phoneNumber, object employNumbers)
        {
            this.OrganizationName = organizationName;
            this.OrganizationType = organizationType;
            this.Adress = adress;
            this.PhoneNumber = phoneNumber;
            this.EmployNumbers = employNumbers;
        }
        /// <summary>
        /// CreateList
        /// </summary>
        /// <returns></returns>
        public List<object> CreateList()
        {
            GetProperties get = new GetProperties(OrganizationName,OrganizationType,Adress,PhoneNumber,EmployNumbers);
            DirList.Add(get);
            return DirList;
        }
        /// <summary>
        /// Get record
        /// </summary>
        /// <param name="record">record</param>
        public void GetRecords(Records record)
        {
            Type type = record.GetType();
            var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            foreach (FieldInfo fieldInfo in fields)
            {

                switch (fieldInfo.Name)
                {
                    case "orgName":
                        OrganizationName = fieldInfo.GetValue(record);
                        break;
                    case "orgType":
                        OrganizationType = fieldInfo.GetValue(record);
                        break;
                    case "orgAdress":
                        Adress = fieldInfo.GetValue(record);
                        break;
                    case "phnNumber":
                        PhoneNumber = fieldInfo.GetValue(record);
                        break;
                    case "emplNumbers":
                        EmployNumbers = fieldInfo.GetValue(record);
                        break;
                    default:
                        break;
                }
  
            }
        }



        public void  CallMethods(Records record)
        {
            Type type = record.GetType();
            var methods = type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var parametrsOne = methods[0].GetParameters();
            for (int i = 0; i < parametrsOne.Length; i++)
            {
                Debug.WriteLine(parametrsOne[i]);
            }

            object[] temp = new object[parametrsOne.Length];
            temp[0] = 2;
            temp[1] = 2;

            var s = methods[0].Invoke(record, temp);
            Debug.WriteLine(s);
        }
        


        public string GetMethods(Records record)
        {
            Type type = record.GetType();
            var methods = type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            string s = $"";
            int i = 1;
            foreach (var m in methods)
            {
                s = s + i + " " + m.Name + "\n";
                i++;
            }
            return s;
        }
    }
}
