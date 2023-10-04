using SFC.SharedKernel;

namespace SFC.Notifications
{
   interface IEmailRepository
  {
    Email GetEmail(LoginName loginName);
    void Set(LoginName loginName, Email email);
  }
}