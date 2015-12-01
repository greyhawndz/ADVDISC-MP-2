using UnityEngine;
using System.Collections;

public class WallBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		Bullet bull = col.gameObject.GetComponent<Bullet>();
				if(bull){
			bull.Hit();
				}
		}
}
