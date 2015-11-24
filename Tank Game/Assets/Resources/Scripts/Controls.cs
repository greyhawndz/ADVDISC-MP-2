using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public GameObject bulletPrefab;
	private Vector2 coordinates;
	public float speed = 1.5f;
	public float shotSpeed = 2.0f;
	private int currentSprite =1;
	public Sprite[] sprites;
	private SpriteRenderer spriteRenderer;
	private enum Facing{
		faceUp,
		faceDown,
		faceLeft,
		faceRight	
	};
	private Facing facing;
	private Rigidbody2D rigid;
	void Awake(){
		sprites = Resources.LoadAll<Sprite>("Sprites/sprite"); 
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigid = this.GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
		
		
		print("Hamburger");
		facing = Facing.faceUp;
		coordinates = this.transform.position;
		
	}
	
	// Update is called once per frame
	

	void Update () {
		Move();
		Shoot ();
	}
	

	void Move(){
		if (Input.GetKey (KeyCode.W)) {
			facing = Facing.faceUp;
			coordinates.y +=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,0);
			currentSprite++;	
			
		}
		else if (Input.GetKey (KeyCode.S)) {
			facing = Facing.faceDown;
			coordinates.y-=speed * Time.deltaTime;
			
			transform.localEulerAngles = new Vector3(0,0,180);
			currentSprite++;
			
		}
		else if (Input.GetKey (KeyCode.A)) {
			coordinates.x-=speed * Time.deltaTime;
			facing = Facing.faceLeft;
			transform.localEulerAngles = new Vector3(0,0,90);
			currentSprite++;
			
		}
		
		else if (Input.GetKey (KeyCode.D)) {
			coordinates.x+=speed * Time.deltaTime;
			facing = Facing.faceRight;
			transform.localEulerAngles = new Vector3(0,0,-90);
			currentSprite++;
			
		}
		if (currentSprite >= 9) {
			currentSprite = (currentSprite % 9) + 1;
		}
		
		//this.transform.position = coordinates;
		rigid.MovePosition(coordinates);
		spriteRenderer.sprite = sprites[currentSprite];
		
	}
	
	void OnCollisionEnter2D(Collision2D col){
		Debug.Log("collided with wall");
		
	}
	
	void Shoot(){
		
		if(Input.GetKeyDown(KeyCode.Q)){
			GameObject bullet = (GameObject) Instantiate(bulletPrefab,this.transform.position,Quaternion.identity);
			Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
			if(facing == Facing.faceUp){
				bulletRB.velocity = new Vector3(0, shotSpeed,this.transform.position.z);
			}
			else if(facing == Facing.faceDown){
				bulletRB.velocity = new Vector3(0, -shotSpeed,this.transform.position.z);
			}
			else if(facing == Facing.faceLeft){
				bulletRB.velocity = new Vector3(-shotSpeed, 0,this.transform.position.z);
			}
			else if(facing == Facing.faceRight){
				bulletRB.velocity = new Vector3(shotSpeed, 0,this.transform.position.z);
			}
		}
	}
}
