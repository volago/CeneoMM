using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using FluentMigrator;
using RestEase;
using SFC.Notifications;
using System;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using ArchUnitNET.xUnit;
using Xunit;
using SFC.Infrastructure.Interfaces;

namespace SFC.Tests.Architecture
{
  public class ArchitectureTests
  {
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
    new ArchLoader().LoadAssemblies(
      typeof(AutofacNotificationsModule).Assembly).Build();

    [Fact]
    public void CheckArchitecture()
    {
      IArchRule allowedPublicTypesInModules =
        Types().That()
          .AreNotAssignableTo(typeof(Migration)).And()
          .DoNotImplementInterface(typeof(IService)).And()
          .DoNotImplementInterface(typeof(IResult)).And()
          .AreNotAssignableTo(typeof(Module))
          .Should().NotBePublic();

      allowedPublicTypesInModules.Check(Architecture);
    }
  }
}
