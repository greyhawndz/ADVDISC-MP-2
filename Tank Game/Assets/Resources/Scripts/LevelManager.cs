using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log("Request load level for: " +name);
		Cursor.visible = true;
		
		Application.LoadLevel(name);
		
	}
}
