
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

using System.Threading;

namespace Mod
{
    public class Utilities 
    {
        public const string ManifestModuleName = "Assembly-CSharp.dll";
        public const string PathChatCommand = @"ModData\chatCommands.json";
        public const string PathChatHistory = @"ModData\chat.txt";
        public const string PathHotkeyCommand = @"ModData\hotkeyCommands.json";

        public const sbyte ID_SKILL_BUFF = 7;
        public const int ID_ICON_ITEM_TDLT = 4387;
        public const short ID_NPC_MOD_FACE = 7333;// Doraemon, TODO: custom npc avatar

        private const BindingFlags PUBLIC_STATIC_VOID =
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.InvokeMethod;

        public static string status = "Đã kết nối";

        #region Singleton
        private Utilities() { }
        static Utilities() { }
        public static Utilities gI { get; } = new Utilities();
        #endregion

        public static int speedRun = 8;

        public static Waypoint waypointLeft;
        public static Waypoint waypointMiddle;
        public static Waypoint waypointRight;

        public static string username = "";
        public static string password = "";


        #region Get Methods
        /// <summary>
        /// Lấy danh sách các hàm trong theo tên của class.
        /// </summary>
        /// <remarks> Lưu ý:
        /// <list type="bullet">
        /// <item><description>Chỉ lấy các hàm public static void.</description></item>
        /// <item><description>Tên class phải bao gồm cả namespace.</description></item>
        /// </list>
        /// </remarks>
        /// <param name="typeFullName"></param>
        /// <returns>Danh sách các hàm trong class.</returns>
        public static MethodInfo[] getMethods(string typeFullName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .First(x => x.ManifestModule.Name == Utilities.ManifestModuleName)
                .GetTypes().FirstOrDefault(x => x.FullName.ToLower() == typeFullName.ToLower())
                .GetMethods(PUBLIC_STATIC_VOID);
        }

        /// <summary>
        /// Lấy danh sách tất cả các hàm của tệp Assembly-CSharp.dll.
        /// </summary>
        /// <remarks> Lưu ý:
        /// <list type="bullet">
        /// <item><description>Chỉ lấy các hàm public static void.</description></item>
        /// <item><description>Tên class phải bao gồm cả namespace.</description></item>
        /// </list>
        /// </remarks>
        /// <returns>Danh sách các hàm của tệp Assembly-CSharp.dll.</returns>
        public static IEnumerable<MethodInfo> GetMethods()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .First(x => x.ManifestModule.Name == ManifestModuleName)
                .GetTypes().Where(x => x.IsClass)
                .SelectMany(x => x.GetMethods(PUBLIC_STATIC_VOID));
        }
        #endregion

        #region Get info
        /// <summary>
        /// Lấy MyVector chứa nhân vật của người chơi.
        /// </summary>
        /// <returns></returns>
        public static MyVector getMyVectorMe()
        {
            var vMe = new MyVector();
            vMe.addElement(Char.myCharz());
            return vMe;
        }

        /// <summary>
        /// Kiểm tra khả năng sử dụng skill Trị thương vào bản thân.
        /// </summary>
        /// <param name="skillBuff">Skill trị thương.</param>
        /// <returns>true nếu có thể sử dụng skill trị thương vào bản thân.</returns>
        public static bool canBuffMe(out Skill skillBuff)
        {
            skillBuff = Char.myCharz().
                getSkill(new SkillTemplate { id = ID_SKILL_BUFF });

            if (skillBuff == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// check skill có thể dùng hay ko
        /// </summary>
        /// <param name="idSkill"></param>
        /// <returns>true nếu có thể sử dụng skill.</returns>
        public static bool canSkillUse(sbyte idSkill)
        {
            Skill skill = Char.myCharz().
                getSkill(new SkillTemplate { id = idSkill });

            if (skill == null)
            {
                return false;
            }

            return true;
        }
        public static string getTextPopup(PopUp popUp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < popUp.says.Length; i++)
            {
                stringBuilder.Append(popUp.says[i]);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }

        /// <summary>
        /// Kiểm tra trạng thái sử dụng TĐLT.
        /// </summary>
        /// <returns>true nếu đang sử dụng tự động luyên tập</returns>
        public static bool isUsingTDLT() =>
            ItemTime.isExistItem(ID_ICON_ITEM_TDLT);

        public static int getXWayPoint(Waypoint waypoint)
        {
            return waypoint.maxX < 60 ? 15 :
                waypoint.minX > TileMap.pxw - 60 ? TileMap.pxw - 15 :
                waypoint.minX + 30;
        }

        public static int getYWayPoint(Waypoint waypoint)
        {
            return waypoint.maxY;
        }

        /// <summary>
        /// Sử dụng một item có id là một trong số các id truyền vào.
        /// </summary>
        /// <param name="templatesId">Mảng chứa các id của các item muốn sử dụng.</param>
        /// <returns>true nếu có vật phẩm được sử dụng.</returns>
        public static sbyte getIndexItemBag(params short[] templatesId)
        {
            var myChar = Char.myCharz();
            int length = myChar.arrItemBag.Length;
            for (sbyte i = 0; i < length; i++)
            {
                var item = myChar.arrItemBag[i];
                if (item != null && templatesId.Contains(item.template.id))
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        /// <summary>
        /// Dịch chuyển tới npc trong map.
        /// </summary>
        /// <param name="npc">Npc cần dịch chuyển tới</param>
        public static void teleToNpc(Npc npc)
        {
            teleportMyChar(npc.cx, npc.ySd - npc.ySd % 24);
            Char.myCharz().npcFocus = npc;
        }

        public static void requestChangeMap(Waypoint waypoint)
        {
            if (waypoint.isOffline)
            {
                Service.gI().getMapOffline();
                return;
            }

            Service.gI().requestChangeMap();
        }

        public static void setWaypointChangeMap(Waypoint waypoint)
        {
            int cMapID = TileMap.mapID;
            var textPopup = getTextPopup(waypoint.popup);

            if (cMapID == 27 && textPopup == "Tường thành 1")
                return;

            if (cMapID == 70 && textPopup == "Vực cấm" ||
                cMapID == 73 && textPopup == "Vực chết" ||
                cMapID == 110 && textPopup == "Rừng tuyết")
            {
                waypointLeft = waypoint;
                return;
            }

            if (((cMapID == 106 || cMapID == 107) && textPopup == "Hang băng") ||
                ((cMapID == 105 || cMapID == 108) && textPopup == "Rừng băng") ||
                (cMapID == 109 && textPopup == "Cánh đồng tuyết"))
            {
                waypointMiddle = waypoint;
                return;
            }

            if (cMapID == 70 && textPopup == "Căn cứ Raspberry")
            {
                waypointRight = waypoint;
                return;
            }

            if (waypoint.maxX < 60)
            {
                waypointLeft = waypoint;
                return;
            }

            if (waypoint.minX > TileMap.pxw - 60)
            {
                waypointRight = waypoint;
                return;
            }

            waypointMiddle = waypoint;
        }

        public static void updateWaypointChangeMap()
        {
            waypointLeft = waypointMiddle = waypointRight = null;

            var vGoSize = TileMap.vGo.size();
            for (int i = 0; i < vGoSize; i++)
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                setWaypointChangeMap(waypoint);
            }
        }

        /// <summary>
        /// set tốc độ chạy {int}
        /// </summary>
        /// <param name="speed"></param>
        public static void setSpeedRun(int speed)
        {
            speedRun = speed;

            GameScr.info1.addInfo("Tốc độ chạy: " + speed, 0);
        }

        /// <summary>
        /// set tốc độ game {float}
        /// </summary>
        /// <param name="speed"></param>
        public static void setSpeedGame(float speed)
        {
            Time.timeScale = speed;
            GameScr.info1.addInfo("Tốc độ game: " + speed, 0);
        }

        /// <summary>
		/// Sử dụng skill Trị thương của namec vào bản thân.
		/// </summary>

        public static void buffMe()
        {
            if (!canBuffMe(out Skill skillBuff))
            {
                GameScr.info1.addInfo("Không tìm thấy kỹ năng Trị thương", 0);
                return;
            }

            // Đổi sang skill hồi sinh
            Service.gI().selectSkill(ID_SKILL_BUFF);

            // Tự tấn công vào bản thân
            Service.gI().sendPlayerAttack(new MyVector(), getMyVectorMe(), -1);

            // Trả về skill cũ
            Service.gI().selectSkill(Char.myCharz().myskill.template.id);

            // Đặt thời gian hồi cho skill
            skillBuff.lastTimeUseThisSkill = mSystem.currentTimeMillis();
        }

        public static void autoAttackSkillLong(MyVector myVector,MyVector myVector1)
        {
            //23: trói , 5: automic , 3: masenko ,6 kame
            sbyte[] skill = { 23, 5, 3, 6 };
            for(int i = 0; i < skill.Length; i++)
            {
                if (canSkillUse(skill[i]))
                {
                    // Đổi sang skill 
                    Service.gI().selectSkill(skill[i]);

                    // Tự tấn công vào bản thân
                    Service.gI().sendPlayerAttack(myVector, myVector1, -1);

                    // Trả về skill cũ
                    Service.gI().selectSkill(Char.myCharz().myskill.template.id);
                    break;
                }
            }
        }

        /// <summary>
        /// Dịch chuyển tới một toạ độ cụ thể trong map.
        /// </summary>
        /// <param name="x">Toạ độ x.</param>
        /// <param name="y">Toạ độ y.</param>
        public static void teleportMyChar(int x, int y)
        {
            Char.myCharz().currentMovePoint = null;
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();

            if (isUsingTDLT())
                return;

            Char.myCharz().cx = x;
            Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
        }


        
        public static void useCapsule()
        {
            var index = getIndexItemBag(193, 194);
            if (index == -1)
            {
                GameScr.info1.addInfo("Không tìm thấy capsule", 0);
                return;
            }

            Service.gI().useItem(0, 1, index, -1);
        }

        public static void usePorata()
        {
            var index = getIndexItemBag(921, 454);
            if (index == -1)
            {
                GameScr.info1.addInfo("Không tìm thấy bông tai", 0);
                return;
            }

            Service.gI().useItem(0, 1, index, -1);
        }


        /// <summary>
        /// next map left
        /// </summary>
        public static void changeMapLeft()
        {
            Waypoint waypoint = waypointLeft;
            if (waypoint != null)
            {
                teleportMyChar(getXWayPoint(waypoint), getYWayPoint(waypoint));
                requestChangeMap(waypoint);
            }
        }

        /// <summary>
        /// next map mid
        /// </summary>
        public static void changeMapMiddle()
        {
            Waypoint waypoint = waypointMiddle;
            if (waypoint != null)
            {
                teleportMyChar(getXWayPoint(waypoint), getYWayPoint(waypoint));
                requestChangeMap(waypoint);
            }
        }

        /// <summary>
        /// next map right
        /// </summary>
        public static void changeMapRight()
        {
            Waypoint waypoint = waypointRight;
            if (waypoint != null)
            {
                teleportMyChar(getXWayPoint(waypoint), getYWayPoint(waypoint));
                requestChangeMap(waypoint);
            }
        }

        /// <summary>
        /// gửi giao dich cho người bạn đang focus
        /// </summary>
        public static void sendGiaoDichToCharFocusing()
        {
            var charFocus = Char.myCharz().charFocus;
            if (charFocus == null)
            {
                GameScr.info1.addInfo("Trỏ vào nhân vật để giao dịch", 0);
                return;
            }

            Service.gI().giaodich(0, charFocus.charID, -1, -1);
            GameScr.info1.addInfo("Đã gửi lời mời giao dịch đến " + charFocus.cName, 0);
        }

        /// <summary>
        /// open ui khu
        /// </summary>
        public static void openUiZone()
        {
            Service.gI().openUIZone();
        }

        /// <summary>
        /// đổi khu dựa vào số khu
        /// </summary>
        /// <param name="zone"></param>
        public static void changeZone(int zone)
        {
            Service.gI().requestChangeZone(zone, -1);
        }


        public static bool isMeInNRDMap()
        {
            return TileMap.mapID >= 85 && TileMap.mapID <= 91;
        }

        public static void ResetTF()
        {
            ChatTextField.gI().strChat = "Chat";
            ChatTextField.gI().tfChat.name = "chat";
            ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
            ChatTextField.gI().isShow = false;
        }


        public static void saveRMSInt(string name, int value)
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Create);
            fileStream.Write(BitConverter.GetBytes(value), 0, 4);
            fileStream.Flush();
            fileStream.Close();
        }

        public static int loadRMSInt(string name)
        {
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Open);
            byte[] array = new byte[4];
            fileStream.Read(array, 0, 4);
            fileStream.Close();
            return BitConverter.ToInt32(array, 0);
        }

        public static void saveRMSBool(string name, bool status)
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Create);
            fileStream.Write(new byte[] { (byte)(status ? 1 : 0) }, 0, 1);
            fileStream.Flush();
            fileStream.Close();
        }

        public static bool loadRMSBool(string name)
        {
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Open);
            byte[] array = new byte[1];
            fileStream.Read(array, 0, 1);
            fileStream.Close();
            return array[0] == 1;
        }

        public static string loadRMSString(string name)
        {
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            string result = streamReader.ReadToEnd();
            streamReader.Close();
            fileStream.Close();
            return result;
        }

        public static void saveRMSString(string name, string data)
        {
            if (!Directory.Exists("Data")) Directory.CreateDirectory("Data");
            FileStream fileStream = new FileStream("Data\\" + name, FileMode.Create);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Flush();
            fileStream.Close();
        }



        /// <summary>
        /// Dịch chuyển đến đối tượng trong map
        /// </summary>
        /// <param name="obj">Đối tượng cần dịch chuyển tới</param>
        public static void teleportMyChar(IMapObject obj)
        {
            teleportMyChar(obj.getX(), obj.getY());
        }

        /// <summary>
        /// Dịch chuyển đến vị trí trên mặt đất có hoành độ x
        /// </summary>
        /// <param name="x">Hoành độ</param>
        public static void teleportMyChar(int x)
        {
            teleportMyChar(x, getYGround(x));
        }
        [Obsolete("Không dùng nữa")]

        internal static int getWidth(GUIStyle gUIStyle, string s)
        {
            return (int)gUIStyle.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel + 30;
        }

        /// <summary>
        /// Lấy tung độ mặt đất từ hoành độ
        /// </summary>
        /// <param name="x">Hoành độ x</param>
        /// <returns>Tung độ y thỏa mãn (x, y) là mặt đất</returns>
        public static int getYGround(int x)
        {
            int y = 50;
            for (int i = 0; i < 30; i++)
            {
                y += 24;
                if (TileMap.tileTypeAt(x, y, 2))
                {
                    if (y % 24 != 0) y -= y % 24;
                    return y;
                }
            }
            return -1;
        }

        public static int getDistance(IMapObject mapObject1, IMapObject mapObject2)
        {
            return Res.distance(mapObject1.getX(), mapObject1.getY(), mapObject2.getX(), mapObject2.getY());
        }

        //kinh cong
        public static void KhinhCong()
        {
            Char.myCharz().cy -= 50;
            Service.gI().charMove();
        }
        //don thổ
        public static void DonTho()
        {
            Char.myCharz().cy += 50;
            Service.gI().charMove();
        }
        //dịch sang trái
        public static void DichTrai()
        {
            Char.myCharz().cx -= 50;
            Service.gI().charMove();
        }
        //dịch sang phải
        public static void DichPhai()
        {
            Char.myCharz().cx += 50;
            Service.gI().charMove();
        }

        public static short getNRSDId()
        {
            if (isMeInNRDMap()) return (short)(2400 - TileMap.mapID);
            return 0;
        }

        public static bool isMeWearingActivationSet(int idSet)
        {
            int activateCount = 0;
            for (int i = 0; i < 5; i++)
            {
                Item item = Char.myCharz().arrItemBody[i];
                if (item == null) return false;
                if (item.itemOption == null) return false;
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id == idSet)
                    {
                        activateCount++;
                        break;
                    }
                }
            }
            return activateCount == 5;
        }

        public static bool isMeWearingTXHSet()
        {
            return Char.myCharz().cgender == 0 && isMeWearingActivationSet(141);
        }

        public static bool isMeWearingCadicSet()
        {
            return Char.myCharz().cgender == 2 && isMeWearingActivationSet(0);  //TODO: Tìm id set Cadic
        }

        public static bool isMeWearingPikkoroDaimaoSet()
        {
            return Char.myCharz().cgender == 1 && isMeWearingActivationSet(0);  //TODO: Tìm id set Pikkoro Daimao
        }


        public static void DoDoubleClickToObj(IMapObject mapObject)
        {
            typeof(GameScr).GetMethod("doDoubleClickToObj", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod).Invoke(GameScr.gI(), new object[] { mapObject });
        }
    }
}