using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoredProcedure = false);
        Task<List<T>> LoadSingleDataAsync<T>(string sqlStatement, string connectionStringName, bool isStoredProcedure = false);
        Task<int> SaveDataAsync<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}
