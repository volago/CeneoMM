using System;
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
using SFC.Infrastructure;

namespace SFC.Tests.Notification
{


    public class NotificationServiceTests
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
      builder.RegisterModule(new AutofacNotificationsModule());
      builder.RegisterModule(new AutofacInfrastructureModule());
      builder.RegisterInstance(new ConnectionString(connectionString)); 
      builder.RegisterType<TestSmtpClient>().AsImplementedInterfaces();
      builder.RegisterType<TestDateTimeProvider>().AsImplementedInterfaces();

      _container = builder.Build();
    }

    [Fact]
    public void Test1()
    {
      var commandBus = _container.Resolve<ICommandBus>();
      var query = _container.Resolve<IQuery>();

      LoginName loginName = "ala" + Guid.NewGuid();
      string notificationType = "type1";

      commandBus.Send(new SetNotificationEmailCommand
      {
        Email = "example@exmaple.com",
        LoginName = loginName
      });

      commandBus.Send(new SendNotificationCommand()
      {
        LoginName = loginName,
        Title = "title",
        Body = "body",
        NotificationType = notificationType
      });

      IEnumerable<NotificationsCountResult> result = query.Query(
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
