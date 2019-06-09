using System;
using Testul.Services;
using Xunit;

namespace Testul.Tests
{
    public class OperatorProcessTest
    {
        [Theory]
        [InlineData("4+5*2", "14")]
        [InlineData("4+5/2", "6.5")]
        [InlineData("4+5/2-1", "5.5")]
        [InlineData("3*2+1", "7")]
        [InlineData("2/4+1*8", "8.5")]
        [InlineData("1+2*3/2", "4")]
        [InlineData("0-2/4+1*8", "7.5")]
        [InlineData("8-2+7-23", "-10")]
        public void PassedExpressionTest(string mathExpression, string expectedResult)
        {
            Assert.Equal(OperatorProcess.Evaluate(mathExpression), expectedResult);
        }

        [Theory]
        [InlineData("(3*2)+1")]
        [InlineData("a1+2")]
        [InlineData("2'7&8)9+4")]
        [InlineData("a+b*8+9")]
        [InlineData("@*8+9*8-1")]
        public void FailedExpressionFormatTest(string mathExpression)
        {
            var ex = Assert.Throws<FormatException>(() => OperatorProcess.Evaluate(mathExpression));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Theory]
        [InlineData("2+1*9/0")]
        [InlineData("1*0+1/0+9/0")]
        public void FailedExpressionDivideByZeroTest(string mathExpression)
        {
            var ex = Assert.Throws<DivideByZeroException>(() => OperatorProcess.Evaluate(mathExpression));
            Assert.Equal("Attempted to divide by zero.", ex.Message);
        }

        [Theory]
        [InlineData("-2/4+1*8")]
        [InlineData("-1/4")]
        [InlineData("-6+9")]
        [InlineData("-0+7*2+1")]
        [InlineData("-8*9")]
        public void FailedExpressionNegativeIntegerTest(string mathExpression)
        {
            var ex = Assert.Throws<ArithmeticException>(() => OperatorProcess.Evaluate(mathExpression));
            Assert.Equal("Negative Integer not permitted", ex.Message);
        }
    }
}
