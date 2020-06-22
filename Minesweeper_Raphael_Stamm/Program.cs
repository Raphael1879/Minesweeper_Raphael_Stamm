using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;

namespace Minesweeper_Raphael_Stamm
{
    class Program
    {

        private static Grid grid;
        private static bool gameover;
        private static bool isWon;

        static void Main(string[] args)
        {
            showArt("startscreen_art.txt");
            gameover = false;
            isWon = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            
            try
            {
                grid = createUserGrid();
                //game loop
                while (!gameover && !grid.isWon())
                {
                    
                    grid.showGrid();
                    int userOption = getUserOption();
                    Field f = getUserCords();
                    switch (userOption)
                    {
                        case 1:
                            gameover = f.discoverField();
                            break;

                        case 2:
                            f.flagField();
                            break;

                        case 3:
                            f.unflagField();
                            break;
                    }
                    isWon = grid.isWon();
                }
                grid.showGrid();
                if (isWon)
                {
                    Console.WriteLine("YOU WON");
                    showArt("victory_art.txt");
                } else
                {
                    Console.WriteLine("YOU LOST");
                    showArt("gameover_art.txt");
                }


            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace); 
            } 
            
  
        }

        public static Field getUserCords()
        {
            String input = "";
            String x = "";
            String y = "";
            while (!Regex.IsMatch(x, @"^[a-zA-Z]+$") || !Regex.IsMatch(y, "^[0-9]+$"))
            {
                Console.WriteLine("Enter Cordinates (ex: 'A2')");
                input = Console.ReadLine();
                while (input.Length < 2)
                {
                    Console.WriteLine("Invalid cordinates, Please enter valid cordinates (ex: 'B4')");
                    input = Console.ReadLine();

                }
                x = input.Substring(0, 1);
                y = input.Substring(1, input.Length - 1);
            }
            return grid.getFieldAt(char.Parse(x),int.Parse(y));
        }

        public static void showArt(string fileName)
        {
            var directory = Environment.CurrentDirectory;
            var path = Path.Combine(directory, fileName);
            var content = File.ReadAllText(path);
            Console.WriteLine(content);
        }

        public static Grid createUserGrid() {

            bool widthValid = false;
            int _width = 0;
            bool heightValid = false;
            int _height = 0;
            Console.WriteLine("Welcome to Minesweeper");
            Console.WriteLine("Enter grid width");
            while (!widthValid)
            {
                try
                {
                    _width = int.Parse(Console.ReadLine());
                    if (_width <= 26 && _width > 0)
                    {
                        widthValid = true;
                    } else
                    {
                        Console.WriteLine("enter a number between 1-26");
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine("please enter a number");
                }
            }

            Console.WriteLine("Enter grid height");
            while (!heightValid)
            {
                try
                {
                    _height = int.Parse(Console.ReadLine());
                    if (_height <= 99 && _height > 0)
                    {
                        heightValid = true;
                    }
                    else
                    {
                        Console.WriteLine("enter a number between 1-99");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("please enter a number");
                }
            }
            return new Grid(_width, _height);
        }

        public static int getUserOption()
        {
            bool inputValid = false;
            int input = 0;

            Console.WriteLine("What do you want to do? (enter '1' or '2' or '3')");
            Console.WriteLine("1 : Discover Field");
            Console.WriteLine("2 : Flag Field");
            Console.WriteLine("3 : Unflag Field");
            while (!inputValid)
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    if(input > 0 && input < 4)
                    {
                        inputValid = true;
                    } else
                    {
                        Console.WriteLine("enter a number between 1-3");
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("please enter a number");
                }
            }
            return input;
        }

    }
}
