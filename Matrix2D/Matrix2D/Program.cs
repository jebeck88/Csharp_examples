using System;
using System.Runtime.InteropServices;

namespace Matrix2D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create a 3x3 Identity matrix
            var mi = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Identity<double>(3)
            };
            Console.WriteLine($"i ={mi}");

            // Create an arbitrary 3x3 matrix
            var m1 = new Matrix2D<double>(3, 3)
            {
                DATA = new double[,] { 
                    { 1, 2, -1 }, 
                    { 2, 1, 2 }, 
                    { -1, 0, 1 } }
            };
            Console.WriteLine($"m1 ={m1}");

            // Get it's transpose
            var m2 = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Transpose<double>(m1.DATA)
            };
            Console.WriteLine($"m2={m2}");

            // Multiply m1 and m2
            var m3 = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Multiply<double>(m1.DATA, m2.DATA)
            };
            Console.WriteLine($"m3={m3}");

            // Transpose m2 in place
            m2.Transpose();
            Console.WriteLine($"m2'={m2}");


            //// LU decompose m2
            //var lu_m2 = new Matrix2D<double>(3, 3) {
            //    DATA = Matrix2DExtensions.LuDecompose<double>(m2.DATA)
            //};
            //Console.WriteLine($"LU(m2)'={lu_m2}");

            // LU decompose m2 into separate matrix 
            (var l, var u) = Matrix2DExtensions.LuDecompose(m1.DATA);
            var l_m1 = new Matrix2D<double>(3, 3)
            {
                DATA = l
            };
            var u_m1 = new Matrix2D<double>(3, 3)
            {
                DATA = u
            };
            Console.WriteLine($"L(m1)'={l_m1}");
            Console.WriteLine($"U(m1)'={u_m1}");

            var l_times_u = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Multiply(l, u)
            };
            Console.WriteLine($"L(m1) * U(m1)={l_times_u}");


            // Get l inverse
            var l_inv = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.InvertL(l)
            };
            Console.WriteLine($"L_inv(m1)={l_inv}");

            // Get l * l_inv
            var l_times_l_inv = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Multiply(l, l_inv.DATA)
            };
            Console.WriteLine($"L * L_inv(m1)={l_times_l_inv}");

            // Get u inverse
            var u_inv = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.InvertU(u)
            };
            Console.WriteLine($"U_inv(m1)={u_inv}");

            // Get u * u_inv
            var u_times_u_inv = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Multiply(u, u_inv.DATA)
            };
            Console.WriteLine($"U * U_inv(m1)={u_times_u_inv}");

            // Invert m1
            var m1_inv = new Matrix2D<double>(3,3)
            {
                DATA = Matrix2DExtensions.Invert(m1.DATA)
            };
            Console.WriteLine($"inv(m1)={m1_inv}");

            // Get m1 * inv_m1
            var m1_times_m1_inv = new Matrix2D<double>(3, 3)
            {
                DATA = Matrix2DExtensions.Multiply(m1.DATA, m1_inv.DATA)
            };
            Console.WriteLine($"M1 * M1_inv(m1)={m1_times_m1_inv}");

        }
    }

}