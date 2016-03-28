#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSpectator;
using System.Configuration;
using FluentAssertions;

namespace SampleSpecs.Bug
{
    class App_config_detection : Spec
    {
        void it_finds_app_config()
        {
            ConfigurationManager.AppSettings["SomeConfigEntry"].Should().Be("Worky");
        }
    }
}
