namespace TerraAngel.Tools.Automation;

public class ButcherTool : Tool
{
    public override string Name => "清怪";

    public override ToolTabs Tab => ToolTabs.NewTab;

    public int ButcherDamage = 1000;

    public bool AutoButcherHostiles = false;

    public override void DrawUI(ImGuiIOPtr io)
    {
        if (ImGui.Button("杀死所有敌怪"))
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
        ImGui.Checkbox("Auto-Butcher Hostiles", ref AutoButcherHostiles);
        if (ImGui.Button("杀死所有NPC"))
        {
            Butcher.ButcherAllFriendlyNPCs(ButcherDamage);
        }
        if (ImGui.Button("杀死所有玩家"))
        {
            Butcher.ButcherAllPlayers(ButcherDamage);
        }
        ImGui.SliderInt("使用伤害", ref ButcherDamage, 1, (int)short.MaxValue);
    }

    public override void Update()
    {
        if (AutoButcherHostiles)
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
    }
}
