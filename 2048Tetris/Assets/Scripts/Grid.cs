using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public int columns = 10;
    public int rows = 20;

    public Tile[] grid;
    public TileGroup piece;

    public void CreateGrid(int width, int height) {
        columns = width;
        rows = height;

        grid = new Tile[columns * (rows+4)];

        CreateTiles();
    }

    private void CreateTiles() {
        var total = grid.Length;

        for (var i = 0; i < total; i++) {
            var tile = new Tile {
                id = i,
                score = 0
            };
            grid[i] = tile;
        }
    }

    public TileGroup CreatePiece() {
        piece = new TileGroup();

        return piece;
    }

    public int GetTileID(Vector2 coordinates) {
        int tileID = (int) coordinates.x + ( (int) (coordinates.y + 4) * columns);
        return tileID;
    }

    public Vector2 GetTileCoordinates(int index) {
        Vector2 coordinates = new Vector2();
        coordinates.x = Mathf.FloorToInt(index % columns);
        coordinates.y = Mathf.FloorToInt(index / columns);

        return coordinates;
    }

    public Vector2 RoundVector(Vector2 vec) {
        return new Vector2(Mathf.Round(vec.x),
                           Mathf.Round(vec.y));
    }
}
