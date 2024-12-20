﻿namespace TerraAngel.Tools.Developer;

public class InfiniteManaTool : Tool
{
    public override string Name => "无限魔力";

    public override ToolTabs Tab => ToolTabs.MainTools;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultInfiniteMana))]
    public bool Enabled;

    public override void DrawUI(ImGuiIOPtr io)
    {
        ImGui.Checkbox(Name, ref Enabled);
    }
}