using DashSystem.DataAccess.DataAccessStrategies;
using DashSystem.DataAccess.DataParserStrategies;
using System.Collections.Generic;

namespace DashSystem.DataAccess
{
    interface IDataAccess<TParsedData, TRawData>
    {
        IDataAccessStrategy<TRawData> AccessStrategy { get; }
        IDataParserStrategy<TParsedData, TRawData> ParserStrategy { get; }
        List<TParsedData> FetchData();
        void AddData(TParsedData data);
        void UpdateData(TParsedData updatedUser);
    }
}
