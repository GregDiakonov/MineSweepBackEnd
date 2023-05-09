using System.Drawing.Drawing2D;

namespace MinesweepBackEnd.Utility
{
    public static class ParseMatrix
    {
        public static int[][] Parse(int[] unparsed, int height, int width)
        {
            int[][] matrix = new int[height][];

            for(int i = 0; i < height; i++)
            {
                int[] row = new int[width];
                for(int j = 0; j < width; j++)
                {
                    row[j] = unparsed[height * i + j];
                }

                matrix[i] = row;
            }

            return matrix;
        }

        public static int[] Unparse(int[][] parsed, int height, int width)
        {
            int[] unparsed = new int[height * width];

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    unparsed[height * i + j] = parsed[i][j];
                }
            }

            return unparsed;
        }
    }
}
