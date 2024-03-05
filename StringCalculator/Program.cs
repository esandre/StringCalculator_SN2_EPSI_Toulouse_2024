using StringCalculator;

var input = Console.ReadLine();

try
{
    Console.WriteLine(StringCalculator.StringCalculator.Parse(input));
}
catch (NombreNegatifException e)
{
    Console.WriteLine($"Nombre négatif {e.NombreFautif} à la position {e.Position}");
}