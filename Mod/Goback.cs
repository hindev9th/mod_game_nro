using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mod
{
    class Goback
    {
        public static bool isDie;
        public static int idMap;
        public static int khu;
        public static int cX;
        public static int cY;
        public static int idMapBanDo;
        public static int npcX, npcY;

        public static bool runToBando;
        public static bool isrunToBando;
        public static bool isBando = true;
        public static bool isGoback = false;
        static Goback goback;


        public static void setGoback(int idMap, int khu, int cX, int cY)
        {
            Goback.idMap = idMap;
            Goback.khu = khu;
            Goback.cX = cX;
            Goback.cY = cY;
        }

        public static void Gobacking()
        {
            while (isGoback) {
                if (Char.myCharz().cHP == 0 || Char.myCharz().cHP < 0)
                {
                    Goback.isDie = true;
                    GameScr.gI().onChatFromMe("xmp" + idMap, "xmp" + idMap);
                }
                else if (TileMap.mapID == idMap && TileMap.zoneID != khu && Goback.isDie)
                {
                    Utilities.changeZone(khu);
                    GameScr.info1.addInfo("Chuyển sang khu: " + khu, 0);
                }
                else if (TileMap.mapID == idMap && TileMap.zoneID == khu && (Char.myCharz().cx != cX || Char.myCharz().cy != cY) && Goback.isDie)
                {
                    Utilities.teleportMyChar(cX, cY);
                }
                else if (TileMap.mapID == idMap && TileMap.zoneID == khu && Char.myCharz().cx == cX && Char.myCharz().cy == cY && Goback.isDie)
                {
                    Goback.isDie = false;
                    isBando = true;
                    GameScr.info1.addInfo("Đã xong goback!", 0);
                }
                Thread.Sleep(2000);
            }
        }

        public static void runBanDo()
        {
            if (Char.myCharz().nClass.classId == 0)
            {
                Goback.idMapBanDo = 0;
                Goback.npcX = 233;
                Goback.npcY = 432;
            }
            if (Char.myCharz().nClass.classId == 1)
            {
                Goback.idMapBanDo = 7;
                Goback.npcX = 300;
                Goback.npcY = 432;
            }
            if (Char.myCharz().nClass.classId == 2)
            {
                Goback.idMapBanDo = 14;
                Goback.npcX = 396;
                Goback.npcY = 408;
            }
            while (isrunToBando)
            {
                if (runToBando)
                {
                    GameScr.gI().onChatFromMe("xmp" + idMapBanDo, "xmp" + idMapBanDo);
                    runToBando = false;
                }
                else if (TileMap.mapID == idMapBanDo && (Char.myCharz().cx != npcX || Char.myCharz().cy != npcY) && GameScr.gI().isBagFull())
                {
                    Utilities.teleportMyChar(npcX, npcY);
                }
                else if (TileMap.mapID == idMapBanDo && Char.myCharz().cx == npcX && Char.myCharz().cy == npcY && GameScr.gI().isBagFull())
                {
                    GameScr.info1.addInfo("Bắt đầu bán đồ!", 0);
                    ModGame.autoBanDo(); 
                }
                Thread.Sleep(2000);
            }
        }

    }
}
