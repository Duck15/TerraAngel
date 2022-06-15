﻿using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using MonoMod.RuntimeDetour;
using Terraria;
using TerraAngel.Loader;
using ImGuiNET;
using ReLogic.OS;
using Terraria.DataStructures;
using Terraria.ID;
using TerraAngel.Cheat;
using TerraAngel.Hooks;
using Microsoft.Xna.Framework.Input;
using TerraAngel.Net;
using TerraAngel.Client.Config;

namespace TerraAngel.Hooks.Hooks
{
    public class PlayerHooks
    {
        public static void Generate()
        {
            HookUtil.HookGen<Player>("Hurt", PlayerHurtHook);
            HookUtil.HookGen<Player>("KillMe", PlayerKillHook);
            HookUtil.HookGen<Player>("ResetEffects", PlayerResetEffectsHook);
        }

        public static double PlayerHurtHook(Func<Player, PlayerDeathReason, int, int, bool, bool, bool, int, double> orig, Player self, PlayerDeathReason damageSource, int Damage, int hitDirection, bool pvp, bool quiet, bool Crit, int cooldownCounter)
        {
            if (self.whoAmI == Main.myPlayer && GlobalCheatManager.AntiHurt)
            {
                if (Main.GameUpdateCount % 6 == 0)
                {
                    NetMessage.SendData(MessageID.PlayerLifeMana, -1, -1, null, self.whoAmI);
                }
                return 0.0d;
            }

            return orig(self, damageSource, Damage, hitDirection, pvp, quiet, Crit, cooldownCounter);
        }
        public static void PlayerKillHook(Action<Player, PlayerDeathReason, double, int, bool> orig, Player self, PlayerDeathReason damageSource, double dmg, int hitDirection, bool pvp)
        {
            if (self.whoAmI == Main.myPlayer && GlobalCheatManager.AntiHurt)
            {
                NetMessage.SendData(MessageID.PlayerLifeMana, -1, -1, null, self.whoAmI);
                return;
            }
            orig(self, damageSource, dmg, hitDirection, pvp);
        }
        public static void PlayerResetEffectsHook(Action<Player> orig, Player self)
        {
            orig(self);

            if (self.whoAmI == Main.myPlayer)
            {
                if (GlobalCheatManager.AntiHurt)
                {
                    self.statLife = self.statLifeMax2;
                }

                if (GlobalCheatManager.InfiniteMinions)
                {
                    self.maxMinions = int.MaxValue - 100000;
                }

                if (GlobalCheatManager.InfiniteMana)
                {
                    self.statMana = self.statLifeMax2;
                    self.manaCost = 0.0f;
                }

                if (Input.InputSystem.IsKeyPressed(ClientConfig.Instance.ToggleNoclip))
                {
                    GlobalCheatManager.NoClip = !GlobalCheatManager.NoClip;
                }

                if (GlobalCheatManager.NoClip)
                {
                    ImGuiIOPtr io = ImGui.GetIO();
                    if (io.KeysDown[(int)Keys.W])
                    {
                        self.position.Y -= GlobalCheatManager.NoClipSpeed;
                    }
                    if (io.KeysDown[(int)Keys.S])
                    {
                        self.position.Y += GlobalCheatManager.NoClipSpeed;
                    }
                    if (io.KeysDown[(int)Keys.A])
                    {
                        self.position.X -= GlobalCheatManager.NoClipSpeed;
                    }
                    if (io.KeysDown[(int)Keys.D])
                    {
                        self.position.X += GlobalCheatManager.NoClipSpeed;
                    }

                    if (Main.GameUpdateCount % GlobalCheatManager.NoClipPlayerSyncTime == 0)
                    {
                        //NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, self.whoAmI);
                        SpecialNetMessage.SendData(MessageID.PlayerControls, null, self.whoAmI, self.position.X, self.position.Y, (float)self.selectedItem);
                    }
                }
            }
        }
    }
}