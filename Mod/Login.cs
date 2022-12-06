using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace Mod
{
    class Login
    {
        public static bool isLogin;

        public static string fileLog = "NRO_2.2.2_Data/Log";

        public static int ID;

        public static string fileAccount = "Data/data.json";

        public static string account;

        public static string password;

        public static int server;

        public static string fileSize = "Data/Size.txt";

        public static int width;

        public static int height;

        /// <summary>
        /// Load file data account
        /// </summary>
        public static void LoadFileAccount()
        {
            if (File.Exists(fileLog))
            {
                string[] array = File.ReadAllText(fileLog).Split(new char[]
                {
                '|'
                });
                ID = int.Parse(array[0]);
                account = array[1];
                password = array[2];
                server = int.Parse(array[3]);
                server--;
                File.Delete(fileLog);
                return;
            }
        }

        public static void login()
        {
            while (!ServerListScreen.loadScreen)
            {
                Thread.Sleep(10);
            }
            if (account != null)
            {
                isLogin = true;
                if (Rms.loadRMSInt("svselect") != server)
                {
                    Rms.saveRMSInt("svselect", server);
                    ServerListScreen.ipSelect = server;
                    GameCanvas.serverScreen.selectServer();
                }
                while (!Session_ME.gI().isConnected())
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(500);
                if (GameCanvas.loginScr == null)
                {
                    GameCanvas.loginScr = new LoginScr();
                }
                GameCanvas.loginScr.switchToMe();
                GameCanvas.loginScr.doLogin();
            }
        }

        public static void GetSize()
        {
            if (File.Exists(fileSize))
            {
                string[] array = File.ReadAllText(fileSize).Split(new char[]
                {
                'x'
                });
                width = int.Parse(array[0]);
                height = int.Parse(array[1]);
                return;
            }
            width = 1024;
            height = 600;
        }
    }
}
