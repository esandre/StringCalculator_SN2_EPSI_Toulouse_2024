using System.Runtime.InteropServices;

namespace StringCalculator.Test
{
    public class StringCalculatorTest
    {
        public static IEnumerable<object[]> CasAPlusB()
        {
            yield return [0, 0];
            yield return [1, 0];
            yield return [0, 1];
            yield return [2, 0];
            yield return [0, 2];
            yield return [0, 2, 1];
            yield return [
                1000, 
                1000
            ];
        }

        [Theory]
        [MemberData(nameof(CasAPlusB))]
        public void APlusB(params int[] parts)
        {
            var input = string.Join(',', parts);

            var result = StringCalculator.Parse(input);

            Assert.Equal(parts.Sum(), result);
        }

        [Fact]
        public void Espaces()
        {
            const string test�e = "1 0, 1 0  ";

            var result = 
                StringCalculator.Parse(test�e);

            var contr�le = StringCalculator.Parse(
                test�e.Replace(" ", "")
            );

            Assert.Equal(contr�le, result);
        }

        [Theory]
        [InlineData("1001")]
        [InlineData("1002")]
        public void Superieur1000(string nbIgnor�)
        {
            var result = 
                StringCalculator.Parse(nbIgnor�);

            var contr�le = StringCalculator.Parse("0");

            Assert.Equal(contr�le, result);
        }

        [Fact]
        public void NegatifsInterdits()
        {
            const string test�e = "-1";
            
            void Act() => StringCalculator.Parse(test�e);

            var exception = Assert.Throws<NombreNegatifException>(Act);
            Assert.Equal(0, exception.Position);
            Assert.Equal(-1, exception.NombreFautif);
        }

        [Fact]
        public void NegatifsInterditsPosition1()
        {
            const string test�e = "0,-1";
            
            void Act() => StringCalculator.Parse(test�e);

            var exception = Assert.Throws<NombreNegatifException>(Act);
            Assert.Equal(1, exception.Position);
            Assert.Equal(-1, exception.NombreFautif);
        }

        [Theory]
        [InlineData("_")]
        [InlineData("#!")]
        [InlineData("\ud83e\udde0")]
        public void D�limiteur(string nouveauD�limiteur)
        {
            const string ancienD�limiteur = ",";
            var changementD�limiteur = $"//{nouveauD�limiteur}";
            var cha�neTest�e = $"3{nouveauD�limiteur}5";

            var result = 
                StringCalculator.Parse(changementD�limiteur + Environment.NewLine + cha�neTest�e);

            var contr�le = StringCalculator.Parse(
                cha�neTest�e.Replace(nouveauD�limiteur, ancienD�limiteur)
            );

            Assert.Equal(contr�le, result);
        }

        [Fact]
        public void PlusieursD�limiteurs()
        {
            const string nouveauD�limiteur1 = "_";
            const string nouveauD�limiteur2 = "\U0001f9e0";
            var d�finitionD�limiteur = $"//{nouveauD�limiteur1},{nouveauD�limiteur2}";

            var contr�le = StringCalculator.Parse("0,1,2");
            var r�sultat = StringCalculator.Parse(d�finitionD�limiteur +
                                                  Environment.NewLine +
                                                  $"0{nouveauD�limiteur1}1{nouveauD�limiteur2}2");

            Assert.Equal(contr�le, r�sultat);
        }
    }
}