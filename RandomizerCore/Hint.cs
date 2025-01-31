﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Z2Randomizer.Core.Overworld;

namespace Z2Randomizer.Core;

public class Hint
{
    public string RawText { get; }
    public List<char> Text { get; private set; }

    public Hint()
    {
        Text = Util.ToGameText("I know$nothing", true);
    }

    public Hint(List<char> text)
    {
        RawText = new string(text.ToArray());
        Text = text;
    }
    public Hint(string text)
    {
        RawText = text;
        Text = Util.ToGameText(text, true);
    }

    public void GenerateHelpfulHint(Location location)
    {
        Item hintItem = location.Item;
        string hint = "";
        if (location.PalaceNumber == 1)
        {
            hint += "horsehead$neighs$with the$";
        }
        else if (location.PalaceNumber == 2)
        {
            hint += "helmethead$guards the$";
        }
        else if (location.PalaceNumber == 3)
        {
            hint += "rebonack$rides$with the$";
        }
        else if (location.PalaceNumber == 4)
        {
            hint += "carock$disappears$with the$";
        }
        else if (location.PalaceNumber == 5)
        {
            hint += "gooma sits$on the$";
        }
        else if (location.PalaceNumber == 6)
        {
            hint += "barba$slithers$with the$";
        }
        else if (location.Continent == Continent.EAST)
        {
            hint += "go east to$find the$";
        }
        else if (location.Continent == Continent.WEST)
        {
            hint += "go west to$find the$";
        }
        else if (location.Continent == Continent.DM)
        {
            hint += "death$mountain$holds the$";
        }
        else
        {
            hint += "in a maze$lies the$";
        }

        switch (hintItem)
        {
            case (Item.BLUE_JAR):
                hint += "blue jar";
                break;
            case (Item.BOOTS):
                hint += "boots";
                break;
            case (Item.CANDLE):
                hint += "candle";
                break;
            case (Item.CROSS):
                hint += "cross";
                break;
            case (Item.XL_BAG):
            case (Item.LARGE_BAG):
            case (Item.MEDIUM_BAG):
            case (Item.SMALL_BAG):
                hint += "pbag";
                break;
            case (Item.GLOVE):
                hint += "glove";
                break;
            case (Item.HAMMER):
                hint += "hammer";
                break;
            case (Item.HEART_CONTAINER):
                hint += "heart";
                break;
            case (Item.FLUTE):
                hint += "flute";
                break;
            case (Item.KEY):
                hint += "small key";
                break;
            case (Item.CHILD):
                hint += "child";
                break;
            case (Item.MAGIC_CONTAINER):
                hint += "magic jar";
                break;
            case (Item.MAGIC_KEY):
                hint += "magic key";
                break;
            case (Item.MEDICINE):
                hint += "medicine";
                break;
            case (Item.ONEUP):
                hint += "link doll";
                break;
            case (Item.RAFT):
                hint += "raft";
                break;
            case (Item.RED_JAR):
                hint += "red jar";
                break;
            case (Item.TROPHY):
                hint += "trophy";
                break;
        }

        Text = Util.ToGameText(hint, true).ToList();
    }

}
