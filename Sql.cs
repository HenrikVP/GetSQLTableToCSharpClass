using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSQLTableToCSharpClass
{
    internal class Sql
    {
        private string connectionString = @"Data Source=.;Initial Catalog=CarDb;Integrated Security=True";

        public DataTable GetDatatable(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Connection = connection;
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                adapter.Dispose();

                return dataTable;
            }
        }


        public List<T> Datatable2List<T>(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows) list.Add(GetItem<T>(row));
            return list;
        }

        private T GetItem<T>(DataRow row)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (System.Reflection.PropertyInfo pInfo in temp.GetProperties())
                {
                    //If property name = rows column name and is not DBNull, then add value
                    if (pInfo.Name.ToLower() == column.ColumnName.ToLower() && row[column.ColumnName] != DBNull.Value)
                        pInfo.SetValue(obj, row[column.ColumnName], null);
                    else continue;
                }
            }
            return obj;
        }


    }
}
