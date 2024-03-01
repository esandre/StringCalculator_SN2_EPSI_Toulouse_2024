namespace StringCalculator;

public class StringCalculator
{
    public static int Parse(string input)
    {
        var parts = input.Split(',');
        return int.Parse(parts.First()) 
            + int.Parse(parts.Last());
    }
}