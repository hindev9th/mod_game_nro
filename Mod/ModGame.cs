using System.Threading;
using Mod;

internal class ModGame
{
	public static bool isXinDau, isThuDau;

	public static bool isChoDau;

	public static bool isTanSat;

	public static bool isShowPet;

	public static bool isShowChar;

	public static bool isSanBoss;
    public static bool isPickAll, isPickMe, isPickPet;
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


    public static void autoPickAll()
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

    /// <summary>
    /// Tự dộng nhặt tất cả item của bản thân đánh rơi ra
    /// </summary>
    public static void autoPickMe()
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
                    if (Char.myCharz().itemFocus.template.id != 673 && Char.myCharz().itemFocus.playerId == Char.myCharz().charID || Char.myCharz().itemFocus.playerId == -1)
                    {
                        Service.gI().pickItem(Char.myCharz().itemFocus.itemMapID);
                    }
                    else
                    {
                        if(Char.myCharz().itemFocus.playerId == Char.myCharz().charID || Char.myCharz().itemFocus.playerId == -1)
                            GameScr.gI().askToPick();
                    }
                }
            }
            Thread.Sleep(500);
    }

    /// <summary>
    /// Tìm kiếm các item của bản thân khi đánh quái rơi ra và focus nó
    /// </summary>
    public static void searchItemMe()
    {
        int[] array = new int[4] { -1, -1, -1, -1 };
        if (Char.myCharz().itemFocus != null && Char.myCharz().itemFocus.playerId == Char.myCharz().charID || Char.myCharz().itemFocus.playerId == -1)
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
                if (itemMap.template.type == 9 && Char.myCharz().itemFocus.playerId == Char.myCharz().charID || Char.myCharz().itemFocus.playerId == -1)
                {
                    Char.myCharz().itemFocus = itemMap;
                    array[3] = num3;
                }
            }
            else
            {
                if(Char.myCharz().itemFocus.playerId == Char.myCharz().charID || Char.myCharz().itemFocus.playerId == -1)
                {
                    Char.myCharz().itemFocus = itemMap;
                    array[3] = num3;
                }
            }
        }
    }
    public static void autoPickPet()
    {
        for(int i = 0; i < GameScr.vItemMap.size(); i++)
        {
            ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
            if(itemMap.playerId == Char.myCharz().charID || itemMap.playerId == -1)
            {
                Char.myCharz().itemFocus = itemMap;
                Utilities.teleportMyChar(itemMap.x, itemMap.y);
                GameScr.gI().pickItem();
                Thread.Sleep(1000);
                Utilities.teleportMyChar(charX, charY);
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

	public static void autoXinDau()
	{

			Service.gI().clanMessage(1, "", -1);
			Thread.Sleep(302000);
		
	}
    public static void autoThuDau()
    {

            try
            {
                if(TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
                {
                    if (GameScr.gI().magicTree.currPeas > 0)
                    {
                        Service.gI().magicTree(1);
                        Thread.Sleep(300);
                        GameCanvas.gI().keyPressedz(-5);
                    }
                    if (isChoDau)
                    {
                        for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
                        {
                            if (Char.myCharz().arrItemBag[i].template.type == 6)
                            {
                                Service.gI().getItem(1, (sbyte)i);
                            }
                        }
                    }
                }
                
            }
            catch
            {
                Thread.Sleep(1000);
            }
        
    }
    public static void autoAttack()
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


    /// <summary>
    /// Các sự kiện auto
    /// </summary>
    public static void update()
    {
        if (isPickAll && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoPickAll).Start();
        }
        if (isPickMe && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoPickMe).Start();
        }
        if (isPickPet && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoPickPet).Start();
        }
        if (isAttack && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoAttack).Start();
        }
        if (isChoDau && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoChoDau).Start();
        }
        if (isThuDau && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoThuDau).Start();
        }
        if (isXinDau && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(autoXinDau).Start();
        }if (Goback.isGoback && GameCanvas.gameTick % 20 == 0)
        {
            new Thread(Goback.Gobacking).Start();
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
                Utilities.useCapsule();
                break;
            case 102:
                Utilities.usePorata();
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
        else if (text.Contains("k "))
        {
            int khu = int.Parse(text.Replace("k ", ""));
            Utilities.changeZone(khu);
            return true;
        }

        switch (text)
        {
            case "ak":
                isAttack = !isAttack;
                GameScr.info1.addInfo((isAttack ? "Bật" : "Tắt") + " ak", 0);
                break;
            case "ts":
                isTanSat = !isTanSat;
                //new Thread(tanSat).Start();
                GameScr.info1.addInfo((isTanSat ? "Bật" : "Tắt") + " tàn sát", 0);
                break;
            case "chodau":
                isChoDau = !isChoDau;
                GameScr.info1.addInfo((isChoDau ? "Bật" : "Tắt") + " cho đậu", 0);
                break;
            case "xindau":
                isXinDau = !isXinDau;
                GameScr.info1.addInfo((isXinDau ? "Bật" : "Tắt") + " xin đậu", 0);
                break;
            case "thudau":
                isThuDau = !isThuDau;
                GameScr.info1.addInfo((isThuDau ? "Bật" : "Tắt") + " thu đậu", 0);
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
                GameScr.info1.addInfo((isPickAll ? "Bật" : "Tắt") + " tự động nhặt", 0);
                break;
            case "anhatpet":
                isPickPet = !isPickPet;
                charX = Char.myCharz().cx;
                charY = Char.myCharz().cy;
                GameScr.info1.addInfo((isPickPet ? "Bật" : "Tắt") + " tự động nhặt đồ của đệ tử", 0);
                break;
            case "anhatme":
                isPickMe = !isPickMe;
                GameScr.info1.addInfo((isPickMe ? "Bật" : "Tắt") + " tự động nhặt đồ của bản thân", 0);
                break;
            case "goback":
                Goback.setGoback(TileMap.mapID, TileMap.zoneID, Char.myCharz().cx, Char.myCharz().cy);
                Goback.isGoback = !Goback.isGoback;
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
