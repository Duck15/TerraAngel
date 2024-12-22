﻿using System;
using System.Collections.Generic;
using TerraAngel.ID;
using TerraAngel.Net;

namespace TerraAngel.WorldEdits;

public class WorldEditBrush : WorldEdit
{
    public enum TileEditActions
    {
        None,
        Break,
        Place,
        Replace,
    }

    private static string[] TileEditActionsNames =
    [
        GetString("None"),
        GetString("Break"),
        GetString("Place"),
        GetString("Replace")
    ];
    
    public enum WallEditActions
    {
        None,
        Break,
        Place,
        Replace,
    }


    private static string[] WallEditActionsNames =
    [
        GetString("None"),
        GetString("Break"),
        GetString("Place"),
        GetString("Replace")
    ];
    public enum LiquidEditActions
    {
        None,
        Remove,
        Water,
        Lava,
        Honey,
        Shimmer,
    }

    private static string[] LiquidEditActionsNames =
    [
        GetString("None"),
        GetString("Remove"),
        GetString("Water"),
        GetString("Lava"),
        GetString("Honey"),
        GetString("Shimmer")
    ];
    

    private bool sqaureFrame = true;
    private bool drawDetailedPreview = true;
    private int brushDiameter = 80;
    private bool teleportToTilesFarAway = true;
    private bool revealMap = true;

    public override bool RunEveryFrame => true;

    public override void DrawPreviewInMap(ImGuiIOPtr io, ImDrawListPtr drawList)
    {
        Vector2 mousePos = Util.ScreenToWorldFullscreenMap(InputSystem.MousePosition);

        if (!drawDetailedPreview)
        {
            Vector2 screenCoords = InputSystem.MousePosition;
            Vector2 screenCoords2 = Util.WorldToScreenFullscreenMap((mousePos + new Vector2(brushDiameter + 16f, 0f)));
            float dist = screenCoords.Distance(screenCoords2);
            drawList.AddCircleFilled(screenCoords, dist, ImGui.GetColorU32(new Vector4(1f, 0f, 0f, 0.5f)));
            return;
        }

        float bd = MathF.Floor(brushDiameter / 16f);
        Vector2 mouseTileCoords = new Vector2(MathF.Floor(mousePos.X / 16f), MathF.Floor(mousePos.Y / 16f));
        for (float x = mouseTileCoords.X - bd; x < mouseTileCoords.X + bd; x++)
        {
            for (float y = mouseTileCoords.Y - bd; y < mouseTileCoords.Y + bd; y++)
            {
                Vector2 tileCoords = new Vector2(x, y);
                if (x < 0 || x > Main.maxTilesX ||
                    y < 0 || y > Main.maxTilesY)
                    continue;

                if (tileCoords.Distance(mouseTileCoords) < bd)
                {
                    Vector2 worldCoords = tileCoords.ToPoint().ToWorldCoordinates(0, 0);
                    Vector2 worldCoords2 = (tileCoords + Vector2.One).ToPoint().ToWorldCoordinates(0, 0);
                    drawList.AddRectFilled(Util.WorldToScreenFullscreenMap(worldCoords), Util.WorldToScreenFullscreenMap(worldCoords2), ImGui.GetColorU32(new Vector4(1f, 0f, 0f, 0.5f)));
                }

            }
        }
    }
    public override void DrawPreviewInWorld(ImGuiIOPtr io, ImDrawListPtr drawList)
    {
        Vector2 mouseWorld = Util.ScreenToWorldWorld(InputSystem.MousePosition);

        if (!drawDetailedPreview)
        {
            Vector2 screenCoords = InputSystem.MousePosition;
            Vector2 screenCoords2 = Util.WorldToScreenWorld((mouseWorld + new Vector2(brushDiameter + 16f, 0f)));
            float dist = screenCoords.Distance(screenCoords2);
            drawList.AddCircleFilled(screenCoords, dist, ImGui.GetColorU32(new Vector4(1f, 0f, 0f, 0.5f)));
            return;
        }

        float bd = MathF.Floor(brushDiameter / 16f);
        Vector2 mouseTileCoords = new Vector2(MathF.Floor(mouseWorld.X / 16f), MathF.Floor(mouseWorld.Y / 16f));
        for (float x = mouseTileCoords.X - bd; x < mouseTileCoords.X + bd; x++)
        {
            for (float y = mouseTileCoords.Y - bd; y < mouseTileCoords.Y + bd; y++)
            {
                Vector2 tileCoords = new Vector2(x, y);
                if (x < 0 || x > Main.maxTilesX ||
                    y < 0 || y > Main.maxTilesY)
                    continue;

                if ((tileCoords).Distance(mouseTileCoords) < bd)
                {
                    Vector2 worldCoords = tileCoords.ToPoint().ToWorldCoordinates(0, 0);
                    Vector2 worldCoords2 = (tileCoords + Vector2.One).ToPoint().ToWorldCoordinates(0, 0);
                    drawList.AddRectFilled(Util.WorldToScreenWorld(worldCoords), Util.WorldToScreenWorld(worldCoords2), ImGui.GetColorU32(new Vector4(1f, 0f, 0f, 0.5f)));
                }

            }
        }
    }

    private int currentTileAction = 0;
    private int currentWallAction = 0;
    private int currentLiquidAction = 0;

    public override bool DrawUITab(ImGuiIOPtr io)
    {
        if (ImGui.BeginTabItem(GetString("Brush")))
        {
            ImGui.Checkbox(GetString("Draw detailed preview"), ref drawDetailedPreview);
            ImGui.Checkbox(GetString("Square tile frame"), ref sqaureFrame);
            ImGui.Checkbox(GetString("Attempt to bypass TShock"), ref teleportToTilesFarAway);
            ImGui.Checkbox(GetString("Reveal Map"), ref revealMap);
            if (ImGui.SliderInt(GetString("Brush Diameter"), ref brushDiameter, 16, 800))
            {
                if (brushDiameter > 600)
                    drawDetailedPreview = false;
                else
                    drawDetailedPreview = true;
            }
            ImGui.Text(GetString("Tile")); ImGui.SameLine();
            ImGui.Combo("##WorldEditTileActions", ref currentTileAction, TileEditActionsNames, TileEditActionsNames.Length);
            ImGui.Text(GetString("Wall")); ImGui.SameLine();
            ImGui.Combo("##WorldEditWallActions", ref currentWallAction, WallEditActionsNames, WallEditActionsNames.Length);
            ImGui.Text(GetString("Liquid")); ImGui.SameLine();
            ImGui.Combo("##WorldEditLiquidActions", ref currentLiquidAction, LiquidEditActionsNames, LiquidEditActionsNames.Length);


            ImGui.EndTabItem();
            return true;
        }
        return false;
    }

    private int currentPlayerCreateTile;
    private int currentPlayerCreateStyle;
    private int currentPlayerCreateWall;
    private bool needsResetPlayerPosition = false;

    public override void Edit(Vector2 mouseTileCoords)
    {
        lastTeleportPosition = Main.LocalPlayer.position;
        currentPlayerCreateTile = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].createTile;
        currentPlayerCreateStyle = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].placeStyle;
        currentPlayerCreateWall = Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].createWall;
        needsResetPlayerPosition = false;

        float bd = MathF.Floor(brushDiameter / 16f);
        for (float x = mouseTileCoords.X - bd; x < mouseTileCoords.X + bd; x++)
        {
            for (float y = mouseTileCoords.Y - bd; y < mouseTileCoords.Y + bd; y++)
            {
                Vector2 tileCoords = new Vector2(x, y);
                if (x < 0 || x > Main.maxTilesX ||
                    y < 0 || y > Main.maxTilesY)
                    continue;

                if (tileCoords.Distance(mouseTileCoords) < bd)
                {
                    Kernel((int)x, (int)y);
                }
            }
        }

        if (needsResetPlayerPosition)
            SpecialNetMessage.SendData(MessageID.PlayerControls, null, Main.myPlayer, Main.LocalPlayer.position.X, Main.LocalPlayer.position.Y, (float)Main.LocalPlayer.selectedItem);
    }

    private Vector2 lastTeleportPosition;
    public void Kernel(int x, int y)
    {
        if (!WorldGen.InWorld(x, y))
            return;

        ref TileData tile = ref Main.tile.GetTileRef(x, y);

        switch ((TileEditActions)currentTileAction)
        {
            case TileEditActions.Break:
                KillTile(ref tile, x, y);
                break;
            case TileEditActions.Place:
                PlaceTile(ref tile, currentPlayerCreateTile, currentPlayerCreateStyle, x, y, false);
                break;
            case TileEditActions.Replace:
                PlaceTile(ref tile, currentPlayerCreateTile, currentPlayerCreateStyle, x, y, true);
                break;
        }

        switch ((WallEditActions)currentWallAction)
        {
            case WallEditActions.Break:
                KillWall(ref tile, x, y);
                break;
            case WallEditActions.Place:
                PlaceWall(ref tile, currentPlayerCreateWall, x, y, false);
                break;
            case WallEditActions.Replace:
                PlaceWall(ref tile, currentPlayerCreateWall, x, y, true);
                break;
        }

        switch ((LiquidEditActions)currentLiquidAction)
        {
            case LiquidEditActions.Remove:
                KillLiquid(ref tile, x, y);
                break;
            case LiquidEditActions.Water:
                PlaceLiquid(ref tile, x, y, Tile.Liquid_Water);
                break;
            case LiquidEditActions.Lava:
                PlaceLiquid(ref tile, x, y, Tile.Liquid_Lava);
                break;
            case LiquidEditActions.Honey:
                PlaceLiquid(ref tile, x, y, Tile.Liquid_Honey);
                break;
            case LiquidEditActions.Shimmer:
                PlaceLiquid(ref tile, x, y, Tile.Liquid_Shimmer);
                break;
        }

        if (revealMap)
        {
            Main.Map.Update(x, y, 255);
            Main.refreshMap = true;
        }
    }

    public void KillTile(ref TileData tile, int x, int y)
    {
        if (WorldGen.CanKillTile(x, y))
        {
            tile.active(false);
            tile.type = 0;
            NetMessage.SendData(MessageID.TileManipulation, number: TileManipulationID.KillTileNoItem, number2: x, number3: y);
            if (sqaureFrame)
                WorldGen.SquareTileFrame(x, y);
        }
    }
    public void PlaceTile(ref TileData tile, int otherType, int otherStyle, int x, int y, bool replace)
    {
        if (!tile.active() || (replace && tile.type != otherType))
        {
            if (otherType == -1)
                return;
            if (MathF.Abs(x * 16f - lastTeleportPosition.X) > 26f * 16f || MathF.Abs(y * 16f - lastTeleportPosition.Y) > 26f * 16f)
            {
                needsResetPlayerPosition = true;
                lastTeleportPosition = new Vector2(x * 16f, y * 16f);
                SpecialNetMessage.SendData(MessageID.PlayerControls, null, Main.myPlayer, x * 16f, y * 16f, (float)Main.LocalPlayer.selectedItem);
            }
            tile.active(true);
            tile.type = (ushort)otherType;
            WorldGen.PlaceTile(x, y, tile.type, true, true, style: otherStyle);

            NetMessage.SendData(MessageID.TileManipulation, number: TileManipulationID.PlaceTile, number2: x, number3: y, number4: otherType, number5: otherStyle);

            if (sqaureFrame)
                WorldGen.SquareTileFrame(x, y);
        }
    }

    public void KillWall(ref TileData tile, int x, int y)
    {
        if (tile.wall != 0)
        {
            tile.wall = 0;
            NetMessage.SendData(MessageID.TileManipulation, number: TileManipulationID.KillWall, number2: x, number3: y);
            if (sqaureFrame)
                WorldGen.SquareWallFrame(x, y);
        }
    }
    public void PlaceWall(ref TileData tile, int otherType, int x, int y, bool replace)
    {
        if (tile.wall == 0 || (replace && tile.wall != otherType))
        {
            if (MathF.Abs(x * 16f - lastTeleportPosition.X) > 26f * 16f || MathF.Abs(y * 16f - lastTeleportPosition.Y) > 26f * 16f)
            {
                needsResetPlayerPosition = true;
                lastTeleportPosition = new Vector2(x * 16f, y * 16f);
                SpecialNetMessage.SendData(MessageID.PlayerControls, null, Main.myPlayer, x * 16f, y * 16f, (float)Main.LocalPlayer.selectedItem);
            }

            tile.wall = (ushort)otherType;

            if (replace)
                NetMessage.SendData(MessageID.TileManipulation, number: TileManipulationID.ReplaceWall, number2: x, number3: y, number4: otherType);
            else
                NetMessage.SendData(MessageID.TileManipulation, number: TileManipulationID.PlaceWall, number2: x, number3: y, number4: otherType);

            if (sqaureFrame)
                WorldGen.SquareWallFrame(x, y);
        }
    }

    public void KillLiquid(ref TileData tile, int x, int y)
    {
        if (tile.liquid != 0)
        {
            if (tile.liquidType() == Tile.Liquid_Water || tile.liquidType() == Tile.Liquid_Honey)
            {
                SpecialNetMessage.SendInventorySlot(0, ItemID.SuperAbsorbantSponge);
            }
            else if (tile.liquidType() == Tile.Liquid_Lava)
            {
                SpecialNetMessage.SendInventorySlot(0, ItemID.LavaAbsorbantSponge);
            }
            SpecialNetMessage.SendPlayerControl(new Vector2(x * 16f, y * 16f), 0);

            lastTeleportPosition = new Vector2(x * 16f, y * 16f);
            needsResetPlayerPosition = true;
            WorldGen.EmptyLiquid(x, y);

            SpecialNetMessage.SendInventorySlot(0, Main.LocalPlayer.inventory[0].netID, Main.LocalPlayer.inventory[0].stack, Main.LocalPlayer.inventory[0].prefix);
        }
    }

    public void PlaceLiquid(ref TileData tile, int x, int y, int liquid)
    {
        if (tile.liquidType() != liquid || tile.liquid != 255)
        {
            switch (liquid)
            {
                case Tile.Liquid_Water:
                    SpecialNetMessage.SendInventorySlot(0, ItemID.WaterBucket);
                    break;
                case Tile.Liquid_Lava:
                    SpecialNetMessage.SendInventorySlot(0, ItemID.LavaBucket);
                    break;
                case Tile.Liquid_Honey:
                    SpecialNetMessage.SendInventorySlot(0, ItemID.HoneyBucket);
                    break;
                case Tile.Liquid_Shimmer:
                    SpecialNetMessage.SendInventorySlot(0, ItemID.BottomlessShimmerBucket);
                    break;
            }

            SpecialNetMessage.SendPlayerControl(new Vector2(x * 16f, y * 16f), 0);

            lastTeleportPosition = new Vector2(x * 16f, y * 16f);
            needsResetPlayerPosition = true;
            WorldGen.PlaceLiquid(x, y, (byte)liquid, 255);

            SpecialNetMessage.SendInventorySlot(0, Main.LocalPlayer.inventory[0].netID, Main.LocalPlayer.inventory[0].stack, Main.LocalPlayer.inventory[0].prefix);
        }
    }
}