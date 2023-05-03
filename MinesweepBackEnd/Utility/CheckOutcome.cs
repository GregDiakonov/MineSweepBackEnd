namespace MinesweepBackEnd.Utility
{
    static public class CheckOutcome
    {
        static public bool checkLoss(int[][] layout)
        {
            for(int i = 0; i < layout.Length; i++)
            {
                for(int j = 0;  j < layout[i].Length; j++)
                {
                    if (layout[i][j] == -4)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static public bool checkWin(int[][] layout, int[][] mines)
        {
            for(int i = 0; i < layout.Length; i++)
            {
                for(int j = 0; j < layout[i].Length; j++)
                {
                    if (mines[i][j] == -1 && (layout[i][j] == 9 || layout[i][j] == -2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static public int[][] highlightMines(int[][] layout, int[][] mines)
        {
            for(int i = 0; i < layout.Length; i++)
            {
                for(int j = 0; j < layout[i].Length; j++)
                {
                    if (mines[i][j] == 1 && !(layout[i][j] == -2 || layout[i][j] == -4))
                    {
                        layout[i][j] = -3;
                    }
                }
            }

            return layout;
        }
    }
}
