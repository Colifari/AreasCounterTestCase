using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreasCounter
{
    /// <summary>
    /// Counts number of areas formed of number 1 in provided matrix
    /// </summary>
    public class AreasCounterInMatrix
    {
        int imax = 0;
        int jmax = 0;

        public int Count(string arg)
        {
            var matrix = createMatrix(arg.Trim().Replace("\r\n", string.Empty).Replace(" ", string.Empty));

            imax = matrix.Count;
            if (imax == 0)
            {
                Console.WriteLine(0);
                return 0;
            }

            jmax = matrix[0].Count;
            var i = 0;
            var j = 0;
            var figures = new List<HashSet<Point>>();

            for (int z = 0; z < imax * jmax; z++)
            {
                if (j >= jmax)
                {
                    i++;
                    j = 0;
                }

                // 0
                if (matrix[i][j] == 0)
                {
                    j++;
                    continue;
                }

                // 1

                if (!figures.Any(f => f.Contains(new(i, j))))
                {
                    var figure = new HashSet<Point>() { new(i, j) };
                    figures.Add(figure);
                    dig(i, j, matrix, figure);
                }

                j++;
            }

            Console.WriteLine(figures.Count);
            return figures.Count;
        }

        /// <summary>
        /// Trying to find neares 1's and add them to provided figure
        /// </summary>
        /// <param name="i">Current matrix row</param>
        /// <param name="j">>Current matrix column</param>
        /// <param name="matrix">Provided matrix</param>
        /// <param name="figure">Current figure of 1's</param>
        void dig(int i, int j, List<List<int>> matrix, HashSet<Point> figure)
        {
            if (i != 0 && matrix[i - 1][j] == 1) // top
            {
                if (figure.Add(new(i - 1, j)))
                    dig(i - 1, j, matrix, figure);
            }
            if (j != jmax - 1 && matrix[i][j + 1] == 1) // right
            {
                if (figure.Add(new(i, j + 1)))
                    dig(i, j + 1, matrix, figure);
            }
            if (i != imax - 1 && matrix[i + 1][j] == 1) // bottom
            {
                if (figure.Add(new(i + 1, j)))
                    dig(i + 1, j, matrix, figure);
            }
            if (j != 0 && matrix[i][j - 1] == 1) // left
            {
                if (figure.Add(new(i, j - 1)))
                    dig(i, j - 1, matrix, figure);
            }
        }

        /// <summary>
        /// Creates matrix from provided string
        /// </summary>
        /// <param name="arg">string that represent matrix (1,0,1;0,1,0)</param>
        /// <returns></returns>
        List<List<int>> createMatrix(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                return new List<List<int>>();

            var matrix = new List<List<int>>
            {
                new() { 0 }
            };

            var i = 0;
            var j = 0;

            for (int z = 0; z < arg.Length; z++)
            {
                if (arg[z] == ',')
                {
                    matrix[i].Add(0);
                    j++;
                }
                else if (arg[z] == ';' && z != arg.Length - 1)
                {
                    matrix.Add(new List<int>() { 0 });
                    i++;
                    j = 0;
                }
                else
                {
                    matrix[i][j] = arg[z] == '1' ? 1 : 0;
                }
            }

            return matrix;
        }

    }
}
