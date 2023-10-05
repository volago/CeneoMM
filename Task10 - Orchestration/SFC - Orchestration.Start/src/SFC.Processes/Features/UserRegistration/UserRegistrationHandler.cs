using Automatonymous;
using SFC.Accounts.Features.CreateAccount.Contract;
using SFC.Accounts.Features.GetAccountByLoginName;
using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;
using SFC.Notifications.Features.SetNotificationEmail.Contract;
using SFC.Processes.Features.UserRegistration.Contract;
using System;

namespace SFC.Processes.Features.UserRegistration
{
  class UserRegistrationHandler : ICommandHandler<RegisterUserCommand>, ICommandHandler<ConfirmUserCommand>
  {
    private readonly ICommandBus _commandBus;
    private readonly IQuery _query;
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserRegistrationHandler(ICommandBus commandBus, IQuery query, IAccountRepository accountRepository, IPasswordHasher passwordHasher)
    {
      _commandBus = commandBus;
      _query = query;
      _accountRepository = accountRepository;
      _passwordHasher = passwordHasher;
    }

    public void Handle(RegisterUserCommand command)
    {
      _commandBus.Send(new SendNotificationCommand()
      {
        Body = $"Witaj użyszkodniku link: /api/v1.0/account/{command.Id}/confirmation",
        Title = "Witaj",
        LoginName = command.LoginName,
        NotificationType = command.NotifiactionType
      });

      _accountRepository.Add(new Account(command.Id, command.Email, command.LoginName, command.ZipCode, _passwordHasher.Hash(command.Password)));
    }

    public void Handle(ConfirmUserCommand command)
    {
      var account = _accountRepository.Get(command.ConfirmationId);

      _commandBus.Send(new CreateAccountCommand() { LoginName = account.LoginName, PasswordHash = account.PasswordHash });

      _commandBus.Send(new CreateAlertCommand() { Id = Guid.NewGuid(), ZipCode = account.ZipCode });
    }
  }
}