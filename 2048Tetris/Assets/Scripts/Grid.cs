using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public int columns;
    public int rows;

    public int[] grid;
    public GameObject[] tiles;

    public void CreateGrid(
        int width,
        int height
        ) {
        columns = width;
        rows = height;

        grid = new int[columns * (rows)];
        tiles = new GameObject[columns * rows];

        CreateTiles();
    }

    private void CreateTiles() {
        var total = grid.Length;

        for (var i = 0; i < total; i++) {
            grid[i] = 0;
        }
    }

    public void InsertTile(Tile tile, Vector2 coordinates, GameObject tileObj) {
        int index = GetTileID(coordinates);
        grid[index] = tile.score;
        tiles[index] = tileObj;
    }

    private void DeleteTile(Vector2 coordinates) {
        int index = GetTileID(coordinates);
        grid[index] = 0;

        Object.Destroy(tiles[index]);
        tiles[index] = null;
    }
    
    public bool HasTile(Vector2 coordinates) {
        coordinates = RoundVector(coordinates);
        return (grid[GetTileID(coordinates)] != 0);   
    }

    public bool InGrid(Vector2 coordinates) {
        coordinates = RoundVector(coordinates);
        return (coordinates.x >= 0 && coordinates.y >= 0 && coordinates.x < columns && coordinates.y < rows);
    }

    // Return true if slot is empty + in grid
    public bool IsValidSlot(Vector2 coordinates) {
        //if (coordinates.y >= rows) return true;

        return (InGrid(coordinates) && !HasTile(coordinates));
    }

    public int GetTileID(Vector2 coordinates) {
        int tileID = (int) coordinates.x + ( (int) (coordinates.y) * columns);
        return tileID;
    }

    public Vector2 GetTileCoordinates(int index) {
        Vector2 coordinates = new Vector2 {
            x = Mathf.FloorToInt(index % columns),
            y = Mathf.FloorToInt(index / columns)
        };

        return coordinates;
    }

    public Vector2 RoundVector(Vector2 vec) {
        return new Vector2(Mathf.Round(vec.x),
                           Mathf.Round(vec.y));
    }

    // Move tile and return coords of tile location. Return true if something moved
    public bool MoveTileDown(Vector2 coordinates) {
        if (HasTile(coordinates)) {

            coordinates = RoundVector(coordinates);

            Vector2 newCoordinates = coordinates + (new Vector2(0, -1));
            if (!InGrid(newCoordinates)) return false;

            int tileIndex = GetTileID(coordinates);
            int newIndex = GetTileID(newCoordinates);


            // If tile matches below tile, remove lower tile and double score
            if (grid[tileIndex] == grid[newIndex]) {
                grid[tileIndex] *= 2;
                tiles[tileIndex].GetComponent<Tile>().score *= 2;
                DeleteTile(newCoordinates);
            }

            if (IsValidSlot(newCoordinates)) {
                // Move tile in coordinates to newcoordinates

                grid[newIndex] = grid[tileIndex];
                grid[tileIndex] = 0;
                tiles[newIndex] = tiles[tileIndex];
                tiles[tileIndex] = null;
                
                tiles[newIndex].transform.position = newCoordinates;
                return true;
            }
        }
        return false;
    }

    public int GetTileScore(Vector2 coordinates) {
        coordinates = RoundVector(coordinates);
        if (!InGrid(coordinates)) return -1;
        else return grid[GetTileID(coordinates)];
    }
}
