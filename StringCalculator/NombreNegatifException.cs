namespace StringCalculator;

public class NombreNegatifException : Exception
{
    public int NombreFautif { get; }
    public int Position { get; }

    public NombreNegatifException(int nombreFautif, int position)
    {
        NombreFautif = nombreFautif;
        Position = position;
    }
}