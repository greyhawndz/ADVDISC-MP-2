using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	public Rect windowRect;
	public string dataBox1Content = "Data Box 1";
	public string dataBox2Content = "Data Box 2";
	//public AudioClip music;
	public AudioSource explodeSound;
	public AudioSource bgm;
	public AudioSource victory;
	Controls2 enemyControls;
	GameObject enemy;
	Controls playerControls;
	GameObject player;
	bool noRepeat=false;
	// Use this for initialization
	void Start () {
		bgm.Play ();
		//music = Resources.Load<AudioClip>("Audio/music"); 
		enemy = GameObject.Find ("Player 2");
		enemyControls = (Controls2)enemy.GetComponent("Controls2");

		player = GameObject.Find ("Player");
		playerControls = (Controls)player.GetComponent("Controls");

		windowRect = new Rect(450, 150, 250, 250);
		print ("Width: " +Screen.width);
		print ("Height: " +Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		dataBox1Content = "HP: " + playerControls.hp + "/200\n\n"; 
		dataBox1Content += "Coordinates: \nX:" + playerControls.coordinates.x + "\nY: " + playerControls.coordinates.y ;
		dataBox2Content = "HP: " + enemyControls.hp + "/200\n\n"; 
		dataBox2Content += "Coordinates: \nX:" + enemyControls.coordinates.x + "\nY: " + enemyControls.coordinates.y ;

	
	}
	public void victoryStuff()
	{		

		if (!noRepeat) {
			victory.Play();
			explodeSound.Play ();
			bgm.Stop ();
			noRepeat =true;
		}

	}
	public void restart()
	{victory.Stop();
		bgm.Play ();
		enemyControls.hp =200;
		enemyControls.coordinates.x =10;
		enemyControls.coordinates.y =12.8f;
		enemy.active =true;
		
		playerControls.hp =200;
		playerControls.coordinates.x =10;
		playerControls.coordinates.y =.6f;
		player.active =true;

		noRepeat = false;
	}
	
	public void OnGUI() {

		GUI.Box (new Rect (Screen.width*0.006f, Screen.height*.68f, Screen.width*0.235f, Screen.height*.31f), dataBox1Content);
		GUI.Box (new Rect (Screen.width*0.7587f, Screen.height*.01f, Screen.width*0.235f, Screen.height*.31f), dataBox2Content);
		if (!enemy.activeSelf)
			{
			victoryStuff ();
			GUI.Box (new Rect (Screen.width*0.4f, Screen.height*.35f, Screen.width*0.25f, Screen.height*0.35f), "Player 1 Wins");
			if (GUI.Button (new Rect (Screen.width*0.42f, Screen.height*0.60f, Screen.width*0.10f, Screen.height*0.05f), "Play Again")) {
				restart ();

			}
			if (GUI.Button (new Rect (Screen.width*0.55f, Screen.height*0.60f, Screen.width*0.08f, Screen.height*0.05f), "Quit")) {
				Application.Quit();
			}
		}

		if (!player.activeSelf)
		{	victoryStuff ();
			GUI.Box (new Rect (Screen.width*0.4f, Screen.height*.35f, Screen.width*0.25f, Screen.height*0.35f), "Player 2 Wins");
			if (GUI.Button (new Rect (Screen.width*0.42f, Screen.height*0.60f, Screen.width*0.10f, Screen.height*0.05f), "Play Again")) {
				restart ();
			}
			if (GUI.Button (new Rect (Screen.width*0.55f, Screen.height*0.60f, Screen.width*0.08f, Screen.height*0.05f), "Quit")) {
				Application.Quit();
			}

		}

	
	}
}
