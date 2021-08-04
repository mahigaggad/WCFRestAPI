using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Reflection;
using System.ServiceModel.Web;

namespace AccessDBDemoRestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccessDBDemoRestService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccessDBDemoRestService.svc or AccessDBDemoRestService.svc.cs at the Solution Explorer and start debugging.
    
    public class AccessDBDemoRestService : IAccessDBDemoRestService
    {
        
        //Function to get data of single employee
        public EmployeData[] getEmpData(string empID)
        {
            try
            {
                Int32 id;
                DataTable dt = new DataTable();
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
               
                dt = objDb.DB_GetEmpData(Int32.Parse(empID));
                return ConvertDataTable<EmployeData>(dt).ToArray();
            }

            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw ex;
            }
        }
        //Function to get data of N employees
        public EmployeData[] getLatestNEmpData(string N)
        {
            try
            {

                DataTable dt = new DataTable();
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
                dt = objDb.DB_GetLatestNEmpData(int.Parse(N));
                return ConvertDataTable<EmployeData>(dt).ToArray();
            }

            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                throw ex;
            }
        }

        //Function to update employee data
        public String UpdateEmployeeData(EmployeData objEmp)
        {
            try
            {
                String result;
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
                result = objDb.DB_UpdateEmpData(objEmp);
                
                if (result == "Invalid date")
                {
                    return "Please send date in dd-MMM-yyyy format.";
                }
                else if(result == "0")
                {
                    return "Employee for this id is not present";
                }
                else if (result == "1")
                {
                    return result + " rows updated.";
                }
                else
                {
                    return "Employee with this id not present";
                }
                

            }
            catch (Exception ex){
                ExceptionLogging.SendErrorToText(ex);
                return ex.Message.ToString();
            }
        }

        //Function to insert employee data and give emp id
        public String InsertEmployeeData(EmployeData objEmp)
        {
            try
            {
                String result;
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
                result = objDb.DB_InsertEmpData(objEmp);

                if (result == "Invalid date")
                {
                    return "Please send date in dd-MMM-yyyy format.";
                }
                
                else
                {
                    return "Employee data inserted and id generated is : " + result;
                }


            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return ex.Message.ToString();
            }
            
        }
        //Function to delete emp data
        public string DeleteEmpData(string EmpID)
        {
            try
            {
                string result = null;
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
                result = objDb.DB_DeleteEmpData(Int32.Parse(EmpID));
                if(result == "-1")
                {
                    return "Employee with id " + EmpID + " deleted successfully.";
                }
                else
                {
                    return "Deletion Unsuccessful.";
                }
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return ex.Message.ToString();
            }
        }


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }


        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    //in case you have a enum/GUID datatype in your model
                    //We will check field's dataType, and convert the value in it.
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            var convertedValue = dr[column.ColumnName];
                            pro.SetValue(obj, convertedValue, null);
                        }
                        catch (Exception e)
                        {
                            //ex handle code                   
                            throw e;
                        }
                        //pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }


        
    }
