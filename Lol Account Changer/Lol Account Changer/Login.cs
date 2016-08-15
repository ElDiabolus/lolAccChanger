using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Lol_Account_Changer
{
    class Login
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        private string strLolPfad;

        private User objUser;

        Timer timerSearchStartButton;
        Timer timerWaitButtonToStartGame;
        Timer timerSearchLoginFields;

        public Login(string strLolPfad, User objUser)
        {
            this.strLolPfad = strLolPfad;
            this.objUser = objUser;
        }

        public void start()
        {
            this.terminateProcess("LoLLauncher");
            this.terminateProcess("LoLPatcher");
            this.terminateProcess("LoLClient");
            this.terminateProcess("LoLPatcherUx");
            Process.Start(this.strLolPfad);

            timerSearchStartButton = new Timer();
            timerSearchStartButton.Tick += new EventHandler(SearchStartButtonTimer);

            // Sets the timer interval to 5 seconds.
            timerSearchStartButton.Interval = 500;
            timerSearchStartButton.Start();
        }

        private void SearchStartButtonTimer(Object myObject, EventArgs myEventArgs)
        {
            Point p = this.searchStartButton();
            if (p.X != 0 && p.Y != 0)
            {
                timerSearchStartButton.Stop();
                this.DoMouseClick(p.X, p.Y);
                this.login();
            }
        }

        private void login()
        {
            timerWaitButtonToStartGame = new Timer();
            timerWaitButtonToStartGame.Tick += new EventHandler(WaitButtonToStartGameTimer);

            // Sets the timer interval to 5 seconds.
            timerWaitButtonToStartGame.Interval = 2000;
            timerWaitButtonToStartGame.Start();
        }

        private void WaitButtonToStartGameTimer(Object myObject, EventArgs myEventArgs)
        {
            timerWaitButtonToStartGame.Stop();

            timerSearchLoginFields = new Timer();
            timerSearchLoginFields.Tick += new EventHandler(SearchLoginFieldsTimer);

            // Sets the timer interval to 5 seconds.
            timerSearchLoginFields.Interval = 500;
            timerSearchLoginFields.Start();

        }

        private void SearchLoginFieldsTimer(Object myObject, EventArgs myEventArgs)
        {
            Point p = this.searchRitoLogo();
            if (p.X != 0 && p.Y != 0)
            {
                //Gefunden
                timerSearchLoginFields.Stop();
                this.pressLoginKeys();
            }
        }

        public void pressLoginKeys()
        {
            //Shift+Tab
            SendKeys.SendWait("+({TAB})");

            //Username
            SendKeys.SendWait(objUser.getUsername());
            SendKeys.SendWait("{TAB}");

            //Paswort
            SendKeys.SendWait(objUser.getPasswort());
            SendKeys.SendWait("{ENTER}");
        }



        public Point searchStartButton()
        {
            PixelSearch objPxSearch = new PixelSearch();
            Color cColor = Color.FromArgb(93, 30, 14);
            Point pPunkt = objPxSearch.GetPixelPosition(cColor, false);

            return pPunkt;
        }

        public Point searchRitoLogo()
        {
            PixelSearch objPxSearch = new PixelSearch();
            Color cColor = Color.FromArgb(237, 46, 36);
            Point pPunkt = objPxSearch.GetPixelPosition(cColor, false);

            return pPunkt;
        }

        public void DoMouseClick(int X, int Y)
        {
            
            Cursor.Position = new Point(X, Y);
            //Klick ausführen
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void terminateProcess(string processName)
        {
            Process[] MatchingProcesses = Process.GetProcessesByName(processName);

            foreach (Process p in MatchingProcesses)
            {
                p.CloseMainWindow();
            }

            System.Threading.Thread.Sleep(500);

            MatchingProcesses = Process.GetProcessesByName(processName);

            foreach (Process p in MatchingProcesses)
            {
                if (p.HasExited == false)
                {
                    p.Kill();
                }
            }
        }
    }
}
