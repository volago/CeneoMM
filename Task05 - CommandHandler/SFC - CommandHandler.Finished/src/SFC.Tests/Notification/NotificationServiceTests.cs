﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Autofac;
using Microsoft.Extensions.Configuration;
using SFC.Notifications;
using SFC.SharedKernel;
using System.Linq;
using SFC.Tests.Mocks;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.NotificationQuery;
using SFC.Notifications.Features.NotificationQuery.Contract;

namespace SFC.Tests.Notification
{


  public partial class NotificationServiceTests
  {
    IContainer _container;

    public NotificationServiceTests()
    {
      var confBuilder = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json");
      var configuration = confBuilder.Build();
      var connectionString = configuration["ConnectionStrings:DefaultConnection"];

      DbMigrations.Run(connectionString);

      var builder = new ContainerBuilder();
      builder.RegisterModule(new AutofacNotificationsModule(connectionString));
      builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      builder.RegisterType<TestDateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterGeneric(typeof(LoggingDecorator<>)).AsImplementedInterfaces();
      builder.RegisterGenericDecorator(typeof(LoggingDecorator<>), typeof(ICommandHandler<>));

      _container = builder.Build();
    }

    [Fact]
    public void SendNotificationTest()
    {
      var setNotificationEmail = _container.Resolve<ICommandHandler<SetNotificationEmailCommand>>();
      var sendNotification = _container.Resolve<ICommandHandler<SendNotificationCommand>>();
      var query = _container.Resolve<IQueryHandler<GetSendNotificationsCountRequest, IEnumerable<NotificationsCountResult>>>();

      LoginName loginName = "ala" + Guid.NewGuid();
      string notificationType = "type1";

      setNotificationEmail.Handle(new SetNotificationEmailCommand
      {
        Email = "example@exmaple.com",
        LoginName = loginName
      });

      sendNotification.Handle(new SendNotificationCommand()
      {
        LoginName = loginName,
        Title = "title",
        Body = "body",
        NotificationType = notificationType
      });

      IEnumerable<NotificationsCountResult> result = query.HandleQuery(
        new GetSendNotificationsCountRequest()
        {
          NotificationType = notificationType,
          LoginNames = new[] { loginName }
        });

      Assert.True(result.Count() == 1);
      Assert.True(result.First().LoginName == loginName);
      Assert.True(result.First().Count == 1);
      Assert.True(TestSmtpClient.SentEmails.Count == 1);
    }
  }
}
