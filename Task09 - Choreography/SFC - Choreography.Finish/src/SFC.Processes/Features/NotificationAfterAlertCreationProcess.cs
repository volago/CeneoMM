using SFC.Alerts.Features.RegisterAlertCondition.Contract;
using SFC.Infrastructure.Interfaces;
using SFC.Notifications.Features.SendNotification.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC.Processes.Features
{
    internal class NotificationAfterAlertCreationProcess : IEventHandler<AlertCreatedEvent>
    {
        private ICommandBus _commandBus;

        public NotificationAfterAlertCreationProcess(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public void Handle(AlertCreatedEvent ev)
        {
            _commandBus.Send(new SendNotificationCommand() { Body = "Alert Created hurey", Title = "AlertCreated", LoginName = ev.LoginName, NotificationType = "AlertCreated" });
        }
    }
}
