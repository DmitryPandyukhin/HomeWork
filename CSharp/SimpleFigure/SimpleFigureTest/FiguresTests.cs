using SimpleFigure;
using System;

namespace SimpleFigureTest
{
    [TestClass]
    public class FiguresTests
    {
        /// <summary>
        /// Тест на отрицательность входных данных круга.
        /// </summary>
        [TestMethod]
        public void TestCircleNegativeArguments()
        {
            double radius = -5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Circle(radius));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Figure().СalculateSquare(radius));
        }

        /// <summary>
        /// Тест на отрицательность входных данных треугольника.
        /// </summary>
        [TestMethod]
        public void TestTriangleNegativeArguments()
        {
            double a = -5;
            double b = 5;
            double c = 5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Figure().СalculateSquare(a, b, c));
        }

        /// <summary>
        /// Второй тест на отрицательность входных данных треугольника.
        /// </summary>
        [TestMethod]
        public void TestTriangleNegativeArguments2()
        {
            double a = 5;
            double b = -5;
            double c = 5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Figure().СalculateSquare(a, b, c));
        }

        /// <summary>
        /// Третий тест на отрицательность входных данных треугольника.
        /// </summary>
        [TestMethod]
        public void TestTriangleNegativeArguments3()
        {
            double a = 5;
            double b = 5;
            double c = -5;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Figure().СalculateSquare(a, b, c));
        }

        /// <summary>
        /// Тест на правильность вычисления площади круга.
        /// </summary>
        [TestMethod]
        public void TestСalculateCircleArea()
        {
            double radius = 5;
            double expected = Math.PI * (Math.Pow(radius, 2));

            // Площадь круга.
            Circle circle = new(radius);
            circle.СalculateSquare();
            Assert.AreEqual(expected, circle.Square);

            Figure figure = new Figure();
            figure.СalculateSquare(radius);
            Assert.AreEqual(expected, figure.Square);
        }

        /// <summary>
        /// Тест на правильность вычисления площади треугольника.
        /// </summary>
        [TestMethod]
        public void TestСalculateTriangleArea()
        {
            double a = 5;
            double b = 6;
            double c = 2.2;
            double s = 5.28;

            Triangle triangle = new(a, b, c);
            triangle.СalculateSquare();
            Assert.AreEqual(s, Math.Round(triangle.Square, 2));

            Figure figure = new Figure();
            figure.СalculateSquare(a, b, c);
            Assert.AreEqual(s, Math.Round(figure.Square, 2));
        }

        /// <summary>
        /// Тест проверки на то, является ли треугольник прямоугольным.
        /// </summary>
        [TestMethod]
        public void TestCheckRightTriangle()
        {
            double a = 6;
            double b = 8;
            double c = 10;

            Triangle triangle = new(a, b, c);
            bool? result = triangle.CheckRightTriangle();
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Тест проверки на то, не является ли треугольник прямоугольным.
        /// </summary>
        [TestMethod]
        public void TestCheckNotRightTriangle()
        {
            double a = 6;
            double b = 8;
            double c = 11;

            Triangle triangle = new(a, b, c);
            bool? result = triangle.CheckRightTriangle();
            Assert.AreEqual(false, result);
        }
    }
}