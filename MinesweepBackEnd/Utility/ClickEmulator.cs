using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MinesweepBackEnd.Utility
{
    public static class ClickEmulator
    {
        public static int[][] RightClick(int[][] layout, int pressedH, int pressedW)
        {
            if (layout[pressedH][pressedW] == -2)
            {
                layout[pressedH][pressedW] = 9;
            }
            if(layout[pressedH][pressedW] == 9)
            {
                layout[pressedH][pressedW] = -2;
            }
            return layout;
        }

        public static int[][] LeftClick(int[][] layout, int[][] mines, int pressedH, int pressedW)
        {
            switch (layout[pressedH][pressedW])
            {
                case 9:
                    {
                        layout = OpenTile(layout, mines, pressedH, pressedW);
                        break;
                    }
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    {
                        layout = OpenAdjacent(layout, mines, pressedH, pressedW);
                        break;
                    }

            }

            return layout;
        }

        private static int[][] OpenTile(int[][] layout, int[][] mines, int thisHeight, int thisWidth)
        {
            bool openAdj = false;

            switch(CountTileNumber(mines, thisHeight, thisWidth))
            {
                case 1:
                    {
                        layout[thisHeight][thisWidth] = 1;
                        break;
                    }
                case 2:
                    {
                        layout[thisHeight][thisWidth] = 2;
                        break;
                    }
                case 3:
                    {
                        layout[thisHeight][thisWidth] = 3;
                        break;
                    }
                case 4:
                    {
                        layout[thisHeight][thisWidth] = 4;
                        break;
                    }
                case 5:
                    {
                        layout[thisHeight][thisWidth] = 5;
                        break;
                    }
                case 6:
                    {
                        layout[thisHeight][thisWidth] = 6;
                        break;
                    }
                case 7:
                    {
                        layout[thisHeight][thisWidth] = 1;
                        break;
                    }
                case 8:
                    {
                        layout[thisHeight][thisWidth] = 1;
                        break;
                    }
                case 0:
                    {
                        layout[thisHeight][thisWidth] = -1;
                        openAdj = true;
                        break;
                    }

            }

            if(openAdj)
            {
                layout = OpenAdjacent(layout, mines, thisHeight, thisWidth);
            }

            return layout;
        }

        private static int[][] OpenAdjacent(int[][] layout, int[][] mines, int thisHeight, int thisWidth) {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        if (layout.ElementAtOrDefault(thisHeight + i) != null &&
                            layout[thisHeight + i].ElementAtOrDefault(thisWidth + j) != 0 &&
                            layout[thisHeight + i][thisWidth + j] == 9)
                        {
                            layout = OpenTile(layout, mines, thisHeight + i, thisWidth + j);
                        }
                    }
                }
            }

            return layout;
        }

        private static int CountTileNumber(int[][] mines, int thisHeight, int thisWidth) {
            int count = 0;

            for(int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if(i != 0 || j != 0)
                    {
                        if (mines.ElementAtOrDefault(thisHeight + i) != null &&
                            mines[thisHeight + i].ElementAtOrDefault(thisWidth + j) != 0 &&
                            mines[thisHeight + i][thisWidth+j] == 1)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}
