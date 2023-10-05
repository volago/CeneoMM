using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.Notifications.Features.NotificationQuery.Contract
{
    public class GetSendNotificationsCountRequest : IRequest
    {
        public string NotificationType { get; set; }
        public LoginName[] LoginNames { get; set; }
    }
}