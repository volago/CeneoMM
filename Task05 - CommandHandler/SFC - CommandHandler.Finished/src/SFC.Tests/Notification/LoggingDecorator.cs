using SFC.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace SFC.Tests.Notification
{


  public partial class NotificationServiceTests
  {
    class LoggingDecorator<T> : ICommandHandler<T> where T : ICommand
    {
      private readonly ICommandHandler<T> _commandHandler;

      public LoggingDecorator(ICommandHandler<T> commandHandler)
      {
        _commandHandler = commandHandler;
      }
      public void Handle(T command)
      {
        // logging
        _commandHandler.Handle(command);  
        // logging
      }
    }
  }
}
