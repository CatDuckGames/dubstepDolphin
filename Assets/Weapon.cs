using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public GameObject bomb;

	public void ShootBullet() {
		GameObject g = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		Destroy(g,3);
	}

	public void ShootBomb() {
		GameObject g = Instantiate(bomb, transform.position,Quaternion.identity) as GameObject;
		g.transform.Rotate(0,0,90);
		Destroy(g,3);
	}

	public void ShootBullet(bool onBeat) {
		GameObject g = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		if (!onBeat) {
			g.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
		} else
			g.transform.localScale = new Vector3(1.0f,0.5f,0.5f);
		Destroy(g,3);
	}
	
	public void ShootBomb(bool onBeat) {
		GameObject g = Instantiate(bomb, transform.position,Quaternion.identity) as GameObject;
		g.transform.Rotate(0,0,90);
		if (!onBeat) {
			g.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
		} else 
			g.transform.localScale = new Vector3(1.0f,0.5f,0.5f);
		Destroy(g,3);
	}
}
