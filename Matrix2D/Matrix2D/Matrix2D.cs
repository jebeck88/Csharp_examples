using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Matrix2D
{
    /// <summary>
    /// A simple class to represent a 2D matrix, that is sized and allocated dynamically at runtime.
    /// </summary>
    public class Matrix2D<T> where T : INumber<T>
    { 
        public int ROW_COUNT { get; }

        public int COL_COUNT { get; }

        public T[,] DATA 
        {
            get => mData;
            set
            {
                if (value.GetLength(0) != mData.GetLength(0) ||
                    value.GetLength(1) != mData.GetLength(1))
                {
                    throw new ArgumentException("Size mismatch"); 
                }

                for (int i = 0; i < ROW_COUNT; i++)
                {
                    for (int j = 0; j < COL_COUNT; j++)
                    {
                        mData[i, j] = value[i, j];
                    }
                }
            }
        }

        public Matrix2D(int rowCount, int colCount)
        {
            ROW_COUNT = rowCount;
            COL_COUNT = colCount;
            mData = new T[ROW_COUNT, COL_COUNT];
        }

        /// <summary>
        /// Performs an in-place transpose
        /// </summary>
        public void Transpose()
        {
            // All rows
            for (int i = 0; i < ROW_COUNT; i++)
            {
                // Only columns below main diagonal
                for (int j = 0; j < i; j++)
                {
                    // Only need to swap off-axis values
                    if ( i != j )
                    {
                        // swap the values
                        var temp = mData[i, j];
                        mData[i, j] = mData[j, i];
                        mData[j, i] = temp;
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = "\n";
            for (int i = 0; i < ROW_COUNT; i++)
            {
                for (int j = 0; j < COL_COUNT; j++)
                {
                    result += DATA[i, j];
                    if (j < COL_COUNT - 1)
                    {
                        result += " ";
                    }
                }
                result += "\n";
            }
            return result;
        }

        private T[,] mData;
    }
}
