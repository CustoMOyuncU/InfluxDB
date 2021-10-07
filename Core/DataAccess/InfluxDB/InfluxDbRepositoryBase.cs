using System;
using System.Threading.Tasks;
using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.InfluxDB
{
    public class InfluxDbRepositoryBase : IInfluxDbRepository
    {

        private const string token = "-3nSWrR7Y4uGydYPn9o54y4Ve14CSNJK5nKZVZT-abV9ASNhxPxa8dC6fpJoaQrCrmqtT9HeW9MBgJQN3gEFww==";



        public WriteApi Write()
        {
            var client = InfluxDBClientFactory.Create("http://192.168.20.60:8086", token);
            var write = client.GetWriteApi();
            return write;
        }

        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create("http://192.168.20.60:8086", token);
            var query = client.GetQueryApi();
            return await action(query);
        }

        public DeleteApi DeleteApi()
        {
            var client = InfluxDBClientFactory.Create("http://192.168.20.60:8086", token);
            var delete = client.GetDeleteApi();
            return delete;
        }
    }
}
