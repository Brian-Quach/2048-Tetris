using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public int columns;
    public int rows;

    public Tile[] grid;

    public void CreateGrid(
        int width,
        int height
        ) {
        columns = width;
        rows = height;

        grid = new Tile[columns * (rows)];

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
    

    public int GetTileID(Vector2 coordinates) {
        int tileID = (int) coordinates.x + ( (int) (coordinates.y) * columns);
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
