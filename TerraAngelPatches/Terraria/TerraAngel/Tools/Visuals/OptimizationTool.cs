﻿namespace TerraAngel.Tools.Visuals;

public class OptimizationTool : Tool
{
    public static OptimizationTool? OptimizationToolCache { get; private set; }

    public override string Name => "Optimization Cringe";

    public override ToolTabs Tab => ToolTabs.VisualTools;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultDisableDust))]
    public bool DisableDust;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultDisableGore))]
    public bool DisableGore;

    public override void DrawUI(ImGuiIOPtr io)
    {
        ImGui.Checkbox("禁用沙漠特效", ref DisableDust);
        ImGui.Checkbox("禁用血腥效果", ref DisableGore);
    }

    public override void Update()
    {
        OptimizationToolCache = this;
        Dust.DustIntersectionRectangle = new Rectangle((int)Main.screenPosition.X - 50, (int)Main.screenPosition.Y - 50, Main.screenWidth + 50, Main.screenHeight + 50);
    }
}
