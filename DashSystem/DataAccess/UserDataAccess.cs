using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using DashSystem.Models.Users;
using System;
using System.Collections.Generic;

namespace DashSystem.DataAccess
{
    class UserDataAccess : IDataAccess<IUser, string>
    {
        public IDataAccessStrategy<string> AccessStrategy { get; }
        public IDataParserStrategy<IUser, string> ParserStrategy { get; }
        public UserDataAccess(IDataParserStrategy<IUser, string> parserStrategy, IDataAccessStrategy<string> accessStrategy)
        {
            ParserStrategy = parserStrategy;
            AccessStrategy = accessStrategy;
        }

        public List<IUser> FetchData()
        {
            string[] productData = AccessStrategy.FetchData();
            List<IUser> parsedData = ParserStrategy.Parse(productData, ParseSingle);

            return parsedData;
        }

        public void AddData(IUser data)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(IUser newUser)
        {
            List<IUser> usersInFile = FetchData();

            int userIndex = usersInFile.FindIndex(x => x.ID == newUser.ID);
            usersInFile[userIndex] = newUser;

            string unparsedData = ParserStrategy.Unparse(usersInFile.ToArray());

            AccessStrategy.OverwriteAllData(unparsedData);
        }

        private IUser ParseSingle(IReadOnlyDictionary<string, string> data)
        {
            return new User(
                uint.Parse(data["id"]),
                data["firstname"],
                data["lastname"],
                data["username"],
                decimal.Parse(data["balance"]),
                data["email"]
            );
        }
    }
}
