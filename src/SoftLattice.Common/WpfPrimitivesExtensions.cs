using System;
using System.Windows;
using System.Windows.Media;

namespace SoftLattice.Common
{
    /// <summary>
    /// Some extension methods to deal with WPF primitives
    /// </summary>
    public static class WpfPrimitivesExtensions
    {
        
        /// <summary>
        /// New point based on an old one and an offset
        /// </summary>
        public static Point Translate(this Point p, double x, double y)
        {
            return p.Translate(new Vector(x, y));
        }

        /// <summary>
        /// New point by translating an old one by a vector
        /// </summary>
        public static Point Translate(this Point p, Vector vector)
        {
            return p + vector;
        }

        /// <summary>
        /// New point by translating along the x-axis
        /// </summary>
        public static Point TranslateX(this Point p, double xOffset)
        {
            return p.Translate(xOffset, 0);
        }

        /// <summary>
        /// New point by translating along the y-axis
        /// </summary>
        public static Point TranslateY(this Point p, double yOffset)
        {
            return p.Translate(0, yOffset);
        }

        /// <summary>
        /// Get a thickness froma point where X,Y components become Left and Top components respectively
        /// </summary>
        public static Thickness ToMargin(this Point p)
        {
            return new Thickness(p.X, p.Y,0,0);
        }

        /// <summary>
        /// Rotate a vector through an angle in radians
        /// </summary>
        public static Vector Rotate(this Vector v, double angleInRadians)
        {
            var rotationMatrix = GetRotationMatrix(angleInRadians);
            return v*rotationMatrix;
        }

        private static Matrix GetRotationMatrix(double angleInRadians)
        {
            var cos = Math.Cos(angleInRadians);
            var sin = Math.Sin(angleInRadians);
            return new Matrix(cos, -sin, sin, cos, 0, 0);
        }

        /// <summary>
        /// Returns the Midpoint of a size
        /// </summary>
        public static Point MidPoint(this Size rectangle)
        {
            return new Point(rectangle.Width / 2.0 , rectangle.Height / 2.0);
        }

        /// <summary>
        /// Returns the midpoint of a rectangle also taking the offset components of the rectangle into consideration
        /// </summary>
        public static Point MidPoint(this Rect rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2.0, rectangle.Y + rectangle.Height / 2.0);
        }

    }
}