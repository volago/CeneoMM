using System;
using SFC.Sensors.Features.RegisterMeasurement.Contract;
using SFC.SharedKernel;

namespace SFC.Sensors.Features.RegisterMeasurement
{

    internal interface IMeasurementRepository
  {
    void Add(Guid sensorId, DateTime date, PolutionType elementName, decimal elementValue);
  }  
}