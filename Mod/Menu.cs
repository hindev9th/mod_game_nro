using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod
{
    class Menu
    {
        /// <summary>
        /// Xử lý các sự kiện trong menu mod
        /// </summary>
        /// <param name="idAction"></param>
        public static void actionMenu(int idAction)
        {
            switch (idAction)
            {
                case 3:
                    menuThongTin();
                    break;
                case 4:
                    menuTanCong();
                    break;
                case 5:
                    menuNhat();
                    break;
                case 6:
                    menuDauThan();
                    break;
                case 7:
                    menuKhac();
                    break;
                case 8:
                    GameScr.gI().onChatFromMe("ttsp", "ttsp");
                    break;
                case 9:
                    GameScr.gI().onChatFromMe("ttdt", "ttdt");
                    break;
                case 10:
                    GameScr.gI().onChatFromMe("sb", "sb");
                    break;
                case 11:
                    GameScr.gI().onChatFromMe("ak", "ak");
                    break;
                case 12:
                    GameScr.gI().onChatFromMe("ts", "ts");
                    break;
                case 13:
                    GameScr.gI().onChatFromMe("anhat", "anhat");
                    break;
                case 14:
                    GameScr.gI().onChatFromMe("anhatts", "anhatts");
                    break;
                case 15:
                    GameScr.gI().onChatFromMe("thudau", "thudau");
                    break;
                case 16:
                    GameScr.gI().onChatFromMe("xindau", "xindau");
                    break;
                case 17:
                    GameScr.gI().onChatFromMe("chodau", "chodau");
                    break;
                case 18:
                    GameScr.gI().onChatFromMe("alogin", "alogin");
                    break;
                case 19:
                    GameScr.gI().onChatFromMe("goback", "goback");
                    break;
                case 20:
                    GameScr.gI().onChatFromMe("bando", "bando");
                    break;
                case 21:
                    GameScr.gI().onChatFromMe("ahsnm", "ahsnm");
                    break;
                case 22:
                    GameScr.gI().onChatFromMe("akhu", "akhu");
                    break;
            }
        }

        /// <summary>
        /// Menu chính của bản mod
        /// </summary>
        public static void mainMenu()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Thông tin",3));
            myVector.addElement(new Command("Tấn công",4));
            myVector.addElement(new Command("Nhặt",5));
            myVector.addElement(new Command("Đậu thần",6));
            myVector.addElement(new Command("Khác",7));
            GameCanvas.menu.startAt(myVector,myVector.size());
        }

        /// <summary>
        /// Menu thông tin
        /// </summary>
        public static void menuThongTin()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Thông tin sư phụ: " + (ModGame.isShowChar ? "Bật" : "Tắt"), 8));
            myVector.addElement(new Command("Thông tin đệ tử: " + (ModGame.isShowPet ? "Bật" : "Tắt"), 9));
            myVector.addElement(new Command("Thông báp boss: " + (ModGame.isSanBoss ? "Bật" : "Tắt"), 10));
            GameCanvas.menu.startAt(myVector, myVector.size());
        }

        /// <summary>
        /// Menu tấn công
        /// </summary>
        public static void menuTanCong()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Tấn công: " + (ModGame.isAttack ? "Bật" : "Tắt"), 11));
            myVector.addElement(new Command("Tàn sát: " + (ModGame.isTanSat ? "Bật" : "Tắt"), 12));
            GameCanvas.menu.startAt(myVector, myVector.size());
        }

        /// <summary>
        /// Menu nhặt
        /// </summary>
        public static void menuNhat()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Nhặt tất cả: " + (ModGame.isPickAll ? "Bật" : "Tắt"), 13));
            myVector.addElement(new Command("Nhặt khi tàn sát: " + (ModGame.isPickTanSat ? "Bật" : "Tắt"), 14));
            GameCanvas.menu.startAt(myVector, myVector.size());
        }

        /// <summary>
        /// Menu đậu thần
        /// </summary>
        public static void menuDauThan()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Thu đậu: " + (ModGame.isThuDau ? "Bật" : "Tắt"), 15));
            myVector.addElement(new Command("Xin đậu: " + (ModGame.isXinDau ? "Bật" : "Tắt"), 16));
            myVector.addElement(new Command("Cho đậu: " + (ModGame.isChoDau ? "Bật" : "Tắt"), 17));
            GameCanvas.menu.startAt(myVector, myVector.size());
        }

        public static void menuKhac()
        {
            MyVector myVector = new MyVector();
            myVector.addElement(new Command("Auto login: " + (ModGame.isLogin ? "Bật" : "Tắt"), 18));
            myVector.addElement(new Command("Goback: " + (Goback.isGoback ? "Bật" : "Tắt"), 19));
            myVector.addElement(new Command("Bán đồ khi full: " + (Goback.isrunToBando ? "Bật" : "Tắt"), 20));
            //if(Char.myCharz().nClass.classId == 1)
                //myVector.addElement(new Command("Auto hsmn: " + (ModGame.isHSNM ? "Bật" : "Tắt"), 21));

            myVector.addElement(new Command("Quay lại khu: " + (ModGame.isKhu ? ModGame.khu :TileMap.zoneID) + " : " + (ModGame.isKhu ? "Bật" : "Tắt"), 22));
            GameCanvas.menu.startAt(myVector, myVector.size());
        }
    }
}
