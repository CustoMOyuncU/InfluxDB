using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Temperature:IEntity
    {
        public string Start { get; set; }
        public string Stop { get; set; }
        public string Time { get; set; }
        public double? Value { get; set; }
        public string FieId { get; set; }
        public string Measurement { get; set; }
    }
}
