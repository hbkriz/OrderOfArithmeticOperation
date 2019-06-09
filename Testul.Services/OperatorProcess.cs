using System;
using System.Collections.Generic;
using System.Linq;

namespace Testul.Services
{
    public class OperatorProcess
    {
        private static readonly Func<char, bool> ValueIsBetweenZeroAndNine = currentChar => (currentChar >= Constants.AsciiValueForZero && currentChar <= Constants.AsciiValueForNine) || currentChar == Constants.AsciiValueForZero;
        private static readonly Func<char, bool> ValueHasValidOperators = currentChar => currentChar == Constants.AsciiValueForAddition || currentChar == Constants.AsciiValueForSubtraction || currentChar == Constants.AsciiValueForMultiplication || currentChar == Constants.AsciiValueForDivision;

        public static string Evaluate(string mathExpression)
        {
            CheckValidAscii(mathExpression);
            return ParseAddAndSubtract(mathExpression.ToCharArray(), Constants.InitialIndex).ToString();
        }

        private static double ParseAddAndSubtract(char[] numberExpression, int index)
        {
            double x = ParseMultiplyAndDivide(numberExpression, ref index);
            while (true)
            {
                var currentOperator = numberExpression[index];
                if (currentOperator != Constants.Addition && currentOperator != Constants.Subtraction)
                    return x;
                index++;
                var y = ParseMultiplyAndDivide(numberExpression, ref index);
                if (currentOperator == Constants.Addition)
                    x += y;
                else
                    x -= y;
            }
        }

        private static double ParseMultiplyAndDivide(char[] numberExpression, ref int index)
        {
            double x = ParseNumber(numberExpression, ref index);
            while (true)
            {
                char currentOperator = numberExpression[index];
                if (currentOperator != Constants.Division && currentOperator != Constants.Multiplication)
                    return x;
                index++;
                double y = ParseNumber(numberExpression, ref index);
                if (currentOperator == Constants.Division)
                {
                    x /= y;
                    if (double.IsInfinity(x))
                    {
                        throw new DivideByZeroException();
                    }
                }
                else
                    x *= y;
            }
        }

        private static double ParseNumber(char[] numberExpression, ref int index)
        {
            var number = string.Empty;
            while (ValueIsBetweenZeroAndNine(numberExpression[index]))
            {
                number = number + numberExpression[index];
                index++;
                if (index == numberExpression.Length)
                {
                    index--;
                    break;
                }
            }
            return double.Parse(number);
        }

        private static void CheckValidAscii(string mathExpression)
        {
            if(mathExpression[Constants.InitialIndex] == Constants.NegativeSymbol)
                throw new ArithmeticException("Negative Integer not permitted");

            foreach (var eachChar in mathExpression)
            {
                if (ValueIsBetweenZeroAndNine(eachChar) || ValueHasValidOperators(eachChar))
                    continue;
                throw new FormatException("Input string was not in a correct format.");
            }
        }
    }
}
