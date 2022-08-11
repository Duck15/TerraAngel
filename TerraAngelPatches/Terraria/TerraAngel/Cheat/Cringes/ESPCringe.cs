﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using Microsoft.Xna.Framework;
using TerraAngel.Graphics;

namespace TerraAngel.Cheat.Cringes
{
    public class ESPCringe : Cringe
    {
        public override string Name => "ESP Boxes";
        public override CringeTabs Tab => CringeTabs.VisualUtility;

        public ref Color LocalPlayerColor => ref ClientLoader.Config.LocalBoxPlayerColor;
        public ref Color OtherPlayerColor => ref ClientLoader.Config.OtherBoxPlayerColor;
        public ref Color OtherTerraAngelUserColor => ref ClientLoader.Config.OtherTerraAngelUserColor;
        public ref Color NPCColor => ref ClientLoader.Config.NPCBoxColor;
        public ref Color NPCNetOffsetColor => ref ClientLoader.Config.NPCNetOffsetBoxColor;
        public ref Color ProjectileColor => ref ClientLoader.Config.ProjectileBoxColor;
        public ref Color ItemColor => ref ClientLoader.Config.ItemBoxColor;
        public ref Color TracerColor => ref ClientLoader.Config.TracerColor;

        public bool DrawAnyESP = true;
        public bool PlayerBoxes = false;
        public bool PlayerTracers = false;
        public bool NPCBoxes = false;
        public bool ProjectileBoxes = false;
        public bool ItemBoxes = false;

        public override void DrawUI(ImGuiIOPtr io)
        {
            ImGui.Checkbox("Draw Any ESP", ref DrawAnyESP);
            if (DrawAnyESP)
            {
                ImGui.Checkbox("Player hitboxes", ref PlayerBoxes);
                ImGui.Checkbox("Player tracers", ref PlayerTracers);
                ImGui.Checkbox("NPC hitboxes", ref NPCBoxes);
                ImGui.Checkbox("Projectile hitboxes", ref ProjectileBoxes);
                ImGui.Checkbox("Item hitboxes", ref ItemBoxes);
                if (ImGui.CollapsingHeader("ESP settings"))
                {
                    ImGuiUtil.ColorEdit4("Local player box color", ref LocalPlayerColor);
                    ImGuiUtil.ColorEdit4("Other player box color", ref OtherPlayerColor);
                    ImGuiUtil.ColorEdit4("Other TerraAngel user box color", ref OtherTerraAngelUserColor);
                    ImGuiUtil.ColorEdit4("Player Tracer color", ref TracerColor);
                    ImGuiUtil.ColorEdit4("NPC box color", ref NPCColor);
                    ImGuiUtil.ColorEdit4("NPC net box color", ref NPCNetOffsetColor);
                    ImGuiUtil.ColorEdit4("Projectile box color", ref ProjectileColor);
                    ImGuiUtil.ColorEdit4("Item box color", ref ItemColor);
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.InputSystem.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.End))
                DrawAnyESP = !DrawAnyESP;
        }
    }
}