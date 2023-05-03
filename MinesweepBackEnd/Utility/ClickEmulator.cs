using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace MinesweepBackEnd.Utility
{
    public static class ClickEmulator
    {
        public static int[][] rightClick(int[][] layout, int pressedH, int pressedW)
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

        public static int[][] leftClick(int[][] layout, int[][] mines, int pressedH, int pressedW)
        {
            switch (layout[pressedH][pressedW])
            {
                case 9:
                    {
                        layout = openTile(layout, mines, pressedH, pressedW);
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
                        layout = openAdjacent(layout, mines, pressedH, pressedW);
                        break;
                    }

            }

            return layout;
        }

        private static int[][] openTile(int[][] layout, int[][] mines, int thisHeight, int thisWidth)
        {
            bool openAdj = false;

            switch(countTileNumber(mines, thisHeight, thisWidth))
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
                layout = openAdjacent(layout, mines, thisHeight, thisWidth);
            }

            return layout;
        }

        private static int[][] openAdjacent(int[][] layout, int[][] mines, int thisHeight, int thisWidth) {
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
                            layout = openTile(layout, mines, thisHeight + i, thisWidth + j);
                        }
                    }
                }
            }

            return layout;
        }

        private static bool checkFlags(int[][] layout, int thisHeight, int thisWidth, int hasToBe)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        if (layout.ElementAtOrDefault(thisHeight + i) != null &&
                            layout[thisHeight + i].ElementAtOrDefault(thisWidth + j) != 0 &&
                            layout[thisHeight + i][thisWidth + j] == -2)
                        {
                            count++;
                        }
                    }
                }
            }

            if(count == hasToBe)
            {
                return true;
            }

            return false;
        }

        private static int countTileNumber(int[][] mines, int thisHeight, int thisWidth) {
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
