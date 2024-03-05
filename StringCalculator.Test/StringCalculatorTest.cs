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
            const string testée = "1 0, 1 0  ";

            var result = 
                StringCalculator.Parse(testée);

            var contrôle = StringCalculator.Parse(
                testée.Replace(" ", "")
            );

            Assert.Equal(contrôle, result);
        }

        [Theory]
        [InlineData("1001")]
        [InlineData("1002")]
        public void Superieur1000(string nbIgnoré)
        {
            var result = 
                StringCalculator.Parse(nbIgnoré);

            var contrôle = StringCalculator.Parse("0");

            Assert.Equal(contrôle, result);
        }

        [Fact]
        public void NegatifsInterdits()
        {
            const string testée = "-1";
            
            void Act() => StringCalculator.Parse(testée);

            var exception = Assert.Throws<NombreNegatifException>(Act);
            Assert.Equal(0, exception.Position);
            Assert.Equal(-1, exception.NombreFautif);
        }

        [Fact]
        public void NegatifsInterditsPosition1()
        {
            const string testée = "0,-1";
            
            void Act() => StringCalculator.Parse(testée);

            var exception = Assert.Throws<NombreNegatifException>(Act);
            Assert.Equal(1, exception.Position);
            Assert.Equal(-1, exception.NombreFautif);
        }

        [Theory]
        [InlineData("_")]
        [InlineData("#!")]
        [InlineData("\ud83e\udde0")]
        public void Délimiteur(string nouveauDélimiteur)
        {
            const string ancienDélimiteur = ",";
            var changementDélimiteur = $"//{nouveauDélimiteur}";
            var chaîneTestée = $"3{nouveauDélimiteur}5";

            var result = 
                StringCalculator.Parse(changementDélimiteur + Environment.NewLine + chaîneTestée);

            var contrôle = StringCalculator.Parse(
                chaîneTestée.Replace(nouveauDélimiteur, ancienDélimiteur)
            );

            Assert.Equal(contrôle, result);
        }

        [Fact]
        public void PlusieursDélimiteurs()
        {
            const string nouveauDélimiteur1 = "_";
            const string nouveauDélimiteur2 = "\U0001f9e0";
            var définitionDélimiteur = $"//{nouveauDélimiteur1},{nouveauDélimiteur2}";

            var contrôle = StringCalculator.Parse("0,1,2");
            var résultat = StringCalculator.Parse(définitionDélimiteur +
                                                  Environment.NewLine +
                                                  $"0{nouveauDélimiteur1}1{nouveauDélimiteur2}2");

            Assert.Equal(contrôle, résultat);
        }
    }
}