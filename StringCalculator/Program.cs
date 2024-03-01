var input = Console.ReadLine();

var parts = input.Split(',');
var numbers = parts.Select(int.Parse);

Console.WriteLine(numbers.Sum());