using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class TemperatureManager : ITemperatureService
    {
        IInfluxDb _influxDb;

        public TemperatureManager(IInfluxDb influxDb)
        {
            _influxDb = influxDb;
        }

        public IResult AddTempatureProperties(string bucket, string org, Mem mem)
        {
            if (bucket == null && org == null)
            {
                bucket = "lorawan_data";
                org = "GrupArge";
            }
            var writeApi = _influxDb.Write();

            writeApi.WriteMeasurement(bucket, org, WritePrecision.Ns, mem);

            return new SuccessResult();
        }

        public IResult WriteTemperatureProperties(Mem mem)
        {
            var point = _influxDb.WriteData(mem);
            using (var writeApi = _influxDb.Write())
            {
                writeApi.WritePoint(mem.Bucket, mem.Org, point);
            }
            return new SuccessResult();
        }

        public IResult DeleteTemperatureProperties(Temperature temperature)
        {
            var predicate = "from(bucket:\"lorawan_data\")\n"
                + "|> range(start: -3h)\n"
                + "|> filter(fn: (r) => r[\"_measurement\"] == \"mem\")\n";

            var deleteApi = _influxDb.DeleteApi();
            deleteApi.Delete(Convert.ToDateTime(temperature.Start).ToUniversalTime(), Convert.ToDateTime(temperature.Stop).ToUniversalTime(), predicate, "lorawan_data", "GrupArge");

            return new SuccessResult();
        }
    }
}
