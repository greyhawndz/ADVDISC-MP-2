using UnityEngine;
using System.Collections;

public class Controls2 : MonoBehaviour {
	public GameObject bulletPrefab;
	public GameObject shotLocation;
	public int hp = 1000;
	private Vector2 coordinates;
	public float speed = 1.5f;
	public float shotSpeed = 2.0f;
	private int currentSprite = 9;
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
		facing = Facing.faceDown;
		coordinates = this.transform.position;
		
	}
	
	// Update is called once per frame
	
	
	void Update () {
		Move();
		Shoot ();
	}
	
	
	void Move(){
		if (Input.GetKey (KeyCode.UpArrow)) {
			facing = Facing.faceUp;
			coordinates.y +=speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3(0,0,180);
			currentSprite++;	
			
		}
		else if (Input.GetKey (KeyCode.DownArrow)) {
			facing = Facing.faceDown;
			coordinates.y-=speed * Time.deltaTime;
			
			transform.localEulerAngles = new Vector3(0,0,0);
			currentSprite++;
			
		}
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			coordinates.x-=speed * Time.deltaTime;
			facing = Facing.faceLeft;
			transform.localEulerAngles = new Vector3(0,0,-90);
			currentSprite++;
			
		}
		
		else if (Input.GetKey (KeyCode.RightArrow)) {
			coordinates.x+=speed * Time.deltaTime;
			facing = Facing.faceRight;
			transform.localEulerAngles = new Vector3(0,0,90);
			currentSprite++;
			
		}
		if (currentSprite >= 16) {
			currentSprite = (currentSprite % 16) + 9;
		}
		
		//this.transform.position = coordinates;
		rigid.MovePosition(coordinates);
		spriteRenderer.sprite = sprites[currentSprite];
		
	}
	
	void DetectCollision(){
		Vector3 direction = this.transform.position;
		if(facing == Facing.faceUp){
			direction = Vector3.up;
		}
		else if(facing == Facing.faceDown){
			direction = Vector3.down;
		}
		else if(facing == Facing.faceRight){
			direction = Vector3.right;
		}
		else if(facing == Facing.faceLeft){
			direction = Vector3.left;
		}
		
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position,direction,0.3f);
		Debug.DrawRay(this.transform.position,direction);
		if(hit.collider.tag == "Breakable" || hit.collider.tag == "Unbreakable"){
			speed = 0f;
			Debug.Log("hit wall");
		}
		else{
			speed = 1.5f;
		}
		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		Bullet bull = col.gameObject.GetComponent<Bullet>();
		if(bull){
			Debug.Log("Hit");
			bull.Hit();
			hp -= bull.GetDamage();
			if(hp <= 0){
				Destroy(gameObject);
			}
		}
	}
	
	void Shoot(){
		
		if(Input.GetKeyDown(KeyCode.Slash)){
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
