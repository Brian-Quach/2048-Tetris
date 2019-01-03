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

    // Used for calculations when needed
    private int tempX;
    private int tempY;

    // Use this for initialization
    void Start () {

    }

    public void Reset() {
        grid = new Grid();
    }


    // Update is called once per frame
    void Update () {
		
	}
}
