using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManager
    {
        //private static string GetConnctionString()
        //{
        //    string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    return val;
        //}

        public static DataTable GetAccountingList(string userID)
        {
            string connStr = DBHelper.GetConnctionString();
            string dbCommand =
                $@"SELECT
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate
                    FROM Accounting
                    WHERE UserID = @userID
                  ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));

            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnctionString();
            string dbCommand =
                $@"SELECT
                        ID,
                        Caption,
                        Amount,
                        ActType,
                        CreateDate,
                        Body
                    FROM Accounting
                    WHERE id = @id AND UserID = @userID;
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));
            return DBHelper.ReadDataRow(connStr, dbCommand, list);

            try
            {
                return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }


        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            // <<<<check input>>>>
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }

            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1.");
            }

            string connStr = DBHelper.GetConnctionString();
            string dbCommand =
                $@" INSERT INTO[dbo].[Accounting]
                    (
                        UserID
                        ,Caption
                        ,Amount
                        ,ActType
                        ,CreateDate
                        ,Body
                     )
                        VALUES
                     (
                        @userID
                        ,@caption
                        ,@amount
                        ,@actType
                        ,@createDate
                        ,@body
                      )
                ";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand comm = new SqlCommand(dbCommand, conn))
                {
                    comm.Parameters.AddWithValue("@userID", userID);
                    comm.Parameters.AddWithValue("@caption", caption);
                    comm.Parameters.AddWithValue("@amount", amount);
                    comm.Parameters.AddWithValue("@actType", actType);
                    comm.Parameters.AddWithValue("@createDate", DateTime.Now);
                    comm.Parameters.AddWithValue("@body", body);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }

        //bool!!!!!!!!!!!!!!!
        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            // <<<<check input>>>>
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }

            if (actType < 0 || actType > 1)
            {
                throw new ArgumentException("ActType must be 0 or 1.");
            }

            string connStr = DBHelper.GetConnctionString();
            string dbCommand =
                $@" UPDATE[Accounting]
                        SET UserID      = @userID
                            ,Caption    = @caption
                            ,Amount     = @amount
                            ,ActType    = @actType
                            ,CreateDate = @createDate
                            ,Body       = @body
                        WHERE
                            ID = @id ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@userID", userID));
            paramList.Add(new SqlParameter("@caption", caption));
            paramList.Add(new SqlParameter("@amount", amount));
            paramList.Add(new SqlParameter("@actType", actType));
            paramList.Add(new SqlParameter("@createDate", DateTime.Now));
            paramList.Add(new SqlParameter("@body", body));
            paramList.Add(new SqlParameter("@id", ID));

            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, paramList);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static void DeleteAccounting(int ID)
        {
            string connStr = DBHelper.GetConnctionString();
            string dbCommand =
                $@" DELETE[Accounting]
                    WHERE ID = @id 
                ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("id", ID));

            try
            {
                DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
    }
}
