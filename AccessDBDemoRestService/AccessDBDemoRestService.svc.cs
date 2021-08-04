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
                
                DataTable dt = new DataTable();
                DB_AccessDBDemoRestService objDb = new DB_AccessDBDemoRestService();
                dt = objDb.DB_GetEmpData(int.Parse(empID));
                return ConvertDataTable<EmployeData>(dt).ToArray();
            }

            catch (Exception ex)
            {
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
                throw ex;
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
