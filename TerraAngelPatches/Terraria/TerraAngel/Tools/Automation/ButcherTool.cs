﻿namespace TerraAngel.Tools.Automation;

public class ButcherTool : Tool
{
    public override string Name => "Butcher";

    public override ToolTabs Tab => ToolTabs.NewTab;

    public int ButcherDamage = 1000;

    public bool AutoButcherHostiles = false;

    public override void DrawUI(ImGuiIOPtr io)
    {
        if (ImGui.Button("Butcher All Hostile NPCs"))
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
        ImGui.Checkbox("Auto-Butcher Hostiles", ref AutoButcherHostiles);
        if (ImGui.Button("Butcher All Friendly NPCs"))
        {
            Butcher.ButcherAllFriendlyNPCs(ButcherDamage);
        }
        if (ImGui.Button("Butcher All Players"))
        {
            Butcher.ButcherAllPlayers(ButcherDamage);
        }
        ImGui.SliderInt("Butcher Damage", ref ButcherDamage, 1, (int)short.MaxValue);
    }

    public override void Update()
    {
        if (AutoButcherHostiles)
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
    }
}
