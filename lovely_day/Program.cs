using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace lovely_day
{
    class Program
    {
        //Variables
        static int com1 = 0, com2 = 0, com3 = 0;
        static bool run1 = true, run2 = true, run3 = true;
        static Random r = new Random();
        static Rectangle screen = Screen.PrimaryScreen.Bounds;
        static string letterContainer = "ABCDEFGHIJKLMNOPQRSTUVXZabcdefghijklmnopqrstuvxyz1234567890";
        static string[] messageArray = letterContainer.ToCharArray().Select(x => x.ToString()).ToArray();

        //Threads
        static Thread randomMove = new Thread(new ThreadStart(() => DirectionMove(r)));
        static Thread randomType = new Thread(new ThreadStart(() => WriteRandomChars(r, messageArray)));
        static Thread randomWarp = new Thread(new ThreadStart(() => WarpMouse(screen, r)));

        static void Main(string[] args)
        {

            while (true)
            {
                string holder = String.Empty;
                Console.Write("LovelyDay - HID-PNKR /// V0.1.0 written by Elias Öh\n\nAvaliable payloads: \n\nRandom Mouse Movement - 1\nRandom Keyboard Input - 2\nRandom Mouse Warp - 3\n\nEnter attack index: ");
                holder = Console.ReadLine(); //get input

                //switch case for input
                switch (holder)
                {
                    case "1":
                        {
                            if (com1 == 0)
                            {
                                randomMove.Start();
                                com1++;
                            }
                            else if (com1 == 1)
                            {
                                com1 = 2;
                                run1 = false;
                            }
                            else if (com1 == 2)
                            {
                                com1 = 1;
                                run1 = true;
                            }
                        }
                        break;
                    case "2":
                        {
                            if (com2 == 0)
                            {
                                randomType.Start();
                                com2++;
                            }
                            else if (com2 == 1)
                            {
                                com2 = 2;
                                run2 = false;
                            }
                            else if (com1 == 2)
                            {
                                com2 = 1;
                                run2 = true;
                            }
                        }
                        break;
                    case "3":
                        {
                            if (com3 == 0)
                            {
                                randomWarp.Start();
                                com3++;
                            }
                            else if (com3 == 1)
                            {
                                com3 = 2;
                                run3 = false;
                            }
                            else if (com3 == 2)
                            {
                                com3 = 1;
                                run3 = true;
                            }
                        }
                        break;
                        //EXIT STATEMENT - ALL OTHER STATEMETS GO ABOVE THIS
                    case "exit":
                        Application.Exit();
                        break;
                    default:
                        Console.WriteLine(":::::::Invalid Input:::::::\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles cleanup before application closes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            randomMove.Abort();
            randomType.Abort();
            randomWarp.Abort();
        }

        /// <summary>
        /// Sends random keyboard strokes.
        /// </summary>
        /// <param name="r">A random object to generate random characters for output.</param>
        /// <param name="toWrite">Character set.</param>
        public static void WriteRandomChars(Random r, string[] toWrite)
        {
            while (true)
            {
                if (run2 == true)
                {
                    SendKeys.SendWait(toWrite[(r.Next() % toWrite.Length)]);
                    Thread.Sleep(10);
                }
            }
        }
        
        #region Move Mouse
        /// <summary>
        /// Move Mouse to a random point at the screen.
        /// </summary>
        /// <param name="screenBorder">Screen measurements.</param>
        /// <param name="r"></param>
        public static void WarpMouse(Rectangle screenBorder, Random r)
        {
            while (true)
            {
                if (run3 == true)
                {
                    int moveX = r.Next() % screenBorder.Width, moveY = r.Next() % screenBorder.Height;
                    int switcher = r.Next() % 20;

                    if (switcher <= 10)
                    {
                        Cursor.Position = new Point(moveX, moveY);
                    }
                    else if (switcher >= 11)
                    {
                        Cursor.Position = new Point(moveX, moveY);
                    }
                    Thread.Sleep(r.Next()%400);
                }
            }
        }

        /// <summary>
        /// Creates directional movement in one of eight directions.
        /// </summary>
        /// <param name="r"></param>
        public static void DirectionMove(Random r)
        {
            while (true)
            {
                if (run1 == true)
                {
                    int index = r.Next() % 8 + 1;
                    MoveMouseDirection(index, r);
                }
            }
        }

        /// <summary>
        /// The actual mover, subsidiary of DirectionMove.
        /// </summary>
        /// <param name="movementDirection"></param>
        /// <param name="r"></param>
        public static void MoveMouseDirection(int movementDirection, Random r)
        {
            int wait = 2, moveMax = 200, mpixelDistance = r.Next() % moveMax;

            if (movementDirection == 1)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 2)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 3)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 4)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 5)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y - 1);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 6)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y + 1);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 7)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y - 1);
                    Thread.Sleep(wait);
                }
            }
            else if (movementDirection == 8)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y + 1);
                    Thread.Sleep(wait);
                }
            }
            Thread.Sleep(r.Next() % wait * 10);
        }
        #endregion
    }

}