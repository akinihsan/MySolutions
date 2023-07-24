namespace Services
{
    public class MatrixAreaFinderService
    {
        private int X { get; set; }
        public int Y { get; set; }
        private int[,]? matrix;
        public MatrixAreaFinderService(string input)
        {
            string[] rows = input.Split(';');
            string[] items = rows[0].Split(',').ToArray();
            X = items.Length;
            Y = rows.Length;

            if (X > 100 || Y > 100)
                throw new System.ArgumentException("Maximum number of rows or columns exceeded");

            matrix = new int[Y, X];

            for (int i = 0; i < Y; i++)
            {
                items = rows[i].Replace(" ", "").Split(',');
                var blockedItemsExist =  items.Where(item => !(item.Equals("0") || item.Equals("1"))).Any();
                if (blockedItemsExist)
                    throw new System.ArgumentException("Items can be only 0 or 1");

                int[] ints = Array.ConvertAll(items, int.Parse);
                 

                if (X != ints.Length)
                    throw new System.ArgumentException("Number of columns and rows must be equal");

                for (int j = 0; j < X; j++)
                {
                    matrix[i, j] = ints[j];
                }
            }
        } 
        public int Calculate()
        {
            int areas = 0;

            for (int i = 0; i < X; ++i)
                for (int j = 0; j < Y; ++j)
                    if (matrix[j, i] == 1)
                        if (blank(i, j) > 0)
                            areas++;

            return areas;
        }
        private int blank(int x, int y)
        {
            if ((x < 0) || (x >= X) || (y < 0) || (y >= Y) || (matrix?[y, x] == 0))
                return 0;

            matrix[y, x] = 0;

            return 1 + blank(x - 1, y) + blank(x + 1, y) + blank(x, y - 1) + blank(x, y + 1);
        }
    }
}