using System.Linq;

namespace MinesweepBackEnd.Utility
{
    static public class MakeMines
    {
        enum TileStatus
        {
            None,
            Mine,
            Free,
            Reserved
        }

        /*
         * 1. Создать занулённую интовую матрицу и свободную матрицу статусов клетки.
         * 2. В некой квадратной окрестности клика зарезервировать все клетки.
         * 3. Вставить мины, уважая резервацию.
         */
        public static int[][] makeMines(int pickedH, int pickedW, int width, int height)
        {
            Random rand = new Random();

            int[][] grid = new int[height][];
            TileStatus[][] statusGrid = new TileStatus[height][];

            for(int i = 0; i < height; i++)
            {
                int[] gridRow = new int[width];
                TileStatus[] statusRow = new TileStatus[width];
                
                for(int j = 0; j < width; j++)
                {
                    gridRow[j] = -1;
                    statusRow[j] = TileStatus.Free;
                }

                grid[i] = gridRow;
                statusGrid[i] = statusRow;
            }

            statusGrid = reserve(pickedH, pickedW, statusGrid);
            int mines = countMines(width, height);

            for(int i = 0; i < mines; i++)
            {
                int mineHeight = rand.Next(0, height);
                int mineWidth = rand.Next(0, width);

                if (statusGrid[mineHeight][mineWidth] == TileStatus.Free)
                {
                    statusGrid[mineHeight][mineWidth] = TileStatus.Mine;
                    grid[mineHeight][mineWidth] = 1;
                } 
                else
                {
                    --i;
                }
            }

            return grid;
        }

        private static int countMines(int width, int height)
        {
            switch(width*height)
            {
                case 100:
                    return 10;
                case 256:
                    return 40;
                case 480:
                    return 99;
            }

            return 10;
        }

        private static TileStatus[][] reserve(int pickedH, int pickedW, TileStatus[][] grid)
        {
            for(int i = -2; i <= 2; i++)
            {
                for(int j = -2; j <= 2; j++)
                {
                    if(grid.ElementAtOrDefault(pickedH + i) != null)
                    {
                        if (grid[pickedH + i].ElementAtOrDefault(pickedW + j) != TileStatus.None)
                        {
                            grid[pickedH + i][pickedW + j] = TileStatus.Reserved;
                        }

                    }
                }
            }

            return grid;
        }
    }
}
