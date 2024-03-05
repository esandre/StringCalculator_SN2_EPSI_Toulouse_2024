namespace StringCalculator;

public class StringCalculator
{
    public static int Parse(string input)
    {
        string[] délimiteurs = [","];
        if (input.StartsWith("//"))
        {
            var lines = input.Split(Environment.NewLine);
            var firstLine = lines.First();

            délimiteurs = firstLine.TrimStart('/').Split(',');

            input = string.Join(Environment.NewLine, lines.Skip(1));
        }

        string[] parts = [];
        foreach (var délimiteur in délimiteurs)
        {
            parts = parts.SelectMany(part => part.Split(délimiteur)).ToArray();
        }

        var numbers = parts
            .EraseAllSpaces()
            .Select(int.Parse)
            .Select(IgnoreTooBig)
            .ToArray();

        for (var i = 0; i < numbers.Length; i++)
        {
            var nb = numbers[i];
            if (nb < 0) throw new NombreNegatifException(nb, i);
        }

        return numbers.Sum();
    }

    private static int IgnoreTooBig(int nb)
    {
        if (nb >= 1001) return 0;
        return nb;
    }
}