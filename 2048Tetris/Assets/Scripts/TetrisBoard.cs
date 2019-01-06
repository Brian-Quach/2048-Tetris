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
        if (!CurrentPositionValid()) {
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
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentTile.MoveRight();

            if (!CurrentPositionValid()) {
                currentTile.MoveLeft();
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentTile.MoveLeft();

            if (!CurrentPositionValid()) {
                currentTile.MoveRight();
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentTile.RotateCW();

            if (!CurrentPositionValid()) {
                currentTile.RotateCCW();
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= fallSpeed) {
            currentTile.MoveDown();

            if (!CurrentPositionValid()) {
                currentTile.MoveUp();

                GameObject[] tiles = currentTile.tiles;

                foreach(GameObject tileObj in tiles) {
                    Debug.Log(tileObj.transform.position.x + "," + tileObj.transform.position.y);
                    Tile tile = tileObj.GetComponent<Tile>();
                    Vector2 tileCoords = tileObj.transform.position;
                    grid.InsertTile(tile, tileCoords);
                }

                SpawnTile();
            }
            lastFall = Time.time;
        }
    }

    private bool CurrentPositionValid() {
        Vector2[] tiles = currentTile.GetTilePositions();

        foreach(Vector2 tile in tiles) {
            if (!grid.IsValidSlot(tile)) {
                return false;
            }
        }
        return true;
    }
}
