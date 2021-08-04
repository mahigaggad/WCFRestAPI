using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AccessDBDemoRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccessDBDemoRestService" in both code and config file together.
    [ServiceContract]
    public interface IAccessDBDemoRestService
    {
        [OperationContract]
        [WebInvoke(Method ="GET", UriTemplate = "/getEmpData/{EmpID}", RequestFormat =WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle =WebMessageBodyStyle.Wrapped)]
        EmployeData[] getEmpData(string EmpID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/getLatestNEmpData/{N}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        EmployeData[] getLatestNEmpData(string N);


        //Function to update employee data given emp id
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateEmployeeData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        String UpdateEmployeeData(EmployeData objEmp);

        //Function to insert employee data and give emp id
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InsertEmployeeData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        String InsertEmployeeData(EmployeData objEmp);

        //Function to delete emp data
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/DeleteEmpData/{EmpID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string DeleteEmpData(string EmpID);
    }
}
