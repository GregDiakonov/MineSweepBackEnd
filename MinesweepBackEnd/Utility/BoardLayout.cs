namespace MinesweepBackEnd.Utility
{
    public class BoardLayout
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[]? Data { get; set; }

        private int[][] matrix;

        public void MakeMatrix()
        {
            matrix = new int[Height][];

            for(int i = 0; i < Height; i++)
            {
                matrix[i] = new int[Width];
                for(int j = 0; j < Width; j++)
                {
                    matrix[i][j] = Data[i*Height+j];
                }
            }
        }

        public BoardLayout() { }
    }
}
