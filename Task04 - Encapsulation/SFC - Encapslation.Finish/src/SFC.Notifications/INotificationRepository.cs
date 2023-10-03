using System;
using System.Collections.Generic;
using SFC.SharedKernel;

namespace SFC.Notifications
{
   interface INotificationRepository
  {
    void Add(Email email, string title, string body, DateTime date, LoginName loginName, string notificationType);
    IEnumerable<NotificationsCountResult> GetSendNotificationsCount(
      string notificationType,
      params LoginName[] loginNames);
  }
}