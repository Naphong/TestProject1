using System.Linq;
using TestProject.DataProcess.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using TestProject.DataProcess;
using System;
using TestProject.Models;

namespace TestProject.DataProcess.Repository.Base
{
    public class AdminRepository : IAdminRepository
    {

        private readonly TestProjectContext  _context;
        public AdminRepository(TestProjectContext context)
        {
            _context = context as TestProjectContext;
        }

        public IList<TransactionData> GetTransactionList()
        {
            var results = _context.TransactionData.ToList();
            return results;
        }

        public IList<TransactionData> GetTransactionByStatus(string status)
        {
            var results = _context.TransactionData.Where(x=>x.Status== status).ToList();
            return results;
        }
        public IList<TransactionData> GetTransactionByCurrency(string currency)
        {
            var results = _context.TransactionData.Where(x => x.CurrencyCode == currency).ToList();
            return results;
        }
        public IList<TransactionData> GetTransactionByRange(string startdate)
        {
            var results = _context.TransactionData.Where(x => x.TransactionDate >= Convert.ToDateTime(startdate)).ToList();
            return results;
        }
        public IList<TransactionData> GetTransactionByRange(string startdate,string enddate)
        {
            var results = _context.TransactionData.Where(x => x.TransactionDate>= Convert.ToDateTime(startdate) && x.TransactionDate <= Convert.ToDateTime(enddate) ).ToList();
            return results;
        }
        public void AddTransaction(TransactionData t)
        {
            _context.TransactionData.Add(t);
            _context.SaveChanges();
        }

        public void AddTransactionList(IList<TransactionData> t)
        {           
                using (var context = new TestProjectContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (TransactionData Item in t)
                            {
                                _context.TransactionData.Add(Item);
                                _context.SaveChanges();
                            }
                            transaction.Commit();
                         }
                        catch (Exception ex)
                        {
                        throw new Exception(ex.Message);
                        }
                    }
               }
          }
            
      
    }
}
