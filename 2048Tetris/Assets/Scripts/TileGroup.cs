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

    public void SplitTiles() {
        foreach (GameObject tile in tiles) {
            tile.transform.SetParent(null);
        }
    }

    public void SetScores(float upgradePercent) {
        foreach(GameObject tile in tiles) {
            Tile tileObj = tile.GetComponent<Tile>();
            int score = 2;
            float rand = Random.Range(0f, 1f);
            while(rand <= upgradePercent) {
                score *= 2;
                rand = Random.Range(0f, 1f);
            }
            tileObj.score = score;
        }
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
