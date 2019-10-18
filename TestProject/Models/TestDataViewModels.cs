using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TestProject.Models
{
    public class TransactionDataViewModel
    {
        public IList<TransactionData> TransactionDataList;
    }

    public class TransactionRawData
    {
        public string TransactionId { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Amount { get; set; }
        [StringLength(3)]
        public string CurrencyCode { get; set; }
        // Format dd/MM/yyyy hh:mm:ss  (dd/mm/yyyy hh:mm:ss GMT)
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4} (2[0-3]|[01]?[0-9]):([0-5]?[0-9]):([0-5]?[0-9])$")]
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }

    public class TransactionData
    {
        [Key]
        public int id {get; set;}

            //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "TransactionId")]
        [StringLength(50)]
        public string TransactionId { get; set; }
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Amount { get; set; }
        [StringLength(3)]
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string RawStatus { get; set; }
        public string FileType { get; set; }
        public string Status { get; set; }

    }
    

    public class TransactionReport
    {
        public string id { get; set; }
        public string payment { get; set; }
        public string Status { get; set; }
    }


}