using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public IResult WriteTemperatureProperty(Mem mem)
        {
            var point = _influxDb.WriteData(mem);
            using (var writeApi = _influxDb.Write())
            {
                writeApi.WritePoint(mem.Bucket, mem.Org, point);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Mem>> WriteTemperatureProperties()
        {
            List<Mem> mem = new List<Mem>();
            using (var writeApi = _influxDb.Write())
            {
                for (int i = 0; i < 22; i++)
                {
                    mem.Add(_influxDb.CreateMemData()); 
                    var point = _influxDb.WriteData(mem[i]);
                    writeApi.WritePoint(mem[i].Bucket, mem[i].Org, point);
                }
            }
            //BinaryFormatter bf = new BinaryFormatter();
            //MemoryStream ms = new MemoryStream();
            //bf.Serialize(ms, mem);
            //ms.ToArray();
            
            //var byteLength = ms.Length;
            return new SuccessDataResult<List<Mem>>(mem);
        }

        //public IResult DeleteTemperatureProperties(Temperature temperature)
        //{
        //    var predicate = "from(bucket:\"lorawan_data\")\n"
        //        + "|> filter(fn: (r) => r[\"_measurement\"] == \"test_temperature\")";

        //    var start = Convert.ToDateTime(temperature.Start);
        //    var stop = Convert.ToDateTime(temperature.Stop);
        //    var prdReq = new DeletePredicateRequest(start, stop, predicate);

        //    var delete = _influxDb.DeleteApi();
            
        //    delete.Delete(prdReq, "lorawan_data","GrupArge");

        //    return new SuccessResult();
        //}
    }
}
