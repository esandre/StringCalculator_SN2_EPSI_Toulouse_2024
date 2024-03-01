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
            yield return [int.MaxValue, int.MaxValue];

            var random = Random.Shared;
            yield return [random.Next(), random.Next()];
        }

        [Theory]
        [MemberData(nameof(CasAPlusB))]
        public void APlusB(int a, int b)
        {
            var input = $"{a},{b}";

            var result = StringCalculator.Parse(input);

            Assert.Equal(a + b, result);
        }
    }
}