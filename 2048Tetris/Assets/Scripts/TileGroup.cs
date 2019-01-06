using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileGroup : MonoBehaviour {

    public GameObject[] tiles;
    
    private void Awake() {
        var innerTiles = new List<GameObject>();
        foreach (Transform childTransform in transform) {
            GameObject child = childTransform.gameObject;
            innerTiles.Add(child);
        }
        tiles = innerTiles.ToArray();
    }

    public Vector2[] GetTilePositions() {
        var positions = new List<Vector2>();

        foreach(GameObject tile in tiles) {
            positions.Add(tile.transform.position);
        }

        return positions.ToArray();
    }

    public void MoveLeft() {
        transform.position += new Vector3(-1, 0, 0);
    }

    public void MoveRight() {
        transform.position += new Vector3(1, 0, 0);
    }

    public void MoveDown() {
        transform.position += new Vector3(0, -1, 0);
    }

    public void MoveUp() {
        transform.position += new Vector3(0, 1, 0);
    }

    public void RotateCW() {
        transform.Rotate(0, 0, -90);
    }

    public void RotateCCW() {
        transform.Rotate(0, 0, 90);
    }
}
