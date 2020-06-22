using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Minesweeper_Raphael_Stamm
{
    class Field
    {
        public int Value { get; set; }
        private const int BOMB_CHANCE = 16;
        public bool isBomb;
        public bool isHidden;
        public bool isFlagged;
        public int Index { get; private set; }
        public Field Right { get;  set; }
        public Field Left { get; set; }
        public Field Top { get; set; }
        public Field Bottom { get; set; }



        
        public Field(int value)
        {
            isHidden = true;
            isFlagged = false;
            Value = value;
            Random random = new Random();
            if (random.Next(0, 100) < BOMB_CHANCE)
            {
                isBomb = true;
            }
            else
            {
                isBomb = false;
            }

        }

        public string getOutputSymbol()
        {
            string output = null;
            if (isHidden)
            {
                output = "?";
            } else
            {
                if (isFlagged)
                {
                    output = "F";
                }
                else {
                    if (isBomb)
                    {
                        output = "B"; //Bombs: 💣 U+F4A3
                    } else
                    {
                        if (getBombsAroundMe() == 0)
                        {
                            output = "~";
                        }
                        else
                        {
                            output = getBombsAroundMe().ToString();
                        }
                    }
                }
            }
            return output;
        }

        public int getBombsAroundMe()
        {
            int bombs = 0;

            if(Top !=null)
            {
                if (Top.isBomb) bombs++;
            }

            if (Bottom != null)
            {
                if (Bottom.isBomb) bombs++;
            }

            if (Right != null)
            {
                if (Right.isBomb) bombs++;
            }

            if (Left != null)
            {
                if (Left.isBomb) bombs++;
            }

            if(Top != null && Top.Right != null)
            {
                if (Top.Right.isBomb) bombs++;
            }

            if (Top != null && Top.Left != null)
            {
                if (Top.Left.isBomb) bombs++;
            }

            if (Bottom != null && Bottom.Right != null)
            {
                if (Bottom.Right.isBomb) bombs++;
            }

            if (Bottom != null && Bottom.Left != null)
            {
                if (Bottom.Left.isBomb) bombs++;
            }

            return bombs;
        }

        public void flagField()
        {
            if (isHidden)
            {
                isFlagged = true;
                isHidden = false;
            } else
            {
                Console.WriteLine("You cant flag a field that is already discovered");
            }
        }

        public bool discoverField()
        {
            bool gameover = false;
            if (!isHidden){
                return false;
            } 
            if (isFlagged)
            {
                Console.WriteLine("You cant discover a flagged field, you must unflag it first");
                return false;
            }
            else
            {
                if (isBomb)
                {
                    isHidden = false;
                    gameover = true;
                }
                else
                {
                    isHidden = false;
                    if (getBombsAroundMe() > 0)
                    {
                        return false;
                    }
                    Top?.discoverField();
                    Bottom?.discoverField();
                    Left?.discoverField();
                    Right?.discoverField();

                    Top?.Right?.discoverField();
                    Top?.Left?.discoverField();
                    Bottom?.Right?.discoverField();
                    Bottom?.Left?.discoverField();

                }
            }
            return gameover;
        }

        public void unflagField()
        {
            if (isFlagged)
            {
                isFlagged = false;
                isHidden = true;
            } else
            {
                Console.WriteLine("You cant unflag a field that is not flagged");
            }
        }
    }
}
