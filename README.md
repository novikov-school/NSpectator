# NSpectator

[![Join the chat at https://gitter.im/nspectator/NSpectator](https://badges.gitter.im/nspectator/NSpectator.svg)](https://gitter.im/nspectator/NSpectator?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![NuGet version (NSpectator)](https://img.shields.io/nuget/v/NSpectator.svg?style=flat)](https://www.nuget.org/packages/NSpectator/)
[![Build Status](https://travis-ci.org/nspectator/NSpectator.svg?branch=master)](https://travis-ci.org/nspectator/NSpectator)
[![Dependencies status](https://img.shields.io/badge/dependencies-up--to--date-brightgreen.svg)](https://github.com/nspectator/NSpectator/blob/master/src/NSpectator/packages.config)
[![Youtrack](https://img.shields.io/badge/issues-youtrack-orange.svg)](https://nspectator.myjetbrains.com)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/nspectator/NSpectator/master/license.txt)


NSpectator is ready-to-use solution for development using .NET with Context/Specification flavor. It can be used in different approaches where Specification-By-Example is applicable, including TDD, ATDD, BDD or another agile technics. Toolset is intended to be used to drive development through specifying behavior and interaction at the unit level. 
NSpectator has only one dependency on popular flexible [FluentAssertions](https://github.com/dennisdoomen/fluentassertions) library. It`s important not to depend on specific unit test framework.

Development was started by [Jonny Novikov](http://jonnynovikov.com) inspired by [RSpec](http://rspec.info/) and based upon hereditary framework [NSpec](https://github.com/mattflo/NSpec) written by [Matt Florence](http://twitter.com/mattflo) and [Amir Rajan] (http://twitter.com/amirrajan). We shaped and benefited by hard work from our [contributors](https://github.com/nspectator/NSpectator/contributors)

# Installation

We recommended installing [the NuGet package](https://www.nuget.org/packages/NSpectator). Install on the command line from your solution directory or use the Package Manager console in Visual Studio:

```powershell

PM> Install-Package NSpectator

```

## Additional info

### Execution order

Please have a look at [this wiki page](https://github.com/nspectator/NSpectator/wiki/Execution-Orders) for an overview on which test hooks are executed when: execution order in xSpec family frameworks can get tricky when dealing with more complicated test configurations, like inherithing from an abstract test class or mixing `before_each` with `before_all` at different context levels.

### Data-driven test cases

Test frameworks of the xUnit family have dedicated attributes in order to support data-driven test cases (so-called *theories*). NSpectator, as a member of the xSpec family, does not make use of attributes and instead obtains the same result with a set of expectations automatically created through code. In detail, to set up a data-driven test case with NSpectator you just: 

1. build a set of data points;
1. name and assign an expectation for each data point by looping though the whole set.

Any NSpectator runner will be able to detect all the (aptly) named expectations and run them. Here you can see a sample test case, where we took advantage of `NSpectator.Each<>` class and `NSpectator.Do()` extension to work more easily with data point enumeration, and `NSpectator.With()` extension to have an easier time composing text:

```c#
class Describe_prime_factors : Spec
{
  void Given_first_ten_integer_numbers()
  {
      new Each<int, int[]>
      {
          { 0, new int[] { } },
          { 1, new int[] { } },
          { 2, new[] { 2 } },
          { 3, new[] { 3 } },
          { 4, new[] { 2, 2 } },
          { 5, new[] { 5 } },
          { 6, new[] { 2, 3 } },
          { 7, new[] { 7 } },
          { 8, new[] { 2, 2, 2 } },
          { 9, new[] { 3, 3 } },

      }.Do((given, expected) =>
          it[$"{given} should be {expected}"] = () => given.Primes().Should().Be(expected)
      );
  }
}
```

## Contributing

Check out [this wiki page](https://github.com/nspectator/NSpectator/wiki/Contributing) for complete guide.

### Issues

Feature requests, bugs & issues are handled on [NSpectator Youtrack InCloud](https://nspectator.myjetbrains.com/youtrack/issues/NS?q=%23Unresolved)

Use github authorization (gray bottom button) for signup or login.

## Thanks to

Jetbrains Community support for providing great tools for NSpectator Development Team

[![Jetbrains Resharper](http://nspectator.org/assets/icon_ReSharper.png)](https://www.jetbrains.com/resharper/)
