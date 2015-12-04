using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	public Rect windowRect;
	public string dataBox1Content = "Data Box 1";
	public string dataBox2Content = "Data Box 2";
	public AudioClip music;
	Controls2 enemyControls;
	GameObject enemy;
	Controls playerControls;
	GameObject player;
	// Use this for initialization
	void Start () {
		music = Resources.Load<AudioClip>("Audio/music"); 
		enemy = GameObject.Find ("Player 2");
		enemyControls = (Controls2)enemy.GetComponent("Controls2");

		player = GameObject.Find ("Player");
		playerControls = (Controls)player.GetComponent("Controls");
		windowRect = new Rect(450, 150, 250, 250);

	}
	
	// Update is called once per frame
	void Update () {

		}
	
	public void OnGUI() {

		GUI.Box (new Rect (5, 405, 250, 185), dataBox1Content);
		GUI.Box (new Rect (805, 5, 250, 185), dataBox2Content);
		if (enemyControls == null)
			{
			GUI.Box (new Rect (405, 205, 250, 185), "Player 1 Wins");
		}

		if (playerControls == null)
		{
			GUI.Box (new Rect (405, 205, 250, 185), "Player 2 Wins");
		}
	
	}
}
