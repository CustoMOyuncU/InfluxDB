using Core.Utilities.Results;
using Entities.Concrete;
using InfluxDB.Client.Api.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITemperatureService
    {
        IResult AddTempatureProperties(string bucket, string org, Mem mem);
        IResult WriteTemperatureProperties(Mem mem);
        IResult DeleteTemperatureProperties(Temperature temperature);
    }
}
