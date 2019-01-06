using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public int columns;
    public int rows;

    public int[] grid;

    public void CreateGrid(
        int width,
        int height
        ) {
        columns = width;
        rows = height;

        grid = new int[columns * (rows)];

        CreateTiles();
    }

    private void CreateTiles() {
        var total = grid.Length;

        for (var i = 0; i < total; i++) {
            grid[i] = 0;
        }
    }

    public void InsertTile(Tile tile, Vector2 coordinates) {

    }
    
    public bool HasTile(Vector2 coordinates) {
        coordinates = RoundVector(coordinates);
        return (grid[GetTileID(coordinates)] != 0);   
    }

    public bool InGrid(Vector2 coordinates) {
        coordinates = RoundVector(coordinates);
        return (coordinates.x >= 0 && coordinates.y >= 0 && coordinates.x < columns);
    }

    // Return true if slot is empty + in grid
    public bool IsValidSlot(Vector2 coordinates) {
        if (coordinates.y >= rows + 3) return false;
        else if (coordinates.y >= rows) return true;

        return (InGrid(coordinates) && !HasTile(coordinates));
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
