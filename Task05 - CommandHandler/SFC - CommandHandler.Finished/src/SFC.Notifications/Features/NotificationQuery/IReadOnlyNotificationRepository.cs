using SFC.Notifications.Features.NotificationQuery.Contract;
using SFC.SharedKernel;
using System.Collections.Generic;

namespace SFC.Notifications.Features.NotificationQuery
{
    internal interface IReadOnlyNotificationRepository
  {
    IEnumerable<NotificationsCountResult> GetSendNotificationsCount(string notificationType, IEnumerable<LoginName> loginNames);
  }
}