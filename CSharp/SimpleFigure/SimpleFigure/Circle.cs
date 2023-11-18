namespace SimpleFigure
{
    internal interface ICircle
    {
        /// <summary>
        /// Радиус круга.
        /// </summary>
        double Radius { get; }
    }

    public class Circle : Figure, ICircle
    {
        public double Radius { get; }

        public Circle(double radius)
        {
            if (radius < 0)
                throw new ArgumentOutOfRangeException("Значение должно быть положительным");

            Radius = radius;
        }

        /// <summary>
        /// Вычисление площади круга.
        /// </summary>
        /// <returns>True - площадь вычислена. False - не удалось вычислить площадь.</returns>
        public bool СalculateSquare()
        {
            try
            {
                Square = Math.PI * Math.Pow(Radius, 2);

                return true;
            }
            catch
            {
                Square = 0;

                return false;
            }
        }
    }
}
