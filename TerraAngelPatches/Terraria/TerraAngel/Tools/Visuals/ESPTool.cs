using ImGuiUtil = TerraAngel.Graphics.ImGuiUtil;

namespace TerraAngel.Tools.Visuals;

public class ESPTool : Tool
{
    public override string Name => "ESP Boxes";

    public override ToolTabs Tab => ToolTabs.ESPTools;

    public ref Color LocalPlayerColor => ref ClientConfig.Settings.LocalBoxPlayerColor;
    public ref Color OtherPlayerColor => ref ClientConfig.Settings.OtherBoxPlayerColor;
    public ref Color OtherTerraAngelUserColor => ref ClientConfig.Settings.OtherTerraAngelUserColor;
    public ref Color NPCColor => ref ClientConfig.Settings.NPCBoxColor;
    public ref Color NPCNetOffsetColor => ref ClientConfig.Settings.NPCNetOffsetBoxColor;
    public ref Color ProjectileColor => ref ClientConfig.Settings.ProjectileBoxColor;
    public ref Color ItemColor => ref ClientConfig.Settings.ItemBoxColor;
    public ref Color TracerColor => ref ClientConfig.Settings.TracerColor;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultDrawAnyESP))]
    public bool DrawAnyESP = true;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultMapESP))]
    public bool MapESP = true;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultPlayerESPBoxes))]
    public bool PlayerBoxes = false;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultPlayerESPTracers))]
    public bool PlayerTracers = false;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultNPCBoxes))]
    public bool NPCBoxes = false;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultProjectileBoxes))]
    public bool ProjectileBoxes = false;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultItemBoxes))]
    public bool ItemBoxes = false;

    [DefaultConfigValue(nameof(ClientConfig.Config.DefaultTileSections))]
    public bool ShowTileSections = false;

    public override void DrawUI(ImGuiIOPtr io)
    {
        ImGui.Checkbox("显示判定框", ref DrawAnyESP);
        if (DrawAnyESP)
        {
            ImGui.Checkbox("在地图上展示判定框", ref MapESP);
            ImGui.Checkbox("玩家判定框", ref PlayerBoxes);
            ImGui.Checkbox("NPC 判定框", ref NPCBoxes);
            ImGui.Checkbox("弹幕判定框", ref ProjectileBoxes);
            ImGui.Checkbox("物品判定框", ref ItemBoxes);
            ImGui.Checkbox("玩家追踪器", ref PlayerTracers);
            ImGui.Checkbox("区块显示", ref ShowTileSections);
            if (ImGui.CollapsingHeader("判定框颜色"))
            {
                ImGui.Indent();
                ImGuiUtil.ColorEdit4("自己", ref LocalPlayerColor);
                ImGuiUtil.ColorEdit4("别的玩家", ref OtherPlayerColor);
                ImGuiUtil.ColorEdit4("别的TerraAngel玩家", ref OtherTerraAngelUserColor);
                ImGuiUtil.ColorEdit4("追踪器颜色", ref TracerColor);
                ImGuiUtil.ColorEdit4("NPC", ref NPCColor);
                ImGuiUtil.ColorEdit4("NPC(多人联机网络延迟)", ref NPCNetOffsetColor);
                ImGuiUtil.ColorEdit4("弹幕", ref ProjectileColor);
                ImGuiUtil.ColorEdit4("物品", ref ItemColor);
                ImGui.Unindent();
            }
        }
    }

    public override void Update()
    {
        if (InputSystem.IsKeyPressed(ClientConfig.Settings.ToggleDrawAnyESP))
        {
            DrawAnyESP = !DrawAnyESP;
        }
    }
}
