using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetChild(0).transform.Rotate(0,1,0);
	}
	
}
