using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] tiles;

    private GameObject nextTile;
    private GameObject currentTile;

    // Use this for initialization
    void Start () {
        SpawnNextTile();
    }

    public GameObject CreateTile() {
        GameObject newTile = Instantiate(tiles[Random.Range(0, tiles.Length)],
                    transform.position,
                    Quaternion.identity);
        newTile.SetActive(false);
        return newTile;
    }

    public void SpawnNextTile() {
        if(nextTile == null) {
            nextTile = CreateTile();
        }
        currentTile = nextTile;
        currentTile.SetActive(true);
        nextTile = CreateTile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
