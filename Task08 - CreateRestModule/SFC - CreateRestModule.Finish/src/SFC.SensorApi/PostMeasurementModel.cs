using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.SensorApi
{
    public class PostMeasurementModel
    {
        public Dictionary<PolutionType, decimal> Values { get; set; }
    }
}