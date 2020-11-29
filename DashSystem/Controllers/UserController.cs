using System;
using DashSystem.DataAccess;
using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Users;
using System.Collections.Generic;
using System.IO;

namespace DashSystem.Controllers
{
    public class UserController : IController<IUser>
    {
        private List<IUser> Users { get; set; } = new List<IUser>();
        private UserDataAccess UserDataAccess { get; }
        private bool AlreadyFetchedData { get; set; } = false;
        public UserController()
        {
            string dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "users.csv");

            CsvDataAccessStrategy dataAccess = new CsvDataAccessStrategy(dataFilePath);
            CsvDataParserStrategy<IUser> parser = new CsvDataParserStrategy<IUser>(',');
            UserDataAccess = new UserDataAccess(parser, dataAccess);
        }

        public List<IUser> Fetch()
        {
            if (!AlreadyFetchedData)
            {
                Users = UserDataAccess.FetchData();
                Users.ForEach(x => x.UserBalanceChangedEvent += Update);
                AlreadyFetchedData = true;
            }

            return Users;
        }

        public void Update(IUser user)
        {
            UserDataAccess.UpdateData(user);
        }

        public void Add(IUser data)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IUser data)
        {
            throw new System.NotImplementedException();
        }

        public uint GetNextUID()
        {
            throw new System.NotImplementedException();
        }
    }
}
