using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using TestProject.Models;
using Newtonsoft.Json;
using TestProject.DataProcess;
using TestProject.BusinessProcess;


namespace TestProject.Controllers
{
    public class TransApiController : ApiController
    {
        private TestProjectContext db = new TestProjectContext();
        [Route("api/TransApi/GetByStatus/{id}")]
        public string GetByStatus(string id)
        { 
            GlobalFunction _GlobalFunction = new GlobalFunction();
            var _TransactionDataList = (from t in db.TransactionData.AsEnumerable()
            where t.Status == id
            select new TransactionReport {id = t.TransactionId, payment = t.Amount.ToString() + t.CurrencyCode, Status = t.Status }).ToList();
            string JSONresult = JsonConvert.SerializeObject(_TransactionDataList);
            return JSONresult;
        }
        [Route("api/TransApi/GetByCurrency/{id}")]
        public string GetByCurrency(string id)
        {
            var _TransactionDataList = (from t in db.TransactionData.AsEnumerable()
            where t.CurrencyCode == id
            select new TransactionReport { id = t.TransactionId, payment = t.Amount.ToString() + t.CurrencyCode, Status = t.Status }).ToList();            
            string JSONresult = JsonConvert.SerializeObject(_TransactionDataList);
            return JSONresult;
        }
      
        [Route("api/TransApi/GetByRange/{id}/{id2}")]
        public string GetByRange(string id,string id2)
        {
            DateTime dtFrom = Convert.ToDateTime(id);
            DateTime dtTo = Convert.ToDateTime(id2);
            var _TransactionDataList = (from t in db.TransactionData.AsEnumerable()
                                        where t.TransactionDate >= dtFrom && t.TransactionDate <= dtTo
                                        orderby t.TransactionDate 
                                        select new TransactionReport { id = t.TransactionId, payment = t.Amount.ToString() + t.CurrencyCode, Status = t.Status }).ToList();
            string JSONresult = JsonConvert.SerializeObject(_TransactionDataList);
            return JSONresult;
        }
        
    }
}