using Core.Entities;
using InfluxDB.Client.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    [Serializable]
    public class Mem : IEntity
    {
        public string Host { get; set; }
        public string Bucket { get; set; }
        public string Measurement { get; set; }
        public string Org { get; set; }
        public double? UsedPercent { get; set; }
        public DateTime Time { get; set; }
    }
}
