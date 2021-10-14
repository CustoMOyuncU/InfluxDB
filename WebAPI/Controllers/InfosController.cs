using Business.Abstract;
using Core.DataAccess.InfluxDB;
using Entities.Concrete;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfosController : ControllerBase
    {
        ITemperatureService _temperatureService;

        public InfosController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        [HttpPost("addtemperatureproperty")]
        public IActionResult AddTemperatureProperty(Mem mem)
        {
            var result = _temperatureService.WriteTemperatureProperty(mem);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("addtemperatureproperties")]
        public IActionResult AddTemperatureProperties()
        {
            var result = _temperatureService.WriteTemperatureProperties();
            
            if (result.Success)
            {           
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("gettemperature")]
        public async Task<IActionResult> GetTemperatureSettings([FromServices] InfluxDbRepositoryBase service, string time)
        {
            if (time == null) time = "24h";
            var results = await service.QueryAsync(async query =>
            {
                var flux = "from(bucket:\"lorawan_data\")\n"
                + "|> range(start: -" + time + ")\n"
                + "|> filter(fn: (r) => r[\"_measurement\"] == \"device_frmpayload_data_temperature\")\n"
                + "|> filter(fn: (r) => r[\"host\"] == \"host3\")\n"
                + "|> group(columns: [\"_start\", \"_time\", \"_stop\", \"_measurement\", \"_field\"])\n"
                + "|> yield(name: \"mean\")";
                var tables = await query.QueryAsync(flux, "GrupArge");
                return tables.SelectMany(table =>
                    table.Records.Select(record =>
                new Temperature
                {
                    FieId = record.GetField(),
                    Measurement = record.GetMeasurement(),
                    Start = record.GetStart().ToString(),
                    Stop = record.GetStop().ToString(),
                    Time = record.GetTime().ToString(),
                    Value = (double)record.GetValue()
                }));
            });

            return Ok(results);
        }

        //[HttpPost("deletetemperatureproperties")]
        //public IActionResult DeleteTemperatureProperties(Temperature temperature)
        //{
        //    // Not working
        //    var result = _temperatureService.DeleteTemperatureProperties(temperature);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

    }
}
