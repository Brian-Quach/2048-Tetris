using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileGroup : MonoBehaviour {
    
    public Vector2[] GetTilePositions() {
        var positions = new List<Vector2>();

        foreach(Transform tile in transform) {
            positions.Add(tile.position);
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
