using System;
using DashSystem.DataAccess;
using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Products;
using DashSystem.Models.Products.SeasonalProducts;
using System.Collections.Generic;
using System.IO;

namespace DashSystem.Controllers
{
    public class ProductController : IController<IProduct>
    {
        private List<IProduct> Products { get; set; } = new List<IProduct>();
        private ProductDataAccess ProductDataAccess { get; }
        private bool AlreadyFetchedData { get; set; } = false;
        public ProductController()
        {
            string dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.csv");

            CsvDataAccessStrategy dataAccess = new CsvDataAccessStrategy(dataFilePath);
            CsvDataParserStrategy<IProduct> parser = new CsvDataParserStrategy<IProduct>(';');
            ProductDataAccess = new ProductDataAccess(parser, dataAccess);
        }

        public List<IProduct> Fetch()
        {
            if (!AlreadyFetchedData)
            {
                Products = ProductDataAccess.FetchData();

                // Mock data for seasonal products
                Products.Add(new SeasonalProduct(200, "Winter Product", 200, true, new SeasonalDate(1, 11), new SeasonalDate(1, 3)));
                Products.Add(new SeasonalProduct(201, "Summer Product", 200, true, new SeasonalDate(1, 5), new SeasonalDate(1, 9)));
                
                AlreadyFetchedData = true;
            }

            return Products;
        }

        public void Update(IProduct data)
        {
            throw new System.NotImplementedException();
        }

        public void Add(IProduct data)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IProduct data)
        {
            throw new System.NotImplementedException();
        }

        public uint GetNextUID()
        {
            throw new System.NotImplementedException();
        }
    }
}
