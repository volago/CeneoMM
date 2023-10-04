using Autofac;

namespace SFC.Notifications
{

  public class AutofacNotificationsModule : Module
  {

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<EmailRepository>()
        .AsImplementedInterfaces();

      builder.RegisterType<NotificationRepository>()
        .AsImplementedInterfaces();

      builder.RegisterType<NotificationService>()
        .AsImplementedInterfaces();

    }
  }
}
