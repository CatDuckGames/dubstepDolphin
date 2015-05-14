using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "tiles") {
			particles.Instance.Explosion(transform.position);
			Destroy(this.gameObject);	
		}
		if (other.gameObject.tag == "enemy") {
			particles.Instance.Explosion(transform.position);
			particles.Instance.Explosion(other.transform.position);
			Destroy(other.gameObject);	
			Destroy(this.gameObject);	
		}

	}

}
