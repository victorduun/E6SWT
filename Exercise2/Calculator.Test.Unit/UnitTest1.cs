using NUnit.Framework;


namespace Calculator.Test.Unit
{
    public class Tests
    {

        /*
         * 
         * ==== Exercise 4 ====
         * 
         * Extensibility: I would argue that it is easier to extend unit tests by using a unit test framework such as NUnit, because all
         * that is required when adding one or more unit tests in the form of functions in order to test a new feature of a class.
         * 
         * Maintainability:
         * If unit tests are done properly, that is, they are focused on the single-responsibility principle and very descriptive in 
         * their names - then unit tests can become much easier to maintain because they are easy to understand.
         * 
         * Readability:
         * As explained above, good naming convention is important. Also unit testing with a unit test framework is very easy to understand
         * because functions such as "Assert" and "Is" help a great deal when writing unit tests that should be simple to understand.
         * 
         * Autmation:
         * Unit testing with a framework is the easiest to automate. That is because frameworks are also made to enable unit test automation.
         * For example, unit tests can easily be setup to be run every time a project is built.
         * 
         * ==== Exercise 4 ====
         * 
         */

        Exercise2.Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Exercise2.Calculator();
        }

        //Testcase for exercise 5(Optional)
        [TestCase(4, 4, 8)]
        [TestCase(15, 2, 17)]
        [TestCase(-10, 4, -6)]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        public void Add_SumOfParameters_ReturnsCorrectSum(double par1, double par2, double result)
        {
            Assert.That(_calculator.Add(par1, par2), Is.EqualTo(result));
        }

        [Test]
        public void Add_SumOf2Plus2_Returns4()
        {
            Assert.That(_calculator.Add(2, 2), Is.EqualTo(4));
        }

        [Test]
        public void Add_SumOf2Minus8_ReturnsMinus6()
        {
            Assert.That(_calculator.Add(2, -8), Is.EqualTo(-6));
        }


        [Test]
        public void Add_DifferenceBetween30And15_Returns15()
        {
            Assert.That(_calculator.Subtract(30, 15), Is.EqualTo(15));
        }


        [Test]
        public void Multiply_ProductOf10And10_Returns100()
        {
            Assert.That(_calculator.Multiply(10, 10), Is.EqualTo(100));
        }

        [Test]
        public void Power_PowerOf10To3_Returns1000()
        {
            Assert.That(_calculator.Power(10, 3), Is.EqualTo(1000));
        }
        [Test]
        public void Power_PowerOf9To0point5_Returns3()
        {
            Assert.That(_calculator.Power(9, 0.5), Is.EqualTo(3));
        }
    }
}