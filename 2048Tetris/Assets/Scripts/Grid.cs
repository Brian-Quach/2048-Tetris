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

        grid = new Tile[columns * rows];

        CreateTiles();
    }

    private void CreateTiles() {
        var total = grid.Length;

        for (var i = 0; i < total; i++) {
            var tile = new Tile {
                score = 0
            };
            grid[i] = tile;
        }
    }

    public TileGroup CreatePiece() {
        piece = new TileGroup();

        return piece;
    }
}
