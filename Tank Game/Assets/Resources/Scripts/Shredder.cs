using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col){
		Destroy (col.gameObject);
	}
}
