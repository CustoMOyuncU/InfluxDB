using Core.DataAccess.InfluxDB;
using DataAccess.Abstract;
using Entities.Concrete;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class InfluxDbDal : InfluxDbRepositoryBase, IInfluxDb
    {
        public PointData WriteData(Mem mem)
        {
            var point = PointData
                .Measurement(mem.Measurement)
                .Tag("host", mem.Host)
                .Field("used_percent", mem.UsedPercent.Value)
                .Timestamp(mem.Time.ToUniversalTime(), WritePrecision.Ns);
            return point;
        }
        public Mem CreateMemData()
        {
            Random random = new Random();
            var mem = new Mem();
            double number = random.NextDouble() * (24 - 20 + 1) + 20;

            mem.Bucket = "lorawan_data";
            mem.Org = "GrupArge";
            mem.Host = "host6";
            mem.Measurement = "test_temperature";
            mem.UsedPercent = number;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(90);
            int maxDay = (int)(endDate - startDate).TotalDays;
            mem.Time = startDate.AddDays(random.Next(maxDay));
            return mem;
        }
    }
}
