﻿using System;
using System.Reflection;
using ReLogic.Threading;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.UI;

namespace TerraAngel.Hooks;

public class DrawHooks
{
    public static string AffixNameHook(Func<Item, string> orig, Item self)
    {
        if (ClientConfig.Settings.ShowDetailedTooltips)
        {
            if (self.prefix < 0 || self.prefix >= Lang.prefix.Length)
            {
                return self.Name + $" [{Util.ItemFields[self.type].Name}/{self.type}]";
            }

            string text = Lang.prefix[self.prefix].Value;
            if (text == "")
            {
                return self.Name + $" [{Util.ItemFields[self.type].Name}/{self.type}]";
            }

            if (text.StartsWith("("))
            {
                return text + $" [{Util.PrefixFields[self.prefix].Name}/{self.prefix}] " + self.Name + $" [{Util.ItemFields[self.type].Name}/{self.type}]";
            }

            return text + $" [{Util.PrefixFields[self.prefix].Name}/{self.prefix}] " + self.Name + $" [{Util.ItemFields[self.type].Name}/{self.type}]";
        }

        return orig(self);
    }

    public delegate void GetLinesInfoDef(Item item, ref int yoyoLogo, ref int researchLine, float oldKB, ref int numLines, string[] toolTipLine, bool[] preFixLine, bool[] badPreFixLine);
    public static void GetLinesInfoHook(GetLinesInfoDef orig, Item item, ref int yoyoLogo, ref int researchLine, float oldKB, ref int numLines, string[] toolTipLine, bool[] preFixLine, bool[] badPreFixLine)
    {
        orig(item, ref yoyoLogo, ref researchLine, oldKB, ref numLines, toolTipLine, preFixLine, badPreFixLine);
        /// Keep this as is, never change this. 
        /// "please" - An anonymous user
        if (ClientConfig.Settings.ShowDetailedTooltips)
        {
            if (item.useAmmo > 0)
            {
                int projectileType = item.shoot;
                float shootSpeed = item.shootSpeed;
                bool cs = true;
                int dm = 0;
                float kb = 0.0f;
                Main.LocalPlayer.PickAmmo(item, ref projectileType, ref shootSpeed, ref cs, ref dm, ref kb, out _, true);

                if (Util.ProjectileFields.ContainsKey(projectileType))
                {
                    toolTipLine[numLines] = $"[a:[Projectile: {Util.ProjectileFields[projectileType].Name}/{projectileType}]]";
                    numLines++;
                }
                else
                {
                    if (item.shoot > 0 && item.shoot < ProjectileID.Count && Util.ProjectileFields.ContainsKey(item.shoot))
                    {
                        toolTipLine[numLines] = $"[a:[Projectile: {Util.ProjectileFields[item.shoot].Name}/{item.shoot}]]";
                        numLines++;
                    }
                }
            }
            else
            {
                if (item.shoot > 0 && item.shoot < ProjectileID.Count && Util.ProjectileFields.ContainsKey(item.shoot) && (item.ammo == AmmoID.None || !Util.AmmoFields.ContainsKey(item.ammo)))
                {
                    toolTipLine[numLines] = $"[a:[Projectile: {Util.ProjectileFields[item.shoot].Name}/{item.shoot}]]";
                    numLines++;
                }
            }

            if (item.createTile > -1 && item.createTile < TileID.Count && Util.TileFields.ContainsKey(item.createTile))
            {
                toolTipLine[numLines] = $"[a:[Tile: {Util.TileFields[item.createTile].Name}/{item.createTile}{(item.placeStyle > 0 ? $" {item.placeStyle}" : "")}]]";
                numLines++;
            }

            if (item.createWall > 0 && item.createWall < WallID.Count && Util.WallFields.ContainsKey(item.createWall))
            {
                toolTipLine[numLines] = $"[a:[Wall: {Util.WallFields[item.createWall].Name}/{item.createWall}]]";
                numLines++;
            }

            if (item.ammo > 0 && item.ammo < AmmoID.NailFriendly + 1 && Util.AmmoFields.ContainsKey(item.ammo))
            {
                toolTipLine[numLines] = $"[a:[Ammo: {Util.AmmoFields[item.ammo].Name}/{item.ammo}]]";
                numLines++;
            }

            if (item.shootSpeed > 0f)
            {
                toolTipLine[numLines] = $"[a:[Shoot Speed: {item.shootSpeed:F1}]]";
                numLines++;
            }

            if (item.buffType > 0 && item.buffType < BuffID.Count)
            {
                if (item.buffTime > 0)
                {
                    toolTipLine[numLines] = $"[a:[Buff: {Util.BuffFields[item.buffType].Name}/{item.buffType} for {TimeSpan.FromSeconds(item.buffTime / 60f):mm\\:ss}]]";
                }
                else
                {
                    toolTipLine[numLines] = $"[a:[Buff: {Util.BuffFields[item.buffType].Name}/{item.buffType}]]";
                }
                numLines++;
            }
        }
    }
    
    private static Vector2 freecamOriginPoint;
    public static int SpectateOverride = -1;
    public static void UpdateCameraHook(Action orig)
    {
        if (!Main.gameMenu)
        {
            FreecamCringe freecam = CringeManager.GetCringe<FreecamCringe>();
            if (freecam.Enabled)
            {
                ImGuiIOPtr io = ImGui.GetIO();
                if (io.MouseClicked[1])
                {
                    freecamOriginPoint = Util.ScreenToWorldWorld(InputSystem.MousePosition);
                }
                if (io.MouseDown[1])
                {
                    Vector2 diff = freecamOriginPoint - Util.ScreenToWorldWorld(InputSystem.MousePosition);
                    Main.screenPosition = Main.screenPosition + diff;
                }

                Main.floatingCameraY = Main.screenPosition.Y;
                return;
            }
        }

        if (Main.gameMenu || Main.LocalPlayer.controlUp ||
            Main.LocalPlayer.controlLeft ||
            Main.LocalPlayer.controlDown ||
            Main.LocalPlayer.controlRight ||
            Main.LocalPlayer.controlJump ||
            Main.LocalPlayer.controlUseTile ||
            Main.LocalPlayer.controlThrow ||
            Main.LocalPlayer.controlHook ||
            Main.LocalPlayer.controlMount)
            SpectateOverride = -1;

        int temp = Main.myPlayer;
        if (!Main.gameMenu)
        {
            if (SpectateOverride > -1)
                Main.myPlayer = SpectateOverride;
        }
        orig();
        if (!Main.gameMenu)
        {
            Main.myPlayer = temp;
        }
    }

    public static LightingModifierCringe? LightModificationCache;

    public static OptimizationCringe? OptimizationCache;
}
