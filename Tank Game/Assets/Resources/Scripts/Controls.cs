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
	public float wSpeedMult =1f;
	public float aSpeedMult =1f;
	public float sSpeedMult =1f;
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
		//DetectCollision();

	}
	

	void Move(){
		if (Input.GetKey (KeyCode.W)) {
			facing = Facing.faceUp;
			coordinates.y +=speed * Time.deltaTime*wSpeedMult;
			transform.localEulerAngles = new Vector3(0,0,0);
			currentSprite++;	
			aSpeedMult =1f;
			sSpeedMult =1f;
			dSpeedMult =1f;
			
		}
		else if (Input.GetKey (KeyCode.S)) {
			facing = Facing.faceDown;
			coordinates.y-=speed * Time.deltaTime*sSpeedMult;
			
			transform.localEulerAngles = new Vector3(0,0,180);
			currentSprite++;
			wSpeedMult =1f;
			aSpeedMult =1f;
			dSpeedMult =1f;
			
		}
		else if (Input.GetKey (KeyCode.A)) {
			coordinates.x-=speed * Time.deltaTime*aSpeedMult;
			facing = Facing.faceLeft;
			transform.localEulerAngles = new Vector3(0,0,90);
			currentSprite++;
			wSpeedMult =1f;
			sSpeedMult =1f;
			dSpeedMult =1f;
			
		}
		
		else if (Input.GetKey (KeyCode.D)) {
			coordinates.x+=speed * Time.deltaTime*dSpeedMult;
			facing = Facing.faceRight;
			transform.localEulerAngles = new Vector3(0,0,-90);
			currentSprite++;
			wSpeedMult =1f;
			aSpeedMult =1f;
			sSpeedMult =1f;

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

				wSpeedMult = .00f;
							}
			else if(facing == Facing.faceDown){
				sSpeedMult = .00f;
			}
			else if(facing == Facing.faceRight){
				dSpeedMult = .00f;
			}
			else if(facing == Facing.faceLeft){
				aSpeedMult = .00f;
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
