using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DataProcess;
using System.Collections;
using System.Data;
using TestProject.Models;

namespace TestProject.DataProcess.Repository.Interfaces
{
    public interface IAdminRepository
    {
        IList<TransactionData> GetTransactionList();
        IList<TransactionData> GetTransactionByStatus(string status);
        IList<TransactionData> GetTransactionByCurrency(string currency);
        IList<TransactionData> GetTransactionByRange(string startdate, string enddate);
        IList<TransactionData> GetTransactionByRange(string startdate);
        void AddTransaction(TransactionData t);
        void AddTransactionList(IList<TransactionData> t);
    }
}
