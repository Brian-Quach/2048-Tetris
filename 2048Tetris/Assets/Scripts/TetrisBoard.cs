using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBoard : MonoBehaviour {

    [Header("Grid Dimensions")]
    public int gridWidth = 10;
    public int gridHeight = 20;

    [Header("Tile Properties")]
    [Range(0, .5f)]
    public float upgradePercent = .1f;
    public float fallSpeed = 1f;

    public Grid grid;
    public Spawner spawner;
    public TileGroup currentTile;

    // Used for calculations when needed
    private int tempX;
    private int tempY;
    private float lastFall = 0;

    private float singleFallSpeed = .25f;
    private float lastFallSingle = 0;

    //public GameObject[] freeTiles = new GameObject[0];

    private void Awake() {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    // Use this for initialization
    void Start () {
        Reset();
        SpawnTile();
    }

    public void Reset() {
        grid = new Grid();
        GridSetup();
    }

    public void SpawnTile() {
        currentTile = spawner.SpawnTile().GetComponent<TileGroup>();
        currentTile.SetScores(upgradePercent);
        if (CurrentPositionValid() != 1) {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    public void GridSetup() {
        grid.CreateGrid(
            gridWidth,
            gridHeight
            );
    }
    
    void Update () {

        currentTile.transform.position = new Vector3(
            Mathf.Round(currentTile.transform.position.x),
            Mathf.Round(currentTile.transform.position.y),
            0);

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentTile.MoveRight();

            if (CurrentPositionValid() != 1) {
                currentTile.MoveLeft();
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentTile.MoveLeft();

            if (CurrentPositionValid() != 1) {
                currentTile.MoveRight();
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentTile.RotateCW();

            if (CurrentPositionValid() != 1) {
                currentTile.RotateCCW();
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= fallSpeed) {
            currentTile.MoveDown();

            if (CurrentPositionValid() != 1) {
                currentTile.MoveUp();

                GameObject[] tiles = currentTile.tiles;

                foreach(GameObject tileObj in tiles) {
                    Debug.Log(tileObj.transform.position.x + "," + tileObj.transform.position.y);
                    Tile tile = tileObj.GetComponent<Tile>();
                    Vector2 tileCoords = new Vector2(
                        Mathf.Round(tileObj.transform.position.x),
                        Mathf.Round(tileObj.transform.position.y));
                    grid.InsertTile(tile, tileCoords, tileObj);
                    tile.posID = grid.GetTileID(tileCoords);
                }

                //var newFreeTiles = new GameObject[tiles.Length + freeTiles.Length];
                //freeTiles.CopyTo(newFreeTiles, 0);
                //tiles.CopyTo(newFreeTiles, freeTiles.Length);

                //freeTiles = newFreeTiles;

                currentTile.SplitTiles();
                Destroy(currentTile.gameObject);

                SpawnTile();
            }
            

            lastFall = Time.time;
        }

        if (Time.time - lastFallSingle >= singleFallSpeed) {
            TestFreeTiles();
            lastFallSingle = Time.time;
        }
    }

    private void TestFreeTiles() {
        for (int r = grid.rows; r >= 0; r--) {
            for(int c = 0; c < grid.columns; c++) {
                Vector2 coordinates = new Vector2(r, c);
                if (grid.HasTile(coordinates)) {
                    grid.MoveTileDown(coordinates);
                }
            }
        }
    }

    private void MergeTopLayer() {

    }

    private void MergeTiles(GameObject tile1, GameObject tile2) {
        // Check that tile1 is above tile2
    }

    //private void MergeTile(GameObject tileObj) {
    //    Tile tile = tileObj.GetComponent<Tile>();
    //    int tileSore = tile.score;
    //    Vector2 tileUnderCoords = (Vector2)tileObj.transform.position + (new Vector2(0, -1));
    //    if (tileSore == 0) return;
    //    else if (tile.score == grid.GetTileScore(tileUnderCoords)) {
    //        //TODO: Clean this up and make animate it

    //    }
    //}

    // Potential Change >> Return a status code or something instead:
    // Use status to fix issue with spawning tiles (should be able to spawn partially valid stuff)
    // 1 for full validity
    // 0 for full invalidity
    // 2(?) for no partial validity (ie. overlap exists, but there is a piece on board)
    private int CurrentPositionValid() {
        Vector2[] tiles = currentTile.GetTilePositions();

        foreach(Vector2 tile in tiles) {
            if (!grid.IsValidSlot(tile)) {
                return 0;
            }
        }
        return 1;
    }
}
