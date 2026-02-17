using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Matrix2D
{
    public static class Matrix2DExtensions
    {
        /// <summary>
        /// Create an return an identity matrix
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="size">the size</param>
        /// <returns>identity matrix</returns>
        /// <exception cref="ArgumentException"></exception>
        public static T[,] Identity<T>(int size) where T : INumber<T>
        {
            if( size < 1 )
            {
                throw new ArgumentException($"size = { size }, but must be a positive integer");
            }
            // Create result
            T[,] result = new T[size, size];

            // Initialize to identity
            for( int i = 0; i < size; i++)
            {
                result[i, i] = T.CreateChecked(1.0);
            }

            return result;
        }

        /// <summary>
        /// Returns a new matrix which is the transpose if the input
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="input">the input matrix</param>
        /// <returns>A new metrix which is a transpose of the original</returns>
        public static T[,] Transpose<T>(T[,] input) where T : INumber<T>
        {
            var result = new T[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for( int j = 0; j < input.GetLength(1); j++)
                {
                    result[i, j] = input[j, i];
                }
            }
            return result;
        }

        /// <summary>
        /// Returns a new matrix which is the transpose if the input
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="input">the input matrix</param>
        /// <returns>A new metrix which is a transpose of the original</returns>
        public static T[,] Multiply<T>(T[,] i1, T[,] i2) where T : INumber<T> 
        {
            // Intiialize dimensions and check the arguments
            int oRows = i1.GetLength(0);
            int oCols = i2.GetLength(1);

            if ( i1.GetLength(1) != i2.GetLength(0) )
            {
                throw new ArgumentException($"Invalid matrix dims: ({i1.GetLength(0)}, {i1.GetLength(1)}) x ({i2.GetLength(0)},{i2.GetLength(1)})");
            }

            int span = i1.GetLength(1);

            // Intiialize the result
            var result = new T[i1.GetLength(0), i2.GetLength(1)];

            // Do the matrix multiply in a triple loop
            for (int i = 0; i < oRows; i++)
            {
                for (int j = 0; j < oCols; j++)
                {
                    for (int k = 0; k < span; k++)
                    {
                        result[i, j] += i1[i, k] * i2[k, j]; 
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Performs LU decomposition on a matrix
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="a">the matrix</param>
        /// <returns>L and U</returns>
        /// <exception cref="ArgumentException">if the argument is invalid</exception>        
        public static (T[,], T[,]) LuDecompose<T>(T[,] a) where T: INumber<T>
        {
            if (a.GetLength(0) != a.GetLength(1))
            {
                throw new ArgumentException("Only supports square matrices");
            }
            int n = a.GetLength(0);

            // Initialize L and U as separate matrices
            var l = new T[n, n];
            var u = new T[n, n];

            // Separate LU into separate L and U matrices
            for( int i = 0; i < n; i++)
            {
                // Upper Triangular
                for (int k = i; k < n; k++)
                {
                    // Summation of L(i, j) * U(j, k)
                    T sum = T.CreateChecked(0.0);
                    for (int j = 0; j < i; j++)
                        sum += (l[i, j] * u[j, k]);

                    // Evaluating U(i, k)
                    u[i, k] = a[i, k] - sum;
                }

                // Lower Triangular
                for (int k = i; k < n; k++)
                {
                    if (i == k)
                        l[i, i] = T.CreateChecked(1.0); // Diagonal as 1
                    else
                    {
                        // Summation of L(k, j) * U(j, i)
                        T sum = T.CreateChecked(0.0);
                        for (int j = 0; j < i; j++)
                            sum += (l[k, j] * u[j, i]);

                        // Evaluating L(k, i)
                        l[k, i]
                            = (a[k, i] - sum) / u[i, i];
                    }
                }
            }
            return (l, u);
        }

        /// <summary>
        /// Inverts a matrix
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="a">the input matrix</param>
        /// <returns>the inverse</returns>
        public static T[,] Invert<T>(T[,] a) where T : INumber<T>
        {
            // LU decompose a
            (var l, var u) = LuDecompose<T>(a);

            // invert L
            var l_inv = InvertL(l);

            // invert U
            var u_inv = InvertU(u);

            // Calc result
            var result = Multiply(u_inv, l_inv);

            return result;
        }

        public static T[,] InvertL<T>(T[,] l) where T : INumber<T>
        {
            double TOLERANCE = 1e-9;

            int n = l.GetLength(0);

            for (int i = 0; i < n; ++i)
            {
                if (Math.Abs(Convert.ToDouble(l[i, i])) < TOLERANCE)
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            // Initialize result
            T[,] result = Identity<T>(n);

            // Iterate once over all elements
            for (int k = 0; k < n; ++k)
            {
                for (int j = 0; j < n; ++j)
                {
                    for (int i = 0; i < k; ++i)
                    {
                        result[k, j] -= result[i, j] * l[k, i];
                    }
                    result[k, j] /= l[k, k];
                }
            }
            return result;
        }

        public static T[,] InvertU<T>(T[,] u) where T : INumber<T>
        {
            double TOLERANCE = 1e-9;

            int n = u.GetLength(0);

            for (int i = 0; i < n; ++i)
            {
                if (Math.Abs(Convert.ToDouble(u[i, i])) < TOLERANCE)
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            // Initialize result
            T[,] result = Identity<T>(n);
            for (int k = 0; k < n; ++k)
            {
                for (int j = 0; j < n; ++j)
                {
                    for (int i = 0; i < k; ++i)
                    {
                        result[j, k] -= result[j, i] * u[i, k];
                    }
                    result[j, k] /= u[k, k];
                }
            }
            return result;
        }
    }
}