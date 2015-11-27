using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 20;
	private int damageInc;
	public GameObject shooter;
	
	void Start(){
		damageInc = 0;
	}
	
	
	public void Hit(){
		Debug.Log("Shooter position" + shooter.transform.position);
		Debug.Log("Bullet  local position" + this.transform.localPosition);
		Debug.Log("Bullet world position" + this.transform.position);
		float magnitude = Mathf.Sqrt(Mathf.Pow(this.transform.localPosition.x,2) + Mathf.Pow(this.transform.localPosition.y,2));
		Debug.Log("Magnitude " +this.transform.position.magnitude);
		damageInc = Mathf.CeilToInt(magnitude);
		Debug.Log("Damage " +damageInc);
		damage += damageInc;
		Debug.Log("Damage is " +damage);
		Destroy(transform.parent.gameObject);
		Destroy(gameObject);
		
	}
	
	public int GetDamage(){
		return damage;
	}
}
