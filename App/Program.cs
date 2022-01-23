using System;

namespace App
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
                Console.WriteLine($"{formulas} = {Calculator.Calculate(formulas)}");
            }
        }
    }
}
