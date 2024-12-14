namespace TerraAngel.Tools.Automation;

public class ButcherTool : Tool
{
    public override string Name => "清怪";

    public override ToolTabs Tab => ToolTabs.NewTab;

    public int ButcherDamage = 1000;

    public bool AutoButcherHostiles = false;

    public override void DrawUI(ImGuiIOPtr io)
    {
        if (ImGui.Button("一键清怪"))
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
        ImGui.Checkbox("自动清怪", ref AutoButcherHostiles);
        if (ImGui.Button("屠村"))
        {
            Butcher.ButcherAllFriendlyNPCs(ButcherDamage);
        }
        if (ImGui.Button("让所有玩家飞起来!"))
        {
            Butcher.ButcherAllPlayers(ButcherDamage);
        }
        ImGui.SliderInt("伤害", ref ButcherDamage, 1, (int)short.MaxValue);
    }

    public override void Update()
    {
        if (AutoButcherHostiles)
        {
            Butcher.ButcherAllHostileNPCs(ButcherDamage);
        }
    }
}
