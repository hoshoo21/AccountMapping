using Accounts.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Accounts
{
    public class AccountCodeController : ApiController
    {

        public System.Web.Http.Results.JsonResult<string> InsertGeneralCode(AccountData objData) {
            dal objdal = new dal();
            objdal.InsertGeneralAccountCode(objData.AccountCode, objData.ACShortCode, objData.RPTCode);

            return Json<string>("");
        }

        public System.Web.Http.Results.JsonResult<string> InsertMappedCode(AccountData objData)
        {
            dal objdal = new dal();
            objdal.InsertMappedCode(objData.AccountCode, objData.GeneralCode, int.Parse(objData.Year));

            return Json<string>("");
        }

        public System.Web.Http.Results.JsonResult<string> UpdateMappedCode(AccountData objData)
        {
            dal objdal = new dal();
            objdal.UpdateMappedCode(objData.AccountCode, objData.GeneralCode, int.Parse(objData.Year));

            return Json<string>("");
        }


        public System.Web.Http.Results.JsonResult<DataTable> GetGeneralCode()
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetGeneralCode();

            return Json<DataTable>(dt);


        }

        public System.Web.Http.Results.JsonResult<DataTable> GetGeneralCodeFull()
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetGeneralCodeFull();

            return Json<DataTable>(dt);


        }

        public System.Web.Http.Results.JsonResult<DataTable> GetYear()
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetYear();

            return Json<DataTable>(dt);


        }


        public System.Web.Http.Results.JsonResult<DataTable> GetSymbol()
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetSymbol();

            return Json<DataTable>(dt);


        }


        public System.Web.Http.Results.JsonResult<DataTable> GetMappedValues(string Ticker, int Year, string MappingType )
        {

            dal objdal = new dal();
            DataTable dt = objdal.GetMappedData(Ticker, Year, MappingType);

            return Json<DataTable>(dt);


        }


    }
}