using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Products;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DashSystem.DataAccess
{
    class ProductDataAccess : IDataAccess<IProduct, string>
    {
        public IDataParserStrategy<IProduct, string> ParserStrategy { get; }
        public IDataAccessStrategy<string> AccessStrategy { get; }

        public ProductDataAccess(IDataParserStrategy<IProduct, string> parserStrategy, IDataAccessStrategy<string> accessStrategy)
        {
            ParserStrategy = parserStrategy;
            AccessStrategy = accessStrategy;
        }

        public List<IProduct> FetchData()
        {
            string[] productData = AccessStrategy.FetchData();
            List<IProduct> parsedData = ParserStrategy.Parse(productData, ParseSingle);

            return parsedData;
        }

        public void AddData(IProduct data)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateData(IProduct data)
        {
            throw new System.NotImplementedException();
        }

        private IProduct ParseSingle(Dictionary<string, string> data)
        {
            data["name"] = Regex.Replace(data["name"], "<.*?>", string.Empty);
            data["name"] = data["name"].Replace("\"", "");

            return new Product(
                uint.Parse(data["id"]),
                data["name"],
                decimal.Parse(data["price"]),
                int.Parse(data["active"]) != 0
            );
        }
    }
}
