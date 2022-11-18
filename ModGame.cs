using System.Threading;
using Mod;

internal class ModGame
{
	public static bool isXinDau;

	public static bool isChoDau;

	public static bool isTanSat;

	public static bool isShowPet;

	public static bool isShowChar;

	public static bool isSanBoss;
    public static bool isPickAll, isPickMe;
    public static bool isAttack;
    public static int charX, charY;
    public static string[] petStatus = { "Đi theo" , "Bảo vệ" , "Tấn Công", "Về nhà","Hợp thể","Hợp thể vĩnh viễn" };
    static ModGame modGame;
    private static bool isHutItem;
    public static int attackCooldown = 500;

    public static ModGame clone()
    {
        if(modGame == null)
        {
            modGame = new ModGame();
        }
        return modGame;
    }

    /*
    public static void tanSat()
    {
        while (ModGame.isTanSat)
        {

            if (!ModGame.isTanSat || GameScr.isChangeZone || Char.myCharz().statusMe == 14 || Char.myCharz().statusMe == 5 || Char.myCharz().isCharge || Char.myCharz().isFlyAndCharge || Char.myCharz().isUseChargeSkill())
            {
                return;
            }
            bool flag = false;
            for (int i = 0; i < GameScr.vMob.size(); i++)
            {
                Mob mob = (Mob)GameScr.vMob.elementAt(i);
                if (mob.status != 0 && mob.status != 1)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                return;
            }
            bool flag2 = false;
            for (int j = 0; j < Char.myCharz().arrItemBag.Length; j++)
            {
                Item item = Char.myCharz().arrItemBag[j];
                if (item != null && item.template.type == 6)
                {
                    flag2 = true;
                    break;
                }
            }
            if (!flag2 && GameCanvas.gameTick % 150 == 0)
            {
                Service.gI().requestPean();
            }
            if (Char.myCharz().cHP <= Char.myCharz().cHPFull * 20 / 100 || Char.myCharz().cMP <= Char.myCharz().cMPFull * 20 / 100)
            {
                new GameScr().doUseHP();
            }
            int[] array = new int[4] { -1, -1, -1, -1 };
            int num = 0;
            if (Char.myCharz().nClass.classId == 0 || Char.myCharz().nClass.classId == 1 || Char.myCharz().nClass.classId == 3 || Char.myCharz().nClass.classId == 5)
            {
                num = 400;
            }
            int num2 = Char.myCharz().cx - Char.myCharz().getdxSkill() - 300;
            int num3 = Char.myCharz().cx + Char.myCharz().getdxSkill() + 300;
            int num4 = Char.myCharz().cy - Char.myCharz().getdySkill() - num - 200;
            int num5 = Char.myCharz().cy + Char.myCharz().getdySkill() + 200;
            if (num5 > Char.myCharz().cy + 300)
            {
                num5 = Char.myCharz().cy + 300;
            }
            if (Char.myCharz().mobFocus == null || (Char.myCharz().mobFocus != null && Char.myCharz().mobFocus.isMobMe))
            {
                for (int k = 0; k < GameScr.vMob.size(); k++)
                {
                    Mob mob2 = (Mob)GameScr.vMob.elementAt(k);
                    int num6 = Math.abs(Char.myCharz().cx - mob2.x);
                    int num7 = Math.abs(Char.myCharz().cy - mob2.y);
                    int num8 = ((num6 <= num7) ? num7 : num6);
                    //if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe && !mob2.isBigBoss() && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5 && (Char.myCharz().mobFocus == null || num8 < array[0]))
                   // {
                        //Char.myCharz().cx = mob2.x;
                        //Char.myCharz().cy = mob2.y;
                       // Utilities.teleportMyChar(mob2.x, mob2.y);
                       // Char.myCharz().mobFocus = mob2;
                      //  array[0] = num8;
                       // Service.gI().charMove();
                       // Res.outz("focus 1 con bossssssssssssssssssssssssssssssssssssssssssssssssss");
                       // break;
                    //}
                    if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe && !mob2.isBigBoss())
                    {
                        Utilities.teleportMyChar(mob2.x, mob2.y);
                        Char.myCharz().mobFocus = mob2;
                        Service.gI().charMove();
                        Res.outz("focus 1 con bossssssssssssssssssssssssssssssssssssssssssssssssss");
                        break;
                    }
                }
            }
            else if (Char.myCharz().mobFocus.hp <= 0 || Char.myCharz().mobFocus.status == 1 || Char.myCharz().mobFocus.status == 0)
            {
                Char.myCharz().mobFocus = null;
                Char.myCharz().searchItem();
                while(Char.myCharz().itemFocus != null && ModGame.isPickAll)
                {
                    GameScr.gI().pickItem();
                    Char.myCharz().searchItem();
                    Thread.Sleep(500);
                }
            }
            if(Char.myCharz().mobFocus != null)
            {
                new GameScr().doDoubleClickToObj(Char.myCharz().mobFocus);
            }
            Thread.Sleep(350);
        }
    }
    */
    public static void autoPickAll()
    {
        while (ModGame.isPickAll)
        {
            Char.myCharz().searchItem();
            if(Char.myCharz().itemFocus != null)
            {
                int num = Math.abs(Char.myCharz().cx - Char.myCharz().itemFocus.x);
                int num2 = Math.abs(Char.myCharz().cy - Char.myCharz().itemFocus.y);
                if (num <= 40 && num2 < 40)
                {
                    GameCanvas.clearKeyHold();
                    GameCanvas.clearKeyPressed();
                    if (Char.myCharz().itemFocus.template.id != 673)
                    {
                        Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
                    }
                    else
                    {
                        GameScr.gI().askToPick();
                    }
                }
            }
            Thread.Sleep(500);
        }
    }

    /// <summary>
    /// Tự dộng nhặt tất cả item của bản thân đánh rơi ra
    /// </summary>
    public static void autoPickMe()
    {
        while (ModGame.isPickMe)
        {
            searchItemMe();
            if (Char.myCharz().itemFocus != null)
            {
                int num = Math.abs(Char.myCharz().cx - Char.myCharz().itemFocus.x);
                int num2 = Math.abs(Char.myCharz().cy - Char.myCharz().itemFocus.y);
                if (num <= 450 && num2 < 500)
                {
                    GameCanvas.clearKeyHold();
                    GameCanvas.clearKeyPressed();
                    if (Char.myCharz().itemFocus.template.id != 673 && Char.myCharz().itemFocus.playerId == Char.myCharz().charID)
                    {
                        Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
                    }
                    else
                    {
                        if(Char.myCharz().itemFocus.playerId == Char.myCharz().charID)
                            GameScr.gI().askToPick();
                    }
                }
            }
            Thread.Sleep(500);
        }
    }

    /// <summary>
    /// Tìm kiếm các item của bản thân khi đánh quái rơi ra và focus nó
    /// </summary>
    public static void searchItemMe()
    {
        int[] array = new int[4] { -1, -1, -1, -1 };
        if (Char.myCharz().itemFocus != null && Char.myCharz().itemFocus.playerId == Char.myCharz().charID)
        {
            return;
        }
        for (int i = 0; i < GameScr.vItemMap.size(); i++)
        {
            ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
            int num = Math.abs(Char.myCharz().cx - itemMap.x);
            int num2 = Math.abs(Char.myCharz().cy - itemMap.y);
            int num3 = ((num <= num2) ? num2 : num);
            if (num > 450 || num2 > 500 || (Char.myCharz().itemFocus != null && Char.myCharz().itemFocus.playerId == Char.myCharz().charID && num3 >= array[3]))
            {
                continue;
            }
            if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
            {
                if (itemMap.template.type == 9 && Char.myCharz().itemFocus.playerId == Char.myCharz().charID)
                {
                    Char.myCharz().itemFocus = itemMap;
                    array[3] = num3;
                }
            }
            else
            {
                if(Char.myCharz().itemFocus.playerId == Char.myCharz().charID)
                {
                    Char.myCharz().itemFocus = itemMap;
                    array[3] = num3;
                }
            }
        }
    }
    
    public static void autoAttackPet()
	{
		try
		{
			int num = 0;
			if (Char.myCharz().nClass.classId == 0 || Char.myCharz().nClass.classId == 1 || Char.myCharz().nClass.classId == 3 || Char.myCharz().nClass.classId == 5)
			{
				num = 400;
			}
			int num2 = Char.myCharz().cx - Char.myCharz().getdxSkill() - 500;
			int num3 = Char.myCharz().cx + Char.myCharz().getdxSkill() + 500;
			int num4 = Char.myCharz().cy - Char.myCharz().getdySkill() - num - 200;
			int num5 = Char.myCharz().cy + Char.myCharz().getdySkill() + 200;
			if (num5 > Char.myCharz().cy + 300)
			{
				num5 = Char.myCharz().cy + 300;
			}
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				Math.abs(Char.myCharz().cx - mob.x);
				Math.abs(Char.myCharz().cy - mob.y);
				if (mob.status != 0 && mob.status != 1 && mob.hp > 0 && !mob.isMobMe && num2 <= mob.x && mob.x <= num3 && num4 <= mob.y && mob.y <= num5)
				{
					MyVector myVector = new MyVector();
					myVector.addElement(mob);
                    Utilities.autoAttackSkillLong(myVector, new MyVector());
                    //Service.gI().sendPlayerAttack(myVector, new MyVector(), -1);
					Res.outz("focus 1 con bossssssssssssssssssssssssssssssssssssssssssssssssss");
					break;
				}
			}
		}
		catch
		{
		}
	}



	public static void autoChoDau()
	{
		while (isChoDau)
		{
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.maxCap != 0 && clanMessage.playerName != Char.myCharz().cName && clanMessage.recieve != clanMessage.maxCap)
				{
					for (int j = 0; j < 5; j++)
					{
						Service.gI().clanDonate(clanMessage.id);
						Thread.Sleep(150);
					}
				}
			}
			Thread.Sleep(150);
		}
	}

	public static void autoXinDau()
	{
		while (isXinDau)
		{
			Service.gI().clanMessage(1, "", -1);
			Thread.Sleep(302000);
		}
	}
    public static void autoAttack()
    {
        while (isAttack)
        {
            MyVector vChar = new MyVector();
            MyVector vMob = new MyVector();
            if (Char.myCharz().mobFocus != null)
            {
                vMob.addElement(Char.myCharz().mobFocus);  
            }
            if (Char.myCharz().charFocus != null)
            {
                vMob.addElement(Char.myCharz().charFocus);
            }
            if (vChar.size() > 0 || vMob.size() > 0)
            {
                Service.gI().sendPlayerAttack(vMob, vChar, -1);
            }
            Thread.Sleep(attackCooldown);
        }
    }

    public static void useBongTai()
	{
		try
		{
			Item[] arrItemBag = Char.myCharz().arrItemBag;
			for (sbyte b = 0; b < arrItemBag.Length; b = (sbyte)(b + 1))
			{
				if (arrItemBag[b].template.id == 454 )
				{
					Service.gI().useItem(0, 1, b, -1);
					break;
				}
			}
		}
		catch
		{
            GameScr.info1.addInfo("Lỗi không tìm thấy Bông tai!", 0);
        }
	}

	public static void useCapsule()
	{
		try
		{
			Item[] arrItemBag = Char.myCharz().arrItemBag;
			for (sbyte b = 0; b < arrItemBag.Length; b = (sbyte)(b + 1))
			{
				if (arrItemBag[b].template.id == 194)
				{
					Service.gI().useItem(0, 1, b, -1);
					break;
				}
                if (arrItemBag[b].template.id == 193)
                {
                    Service.gI().useItem(0, 1, b, -1);
                    break;
                }
            }
		}
		catch
		{
            GameScr.info1.addInfo("Lỗi không tìm thấy Capsule!", 0);
        }
	}

    /// <summary>
    /// Sử lý các lệnh bằng phím tắt
    /// </summary>
    /// <returns></returns>
	public static bool keyAction()
	{
        switch (GameCanvas.keyAsciiPress)
        {
            case 'j':
                Utilities.changeMapLeft();
                break;
            case 'k':
                Utilities.changeMapMiddle();
                break;
            case 'l':
                Utilities.changeMapRight();
                break;
            case 'h':
                Utilities.buffMe();
                break;
            case 'm':
                Utilities.openUiZone();
                break;
            case 'g':
                Utilities.sendGiaoDichToCharFocusing();
                break;
            case 't':
                GameScr.gI().onChatFromMe("ts", "ts");
                break;
            case 97:
                GameScr.gI().onChatFromMe("ak", "ak");
                break;
            case 99:
                useCapsule();
                break;
            case 102:
                useBongTai();
                break;
            case 120:
                Service.gI().chat("xmp");
                break;
            default:
                return false;

        }
        return true;
	}

    /// <summary>
    /// Xử lý các lệnh chat của mod
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
	public static bool chatAction(string text)
	{
        if(text.Contains("s "))
        {
            int speedRun = int.Parse(text.Replace("s ", ""));
            Utilities.setSpeedRun(speedRun);
            return true;
        }else if (text.Contains("speed "))
        {
            string textSp = text.Replace("speed ", "");
            float speedRun = 1.7f;
            if ( text.Contains("speed 1_") || text.Contains("speed 2_")|| text.Contains("speed 3_")|| text.Contains("speed 4_")|| text.Contains("speed 5_")|| text.Contains("speed 6_")|| text.Contains("speed 7_")|| text.Contains("speed 8_")|| text.Contains("speed 9_") || text.Contains("speed 10_"))
            {
                speedRun = float.Parse(textSp.Replace("_",".")+"0");
            }
            else
            {
                speedRun = float.Parse(textSp);
            }
            
            Utilities.setSpeedGame(speedRun);
            return true;
        }
        else if (text.Contains("ak "))
        {
            attackCooldown = int.Parse(text.Replace("ak ", ""));
            isAttack = !isAttack;
            new Thread(ModGame.autoAttack).Start();
            GameScr.info1.addInfo((isAttack ? "Bật" : "Tắt") + " ak", 0);
            return true;
        }

        switch (text)
        {
            case "ak":
                isAttack = !isAttack;
                new Thread(ModGame.autoAttack).Start();
                GameScr.info1.addInfo((isAttack ? "Bật" : "Tắt") + " ak", 0);
                break;
            case "ts":
                isTanSat = !isTanSat;
                //new Thread(tanSat).Start();
                GameScr.info1.addInfo((isTanSat ? "Bật" : "Tắt") + " tàn sát", 0);
                break;
            case "chodau":
                isChoDau = !isChoDau;
                new Thread(autoChoDau).Start();
                GameScr.info1.addInfo((isChoDau ? "Bật" : "Tắt") + " cho đậu", 0);
                break;
            case "xindau":
                isXinDau = !isXinDau;
                new Thread(autoXinDau).Start();
                GameScr.info1.addInfo((isXinDau ? "Bật" : "Tắt") + " xin đậu", 0);
                break;
            case "ttdt":
                isShowPet = !isShowPet;
                GameScr.info1.addInfo((isShowPet ? "Bật" : "Tắt") + " thông tin đệ tử", 0);
                break;
            case "ttsp":
                isShowChar = !isShowChar;
                GameScr.info1.addInfo((isShowChar ? "Bật" : "Tắt") + " thông tin sư phụ", 0);
                break;
            case "sb":
                isSanBoss = !isSanBoss;
                GameScr.info1.addInfo((isSanBoss ? "Bật" : "Tắt") + " thông báo boss", 0);
                break;
            case "anhat":
                isPickAll = !isPickAll;
                new Thread(ModGame.autoPickAll).Start();
                GameScr.info1.addInfo((isPickAll ? "Bật" : "Tắt") + " tự động nhặt", 0);
                break;
            case "anhatme":
                isPickMe = !isPickMe;
                new Thread(ModGame.autoPickMe).Start();
                GameScr.info1.addInfo((isPickMe ? "Bật" : "Tắt") + " tự động nhặt đồ của bản thân", 0);
                break;
            case "goback":
                Goback.setGoback(TileMap.mapID, TileMap.zoneID, Char.myCharz().cx, Char.myCharz().cy);
                Goback.isGoback = !Goback.isGoback;
                new Thread(Goback.Gobacking).Start();
                GameScr.info1.addInfo((Goback.isGoback ? "Bật" : "Tắt") + " goback", 0);
                break;
            case "w":
                Utilities.KhinhCong();
                break;
            case "s":
                Utilities.DonTho();
                break;
            case "a":
                Utilities.DichTrai();
                break;
            case "d":
                Utilities.DichPhai();
                break;
            default:
                return false;
        }
        
        return true;
    }
}
