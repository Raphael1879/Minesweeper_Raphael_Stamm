using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Minesweeper_Raphael_Stamm
{
    class Grid
    {
        public int GridHeight { get; set; }
        public int GridWidth { get; private set; }
        public int BombCount { get; private set; }

        private Field topLeft;

        public Grid(int gridWidth , int gridHeight)
        {
            this.GridHeight = gridHeight;
            this.GridWidth = gridWidth;

            topLeft = null;
            Field firstOfRow = null;
            for (int i = 0; i < GridHeight; i++)// for every row
            {
                Field lastTopFirstOfRow = firstOfRow;
                firstOfRow = new Field((i + 1) * 1000);
                

                if (i == 0) topLeft = firstOfRow;


                Field currentField = null;
                for(int j = 0; j < GridWidth; j++)//for every column
                {
                    
                    if (j == 0) 
                    {
                        currentField = firstOfRow;
                    }
                    var newField = new Field((i + 1) * 1000 + j + 1);
                    if (j != gridWidth - 1)
                    {
                        
                        currentField.Right = newField;
                        newField.Left = currentField;
                    }                
                    if (i != 0)
                    {
                        currentField.Top = lastTopFirstOfRow;        
                        lastTopFirstOfRow.Bottom = currentField;
                       
                    }

                    // prepare for next loop
                    currentField = newField;
                    if(i!=0) lastTopFirstOfRow = lastTopFirstOfRow.Right;

                }

            }

        }

        public void showGrid()
        {
            var fieldX = topLeft;
            var fieldY = topLeft;
            int yAxis = 1;

            //draw X axis
            Console.Write("      ");
            for (int i = 0; i <= GridWidth-1; i++)
            {
                Console.Write(char.ToUpper((char)(i + (int)'a')) + " ");
            }
            Console.WriteLine();
            Console.Write("   ++ ");
            for (int i = 0; i <= GridWidth - 1; i++)
            {
                Console.Write("--");
            }
            Console.WriteLine();
            while (fieldY != null)
            {
                //draw Y axis
                Console.Write(yAxis);
                if (yAxis < 10) Console.Write(" ");
                Console.Write(" || ");


                fieldX = fieldY;
                while(fieldX != null)
                {
                    Console.Write(fieldX.getOutputSymbol() + " ");

                    fieldX = fieldX.Right;
                }
                Console.WriteLine("");

                yAxis++;
                fieldY = fieldY.Bottom;
            }

        }

        public Field getTopLeft()
        {
            return topLeft;
        }

        public Field getFieldAt(Char xChar, int y)
        {
            try
            {
                int x = (int)Char.ToLower(xChar) - (int)'a' + 1;
                Field row = null;
                Field result = null;
                for (int i = 0; i < y; i++)
                {
                    if (i == 0)
                    {
                        row = topLeft;
                    }
                    else
                    {
                        row = row.Bottom;
                    }
                }
                for (int j = 0; j < x; j++)
                {
                    if (j == 0)
                    {
                        result = row;
                    }
                    else
                    {
                        result = result.Right;
                    }
                }
                return result;
            } catch(Exception ex)
            {
                Console.WriteLine("invalid cordinates");
                return topLeft;
            }
            
        }

        public bool isWon()
        {
            bool isWon = true;
            var fieldX = topLeft;
            var fieldY = topLeft;

            while (fieldY != null)
            { 
                fieldX = fieldY;
                while (fieldX != null)
                {

                    if (!fieldX.isBomb)
                    {
                        if (fieldX.isHidden)
                        {
                            isWon = false;
                        }
                    } else
                    {
                        if (!fieldX.isFlagged)
                        {
                            isWon = false;
                        }
                    }
                    fieldX = fieldX.Right;
                }
                fieldY = fieldY.Bottom;
            }
            return isWon;
        }
    }
}
