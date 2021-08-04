using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;

namespace AccessDBDemoRestService
{
    public class DB_AccessDBDemoRestService
    {
        static string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        SqlConnection conn = new SqlConnection(connectionString);

        public DataTable DB_GetEmpData(int ID)
        {
            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand("Select * From EmployeeData where EMP_ID=@id", conn);
                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                DataSet dt = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                {
                    da.Fill(dt, "EmployeeData");
                }
                conn.Close();
                return dt.Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        public DataTable DB_GetLatestNEmpData(int N)
        {
            try
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand("Select top "+ N + " * From EmployeeData order by EMP_ID Desc", conn);
                DataSet dt = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                {
                    da.Fill(dt, "EmployeeData");
                }
                conn.Close();
                return dt.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }
        //Function to update employee data
        public string DB_UpdateEmpData(EmployeData objEmp)
        {
            try
            {
                DateTime dateValue;
                string result = null;
                conn.Open();
                //Date validation for DOB
                if (DateTime.TryParseExact(objEmp.Emp_DOB, "dd-MMM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    objEmp.Emp_DOB = dateValue.ToString();
                    
                }
                else {
                    return "Invalid date";
                }
                //Date validation for joining date
                if (DateTime.TryParseExact(objEmp.Emp_JoiningDate, "dd-MMM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    objEmp.Emp_JoiningDate = dateValue.ToString();
                   
                }
                else
                {
                    return "Invalid date";
                }



                SqlCommand sqlCmd = new SqlCommand("Update EmployeeData set Emp_name = @Name, Emp_DOB = @DOB, Emp_JoiningDate = @joiningDate, Emp_JobName = @jobName, Emp_DeptID = @DeptID, Emp_ManagerID = @ManagerID where EMP_ID=@id", conn);
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = objEmp.Emp_Name;
                sqlCmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = objEmp.Emp_DOB;
                sqlCmd.Parameters.Add("@joiningDate", SqlDbType.DateTime).Value = objEmp.Emp_JoiningDate;
                sqlCmd.Parameters.Add("@jobName", SqlDbType.NVarChar).Value = objEmp.Emp_JobName;
                sqlCmd.Parameters.Add("@DeptID", SqlDbType.Int).Value = objEmp.Emp_DeptID;
                sqlCmd.Parameters.Add("@ManagerID", SqlDbType.Int).Value = objEmp.Emp_ManagerID;
                sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = objEmp.Emp_ID;
                result = sqlCmd.ExecuteNonQuery().ToString();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
    }
}