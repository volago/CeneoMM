using System;
using System.Collections.Generic;
using System.Text;
using SFC.Notifications.Features.NotificationQuery.Contract;
using SFC.Notifications.Features.SendNotification;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.NotificationQuery
{
    class GetSendNotificationsCountQueryHandler : IQueryHandler<GetSendNotificationsCountRequest, IEnumerable<NotificationsCountResult>>
  {
    private IReadOnlyNotificationRepository _notificationRepository;

    public GetSendNotificationsCountQueryHandler(IReadOnlyNotificationRepository notificationRepository)
    {
      _notificationRepository = notificationRepository;
    }

    public IEnumerable<NotificationsCountResult> HandleQuery(GetSendNotificationsCountRequest request)
    {
      return _notificationRepository.GetSendNotificationsCount(request.NotificationType, request.LoginNames);
    }
  }
}
