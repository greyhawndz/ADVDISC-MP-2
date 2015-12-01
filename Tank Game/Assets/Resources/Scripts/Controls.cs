using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public int hp = 1000;
	public LayerMask mask;
	public GameObject bulletPrefab;
	public GameObject shotLocation;
	private Vector2 coordinates;
	public float speed = 1.5f;
	public float shotSpeed = 2.0f;
	public bool lockW = false;
	public bool lockA = false;
	public bool lockS = false;
	public bool lockD = false;
	public bool colliding =false;
	public float dSpeedMult =1f;
	private int currentSprite =1;
	public Sprite[] sprites;
	public AudioSource shootSound;
	private SpriteRenderer spriteRenderer;
	Controls2 enemyControls;
	GameObject enemy;
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
		enemy = GameObject.Find ("Player 2");
		enemyControls = (Controls2)enemy.GetComponent("Controls2");


		
	}
	
	// Update is called once per frame
	

	void Update () {
		Move ();
		Shoot ();
		colliding = false;


	}

	void Move(){

		if (Input.GetKey (KeyCode.W) && !(lockW)) {
			facing = Facing.faceUp;
			coordinates.y += speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			currentSprite++;
			if(!Input.GetKey (KeyCode.S)&& !Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.A))
			{
			lockA=false;
			lockS=false;
			lockD=false;
			}
	
		} else if (Input.GetKey (KeyCode.S) && !(lockS)) {
			facing = Facing.faceDown;
			coordinates.y -= speed * Time.deltaTime;
			transform.localEulerAngles = new Vector3 (0, 0, 180);
			currentSprite++;
			if(!Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.A))
			{
			lockW=false;
			lockA=false;
			lockD=false;
			}

			
		} else if (Input.GetKey (KeyCode.A) && !(lockA)) {
			coordinates.x -= speed * Time.deltaTime;
			facing = Facing.faceLeft;
			transform.localEulerAngles = new Vector3 (0, 0, 90);
			currentSprite++;
			if(!Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.D)&& !Input.GetKey (KeyCode.S))
			{
			lockD=false;
			lockW=false;
			lockS=false;
			}

			
		} else if (Input.GetKey (KeyCode.D) && !(lockD)) {
			coordinates.x += speed * Time.deltaTime;
			facing = Facing.faceRight;
			transform.localEulerAngles = new Vector3 (0, 0, -90);
			currentSprite++;
			if(!Input.GetKey (KeyCode.A)&& !Input.GetKey (KeyCode.W)&& !Input.GetKey (KeyCode.S))
			{
			lockA=false;
			lockW=false;
			lockS=false;
			}



		} 
		if (currentSprite >= 9) {
			currentSprite = (currentSprite % 9) + 1;
		}
		
		//this.transform.position = coordinates;
		rigid.MovePosition(coordinates);
		spriteRenderer.sprite = sprites[currentSprite];
		
	}

	
	void OnTriggerEnter2D(Collider2D col){
		colliding = true;
		Bullet bull = col.gameObject.GetComponent<Bullet>();
		BoxCollider2D bump = col.gameObject.GetComponent<BoxCollider2D>();
		if (bump) {
			print ("ouch my head" + facing);
			
			if(facing == Facing.faceUp){

				lockW =true;
							}
			else if(facing == Facing.faceDown){
				lockS=true;
			}
			else if(facing == Facing.faceRight){
				lockD=true;
			}
			else if(facing == Facing.faceLeft){
				lockA=true;
			}
		}



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
	
		if(Input.GetKeyDown(KeyCode.Q)){
			//print ("Spied you!:" + enemyControls.hp);
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
