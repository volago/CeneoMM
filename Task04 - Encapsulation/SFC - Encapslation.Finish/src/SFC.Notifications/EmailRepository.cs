using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SFC.SharedKernel;

namespace SFC.Notifications
{
   class EmailRepository : IEmailRepository
  {
    private readonly IDbConnection _connection;

    public EmailRepository(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }
    public void Set(LoginName loginName, Email email)
    {
      if (GetEmail(loginName) != null)
      {
        _connection.Execute(
          @"update Notifications.Emails set email = @email where loginName = loginName",
          new { loginName = loginName.ToString(), email = email.ToString() });
      }
      else
      {
        _connection.Execute(
          @"insert into Notifications.Emails(loginName, email)
          values(@loginName, @email)",
          new { loginName = loginName.ToString(), email = email.ToString() });
      }
    }

    public Email GetEmail(LoginName loginName)
    {
      return _connection.QueryFirstOrDefault<string>(
        "select email from Notifications.Emails where loginName = @loginName",
        new { loginName = loginName.ToString() });
    }
  }
}
