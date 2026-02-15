using ConsoleApp1;
using System;
using static System.Console;


RunInput();
static void RunInput()
{
    Console.Write("Enter start (example D4): ");
    string startInput = Console.ReadLine().ToUpper();
    Console.Write("Enter target (example F7): ");
    string targetInput = Console.ReadLine().ToUpper();
    Columns startColumnEnum = (Columns)Enum.Parse(typeof(Columns), startInput[0].ToString());
    int startCol = (int)startColumnEnum;
    int startRow = startInput[1] - '1';

    Columns targetColumnEnum = (Columns)Enum.Parse(typeof(Columns), targetInput[0].ToString());
    int targetCol = (int)targetColumnEnum;
    int targetRow = targetInput[1] - '1';

    Point startPoint = new Point(startRow, startCol);
    Point targetPoint = new Point(targetRow, targetCol);

    int[,] startSteps = new int[8, 8];
    int[,] targetSteps = new int[8, 8];
    for (int i = 0; i < 8; i++)
        for (int j = 0; j < 8; j++)
        {
            startSteps[i, j] = -1;
            targetSteps[i, j] = -1;
        }

    FillSteps(startPoint.Row, startPoint.Col, 0, startSteps);
    FillSteps(targetPoint.Row, targetPoint.Col, 0, targetSteps);
    int min = int.MaxValue;
    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (startSteps[i, j] != -1 && targetSteps[i, j] != -1)
            {
                int sum = startSteps[i, j] + targetSteps[i, j];
                if (sum < min)
                    min = sum;
            }
        }
    }
    Console.WriteLine("Minimum steps = " + min);
}
static void FillSteps(int row, int col, int step, int[,] board)
{
    int[,] knightMoves = new int[,]
    {
        { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
        { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
    };

    if (row < 0 || row >= 8 || col < 0 || col >= 8)
        return;

    if (board[row, col] != -1 && board[row, col] <= step)
        return;

    board[row, col] = step;

    for (int i = 0; i < 8; i++)
    {
        int newRow = row + knightMoves[i, 0];
        int newCol = col + knightMoves[i, 1];

        FillSteps(newRow, newCol, step + 1, board);
    }
}