﻿using Newtonsoft.Json;
using NLog;
using SD.Tools.Algorithmia.GeneralDataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Z2Randomizer.Core.Sidescroll;

//We want this to be statically initialized for performance in bulk seed generation, but C# doesn't allow static overrides
//I looked at other options and this was the least hacky. I would be shocked if there weren't a better way.
public class PalaceRooms
{
    private static Dictionary<string, List<Room>> roomsByGroup = new Dictionary<string, List<Room>>();
    private static Dictionary<string, List<Room>> customRoomsByGroup = new Dictionary<string, List<Room>>();

    public static readonly string roomsMD5 = "W/kxndGONzYjtM1xkW63Iw==";

    static PalaceRooms()
    {
        if (File.Exists("PalaceRooms.json"))
        {
            string roomsJson = File.ReadAllText("PalaceRooms.json");

            MD5 hasher = MD5.Create();
            byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(Regex.Replace(roomsJson, @"[\n\r\f]", "")));
            if (roomsMD5 != Convert.ToBase64String(hash))
            {
                throw new Exception("Invalid PalaceRooms.json");
            }
            dynamic rooms = JsonConvert.DeserializeObject(roomsJson);
            foreach (var obj in rooms)
            {
                Room room = new Room(obj.ToString());
                if (room.Enabled)
                {
                    if (!roomsByGroup.ContainsKey(room.Group))
                    {
                        roomsByGroup.Add(room.Group, new List<Room>());
                    }
                    roomsByGroup[room.Group].Add(room);
                }
            }
        }
        else
        {
            throw new Exception("Unable to find PalaceRooms.json. Consider reinstalling or contact Ellendar.");
        }

        if (File.Exists("CustomRooms.json"))
        {
            string roomsJson = File.ReadAllText("CustomRooms.json");
            dynamic rooms = JsonConvert.DeserializeObject(roomsJson);
            foreach (var obj in rooms)
            {
                Room room = new Room(obj.ToString());
                if (room.Enabled)
                {
                    if (!customRoomsByGroup.ContainsKey(room.Group))
                    {
                        customRoomsByGroup.Add(room.Group, new List<Room>());
                    }
                    customRoomsByGroup[room.Group].Add(room);
                }
            }
        }
    }

    public static List<Room> PalaceRoomsByNumber(int palaceNum, bool customRooms) => palaceNum switch
    {
        1 => customRooms ? customRoomsByGroup["palace1vanilla"] : roomsByGroup["palace1vanilla"],
        2 => customRooms ? customRoomsByGroup["palace2vanilla"] : roomsByGroup["palace2vanilla"],
        3 => customRooms ? customRoomsByGroup["palace3vanilla"] : roomsByGroup["palace3vanilla"],
        4 => customRooms ? customRoomsByGroup["palace4vanilla"] : roomsByGroup["palace4vanilla"],
        5 => customRooms ? customRoomsByGroup["palace5vanilla"] : roomsByGroup["palace5vanilla"],
        6 => customRooms ? customRoomsByGroup["palace6vanilla"] : roomsByGroup["palace6vanilla"],
        7 => customRooms ? customRoomsByGroup["palace7vanilla"] : roomsByGroup["palace7vanilla"],
        _ => throw new ArgumentException("Invalid palace number: " + palaceNum)
    };

    public static Room Thunderbird(bool useCustomRooms)
    { 
        return useCustomRooms ? customRoomsByGroup["thunderbird"].First() : roomsByGroup["thunderbird"].First();
    }

    public static List<Room> TBirdRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["tbirdRooms"]: roomsByGroup["tbirdRooms"];
    }
    public static List<Room> BossRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["bossrooms"] : roomsByGroup["bossrooms"];
    }
    public static List<Room> ItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["itemRooms"] : roomsByGroup["itemRooms"];
    }
    public static List<Room> Entrances(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["entrances"] : roomsByGroup["entrances"];
    }
    public static Room MaxBonusItemRoom(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["maxBonusItemRoom"].First() : roomsByGroup["maxBonusItemRoom"].First();
    }
    public static List<Room> AaronRoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["aaronRoomJam"] : roomsByGroup["aaronRoomJam"];
    }
    public static List<Room> BenthicKing(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["benthicKing"] : roomsByGroup["benthicKing"];
    }
    public static List<Room> DMInPalaces(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["dmInPalaces"] : roomsByGroup["dmInPalaces"];
    }
    public static List<Room> DusterRoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["dusterRoomJam"] : roomsByGroup["dusterRoomJam"];
    }
    public static List<Room> EasternShadow(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["easternShadow"] : roomsByGroup["easternShadow"];
    }
    public static List<Room> EonRoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["eonRoomjam"] : roomsByGroup["eonRoomjam"];
    }
    public static List<Room> EunosGpRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["eunosGpRooms"] : roomsByGroup["eunosGpRooms"];
    }
    public static List<Room> EunosRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["eunosRooms"] : roomsByGroup["eunosRooms"];
    }
    public static List<Room> FlippedGP(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["flippedGp"] : roomsByGroup["flippedGp"];
    }
    public static List<Room> GTMNewGpRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["gtmNewgpRooms"] : roomsByGroup["gtmNewgpRooms"];
    }
    public static List<Room> KnightcrawlerRoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["knightCrawlerRoomJam"] : roomsByGroup["knightCrawlerRoomJam"];
    }
    public static List<Room> Link7777RoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["link7777RoomJam"] : roomsByGroup["link7777RoomJam"];
    }
    public static List<Room> MaxRoomJam(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["maxRoomJam"] : roomsByGroup["maxRoomJam"];
    }
    public static List<Room> Palace1Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace1vanilla"] : roomsByGroup["palace1vanilla"];
    }
    public static List<Room> Palace2Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace2vanilla"] : roomsByGroup["palace2vanilla"];
    }
    public static List<Room> Palace3Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace3vanilla"] : roomsByGroup["palace3vanilla"];
    }
    public static List<Room> Palace4Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace4vanilla"] : roomsByGroup["palace4vanilla"];
    }
    public static List<Room> Palace5Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace5vanilla"] : roomsByGroup["palace5vanilla"];
    }
    public static List<Room> Palace6Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace6vanilla"] : roomsByGroup["palace6vanilla"];
    }
    public static List<Room> Palace7Vanilla(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["palace7vanilla"] : roomsByGroup["palace7vanilla"];
    }
    public static List<Room> RoomJamGTM(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["roomJamGTM"] : roomsByGroup["roomJamGTM"];
    }
    public static List<Room> TriforceOfCourage(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["triforceOfCourage"] : roomsByGroup["triforceOfCourage"];
    }
    public static List<Room> TriforceOfCourageGP(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["triforceOfCourageGP"] : roomsByGroup["triforceOfCourageGP"];
    }
    public static List<Room> WinterSolstice(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["winterSolstice"] : roomsByGroup["winterSolstice"];
    }
    public static List<Room> WinterSolsticeGP(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["winterSolsticeGP"] : roomsByGroup["winterSolsticeGP"];
    }
    public static List<Room> GTMOldGPRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["gtmOldgpRooms"] : roomsByGroup["gtmOldgpRooms"];
    }
    public static List<Room> NewP6BossRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["newp6BossRooms"] : roomsByGroup["newp6BossRooms"];
    }
    public static List<Room> NewBossRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["newbossrooms"] : roomsByGroup["newbossrooms"];
    }
    public static List<Room> LeftOpenItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["leftOpenItemRooms"] : roomsByGroup["leftOpenItemRooms"];
    }
    public static List<Room> RightOpenItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["rightOpenItemRooms"] : roomsByGroup["rightOpenItemRooms"];
    }
    public static List<Room> UpOpenItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["upOpenItemRooms"] : roomsByGroup["upOpenItemRooms"];
    }
    public static List<Room> DownOpenItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["downOpenItemRooms"] : roomsByGroup["downOpenItemRooms"];
    }
    public static List<Room> ThroughItemRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["throughItemRooms"] : roomsByGroup["throughItemRooms"];
    }
    public static List<Room> DarkLinkRooms(bool useCustomRooms)
    {
        return useCustomRooms ? customRoomsByGroup["darkLinkRooms"] : roomsByGroup["darkLinkRooms"];
    }
}
