# NSpectator

[![Join the chat at https://gitter.im/nspectator/NSpectator](https://badges.gitter.im/nspectator/NSpectator.svg)](https://gitter.im/nspectator/NSpectator?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build Status](https://travis-ci.org/nspectator/NSpectator.svg?branch=master)](https://travis-ci.org/nspectator/NSpectator)
[![Dependencies status](https://img.shields.io/badge/dependencies-up--to--date-brightgreen.svg)](#)
[![Youtrack](https://img.shields.io/badge/issues-youtrack-orange.svg)](https://nspectator.myjetbrains.com)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/nspectator/NSpectator/master/license.txt)


NSpectator is inspired by [RSpec](http://rspec.info/) and originally based on [NSpec](https://github.com/mattflo/NSpec) BDD framework for .NET of the xSpec (context/specification) flavor. NSpectator is intended to be used to drive development through specifying behavior at the unit level. 
NSpectator using more flexible [FluentAssertions](https://github.com/dennisdoomen/fluentassertions) library. It`s important not to depend on specific unit test framework.

NSpectator development was statarted by [Novikov Ivan](http://jonnynovikov.com), it`s parent is written by [Matt Florence](http://twitter.com/mattflo) and [Amir Rajan] (http://twitter.com/amirrajan). We shaped and benefited by hard work from our [contributors](https://github.com/nspectator/NSpectator/contributors)

# Installation

We recommended installing [the NuGet package](https://www.nuget.org/packages/NSpectator). Install on the command line from your solution directory or use the Package Manager console in Visual Studio:

```powershell

PM> Install-Package NSpectator

```

## Additional info

### Execution order

Please have a look at [this wiki page](https://github.com/nspectator/NSpectator/wiki/Execution-Orders) for an overview on which test hooks are executed when: execution order in xSpec family frameworks can get tricky when dealing with more complicated test configurations, like inherithing from an abstract test class or mixing `before_each` with `before_all` at different context levels.

### Data-driven test cases

Test frameworks of the xUnit family have dedicated attributes in order to support data-driven test cases (so-called *theories*). NSpec, as a member of the xSpec family, does not make use of attributes and instead obtains the same result with a set of expectations automatically created through code. In detail, to set up a data-driven test case with NSpec you just: 

1. build a set of data points;
1. name and assign an expectation for each data point by looping though the whole set.

Any NSpectator runner will be able to detect all the (aptly) named expectations and run them. Here you can see a sample test case, where we took advantage of `NSpectator.Each<>` class and `NSpectator.Do()` extension to work more easily with data point enumeration, and `NSpec.With()` extension to have an easier time composing text:

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
          it["{0} should be {1}".With(given, expected)] = () => given.Primes().Should().Be(expected)
      );
  }
}
```

## Contributing

The NSpectator test suite is written in NUnit. The test project is NSpectatorDescriber. Not to be confused with SampleSpecs which hosts numerous tests written with Specs, some of which are intended to fail.

If you have Resharper there is a team-shared settings file in the repository. Please use the settings to format any new code you write.

Fork the project, make your changes, and then send a Pull Request.

### Branch housekeeping

If you are a direct contributor to the project, please keep an eye on your past development or features branches and think about archiving them once they're no longer needed. 
No worries, their commits will still be available under named tags, it's just that they will not pollute the branch list.

If you're running on a Windows OS, there's a batch script available at `scripts\archive-branch.bat`. Otherwise, the command sequence to run in a *nix shell is the following:

```bash
# Get local branch from remote, if needed
git checkout <your-branch-name>

# Go back to master
git checkout master

# Create local tag
git tag archive/<your-branch-name> <your-branch-name>

# Create remote tag
git push origin archive/<your-branch-name>

# Delete local branch
git branch -d <your-branch-name>

# Delete remote branch
git push origin --delete <your-branch-name>
```

If you need to later retrieve an archived branch, just run the following commands:

```bash
# Checkout archive tag
git checkout archive/<your-branch-name>

# (Re)Create branch
git checkout -b <some-branch-name>
```
