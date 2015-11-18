using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	private Vector2 coordinates = new Vector2(0,0);
	private float speed =1.5f;
	private int currentSprite =1;
	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer; 
	// Use this for initialization
	void Start () {

		sprites = Resources.LoadAll<Sprite>("Sprites/sprite"); 
		print("Hamburger");
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	

	void Update () {

		/*transform.position = new Vector3(0, 0, 0);
		print(transform.position.x);*/


		if (Input.GetKey ("w")) {
			coordinates.y +=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,0);
			currentSprite++;
		}
		else if (Input.GetKey ("s")) {
			coordinates.y-=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,180);
			currentSprite++;
		}
		else if (Input.GetKey ("a")) {
			coordinates.x-=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,90);
			currentSprite++;
		}

		else if (Input.GetKey ("d")) {
			coordinates.x+=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,-90);
			currentSprite++;
		}
		if (currentSprite >= 9) {
			currentSprite = (currentSprite % 9) + 1;
		}

		transform.position = coordinates;
		spriteRenderer.sprite = sprites[currentSprite];
		if( Input.GetKeyDown("space"))
		   {
			print ("Canon Shot!");



				//GameObject.CreatePrimitive(PrimitiveType.Cube);

		  }


	}
}
