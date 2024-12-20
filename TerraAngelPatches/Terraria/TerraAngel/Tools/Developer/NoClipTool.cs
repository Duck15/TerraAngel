﻿using Microsoft.Xna.Framework.Input;

namespace TerraAngel.Tools.Developer;

public class NoClipTool : Tool
{
    public override string Name => "无视重力穿墙";

    public override ToolTabs Tab => ToolTabs.MainTools;

    public float NoClipSpeed = 20.8f;
    public int NoClipPlayerSyncTime = 1;

    public bool Enabled;

    public override void DrawUI(ImGuiIOPtr io)
    {
        ImGui.Checkbox(Name, ref Enabled);
        if (Enabled)
        {
            if (ImGui.CollapsingHeader("穿墙设置"))
            {
                ImGui.Indent();
                ImGui.TextUnformatted("速度"); ImGui.SameLine();
                ImGui.SliderFloat("##Speed", ref NoClipSpeed, 1f, 100f);

                ImGui.TextUnformatted("帧同步时间"); ImGui.SameLine();
                ImGui.SliderInt("##SyncTime", ref NoClipPlayerSyncTime, 1, 60);
                ImGui.Unindent();
            }
        }
    }

    public override void Update()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        if (!Main.mapFullscreen)
        {
            Player self = Main.LocalPlayer;

            if (InputSystem.IsKeyPressed(ClientConfig.Settings.ToggleNoclip))
            {
                Enabled = !Enabled;
            }

            if (!io.WantCaptureKeyboard && !io.WantTextInput && !Main.drawingPlayerChat)
            {
                if (Enabled)
                {
                    self.oldPosition = self.position;
                    if (ImGui.IsKeyDown(ImGuiKey.W))
                    {
                        self.position.Y -= NoClipSpeed;
                    }
                    if (ImGui.IsKeyDown(ImGuiKey.S))
                    {
                        self.position.Y += NoClipSpeed;
                    }
                    if (ImGui.IsKeyDown(ImGuiKey.A))
                    {
                        self.position.X -= NoClipSpeed;
                    }
                    if (ImGui.IsKeyDown(ImGuiKey.D))
                    {
                        self.position.X += NoClipSpeed;
                    }
                }
            }
        }
    }
}
