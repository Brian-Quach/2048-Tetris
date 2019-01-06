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

    public Grid grid;
    public Spawner spawner;
    public TileGroup currentTile;

    // Used for calculations when needed
    private int tempX;
    private int tempY;

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
    }

    public void SpawnTile() {
        currentTile = spawner.SpawnTile().GetComponent<TileGroup>();
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
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentTile.MoveLeft();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentTile.RotateCW();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentTile.MoveDown();
        }

    }
}
