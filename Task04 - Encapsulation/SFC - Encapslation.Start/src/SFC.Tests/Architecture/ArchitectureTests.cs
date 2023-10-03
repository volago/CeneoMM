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

namespace SFC.Tests.Architecture
{
  public class ArchitectureTests
  {
    private static readonly ArchUnitNET.Domain.Architecture Architecture =
    new ArchLoader().LoadAssemblies(
      typeof(Module).Assembly,
      typeof(INotificationService).Assembly).Build();

    [Fact]
    public void CheckArchitecture()
    {
      IArchRule allowedPublicTypesInModules =
        Types().That()
          .ResideInAssembly(typeof(INotificationService).Assembly)
          .And()
          .AreNotAssignableTo(typeof(Migration)).And()
          .AreNot(typeof(INotificationService)).And()
          .AreNotAssignableTo(typeof(Module))
          .Should().NotBePublic();

      allowedPublicTypesInModules.Check(Architecture);
    }
  }
}
