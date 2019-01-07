using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBoard : MonoBehaviour {

    [Header("Grid Dimensions")]
    public int gridWidth = 10;
    public int gridHeight = 20;

    [Header("Tile Properties")]
    [Range(0, .5f)]
    public float upgradePercent = .05f;
    public float fallSpeed = 1;

    public Grid grid;
    public Spawner spawner;
    public TileGroup currentTile;

    // Used for calculations when needed
    private int tempX;
    private int tempY;
    private float lastFall = 0;

    public GameObject[] freeTiles = new GameObject[0];

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
                    grid.InsertTile(tile, tileCoords);
                    tile.posID = grid.GetTileID(tileCoords);
                }

                var newFreeTiles = new GameObject[tiles.Length + freeTiles.Length];
                freeTiles.CopyTo(newFreeTiles, 0);
                tiles.CopyTo(newFreeTiles, freeTiles.Length);

                freeTiles = newFreeTiles;

                currentTile.SplitTiles();
                Destroy(currentTile.gameObject);

                SpawnTile();
            }
            lastFall = Time.time;
        }
    }

    // Potential Change >> Return a status code or something instead:
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
