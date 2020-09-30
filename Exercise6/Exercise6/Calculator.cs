using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Exercise6
{
    public class Calculator
    {

        public double Accumulator { get; private set; } = 0;

        public void Clear()
        {
            Accumulator = 0;
        }

        public double Add(double a, double b)
        {
            Accumulator = a + b;
            return Accumulator;
        }

        public double Add(double addend)
        {
            Accumulator += addend;
            return Accumulator;
        }

        public double Subtract(double a, double b)
        {
            Accumulator = a - b;
            return Accumulator;
        }

        public double Subtract(double subtractor)
        {
            Accumulator -= subtractor;
            return Accumulator;
        }

        public double Multiply(double a, double b)
        {
            Accumulator = a * b;
            return Accumulator;
        }

        public double Multiply(double multiplier)
        {
            Accumulator *= multiplier;
            return Accumulator;
        }

        public double Power(double x, double exp)
        {
            Accumulator = Math.Pow(x, exp);
            return Accumulator;
        }

        public double Power(double exponent)
        {
            Accumulator = Math.Pow(Accumulator, exponent);
            return Accumulator;
        }

        public double Divide(double dividend, double divisor)
        {
            if (divisor == 0)
            {
                Accumulator = 0;
                throw new ArgumentException("Der kan ikke divideres med nul");
            }

            /*
             * Alternativ løsning
            try
            {
                Accumulator = a / b;
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
                Accumulator = 0;
            }
            */
            Accumulator = dividend / divisor;
            return Accumulator;
        }

        public double Divide(double divisor)
        {
            if (divisor == 0)
            {
                Accumulator = 0;
                throw new ArgumentException("Der kan ikke divideres med nul");
            }

            Accumulator /= divisor;
            return Accumulator;
        }

    }
}
