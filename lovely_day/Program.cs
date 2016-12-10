using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Media;

namespace lovely_day
{
    public class Program
    {
        //Variables
        public static string[] holder = new string[10];
        public static List<Thread> formCollection = new List<Thread>();
        public static CancellationTokenSource cts = new CancellationTokenSource();
        static System.Timers.Timer spacer = new System.Timers.Timer(100);
        static ManualResetEvent[] handle = new ManualResetEvent[] { new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true) };
        static int[] com = new int[] { 0, 0, 0, 0, 0, 0 };
        static string[] comName = new string[] { "DirectionMove", "WriteRandomChars", "WarpMouse", "ClickMouse", "ScreenText", "ErrorSounds" };
        static Random r = new Random();
        static Screen[] screens = Screen.AllScreens;
        static string[] messageArray = "ABCDEFGHIJKLMNOPQRSTUVXZabcdefghijklmnopqrstuvxyz1234567890".ToCharArray().Select(x => x.ToString()).ToArray();

        //Threads
        static Thread randomMove = new Thread(new ThreadStart(() => DirectionMove(r)));
        static Thread randomType = new Thread(new ThreadStart(() => WriteRandomChars(r, messageArray)));
        static Thread randomWarp = new Thread(new ThreadStart(() => WarpMouse(screens, r)));
        static Thread randomClick = new Thread(new ThreadStart(() => ClickMouse(r)));
        static Thread screenText = new Thread(new ThreadStart(() => ScreenText()));
        static Thread errorSound1 = new Thread(new ThreadStart(() => ErrorSound1()));
        static Thread errorSound2 = new Thread(new ThreadStart(() => ErrorSound2()));

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main(string[] args)
        {
            var windowHandle = GetConsoleWindow();
            Console.Title = "LD";
            messageArray[messageArray.Length - 1] = "{TAB}";

            banner(2); //EXPERIMENTAL

            bool bypassGate = false;

            Console.Write("\nLovelyDay V0.5.7 Written by Ergo and Terra - \"help\" for instructions\n");
            while (true)
            {
                if (!bypassGate)
                {
                    Console.Write("\nAttack index:> ");

                    holder = Console.ReadLine().Split(','); //get input

                    for (int i = 0; i < holder.Length; i++)
                    {
                        holder[i] = holder[i].Trim();
                    }
                }
                else
                {
                    bypassGate = false;
                    ShowWindow(windowHandle, SW_HIDE);
                }

                for (var i = 0; i < holder.Length; i++)
                {
                    //switch case for input
                    switch (holder[i])
                    {
                        case "1":
                            {
                                if (com[0] == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning DirectionMove...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    randomMove.Start();
                                    com[0]++;
                                }
                                else if (com[0] == 1)
                                {
                                    com[0] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping DirectionMove...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[0].Reset();
                                }
                                else if (com[0] == 2)
                                {
                                    com[0] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning DirectionMove...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[0].Set();
                                }
                            }
                            break;
                        case "2":
                            {
                                if (com[1] == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning WriteRandomChars...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    randomType.Start();
                                    com[1]++;
                                }
                                else if (com[1] == 1)
                                {
                                    com[1] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping WriteRandomChars...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[1].Reset();
                                }
                                else if (com[1] == 2)
                                {
                                    com[1] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning WriteRandomChars...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[1].Set();
                                }
                            }
                            break;
                        case "3":
                            {
                                if (com[2] == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning WarpMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    randomWarp.Start();
                                    com[2]++;
                                }
                                else if (com[2] == 1)
                                {
                                    com[2] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping WarpMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[2].Reset();
                                }
                                else if (com[2] == 2)
                                {
                                    com[2] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning WarpMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[2].Set();
                                }
                            }
                            break;
                        case "4":
                            {
                                if (com[3] == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ClickMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    randomClick.Start();
                                    com[3]++;
                                }
                                else if (com[3] == 1)
                                {
                                    com[3] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping ClickMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[3].Reset();
                                }
                                else if (com[3] == 2)
                                {
                                    com[3] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ClickMouse...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[3].Set();
                                }
                            }
                            break;
                        case "5":
                            {
                                if (com[4] == 0)
                                {
                                    formCollection = new List<Thread>();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ScreenText...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    screenText.Start();
                                    com[4]++;
                                }
                                else if (com[4] == 1)
                                {
                                    com[4] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping ScreenText...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[4].Reset();
                                    cts.Cancel();
                                }
                                else if (com[4] == 2)
                                {
                                    cts.Dispose();
                                    com[4] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ScreenText...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    cts = new CancellationTokenSource();
                                    formCollection = new List<Thread>();
                                    handle[4].Set();
                                }
                            }
                            break;
                        case "6": 
                            {
                                if (com[5] == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ErrorSounds...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    errorSound1.Start();
                                    errorSound2.Start();
                                    com[5]++;
                                }
                                else if (com[5] == 1)
                                {
                                    com[5] = 2;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write("\nStopping ErrorSounds...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[5].Reset();
                                }
                                else if (com[5] == 2)
                                {
                                    com[5] = 1;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("\nRunning ErrorSounds...\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    handle[5].Set();
                                }
                            }
                            break;
                        case "help":
                            {
                                //LOOK, IT'S READABLE
                                Console.Write(@"------------------------
Usage: Attack index:> < attack index numbers/names sepparated by commas >

Avaliable attacks:

    Attack index = 1     ->   Move Mouse Randomly

    Attack index = 2     ->   Random Keyboard Input

    Attack index = 3     ->   Warp Mouse Around the Monitor

    Attack index = 4     ->   Random Mouse Clicks

    Attack index = 5     ->   Spawns text on screen

    Attack index = 6     ->   Plays Error Sounds

    Attack index = clear ->   Clear the console, when possible

    Attack index = exit  ->   Close the program
    
    Attack index = havoc ->   EVERYTHING AT ONCE

    Attack index = stat ->   View active threads
------------------------
");
                            }
                            break;
                        case "clear":
                            {
                                try
                                {
                                    Console.Clear();
                                }
                                catch (Exception e)
                                {
                                    Console.Write("\nAn error occured! \nProbably can't clear console...\n" + e.Message);
                                } 
                            }
                            break;
                        case "havoc":
                            {
                                if (ActivateHavocMode())
                                {
                                    bypassGate = true;
                                    holder = "1,2,3,4,5,6".Split(',');
                                }
                                else
                                {
                                    Console.Write("\n\n");
                                }
                            }
                            break;
                        case "stat":
                            {
                                for (var proc = 0; proc < com.Length; proc++)
                                {
                                    if(com[proc] == 1)
                                    {
                                        Console.Write("\n-------------------------------------------------\n\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Status of {0}     \t-  ", comName[proc]);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("\tACTIVE");
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        Console.Write("  \t    [ATTACK_ID: {0}]\n", proc + 1);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    else
                                    {
                                        Console.Write("\n-------------------------------------------------\n\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Status of {0}     \t-  ", comName[proc]);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write("\tDISABLED");
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        Console.Write("  \t    [ATTACK_ID: {0}]\n", proc + 1);
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                }
                            }
                            break;
                        //EXIT STATEMENT - ALL OTHER STATEMETS GO ABOVE THIS
                        case "exit":
                            {
                                Console.Write("Killing...\n");
                                for (int j = 0; j < handle.Length - 1; j++)
                                {
                                    handle[i].Reset();
                                }
                                if (cts.IsCancellationRequested == false)
                                    cts.Cancel();
                                Thread.Sleep(500);
                                Environment.Exit(0);
                            }
                            break;
                        default:
                            Console.WriteLine("\n:::::::Invalid Input:::::::");
                            break;
                    }
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
            args.Cancel = true;
        }

        public static bool ActivateHavocMode()
        {
            Console.Write("\n\nAre you sure? Capital \"YES\" to confirm: ");
            string rouge = Console.ReadLine();
            if (rouge == "YES")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Send Keyboard strokes
        /// <summary>
        /// Sends random keyboard strokes.
        /// </summary>
        /// <param name="r">A random object to generate random characters for output.</param>
        /// <param name="toWrite">Character set.</param>
        public static void WriteRandomChars(Random r, string[] toWrite)
        {
            while (true)
            {
                handle[1].WaitOne();
                SendKeys.SendWait(toWrite[r.Next() % toWrite.Length]);
            }
        }
        #endregion

        #region Move Mouse
        /// <summary>
        /// Move Mouse to a random point at the screen.
        /// </summary>
        /// <param name="screenBorder">Screen measurements.</param>
        /// <param name="r"></param>)
        public static void WarpMouse(Screen[] screens, Random r)
        {
            while (true)
            {
                handle[2].WaitOne();

                int index = r.Next(0, screens.Length);
                int moveX = r.Next(screens[index].WorkingArea.Left, screens[index].WorkingArea.Right), moveY = r.Next(screens[index].WorkingArea.Top, screens[index].WorkingArea.Bottom);
                int switcher = r.Next() % 20;

                if (switcher <= 10)
                {
                    Cursor.Position = new Point(moveX, moveY);
                }
                else if (switcher >= 11)
                {
                    Cursor.Position = new Point(moveX, moveY);
                }
                Thread.Sleep(r.Next() % 400);
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
                handle[0].WaitOne();
                int index = r.Next() % 8 + 1;
                MoveMouseDirection(index, r);
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
                }
            }
            else if (movementDirection == 2)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                }
            }
            else if (movementDirection == 3)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                }
            }
            else if (movementDirection == 4)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                }
            }
            else if (movementDirection == 5)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y - 1);
                }
            }
            else if (movementDirection == 6)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y + 1);
                }
            }
            else if (movementDirection == 7)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y - 1);
                }
            }
            else if (movementDirection == 8)
            {
                for (var i = 0; i < mpixelDistance; i++)
                {
                    Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y + 1);
                }
            }
            Thread.Sleep(r.Next() % wait * 10);
        }
        #endregion

        #region Click Mouse
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, UIntPtr dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        public static void ClickMouse(Random r)
        {
            while (true)
            {
                handle[3].WaitOne();
                int chance = r.Next() % 20;
                if (chance <= 10)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, new UIntPtr(0));
                    Thread.Sleep(r.Next() % 500 + 50);
                }
                else if (chance <= 20 && chance > 10)
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, new UIntPtr(0));
                    Thread.Sleep(r.Next() % 500 + 50);
                }
                else
                {
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, Cursor.Position.X, Cursor.Position.Y, 0, new UIntPtr(0));
                    Thread.Sleep(r.Next() % 500 + 50);
                }
            }
        }
        #endregion

        /// <summary>
        /// Draws a cool ascii signature!
        /// </summary>
        /// <param name="onAscaleFromOneToTwoHowCoolDoYouWantIt">on a scale From One To Two How Cool Do You Want It?</param>
        public static void banner(int onAscaleFromOneToTwoHowCoolDoYouWantIt)
        {
            if (onAscaleFromOneToTwoHowCoolDoYouWantIt == 1)
            {
                Console.WriteLine(@" _______  ______  ______  _____ 
 |______ |_____/ |  ____ |     |
 |______ |    \_ |_____| |_____|

	AND
 _______ _______  ______  ______ _______
    |    |______ |_____/ |_____/ |_____|
    |    |______ |    \_ |    \_ |     |");
                Console.WriteLine();
            }
            else if (onAscaleFromOneToTwoHowCoolDoYouWantIt == 2)
            {
                List<string> ergRows = new List<string>();
                ergRows.Add(" @@@@@@@@  @@@@@@@    @@@@@@@@   @@@@@@       @@@@@@@  @@@@@@@@  @@@@@@@   @@@@@@@    @@@@@@ ");
                ergRows.Add(" @@@@@@@@  @@@@@@@@  @@@@@@@@@  @@@@@@@@      @@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@");
                ergRows.Add(" @@!       @@!  @@@  !@@        @@!  @@@        @@!    @@!       @@!  @@@  @@!  @@@  @@!  @@@");
                ergRows.Add(" !@!       !@!  @!@  !@!        !@!  @!@        !@!    !@!       !@!  @!@  !@!  @!@  !@!  @!@");
                ergRows.Add(" @!!!:!    @!@!!@!   !@! @!@!@  @!@  !@!        @!!    @!!!:!    @!@!!@!   @!@!!@!   @!@!@!@!");
                ergRows.Add(" !!!!!:    !!@!@!    !!! !!@!!  !@!  !!!  AND   !!!    !!!!!:    !!@!@!    !!@!@!    !!!@!!!!");
                ergRows.Add(" !!:       !!: :!!   :!!   !!:  !!:  !!!        !!:    !!:       !!: :!!   !!: :!!   !!:  !!!");
                ergRows.Add(" :!:       :!:  !:!  :!:   !::  :!:  !:!        :!:    :!:       :!:  !:!  :!:  !:!  :!:  !:!");
                ergRows.Add("  :: ::::  ::   :::   ::: ::::  ::::: ::         ::     :: ::::  ::   :::  ::   :::  ::   :::");
                ergRows.Add(" : :: ::    :   : :   :: :: :    : :  :          :     : :: ::    :   : :   :   : :   :   : :");

                //rgb(0, 89, 224)
                int er = 0;
                int eg = 89;
                int eb = 224;
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(ergRows[i], Color.FromArgb(er, eg, eb));

                    er += 18;
                    eb -= 9;
                }

                List<string> lovRows = new List<string>();
                lovRows.Add("-------------------------------------------------------------------------------------\n");
                lovRows.Add(@"  ██╗      ██████╗ ██╗   ██╗███████╗██╗  ██╗   ██╗    ██████╗  █████╗ ██╗   ██╗");
                lovRows.Add(@"  ██║     ██╔═══██╗██║   ██║██╔════╝██║  ╚██╗ ██╔╝    ██╔══██╗██╔══██╗╚██╗ ██╔╝");
                lovRows.Add(@"  ██║     ██║   ██║██║   ██║█████╗  ██║   ╚████╔╝     ██║  ██║███████║ ╚████╔╝ ");
                lovRows.Add(@"  ██║     ██║   ██║╚██╗ ██╔╝██╔══╝  ██║    ╚██╔╝      ██║  ██║██╔══██║  ╚██╔╝  ");
                lovRows.Add(@"  ███████╗╚██████╔╝ ╚████╔╝ ███████╗███████╗██║       ██████╔╝██║  ██║   ██║   ");
                lovRows.Add(@"  ╚══════╝ ╚═════╝   ╚═══╝  ╚══════╝╚══════╝╚═╝       ╚═════╝ ╚═╝  ╚═╝   ╚═╝   ");

                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine(lovRows[i]);
                }
            }
        }


        [STAThread]
        public static void ScreenText()
        {
            while (true)
            {
                handle[4].WaitOne();

                formCollection.Add(new Thread(new ThreadStart(() => Application.Run(new Form1(cts.Token)))));
                formCollection[formCollection.Count - 1].Start();
                Thread.Sleep(100);
            }
        }

        public static void ErrorSound1()
        {
            while (true)
            {
                handle[5].WaitOne();
                Random r = new Random();
                SystemSounds.Exclamation.Play();
                SystemSounds.Asterisk.Play();
                Thread.Sleep(r.Next(210, 800));
            }
        }
        public static void ErrorSound2()
        {
            while (true)
            {
                handle[5].WaitOne();
                Random r = new Random();
                SystemSounds.Hand.Play();
                Thread.Sleep(r.Next(200, 800));
            }
        }
    }
}