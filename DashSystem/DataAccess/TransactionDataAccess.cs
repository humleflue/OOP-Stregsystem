using DashSystem.Controllers;
using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Products;
using DashSystem.Models.Transactions;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;

namespace DashSystem.DataAccess
{
    class TransactionDataAccess : IDataAccess<ITransaction, string>
    {
        public IDataParserStrategy<ITransaction, string> ParserStrategy { get; }
        public IDataAccessStrategy<string> AccessStrategy { get; }

        public TransactionDataAccess(IDataParserStrategy<ITransaction, string> parserStrategy, IDataAccessStrategy<string> accessStrategy)
        {
            ParserStrategy = parserStrategy;
            AccessStrategy = accessStrategy;
        }

        public List<ITransaction> FetchData()
        {
            string[] productData = AccessStrategy.FetchData();
            List<ITransaction> parsedData = ParserStrategy.Parse(productData, ParseSingle);

            return parsedData;
        }

        public void AddData(ITransaction data)
        {
            string unparsedData = ParserStrategy.Unparse(data);
            AccessStrategy.AddData(unparsedData);
        }

        public void UpdateData(ITransaction data)
        {
            throw new NotImplementedException();
        }

        private ITransaction ParseSingle(IReadOnlyDictionary<string, string> data)
        {
            IUser user = new UserController().Fetch().Find(x => uint.Parse(data["user_id"]) == x.ID);

            switch (data["type"])
            {
                case "BuyTransaction":
                    IProduct product = new ProductController().Fetch().Find(x => uint.Parse(data["product_id"]) == x.ID);
                    return new BuyTransaction(uint.Parse(data["id"]), user, DateTime.Parse(data["date"]), product);
                case "InsertCashTransaction":
                    return new InsertCashTransaction(uint.Parse(data["id"]), user, DateTime.Parse(data["date"]), decimal.Parse(data["amount"]));
                default:
                    throw new NotImplementedException($"No parsing behavior available for transaction type {data["type"]}.");
            }
        }
    }
}
