using SFC.Infrastructure.Interfaces;

namespace SFC.Notifications.Features.NotificationQuery
{
  public interface IQueryHandler<TRequest, TResult>where TRequest : IRequest 
  {
    TResult HandleQuery(TRequest request);
  }
}