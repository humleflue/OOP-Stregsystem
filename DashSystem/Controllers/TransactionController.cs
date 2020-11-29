using System;
using DashSystem.DataAccess;
using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Transactions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DashSystem.Controllers
{
    public class TransactionController : IController<ITransaction>
    {
        private bool AlreadyFetchedData { get; set; } = false;
        private List<ITransaction> Transactions { get; set; } = new List<ITransaction>();
        private TransactionDataAccess TransactionDataAccess { get; }

        public TransactionController()
        {
            string dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "transactions.csv");

            CsvDataAccessStrategy dataAccess = new CsvDataAccessStrategy(dataFilePath);
            CsvDataParserStrategy<ITransaction> parser = new CsvDataParserStrategy<ITransaction>(';');
            TransactionDataAccess = new TransactionDataAccess(parser, dataAccess);
        }

        public List<ITransaction> Fetch()
        {
            if (!AlreadyFetchedData)
            {
                Transactions = TransactionDataAccess.FetchData();
                AlreadyFetchedData = true;
            }

            return Transactions;
        }

        public void Update(ITransaction data)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ITransaction transaction)
        {
            Transactions.Add(transaction);
            TransactionDataAccess.AddData(transaction);
        }

        public void Delete(ITransaction data)
        {
            throw new System.NotImplementedException();
        }

        public uint GetNextUID()
        {
            List<ITransaction> data = Fetch();
            return data.Count == 0 ? 1 : data.Max(x => x.ID) + 1;
        }
    }
}
