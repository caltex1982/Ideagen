using Calculator;
using System;

namespace Ideagen
{
    class Program
    {
        private static readonly string[] _formulas = new string[] {
            "1 + 1",                        //2
            "2 * 2",                        //4
            "1 + 2 + 3",                    //6
            "6 / 2",                        //3
            "11 + 23",                      //34
            "11.1 + 23",                    //34.1
            "1 + 1 * 3",                    //4
            "( 11.5 + 15.4 ) + 10.1",       //37
            "23 - ( 29.3 - 12.5 )",         //6.199999999999999
            "( 1 / 2 ) - 1 + 1",            //-1.5
            "10 - ( 2 + 3 * ( 7 - 5 ) )"    //2
        };

        static void Main(string[] args)
        {
            foreach (string formulas in _formulas)
            {
                Console.WriteLine($"{formulas} = {Calculate(formulas)}");
            }
        }

        private static double Calculate(string sum)
        {
            var result = 0.0;
            var isBracket = false;
            var formula = sum.ToFormula(out isBracket);
            var originalFormula = formula;

            do
            {
                var op = formula.ToOperatorSequence();
                do
                {
                    var algorithm = formula.ToAlgorithm(op.Dequeue());

                    var value = 0.0;
                    if (algorithm.Contains(StringExtensions._multiplyOperator))
                        value = algorithm.Multiply();
                    else if (algorithm.Contains(StringExtensions._divideOperator))
                        value = algorithm.Divide();
                    else if (algorithm.Contains(StringExtensions._addOperator))
                        value = algorithm.Add();
                    else if (algorithm.Contains(StringExtensions._subtractOperator))
                        value = algorithm.Subtract();

                    formula = formula.Replace(algorithm, value.ToString());
                    op = formula.ToOperatorSequence();
                    result = Convert.ToDouble(value);
                } while (op.Count >= 1);

                if (isBracket) originalFormula = $"({originalFormula})";

                sum = sum.Replace(originalFormula, result.ToString());
                formula = sum.ToFormula(out isBracket);
                originalFormula = formula;
            } while (sum.ToOperatorSequence().Count >= 1);

            return result;
        }
    }
}