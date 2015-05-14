using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(Rigidbody2D))]
public class comboText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	public void spawn(string t) {
		GetComponent<TextMesh>().text = t;
		GetComponent<Rigidbody2D>().velocity = new Vector2(2,0);
	}
}
