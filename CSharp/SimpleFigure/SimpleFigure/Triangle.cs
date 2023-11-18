namespace SimpleFigure
{
    internal interface ITriangle
    {
        double A { get; }
        double B { get; }
        double C { get; }
        bool? CheckRightTriangle();
    }

    public class Triangle : Figure, ITriangle
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public Triangle(double a, double b, double c)
        {
            if ((a < 0) || (b < 0) || (c < 0))
                throw new ArgumentOutOfRangeException("Значение должно быть положительным");

            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Вычисление площади треугольника.
        /// </summary>
        /// <returns>True - площадь вычислена. False - не удалось вычислить площадь.</returns>
        public bool СalculateSquare()
        {
            try
            {
                double p = (A + B + C) / 2;
                Square = Math.Sqrt(p * (p - A) * (p - B) * (p - C));

                return true;
            }
            catch
            {
                Square = 0;

                return false;
            }
        }

        /// <summary>
        /// Проверка на то, является ли треугольник прямоугольным
        /// </summary>
        /// <returns>True - треугольник прямоугольный. False - треугольник непрямоугольный. Null - не удалось выполнить проверку.</returns>
        public bool? CheckRightTriangle()
        {
            try
            {
                bool result = false;

                // Проверяем теорему для прямоугольного треугольника:
                // сумма квадратов катетов равна квадрату гипотенузы.
                result = (A * A) == (B * B) + (C * C);

                if (!result)
                    result = (B * B) == (A * A) + (C * C);

                if (!result)
                    result = (C * C) == (A * A) + (B * B);

                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
