using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 20;
	private int damageInc;
	public GameObject shooter;
	private Vector3 startpos;
	
	void Start(){
		startpos = shooter.transform.position;
		Debug.Log(startpos);
		damageInc = 0;
	}
	
	public void Hit(){
		float magnitudeX = startpos.x + this.transform.position.x;
		float magnitudeY = startpos.y + this.transform.position.y;
		float magnitude = Mathf.Sqrt(Mathf.Pow(this.transform.position.x,2) + Mathf.Pow(this.transform.position.y,2));
		Debug.Log("Magnitude " +this.transform.position.magnitude);
		damageInc = Mathf.CeilToInt(magnitude);
		Debug.Log("Damage " +damageInc);
		damage += damageInc;
		Debug.Log("Damage is " +damage);
		Destroy(gameObject);
	}
	
	public int GetDamage(){
		return damage;
	}
}
