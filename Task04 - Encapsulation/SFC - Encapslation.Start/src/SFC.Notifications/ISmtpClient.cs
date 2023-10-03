using SFC.SharedKernel;

namespace SFC.Notifications
{
  public interface ISmtpClient
  {
    void Send(Email email, string title, string body);
  }
}