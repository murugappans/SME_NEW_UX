using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace SMEPayroll
{
    public class DataAccess
    {
        public DataAccess()
        {
            // this is vss testing 
        }
        public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParams)
        {

            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);
            
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;	//CommandType.Text 
                cmd.CommandTimeout = 600;
                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch (Exception ex)
            {
                string sMsg = ex.Message.ToString();
                conn.Close();
                throw;
            }
        }

        public static System.Data.DataSet ExecuteSPDataSet(string sp_name, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
            System.Data.DataSet ds = new DataSet();
            try
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sp_name;//"sp_addr_book_users_get";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60000;
                adapter.SelectCommand = cmd;

                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

              
                adapter.Fill(ds);
                cmd.Dispose();
                conn.Close();
                return ds;
            }
            catch
            {
                ds = null;
                conn.Close();
                throw;
            }
        }

        public static int ExecuteStoreProc(string sp_name, params SqlParameter[] cmdParams)
        {

            string strSendSPPar = "NO";

            if (strSendSPPar == "YES")
            {
                string sFileName = HttpContext.Current.Request.PhysicalApplicationPath + "Documents\\SPFiles.txt";
                //StreamWriter objWriter = new StreamWriter(sFileName, false);
                //objWriter.Close();
                StreamWriter objStrWriter1 = new StreamWriter(sFileName, true);
                objStrWriter1.WriteLine("Exec SP_NAME ");
                for (int iCount = 0; iCount < cmdParams.Length - 1; iCount++)
                {
                    objStrWriter1.WriteLine("," + cmdParams[iCount].ParameterName + "='" + cmdParams[iCount].Value.ToString() + "'");
                }
                objStrWriter1.Close();
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
            }


            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);
            //  SqlTransaction objTrans = cn.BeginTransaction();

            int RowsEffected = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sp_name;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Transaction = objTrans;
                cmd.CommandTimeout = 600;

                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

                RowsEffected = cmd.ExecuteNonQuery();
                // objTrans.Commit();

                cmd.Parameters.Clear();
                return RowsEffected;
            }
            catch (Exception ex)
            {
                // objTrans.Rollback();            
                conn.Close();
                throw;
            }
        }

        public static int ExecuteStoreProc(string sSQL)
        {
            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);
            int RowsEffected = 0;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sSQL;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;
                RowsEffected = cmd.ExecuteNonQuery();

                return RowsEffected;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        // the following method is overloaded to be used with Access db.
        public static OleDbDataReader ExecuteOleReader(string connString, string cmdText)
        {

            string ConnString = connString;
            OleDbCommand cmd = new OleDbCommand();
            OleDbConnection conn = new OleDbConnection(ConnString);
            try
            {
                conn.Close();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 600;
                OleDbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static DataSet FetchRS(CommandType cmdType, string cmdText, params SqlParameter[] cmdParams)
        {

            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;	//CommandType.Text 
                cmd.CommandTimeout = 600;
                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

                da.SelectCommand = cmd;
                da.Fill(ds);

                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        public static DataSet FetchRSDS(CommandType cmdType, string cmdText)
        {

            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;	//CommandType.Text 
                cmd.CommandTimeout = 600;
                //if (cmdParams != null)
                //{
                //    foreach (SqlParameter param in cmdParams)
                //        cmd.Parameters.Add(param);
                //}

                da.SelectCommand = cmd;
                da.Fill(ds);

                cmd.Parameters.Clear();
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        public static string ExecuteSPScalar(string sp_name, params SqlParameter[] cmdParams)
        {
            string sRetVal = "";
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sp_name;//"sp_addr_book_users_get";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                adapter.SelectCommand = cmd;

                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

                System.Data.DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.Dispose();
                conn.Close();
                sRetVal = Utility.ToString(ds.Tables[0].Rows[0][0]);
                ds.Dispose();
                //r
                cmd.Parameters.Clear();
                return sRetVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public static int ExecuteNonQuery(string sqlQuery, params SqlParameter[] cmdParams)
        {
            int sRetVal = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlQuery;
                cmd.CommandTimeout = 600;
                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                    {
                        if (param.Value == null)
                        {
                           // sqlCmd.Parameters[0].IsNullable = true;
                            param.IsNullable = true;
                          param.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(param);
                    }
                }
                sRetVal = Utility.ToInteger(cmd.ExecuteNonQuery());
                return sRetVal;
            }
            catch (Exception  ex)
            {
                string msg = ex.Message.ToString();
                conn.Close();
                throw;
            }
        }
        public static int ExecuteScalar(string sqlQuery, params SqlParameter[] cmdParams)
        {
            int sRetVal = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlQuery;//"sp_addr_book_users_get";
                cmd.CommandType = CommandType.Text ;
                cmd.CommandTimeout = 600;
                adapter.SelectCommand = cmd;

                if (cmdParams != null)
                {
                    foreach (SqlParameter param in cmdParams)
                        cmd.Parameters.Add(param);
                }

                System.Data.DataSet ds = new DataSet();
                adapter.Fill(ds);
                cmd.Dispose();
                conn.Close();
                // sRetVal = Utility.ToInteger(ds.Tables[0].Rows[0][0]);
                sRetVal = ds.Tables[0].Rows.Count > 0 ? Utility.ToInteger(ds.Tables[0].Rows[0][0]) : 0;  //Changed by Astha jammu office               
                ds.Dispose();
                return sRetVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
    
    
       public static int ExecuteScalarQuery(string sqlQuery)
        {
            int sRetVal = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {

                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text ;
                cmd.CommandTimeout = 600;
                adapter.SelectCommand = cmd;

                sRetVal = Convert.ToInt32(cmd.ExecuteScalar());
    
                cmd.Dispose();
                conn.Close();
               
                return sRetVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader GetDataReader(string sqlQuery)
        {
            string ConnString = Constants.CONNECTION_STRING;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnString);

            try
            {
                conn.Open();

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = sqlQuery;
             
         
                cmd.CommandTimeout = 600;
              
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                if (dr.HasRows)
                {
                    //return reader;

                    return dr;
                }

                return dr;


            }
          

            catch (Exception ex)
            {
                string sMsg = ex.Message.ToString();
                conn.Close();
                throw;
            }
        }


    }
}

