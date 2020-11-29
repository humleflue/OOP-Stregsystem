using System;
using System.Collections.Generic;

namespace DashSystem.DataAccess.DataParserStrategies
{
    public interface IDataParserStrategy<TModel, TRawData>
    {
        List<TModel> Parse(TRawData[] rawData, Func<Dictionary<string, string>, TModel> parseFunc);
        TModel Parse(string header,string rawData, Func<Dictionary<string, string>, TModel> parseFunc);
        TRawData Unparse(TModel dataModel);
        TRawData Unparse(TModel[] datamodels);
    }
}
