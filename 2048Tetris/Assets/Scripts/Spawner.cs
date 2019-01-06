using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] tiles;

    private GameObject nextTile;
    

    private GameObject CreateTile() {
        GameObject newTile = Instantiate(tiles[Random.Range(0, tiles.Length)],
                    transform.position,
                    Quaternion.identity);
        newTile.SetActive(false);
        return newTile;
    }

    public GameObject SpawnTile() {
        if(nextTile == null) {
            nextTile = CreateTile();
        }
        GameObject next = nextTile;
        next.SetActive(true);
        nextTile = CreateTile();

        return next;
    }
	
}
