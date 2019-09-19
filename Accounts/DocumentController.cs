using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Accounts
{
    public class DocumentController : ApiController
    {
        // GET api/<controller>
        public System.Web.Http.Results.JsonResult<string> UploadDocuments()
        {
            string response = "";
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];
                
                string directory_path = HttpContext.Current.Server.MapPath("~/UploadedFiles/" );
                if (!System.IO.Directory.Exists(directory_path))
                {
                    System.IO.Directory.CreateDirectory(directory_path);
                }

                var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/"+ httpPostedFile.FileName);
                httpPostedFile.SaveAs(filePath);
                string conString = string.Empty;

                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                       
                
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                            foreach (DataRow row in dt.Rows) {
                                dal obj = new dal();
                                if (!String.IsNullOrEmpty(row["SYMBOL"].ToString()) && !String.IsNullOrEmpty(row["RPTCODE"].ToString()) && !String.IsNullOrEmpty(row["ACSHORT"].ToString()) && !String.IsNullOrEmpty(row["ACCOUNTCODE"].ToString()) && string.IsNullOrEmpty(row["AMOUNT"].ToString()))
                                {
                                    obj.InsertRawData(row["SYMBOL"].ToString(), row["RPTCODE"].ToString(), row["ACSHORT"].ToString(), row["ACCOUNTCODE"].ToString(), int.Parse(row["YEAR"].ToString()), row["AMOUNT"].ToString());
                                    response = row["SYMBOL"].ToString() + "|" + row["YEAR"].ToString();
                                }
                            
                                
                            }
                        }
                    }
                }


            }

            return Json<string>(response);


        }
        public System.Web.Http.Results.JsonResult<DataTable> GetRawData(string Ticker, string Year)
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetRawData(Ticker,int.Parse(Year));

            return Json<DataTable>(dt);


        }
    }
}