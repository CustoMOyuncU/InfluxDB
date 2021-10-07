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
    public class InfluxDbDal:InfluxDbRepositoryBase,IInfluxDb
    {
        public PointData WriteData(Mem mem)
        {
            var point = PointData
                .Measurement(mem.Measurement)
                .Tag("host", mem.Host)
                .Field("used_percent", (byte)mem.UsedPercent)
                .Timestamp(mem.Time.ToUniversalTime(), WritePrecision.Ns);
            return point;
        }
    }
}
