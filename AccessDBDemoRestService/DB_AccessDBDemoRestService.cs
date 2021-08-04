using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

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

                return dt.Tables[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {

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
    }
}