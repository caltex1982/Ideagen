using System;

namespace App
{
    public class Calculator
    {
        public static double Calculate(string sum)
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
                    if (algorithm.Contains(OperatorExtensions.MultiplyOperator))
                        value = algorithm.Multiply();
                    else if (algorithm.Contains(OperatorExtensions.DivideOperator))
                        value = algorithm.Divide();
                    else if (algorithm.Contains(OperatorExtensions.AddOperator))
                        value = algorithm.Add();
                    else if (algorithm.Contains(OperatorExtensions.SubtractOperator))
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
