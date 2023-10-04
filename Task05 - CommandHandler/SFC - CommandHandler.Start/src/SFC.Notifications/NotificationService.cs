using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;
using System;
using System.Collections.Generic;

namespace SFC.Notifications
{
  class NotificationService : INotificationService
  {
    private readonly IEmailRepository _emailRepository;
    private readonly ISmtpClient _smtpClient;
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(
      IEmailRepository emailRepository,
      ISmtpClient smtpClient,
      INotificationRepository notificationRepository)
    {
      _emailRepository = emailRepository;
      _smtpClient = smtpClient;
      _notificationRepository = notificationRepository;
    }

    public IEnumerable<NotificationsCountResult> GetSendNotificationsCount(string notificationType, params LoginName[] loginNames)
    {
      return _notificationRepository.GetSendNotificationsCount(notificationType, loginNames);
    }

    public void SetNotificationEmail(Email email, LoginName loginName)
    {
      _emailRepository.Set(loginName, email);
    }

    public void SendNotification(LoginName loginName, string title, string body, string notificationType)
    {
      Email email = _emailRepository.GetEmail(loginName);

      _smtpClient.Send(email, title, body);

      _notificationRepository.Add(email, title, body, DateTime.Now, loginName, notificationType);
    }
  }
}
