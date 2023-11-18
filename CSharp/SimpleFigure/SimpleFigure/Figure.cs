namespace SimpleFigure
{
    public class Figure : IFigure
    {
        /// <summary>
        /// Площадь
        /// </summary>
        public double Square { get; protected set; }

        public bool СalculateSquare(params double[] prm)
        {
            if (prm.Length == 1)
            {
                return СalculateSquare(prm[0]);
            }
            if (prm.Length == 3)
            {
                return СalculateSquare(prm[0], prm[1], prm[2]);
            }

            return false;
        }

        /// <summary>
        /// Вычисление круга по радиусу.
        /// </summary>
        /// <param name="radius">Радиус</param>
        /// <returns>True - площадь вычислена. False - не удалось вычислить площадь.</returns>
        private bool СalculateSquare(double radius)
        {
            Circle circle = new Circle(radius);

            bool result = circle.СalculateSquare();
            Square = circle.Square;

            return result;
        }
        /// <summary>
        /// Вычисление площади по 3 стороном.
        /// </summary>
        /// <param name="a">Сторона треугольника.</param>
        /// <param name="b">Сторона треугольника.</param>
        /// <param name="c">Сторона треугольника.</param>
        /// <returns>True - площадь вычислена. False - не удалось вычислить площадь.</returns>
        private bool СalculateSquare(double a, double b, double c)
        {
            Triangle triangle = new(a, b, c);

            bool result = triangle.СalculateSquare();
            Square = triangle.Square;

            return result;
        }
    }
}
