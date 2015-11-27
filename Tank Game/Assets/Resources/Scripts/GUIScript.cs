using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	public Rect windowRect;
	public string dataBox1Content = "Data Box 1";
	public string dataBox2Content = "Data Box 2";
	// Use this for initialization
	void Start () {
		windowRect = new Rect(450, 150, 250, 250);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void OnGUI() {
		
		GUI.Box (new Rect (5, 405, 250, 185), dataBox1Content);

		GUI.Box (new Rect (805, 5, 250, 185), dataBox2Content);
	
	}
}
