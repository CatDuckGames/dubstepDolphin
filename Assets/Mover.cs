using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	public float rot;

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		if (rot != 0)
			transform.Rotate(0,0,rot);
	}
}
