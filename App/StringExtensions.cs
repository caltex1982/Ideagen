using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public static class StringExtensions
    {
        private static readonly char[] _operators = {
            OperatorExtensions.MultiplyOperator,
            OperatorExtensions.DivideOperator,
            OperatorExtensions.AddOperator,
            OperatorExtensions.SubtractOperator
        };

        public static string ToAlgorithm(this string value, int operatorIndex)
        {
            value = value.RemoveStartAndEndWithSpace();
            var values = value.Split(" ");
            return $"{values[operatorIndex - 1]} {values[operatorIndex]} {values[operatorIndex + 1]}";
        }

        public static Queue<int> ToOperatorSequence(this string value)
        {
            value = value.RemoveStartAndEndWithSpace();

            var values = value.Split(" ");
            if (value.Length == 0) return new Queue<int>();

            Queue<int> queues = new Queue<int>();
            foreach (var op in _operators)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                {
                    if (values[i].Equals(op.ToString())) queues.Enqueue(i);
                }
            }

            return queues;
        }

        public static string ToFormula(this string value, out bool isBracket)
        {
            var index = value.Count(x => x == '(');
            if (index != 0)
            {
                isBracket = true;
                return value.Split('(', ')')[index];
            }

            isBracket = false;
            return value;
        }

        public static string RemoveStartAndEndWithSpace(this string value)
        {
            if (value.StartsWith(" ")) value = value.Remove(0, 1);
            if (value.EndsWith(" ")) value = value.Remove(value.Length - 1, 1);

            return value;
        }

        public static double Multiply(this string value)
        {
            var result = 0.0;
            var values = value.Split(OperatorExtensions.MultiplyOperator, StringSplitOptions.TrimEntries);

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (i == 0)
                {
                    result += Convert.ToDouble(values[i]);
                    continue;
                }

                result *= Convert.ToDouble(values[i]);
            }

            return result;
        }

        public static double Divide(this string value)
        {
            var result = 0.0;
            var values = value.Split(OperatorExtensions.DivideOperator, StringSplitOptions.TrimEntries);

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (i == 0)
                {
                    result += Convert.ToDouble(values[i]);
                    continue;
                }

                result /= Convert.ToDouble(values[i]);
            }

            return result;
        }

        public static double Add(this string value)
        {
            var result = 0.0;
            var values = value.Split(OperatorExtensions.AddOperator, StringSplitOptions.TrimEntries);

            foreach (var v in values)
            {
                result += Convert.ToDouble(v);
            }

            return result;
        }

        public static double Subtract(this string value)
        {
            var result = 0.0;
            var values = value.Split(OperatorExtensions.SubtractOperator, StringSplitOptions.TrimEntries);

            for (int i = 0; i <= values.Length - 1; i++)
            {
                if (i == 0)
                {
                    result += Convert.ToDouble(values[i]);
                    continue;
                }

                result -= Convert.ToDouble(values[i]);
            }

            return result;
        }
    }
}
