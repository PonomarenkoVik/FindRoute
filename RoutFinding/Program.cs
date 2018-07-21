using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutFinding
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {

            Console.CursorVisible = false;
            int width = 10;
            int height = 10;
            Point initPoint = new Point(0, height - 1);
            Point finishPoint = new Point(width - 1, 0);
            Cell[,] field = new Cell[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int rand = rnd.Next(0, 100);
                    if (rand > 20)
                    {
                        field[j, i] = Cell.Free;
                    }
                    else
                    {
                        field[j, i] = Cell.Obst;
                    }
                }
            }

            field[initPoint.X, initPoint.Y] = Cell.Free;
            field[finishPoint.X, finishPoint.Y] = Cell.Free;



            RouteFinder finder = new RouteFinder(field, initPoint, finishPoint);
            finder.ShowEvent += ShowField;

            Cell[,] fieldWithRout = finder.FindRoute();
            Console.SetCursorPosition(4, 30);
            if (fieldWithRout == null)
            {               
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Route isn't found");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Route is found");
            }
            Console.ReadLine();
        }

        private static void ShowField(object sender, RouteShowEventArgs args)
        {
            int delt = 5;
            for (int i = 0; i < args.Field.GetLength(1); i++)
            {
                for (int j = 0; j < args.Field.GetLength(0); j++)
                {
                    Console.SetCursorPosition(j*2 + delt, i*2 + delt);
                    Cell cell = args.Field[j, i];

                    switch (cell)
                    {
                        case Cell.Free:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case Cell.Obst:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case Cell.Excep:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case Cell.Route:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }

                    PrintPoint(args.CurPoint, delt);
                }
            }
            Thread.Sleep(100);
        }

        private static void PrintPoint(Point currPoint, int delt)
        {
            Console.Write("##");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 1);
            Console.Write("##");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition((currPoint.X * 2 + delt), (currPoint.Y * 2 + delt));
            Console.Write("00");
            Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop + 1);
            Console.Write("00"); 
        }
    }
}
