using System.IO;
using System.Linq;

namespace DashSystem.DataAccess.DataAccessStrategies
{
    class CsvDataAccessStrategy : IDataAccessStrategy<string>
    {
        public string FilePath { get; }

        public CsvDataAccessStrategy(string filePath)
        {
            FilePath = filePath;
            // fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
        }

        public string[] FetchData()
        {
            return File.ReadAllLines(FilePath);
        }

        public void AddData(string data)
        {
            using StreamWriter w = File.AppendText(FilePath);
            w.WriteLine(data);
        }

        public void OverwriteAllData(string data)
        {
            string header = GetCollumns();
            File.WriteAllText(FilePath, header + data);
        }

        public string GetLastRecord()
        {
            return File.ReadLines(FilePath).Last() + "\n";
        }

        public string GetCollumns()
        {
            return File.ReadLines(FilePath).First() + "\n";
        }
    }
}
