using NSpectator;

public class My_first_spec : Spec
{
    void given_the_world_has_not_come_to_an_end()
    {
        it["Hello World should be Hello World"] = () => "Hello World".should_be("Hello World");
    }
}

public static class My_first_spec_output
{
    public static string Output = @"
my first spec
  given the world has not come to an end
    Hello World should be Hello World

1 Examples, 0 Failed, 0 Pending
";
    public static int ExitCode = 0;
}