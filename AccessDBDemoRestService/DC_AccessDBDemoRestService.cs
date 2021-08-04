using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AccessDBDemoRestService
{
	[DataContract]
	public class EmployeData
	{
		[DataMember]
        public int Emp_ID { get; set; }
		[DataMember]
        public string Emp_Name { get; set; }
		[DataMember]
        public string Emp_DOB { get; set; }
		[DataMember]
        public string Emp_JoiningDate { get; set; }
		[DataMember]
		public string Emp_JobName { get; set; }
		[DataMember]
		public int Emp_ManagerID { get; set; }
		[DataMember]
		public int Emp_DeptID { get; set; }

	}
}