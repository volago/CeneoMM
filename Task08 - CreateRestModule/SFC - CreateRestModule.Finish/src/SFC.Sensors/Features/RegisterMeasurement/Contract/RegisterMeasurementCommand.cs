using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.Sensors.Features.RegisterMeasurement.Contract
{
    public class RegisterMeasurementCommand
    {
        public Guid SensorId { get; set; }
        public Dictionary<PolutionType, decimal> Elements { get; set; } = new Dictionary<PolutionType, decimal>();
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
    }
}