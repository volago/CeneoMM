using Microsoft.Extensions.Logging;
using SFC.Infrastructure;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SetNotificationEmail.Contract;

namespace SFC.Notifications.Features.SetNotificationEmail
{
  internal class SetNotificationEmailHandler : ICommandHandler<SetNotificationEmailCommand>
  {
    private readonly IEmailWriteRepository _emailRepository;
    private readonly ILogger _log;

    public SetNotificationEmailHandler(IEmailWriteRepository emailRepository)
    {
      _emailRepository = emailRepository;
    }

    public void Handle(SetNotificationEmailCommand command)
    {
      _emailRepository.Set(command.LoginName, command.Email);
    }

    
  }
}