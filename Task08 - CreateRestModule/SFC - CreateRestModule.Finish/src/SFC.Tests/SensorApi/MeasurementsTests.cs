﻿using Autofac;
using Microsoft.Extensions.Configuration;
using RestEase;
using SFC.Accounts;
using SFC.Alerts;
using SFC.Infrastructure;
using SFC.Notifications;
using SFC.SensorApi;
using SFC.Sensors;
using SFC.Sensors.Features.RegisterMeasurement;
using SFC.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using SFC.Tests.UserApi;
using SFC.UserApi;
using SFC.SharedKernel;

namespace SFC.Tests.SensorApi
{
    public class MeasurementsTests
  {
    private const string _url = "http://localhost:5000";
    
    public MeasurementsTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);      

      TestSmtpClient.Clear();
      Bootstrap.Run(new string[0], new Module[]
        {
          new AutofacUserApiModule(),
          new AutofacSensorApiModule(),
          new AutofacAlertsModule(),
          new AutofacNotificationsModule(),
          new AutofacSensorsModule(),
          new AutofacAccountsModule(),
          new AutofacInfrastructureModule()
        },
        builder =>
        {
          builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();          
        });
    }
        
    [Fact]
    public async void Test1()
    {
      Guid sensorId = await RestClient.For<IUserApi>(_url).PostSensor(new PostSensorModel() { ZipCode = "01-102"});


      await RestClient.For<ISensorApi>(_url).PostMeasurements(sensorId, new SensorApi.PostMeasurementModel()
      {
        Values = new Dictionary<PolutionType, decimal>() { { PolutionType.PM2_5, 12 }, { PolutionType.PM10, 34 } }
      });

    }
  }
}
