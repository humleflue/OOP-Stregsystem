namespace DashSystem.DataAccess.DataAccessStrategies
{
    public interface IDataAccessStrategy<T>
    {
        public T[] FetchData();
        public void AddData(T data);
        public void OverwriteAllData(T data);
        public T GetLastRecord();
        public T GetCollumns();
    }
}
