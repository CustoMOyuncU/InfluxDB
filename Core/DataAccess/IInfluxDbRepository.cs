using InfluxDB.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IInfluxDbRepository
    {
        WriteApi Write();
        DeleteApi DeleteApi();
        Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action);
    }
}
