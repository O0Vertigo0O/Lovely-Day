using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace lovely_day
{
    public class Program
    {
        //Variables
        public static List<Thread> formCollection = new List<Thread>();
        static ManualResetEvent[] handle = new ManualResetEvent[] { new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true), new ManualResetEvent(true) };
        static int com1 = 0, com2 = 0, com3 = 0, com4 = 0, com5 = 0;
        static Random r = new Random();
        static Screen[] screens = Screen.AllScreens;
        static string letterContainer = "ABCDEFGHIJKLMNOPQRSTUVXZabcdefghijklmnopqrstuvxyz1234567890";
        static string[] messageArray = letterContainer.ToCharArray().Select(x => x.ToString()).ToArray();
        
        //Threads
        static Thread randomMove = new Thread(new ThreadStart(() => DirectionMove(r)));
        static Thread randomType = new Thread(new ThreadStart(() => WriteRandomChars(r, messageArray)));
        static Thread randomWarp = new Thread(new ThreadStart(() => WarpMouse(screens, r)));
        static Thread randomClick = new Thread(new ThreadStart(() => ClickMouse(r)));
        static Thread screenText = new Thread(new ThreadStart(() => ScreenText()));

        [STAThread]
        static void Main(string[] args)
        {
            messageArray[messageArray.Length - 1] = "{TAB}";

            banner(2); //EXPERIMENTAL

            Console.Write("LovelyDay V0.1.1 Written by Ergo and Terra - \"help\" for instructions\n");
            while (true)
            {
                Console.Write("\nAttack index:> ");

                string[] holder = Console.ReadLine().Split(','); //get input

                for (int i = 0; i < holder.Length; i++)
                {
                    holder[i] = holder[i].Trim();
                }

                for (var i = 0; i < holder.Length; i++)
                {
                    //switch case for input
                    switch (holder[i])
                    {
                        case "1":
                            {
                                if (com1 == 0)
                                {
                                    Console.Write("\nRunning DirectionMove...\n");
                                    randomMove.Start();
                                    com1++;
                                }
                                else if (com1 == 1)
                                {
                                    com1 = 2;
                                    Console.Write("\nStopping DirectionMove...\n");
                                    handle[0].Reset();
                                }
                                else if (com1 == 2)
                                {
                                    com1 = 1;
                                    Console.Write("\nRunning DirectionMove...\n");
                                    handle[0].Set();
                                }
                            }
                            break;
                        case "2":
                            {
                                if (com2 == 0)
                                {
                                    Console.Write("\nRunning WriteRandomChars...\n");
                                    randomType.Start();
                                    com2++;
                                }
                                else if (com2 == 1)
                                {
                                    com2 = 2;
                                    Console.Write("\nStopping WriteRandomChars...\n");
                                    handle[1].Reset();
                                }
                                else if (com2 == 2)
                                {
                                    com2 = 1;
                                    Console.Write("\nRunning WriteRandomChars...\n");
                                    handle[1].Set();
                                }
                            }
                            break;
                        case "3":
                            {
                                if (com3 == 0)
                                {
                                    Console.Write("\nRunning WarpMouse...\n");
                                    randomWarp.Start();
                                    com3++;
                                }
                                else if (com3 == 1)
                                {
                                    com3 = 2;
                                    Console.Write("\nStopping WarpMouse...\n");
                                    handle[2].Reset();
                                }
                                else if (com3 == 2)
                                {
                                    com3 = 1;
                                    Console.Write("\nRunning WarpMouse...\n");
                                    handle[2].Set();
                                }
                            }
                            break;
                        case "4":
                            {
                                if (com4 == 0)
                                {
                                    Console.Write("\nRunning ClickMouse...\n");
                                    randomClick.Start();
                                    com4++;
                                }
                                else if (com4 == 1)
                                {
                                    com4 = 2;
                                    Console.Write("\nStopping ClickMouse...\n");
                                    handle[3].Reset();
                                }
                                else if (com4 == 2)
                                {
                                    com4 = 1;
                                    Console.Write("\nRunning ClickMouse...\n");
                                    handle[3].Set();
                                }
                            }
                            break;
                        case "5":
                            {
                                if (com5 == 0)
                                {
                                    formCollection = new List<Thread>();
                                    Console.Write("\nRunning ScreenText...\n");
                                    screenText.Start();
                                    com5++;
                                }
                                else if (com5 == 1)
                                {
                                    com5 = 2;
                                    Console.Write("\nStopping ScreenText...\n");
                                    handle[4].Reset();
                                    for (int j = 0; j < formCollection.Count; j++)
                                    {
                                        formCollection[j].Abort();
                                    }
                                }
                                else if (com5 == 2)
                                {
                                    com5 = 1;
                                    Console.Write("\nRunning ScreenText...\n");
                                    handle[4].Set();
                                }
                            }
                            break;
                        case "help":
                            {
                                //LOOK, IT'S READABLE
                                Console.Write(@"------------------------
Usage: Attack index:> < attack index numbers sepparated by commas >

Avaliable attacks:

    Attack index = 1     ->   Move Mouse Randomly

    Attack index = 2     ->   Random Keyboard Input

    Attack index = 3     ->   Warp Mouse Around the Monitor

    Attack index = 4     ->   Random Mouse Clicks

    Attack index = 5     ->   Spawns text on screen

    Attack index = clear ->   Clear the console, when possible

    Attack index = exit  ->   Close the program
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
                                catch(Exception e)
                                {
                                    Console.Write("\nAn error occured! \nProbably can't clear console...\n");
                                }
                            }
                            break;
                        //EXIT STATEMENT - ALL OTHER STATEMETS GO ABOVE THIS
                        case "exit":
                            {
                                Console.Write("Killing...");
                                handle[4].Reset();
                                for (int j = 0; j < formCollection.Count; j++)
                                {
                                    formCollection[j].Abort();
                                }
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
                SendKeys.SendWait(toWrite[r.Next()%toWrite.Length]);
                Thread.Sleep(10);
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
                else if(chance <= 20 && chance > 10)
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
                ergRows.Add("@@@@@@@@  @@@@@@@    @@@@@@@@   @@@@@@       @@@@@@@  @@@@@@@@  @@@@@@@   @@@@@@@    @@@@@@ ");
                ergRows.Add("@@@@@@@@  @@@@@@@@  @@@@@@@@@  @@@@@@@@      @@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@");
                ergRows.Add("@@!       @@!  @@@  !@@        @@!  @@@        @@!    @@!       @@!  @@@  @@!  @@@  @@!  @@@");
                ergRows.Add("!@!       !@!  @!@  !@!        !@!  @!@        !@!    !@!       !@!  @!@  !@!  @!@  !@!  @!@");
                ergRows.Add("@!!!:!    @!@!!@!   !@! @!@!@  @!@  !@!        @!!    @!!!:!    @!@!!@!   @!@!!@!   @!@!@!@!");
                ergRows.Add("!!!!!:    !!@!@!    !!! !!@!!  !@!  !!!  AND   !!!    !!!!!:    !!@!@!    !!@!@!    !!!@!!!!");
                ergRows.Add("!!:       !!: :!!   :!!   !!:  !!:  !!!        !!:    !!:       !!: :!!   !!: :!!   !!:  !!!");
                ergRows.Add(":!:       :!:  !:!  :!:   !::  :!:  !:!        :!:    :!:       :!:  !:!  :!:  !:!  :!:  !:!");
                ergRows.Add(" :: ::::  ::   :::   ::: ::::  ::::: ::         ::     :: ::::  ::   :::  ::   :::  ::   :::");
                ergRows.Add(": :: ::    :   : :   :: :: :    : :  :          :     : :: ::    :   : :   :   : :   :   : :");

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
                

                List<string> terRows = new List<string>();
                terRows.Add("@@@@@@@  @@@@@@@@  @@@@@@@   @@@@@@@    @@@@@@ ");
                terRows.Add("@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@");
                terRows.Add("  @@!    @@!       @@!  @@@  @@!  @@@  @@!  @@@");
                terRows.Add("  !@!    !@!       !@!  @!@  !@!  @!@  !@!  @!@");
                terRows.Add("  @!!    @!!!:!    @!@!!@!   @!@!!@!   @!@!@!@!");
                terRows.Add("  !!!    !!!!!:    !!@!@!    !!@!@!    !!!@!!!!");
                terRows.Add("  !!:    !!:       !!: :!!   !!: :!!   !!:  !!!");
                terRows.Add("  :!:    :!:       :!:  !:!  :!:  !:!  :!:  !:!");
                terRows.Add("   ::     :: ::::  ::   :::  ::   :::  ::   :::");
                terRows.Add("   :     : :: ::    :   : :   :   : :   :   : :");

                //rgb(129, 58, 216)
                int tr = 255;
                int tg = 255;
                int tb = 255;
                for (int i = 0; i < 10; i++)
                {
                    //Console.WriteLine(terRows[i], Color.FromArgb(er, eg, eb));

                    tr -= 18;
                    tb += 9;
                }

                List<string> lovRows = new List<string>();
                lovRows.Add("-------------------------------------------------------------------------------------\n");
                lovRows.Add(@"██╗      ██████╗ ██╗   ██╗███████╗██╗  ██╗   ██╗    ██████╗  █████╗ ██╗   ██╗");
                lovRows.Add(@"██║     ██╔═══██╗██║   ██║██╔════╝██║  ╚██╗ ██╔╝    ██╔══██╗██╔══██╗╚██╗ ██╔╝");
                lovRows.Add(@"██║     ██║   ██║██║   ██║█████╗  ██║   ╚████╔╝     ██║  ██║███████║ ╚████╔╝ ");
                lovRows.Add(@"██║     ██║   ██║╚██╗ ██╔╝██╔══╝  ██║    ╚██╔╝      ██║  ██║██╔══██║  ╚██╔╝");
                lovRows.Add(@"███████╗╚██████╔╝ ╚████╔╝ ███████╗███████╗██║       ██████╔╝██║  ██║   ██║   ");
                lovRows.Add(@"╚══════╝ ╚═════╝   ╚═══╝  ╚══════╝╚══════╝╚═╝       ╚═════╝ ╚═╝  ╚═╝   ╚═╝   ");

                int lr = 105;
                int lg = 155;
                int lb = 92;
                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine(lovRows[i], Color.FromArgb(lr, lg, lb));

                    lg -= 18;
                    lb += 9;
                }
            }
        }

        [STAThread]
        public static void ScreenText(){
            while (true) {
                handle[4].WaitOne();
                formCollection.Add(new Thread(new ThreadStart(() => Application.Run(new Form1()))));
                formCollection[formCollection.Count - 1].Start();
                Thread.Sleep(100);
            }
        }
    }
}