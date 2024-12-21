﻿using System;
using System.Threading.Tasks;

namespace TerraAngel.WorldEdits;

public class WorldEditCopyPaste : WorldEdit
{
    public override bool RunEveryFrame => false;

    public TileSectionRenderer Renderer = new TileSectionRenderer();
    public TileSection? CopiedSection;
    private bool IsCopying = false;
    private Vector2 StartSelectTile;
    private string[] PlaceModes = StringExtensions.EnumFancyNames<PlaceMode>();
    private int CurrentPlaceMode = 0;
    private bool DestroyTiles = false;

    public override void DrawPreviewInMap(ImGuiIOPtr io, ImDrawListPtr drawList)
    {
        Vector2 worldMouse = Util.ScreenToWorldFullscreenMap(InputSystem.MousePosition);
        Vector2 tileMouse = (worldMouse / 16f).Ceiling();
        if (InputSystem.IsKeyPressed(ClientConfig.Settings.WorldEditSelectKey))
        {
            IsCopying = true;

            StartSelectTile = (worldMouse / 16f).Floor();
        }

        if (IsCopying)
        {
            if (InputSystem.IsKeyDown(ClientConfig.Settings.WorldEditSelectKey))
            {
                drawList.DrawTileRect(StartSelectTile, tileMouse, new Color(1f, 0f, 0f, 0.5f).PackedValue);
            }

            if (InputSystem.IsKeyUp(ClientConfig.Settings.WorldEditSelectKey))
            {
                IsCopying = false;
                Copy(StartSelectTile, tileMouse);
            }
        }
        else
        {
            if (CopiedSection is not null)
            {
                Renderer.DrawPrimitiveMap(CopiedSection, tileMouse * 16f, Vector2.Zero, io.DisplaySize);
            }
        }
    }
    public override void DrawPreviewInWorld(ImGuiIOPtr io, ImDrawListPtr drawList)
    {
        Vector2 worldMouse = Util.ScreenToWorldWorld(InputSystem.MousePosition);
        Vector2 tileMouse = (worldMouse / 16f).Floor();
        if (InputSystem.IsKeyPressed(ClientConfig.Settings.WorldEditSelectKey))
        {
            IsCopying = true;

            StartSelectTile = tileMouse;
        }

        if (IsCopying)
        {
            if (InputSystem.IsKeyDown(ClientConfig.Settings.WorldEditSelectKey))
            {
                drawList.DrawTileRect(StartSelectTile, tileMouse, new Color(1f, 0f, 0f, 0.5f).PackedValue);
            }

            if (InputSystem.IsKeyUp(ClientConfig.Settings.WorldEditSelectKey))
            {
                IsCopying = false;
                Copy(StartSelectTile, tileMouse);
            }
        }
        else
        {
            if (CopiedSection is not null)
            {
                Renderer.DrawDetailed(CopiedSection, Util.WorldToScreenWorld(tileMouse * 16f), Vector2.Zero, io.DisplaySize);
            }
        }
    }

    public override bool DrawUITab(ImGuiIOPtr io)
    {
        if (ImGui.BeginTabItem(GetString("Copy/Paste")))
        {
            ImGui.Checkbox(GetString("Destroy Tiles"), ref DestroyTiles);
            ImGui.Text(GetString("Place Mode")); ImGui.SameLine(); ImGui.Combo("##PlaceMode", ref CurrentPlaceMode, PlaceModeStrings, PlaceModeStrings.Length);
            ImGui.EndTabItem();
            return true;
        }
        return false;
    }

    public override void Edit(Vector2 cursorTilePosition)
    {
        if (CopiedSection is null)
            return;
        switch ((PlaceMode)CurrentPlaceMode)
        {
            case PlaceMode.SendTileRect:
                EditSendTileRect(cursorTilePosition);
                break;
            case PlaceMode.TileManipulation:
                EditSendTileManipulation(cursorTilePosition);
                break;
        }
    }

    private void EditSendTileRect(Vector2 originTile)
    {
        if (CopiedSection is null)
            return;

        int ox = (int)MathF.Round(originTile.X);
        int oy = (int)MathF.Round(originTile.Y);
        Task.Run(
            () =>
            {
                Main.rand = new Terraria.Utilities.UnifiedRandom();
                // pass one, for solid tiles
                for (int x = 0; x < CopiedSection.Width; x++)
                {
                    for (int y = CopiedSection.Height - 1; y > -1; y--)
                    {
                        if (!WorldGen.InWorld(ox + x, oy + y))
                            continue;

                        Tile tile = Main.tile[ox + x, oy + y];
                        Tile copiedTile = CopiedSection.Tiles[x, y];

                        if (tile == null || copiedTile == null)
                            continue;

                        if (!(Main.tileSolid[copiedTile.type] &&
                            copiedTile.type != TileID.GolfTee &&
                            copiedTile.type != TileID.GolfHole &&
                            copiedTile.type != TileID.GolfCupFlag))
                            continue;

                        bool isCopiedTileEmpty = !(copiedTile.active() || copiedTile.wall > 0);
                        if (isCopiedTileEmpty && !DestroyTiles)
                            continue;



                        tile.CopyFrom(copiedTile);
                    }
                }

                // pass two, for non solid tiles
                for (int x = 0; x < CopiedSection.Width; x++)
                {
                    for (int y = CopiedSection.Height - 1; y > -1; y--)
                    {
                        if (!WorldGen.InWorld(ox + x, oy + y))
                            continue;

                        Tile tile = Main.tile[ox + x, oy + y];
                        Tile copiedTile = CopiedSection.Tiles[x, y];

                        if (tile == null || copiedTile == null)
                            continue;

                        if ((Main.tileSolid[copiedTile.type] &&
                            copiedTile.type != TileID.GolfTee &&
                            copiedTile.type != TileID.GolfHole &&
                            copiedTile.type != TileID.GolfCupFlag))
                            continue;

                        bool isCopiedTileEmpty = !(copiedTile.active() || copiedTile.wall > 0);
                        if (isCopiedTileEmpty && !DestroyTiles)
                            continue;

                        tile.CopyFrom(copiedTile);
                    }
                }

                // pass three, for framing and syncing
                for (int x = 0; x < CopiedSection.Width; x++)
                {
                    for (int y = CopiedSection.Height - 1; y > -1; y--)
                    {
                        if (!WorldGen.InWorld(ox + x, oy + y))
                            continue;

                        Tile? tile = Main.tile[ox + x, oy + y];
                        Tile? copiedTile = CopiedSection.Tiles?[x, y];

                        if (tile is null || copiedTile is null)
                            continue;

                        WorldGen.SquareTileFrame(ox + x, oy + y);
                        WorldGen.SquareWallFrame(ox + x, oy + y);

                        NetMessage.SendTileSquare(Main.myPlayer, ox + x, oy + y);
                    }
                }
            });

    }

    private void EditSendTileManipulation(Vector2 originTile)
    {

    }

    public void Copy(Vector2 startTile, Vector2 endTile)
    {
        float width = endTile.X - startTile.X;
        float height = endTile.Y - startTile.Y;

        //for (int x = ((int)startTile.X); x < ((int)endTile.X); x++)
        //{
        //    for (int y = ((int)startTile.Y); y < ((int)endTile.Y); y++)
        //    {
        //        WorldGen.SquareTileFrame(x, y);
        //        WorldGen.SquareWallFrame(x, y);
        //    }
        //}

        if (width * height <= 0) return;

        CopiedSection = new TileSection(((int)startTile.X), ((int)startTile.Y), ((int)width), ((int)height));
    }

    enum PlaceMode
    {
        SendTileRect,
        TileManipulation
    }

    private string[] PlaceModeStrings =
    {
        GetString("SendTileRect"),
        GetString("TileManipulation")
    };
}