using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Web;

namespace Accounts
{
    public class dal
    {
        public void InsertRawData(string Ticker, string RPTCode, string ACSHORT, string AccountCode, int Year, string Amount) {
            string constr =  ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_INSERTRAWDATA", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Symbol", Ticker);
                cmd.Parameters.Add("@RPT_Code", RPTCode);
                cmd.Parameters.Add("@AC_Short", ACSHORT);
                cmd.Parameters.Add("@Account_Code", AccountCode);
                cmd.Parameters.Add("@YearToInsert", Year);
                cmd.Parameters.Add("@AmountToInsert", Amount);
                // execute the command
                cmd.ExecuteNonQuery();
                
            }
        }
        public DataTable GetRawData(string Ticker, int Year)
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("fin_GetRawData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Symbol", Ticker);
                cmd.Parameters.Add("@yeartoInsert", Year);
                cmd.Parameters.Add("prc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);


            }
            return dt;
        }

        public DataTable GetMappedData(string Ticker, int Year, string mappingType)
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("fin_GetMappedValues", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("Symbol", Ticker);
                cmd.Parameters.Add("YearToSearch", Year);
                cmd.Parameters.Add(new OracleParameter("MappedValue", mappingType));
                cmd.Parameters.Add("prc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);


            }
            return dt;
        }


        public DataTable GetGeneralCode()
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("fin_getgeneralcode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("prc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);


            }
            return dt;
        }


        public DataTable GetYear()
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_GETYEAR", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("prc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);


            }
            return dt;
        }

        public DataTable GetSymbol()
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_GETSYMBOL", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("prc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);


            }
            return dt;
        }
        public void InsertGeneralAccountCode(string AccountCode)
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_INSERTGENERALCODE", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@General_Code_tmp", AccountCode);
                // execute the command
                cmd.ExecuteNonQuery();

            }
        }
        public void InsertMappedCode(string AccountCode, string GeneralCode, int Year)
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_InsertMappedCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AccountCode", AccountCode);
                cmd.Parameters.Add("@YearToInsert", Year);
                cmd.Parameters.Add("@GeneralCode", GeneralCode);
                
                // execute the command
                cmd.ExecuteNonQuery();

            }
        }
        public void UpdateMappedCode(string AccountCode, string GeneralCode, int Year)
        {
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;

            using (OracleConnection conn = new OracleConnection(constr))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand("FIN_UpdateMappedCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Account_Code", AccountCode);
                cmd.Parameters.Add("@YearToInsert", Year);
                cmd.Parameters.Add("@General_Code", GeneralCode);
                // execute the command
                cmd.ExecuteNonQuery();

            }
        }
    }
}