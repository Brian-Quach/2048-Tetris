using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public int id = -1;
    public int score = 2;
	
	void Upgrade () {
        score = score * 2;
	}
}
