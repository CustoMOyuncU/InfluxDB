using Core.DataAccess;
using Entities.Concrete;
using InfluxDB.Client.Writes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IInfluxDb : IInfluxDbRepository
    {
        PointData WriteData(Mem mem);
    }
}
