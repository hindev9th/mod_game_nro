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

        public static bool isGoback = false;
        static Goback goback;


        public static void setGoback(int idMap,int khu,int cX,int cY)
        {
            Goback.idMap = idMap;
            Goback.khu = khu;
            Goback.cX = cX;
            Goback.cY = cY;
        }

        public static void Gobacking()
        {
                if(Char.myCharz().cHP == 0 || Char.myCharz().cHP < 0)
                {
                    Goback.isDie = true;
                    GameScr.gI().onChatFromMe("xmp" + idMap, "xmp" + idMap);
                }
                else if (TileMap.mapID == idMap && TileMap.zoneID != khu && Goback.isDie)
                {
                    Utilities.changeZone(khu);
                    GameScr.info1.addInfo("Chuyển sang khu: " + khu, 0);
                }
                else if (TileMap.mapID == idMap && TileMap.zoneID == khu && (Char.myCharz().cx != cX || Char.myCharz().cy != cY)  && Goback.isDie)
                {
                    Utilities.teleportMyChar(cX, cY);
                }
                else if(TileMap.mapID == idMap && TileMap.zoneID == khu && Char.myCharz().cx == cX && Char.myCharz().cy == cY && Goback.isDie)
                {
                    Goback.isDie = false;
                    GameScr.info1.addInfo("Goback thành công!", 0);
                }
                Thread.Sleep(2000);

        }
    }
}
