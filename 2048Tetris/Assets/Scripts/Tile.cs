using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int score = 2;
    public int posID = -1;

    private TextMesh textMesh;

    private void Awake() {
        textMesh = transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        textMesh.text = score.ToString();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        textMesh.text = score.ToString();
        textMesh.gameObject.transform.rotation = Quaternion.identity;
    }
}
