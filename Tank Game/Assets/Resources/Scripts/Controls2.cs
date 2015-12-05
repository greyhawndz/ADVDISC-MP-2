using UnityEngine;
using System.Collections;

public class Controls2 : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject shotLocation;
	public GameObject explosionPrefab;
	public int hp = 1000;
	public Vector2 coordinates;
	public float speed = 1.5f;
	public bool lockUp = false;
	public bool lockLeft = false;
	public bool lockDown = false;
	public bool lockRight = false;
	public float shotSpeed = 2.0f;
	private int currentSprite = 9;
	public Sprite[] sprites;
	public AudioSource shootSound;
	private SpriteRenderer spriteRenderer;
	private enum Facing{
		faceUp,
		faceDown,
		faceLeft,
		faceRight	
	};
	private Facing facing;
	public Rigidbody2D rigid;
	
	void Awake(){
		sprites = Resources.LoadAll<Sprite>("Sprites/sprite"); 
		spriteRenderer = GetComponent<SpriteRenderer>();
		rigid = this.GetComponent<Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
		
		
		print("Hamburger");
		facing = Facing.faceDown;
		coordinates = this.transform.position;
		print (coordinates);
	}
	
	// Update is called once per frame
	
	
	void Update () {
		Move();
		Shoot ();
	}
	
	
	void Move(){
		if (Input.GetKey (KeyCode.UpArrow)&& !(lockUp)) {
			facing = Facing.faceUp;
			coordinates.y +=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,180);
			currentSprite++;	

			if(!Input.GetKey (KeyCode.DownArrow)&& !Input.GetKey (KeyCode.RightArrow)&& !Input.GetKey (KeyCode.LeftArrow))
			{
				lockLeft=false;
				lockDown=false;
				lockRight=false;
			}
			
		}
		else if (Input.GetKey (KeyCode.DownArrow)&& !(lockDown)) {
			facing = Facing.faceDown;
			coordinates.y-=speed * Time.deltaTime;
			
			transform.localEulerAngles = new Vector3(0,0,0);
			currentSprite++;
			if(!Input.GetKey (KeyCode.UpArrow)&& !Input.GetKey (KeyCode.RightArrow)&& !Input.GetKey (KeyCode.LeftArrow))
			{
				lockUp=false;
				lockRight=false;
				lockLeft=false;
			}
			
		}
		else if (Input.GetKey (KeyCode.LeftArrow)&& !(lockLeft)) {
			coordinates.x-=speed * Time.deltaTime;
			facing = Facing.faceLeft;
			transform.localEulerAngles = new Vector3(0,0,-90);
			currentSprite++;
			if(!Input.GetKey (KeyCode.UpArrow)&& !Input.GetKey (KeyCode.RightArrow)&& !Input.GetKey (KeyCode.DownArrow))
			{
				lockRight=false;
				lockUp=false;
				lockDown=false;
			}
			
		}
		
		else if (Input.GetKey (KeyCode.RightArrow)&& !(lockRight)) {
			coordinates.x+=speed * Time.deltaTime;
			facing = Facing.faceRight;
			transform.localEulerAngles = new Vector3(0,0,90);
			currentSprite++;
			if(!Input.GetKey (KeyCode.LeftArrow)&& !Input.GetKey (KeyCode.UpArrow)&& !Input.GetKey (KeyCode.DownArrow))
			{
				lockLeft=false;
				lockUp=false;
				lockDown=false;
			}
			
		}
		if (currentSprite >= 16) {
			currentSprite = (currentSprite % 16) + 9;
		}
		
		//this.transform.position = coordinates;
		rigid.MovePosition(coordinates);
		spriteRenderer.sprite = sprites[currentSprite];
		
	}

	
	void OnTriggerEnter2D(Collider2D col){
		Bullet bull = col.gameObject.GetComponent<Bullet>();
		BoxCollider2D bump = col.gameObject.GetComponent<BoxCollider2D>();
		if (bump) {
			print ("ouch my head" + facing);
			
			if(facing == Facing.faceUp){
				
				lockUp =true;
			}
			else if(facing == Facing.faceDown){
				lockDown=true;
			}
			else if(facing == Facing.faceRight){
				lockRight=true;
			}
			else if(facing == Facing.faceLeft){
				lockLeft=true;
			}
		}

		if(bull){
			Debug.Log("Hit");
			bull.Hit();
			hp -= bull.GetDamage();
			if(hp <= 0){
				//Destroy(gameObject);
				Instantiate(explosionPrefab, new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z), Quaternion.Euler(0,180,0));
				gameObject.active =false;
			}
		}
	}
	
	void Shoot(){
		
		if(Input.GetKeyDown(KeyCode.Slash)){
			shootSound.Play();
			GameObject bullet = (GameObject) Instantiate(bulletPrefab,this.transform.position,Quaternion.identity);
			GameObject shotLoc = (GameObject) Instantiate (shotLocation, this.transform.position, Quaternion.identity);
			Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
			bullet.transform.parent = shotLoc.transform;
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
