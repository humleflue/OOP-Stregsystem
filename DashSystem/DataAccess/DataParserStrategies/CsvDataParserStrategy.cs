using DashSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DashSystem.DataAccess.DataParserStrategies
{
    class CsvDataParserStrategy<T> : IDataParserStrategy<T, string> where T : ICollumnNameGetable
    {
        private char Sepparator { get; }

        public CsvDataParserStrategy(char csvSepparator)
        {
            Sepparator = csvSepparator;
        }

        public T Parse(string header,string rawData, Func<Dictionary<string, string>, T> parseFunc)
        {
            Dictionary<string, string> dataAsDict = CsvStringToDictionary(header.Split(Sepparator), rawData.Split(Sepparator));
            return parseFunc(dataAsDict);
        }

        public string Unparse(T dataModel)
        {
            return string.Join(Sepparator, dataModel.GetCollumnNames());
        }

        public string Unparse(T[] datamodels)
        {
            return datamodels.Aggregate("", (current, model) => current + $"{Unparse(model)}\n");
        }

        public List<T> Parse(string[] rawCsvData, Func<Dictionary<string, string>, T> parseFunc)
        {
            List<T> result = new List<T>();
            List<Exception> exceptions = new List<Exception>();

            IEnumerable<Dictionary<string, string>> formattedData = CsvStringsToDictionary(rawCsvData);

            foreach (var item in formattedData)
            {
                try
                {
                    T parsedUser = parseFunc(item);
                    result.Add(parsedUser);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count != 0)
                throw new FormatException($"Failed to parse {exceptions.Count} items.", exceptions[0]);

            return result;
        }
        private IEnumerable<Dictionary<string, string>> CsvStringsToDictionary(string[] rawData)
        {
            string[] header = rawData.First().Split(Sepparator);
            List<string[]> data = rawData.Skip(1).Select(line => line.Split(Sepparator)).ToList();

            return data.Select(csvLine => CsvStringToDictionary(header, csvLine));
        }

        private Dictionary<string, string> CsvStringToDictionary(IReadOnlyList<string> header, IReadOnlyList<string> csvLine)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < header.Count; i++)
            {
                dict.Add(header[i], csvLine[i]);
            }

            return dict;
        }
    }
}
