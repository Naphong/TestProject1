using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using TestProject.DataProcess.Repository.Interfaces;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
using TestProject.BusinessProcess;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminRepository adminservice;
        public HomeController(IAdminRepository _adminservice)
        {
            adminservice = _adminservice;
        }

        public ActionResult Index()
        {       
            try
            {
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            GlobalFunction GlobalFunction = new GlobalFunction();
            List<TransactionData> TransactionDataList = new List<TransactionData>();
            List<TransactionRawData> TransactionRawDataList = new List<TransactionRawData>();
            DataTable dt = new DataTable();
            DataTable dtgen = new DataTable();
            string line = string.Empty;
            string[] TempArr;
            Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            string filetype = "";
            try
            {
                dt.Columns.Add("Transaction id", typeof(string));
                dt.Columns.Add("Amount", typeof(Decimal));
                dt.Columns.Add("CurrencyCode", typeof(string));
                dt.Columns.Add("TransactionDate", typeof(DateTime));              
                dt.Columns.Add("Status", typeof(string));

                dtgen.Columns.Add("id", typeof(string));
                dtgen.Columns.Add("payment", typeof(string));
                dtgen.Columns.Add("Status", typeof(string));

                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentLength >1048576)
                    {
                       throw new Exception ("File size is max 1 MB.");
                    }
                    string filepath = Server.MapPath("~/Upload/") + Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Upload"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    if (!file.FileName.EndsWith(".xml") && !file.FileName.EndsWith(".csv"))
                    {
                        throw new Exception("Unknown format");
                    }
                    if (file.FileName.EndsWith(".xml"))
                     {
                        filetype = "xml";

                        var doc = XDocument.Load(filepath);
                        var result = doc.Descendants("Transactions");
                        var result2 = result.Descendants("Transaction");
                        var data = result2.ToList();
                        var table = GlobalFunction.ConvertToDataTable(data);

                        foreach (DataRow rowdata in table.Rows)
                        {
                            TransactionRawData _TransactionRawData = new TransactionRawData();
                            _TransactionRawData.TransactionId = rowdata.ItemArray[0].ToString().Trim();//id
                            _TransactionRawData.Amount = Convert.ToDecimal(rowdata.ItemArray[3]);//amount
                            _TransactionRawData.CurrencyCode = rowdata.ItemArray[4].ToString().Trim();//currencycode
                            _TransactionRawData.TransactionDate = Convert.ToDateTime(rowdata.ItemArray[1].ToString().Trim());//date
                            _TransactionRawData.Status = rowdata.ItemArray[5].ToString().Trim();//rawstatus
                            TransactionRawDataList.Add(_TransactionRawData);
                        }
                    }

                    if (file.FileName.EndsWith(".csv"))
                    {
                        filetype = "csv";
                        Stream stream = file.InputStream;
                        using (var reader = new StreamReader(filepath))
                            while ((line = reader.ReadLine()) != null)
                            {
                                TransactionRawData _TransactionRawData = new TransactionRawData();

                                TempArr = r.Split(line);
                                _TransactionRawData.TransactionId = TempArr[0].Replace("\"", "");//id
                                if (TempArr[1].Contains(","))
                                {
                                    TempArr[1] = TempArr[1].Replace("\"", "").Replace(",", "");
                                }
                                else
                                {
                                    TempArr[1] = TempArr[1].Replace("\"", "");
                                }
                                _TransactionRawData.Amount = Convert.ToDecimal(TempArr[1]); //amount
                                _TransactionRawData.CurrencyCode = TempArr[2].Replace("\"", ""); //currencycode
                                TempArr[3] = TempArr[3].Replace("\"", "").Trim();
                                _TransactionRawData.TransactionDate = DateTime.Parse(TempArr[3], new CultureInfo("en-GB"));//transactiondate
                                _TransactionRawData.Status = TempArr[4].Replace("\"", "");//status                                                                
                                TransactionRawDataList.Add(_TransactionRawData);                   
                            }
                    }


                    if (TransactionRawDataList.Count > 0)
                    {
                        foreach (TransactionRawData item in TransactionRawDataList)
                        {
                            TransactionData _TransactionData = new TransactionData();

                            _TransactionData.TransactionId = item.TransactionId.Trim();
                            _TransactionData.Amount = item.Amount;
                            _TransactionData.FileType = filetype;
                            _TransactionData.RawStatus = item.Status.Trim();
                            _TransactionData.Status = GlobalFunction.LoadStatus(filetype, item.Status.Trim());
                            _TransactionData.TransactionDate = Convert.ToDateTime(item.TransactionDate);
                            _TransactionData.CreateDate = DateTime.Now;
                            TransactionDataList.Add(_TransactionData);
                        }

                        adminservice.AddTransactionList(TransactionDataList);
                        ViewBag.Message = "File uploaded successfully";
                    }
                }

                else
                {
                    throw new Exception ("You have not specified a file.");
                }           
            }
            catch (Exception ex)
            {
                if (ex.Message == "Unknown format")
                {
                    ViewBag.ErrorMsg = ex.Message.ToString();
                    return View();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "");
                }
            }
            ViewBag.Message = "";
            ViewBag.ErrorMsg = "";
            return new HttpStatusCodeResult(HttpStatusCode.OK, "OK");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}