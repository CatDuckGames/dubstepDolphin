using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class cTimerExample : MonoBehaviour {

	public cTimer test1;
	public cTimer test2;
	public cTimer test1s;
	public cTimer test2s;

	public float bpm;
	public int subdivisions = 4;
	[Header("Other")]
	public GameObject bar;
	[Header("info")]
	public float timeBetweenBeats;
	public float timeBetweenSubBeats;
	[Header("timings")]
	public int c1Beats = 0;
	public int c2Beats = 0;
	public int c1SubBeats = 0;
	public int c2SubBeats = 0;
	[Header("master")]
	public float time;
	public float fixedTime;
	public int beats;
	public int ftBeats;

	[Range(0.025f,0.142f)]
	public float window = 0.1f;
	public float lag = 0.02f;


	private AudioSource adio;
	// Use this for initialization
	void Start () {
		adio = GetComponent<AudioSource>();
		timeBetweenBeats = 1f/(bpm/60f);
		timeBetweenSubBeats = timeBetweenBeats / subdivisions;


		test1 = new cTimer(timeBetweenBeats);
		test1.OnExpired += ticks;
		test2 = new cTimer(timeBetweenBeats);
		test2.OnExpired += tocks;

		test1s = new cTimer(timeBetweenSubBeats);
		test1s.OnExpired += () =>{
			c1SubBeats++;
			GameObject g = Instantiate (bar, this.transform.position, Quaternion.identity) as GameObject;
			g.transform.localScale = new Vector3(1,0.15f,1);
			Destroy(g, 3);
		};
		test2s = new cTimer(timeBetweenSubBeats);
		test2s.OnExpired += () =>{c2SubBeats++;};
	}

	private void ticks() {
		//print("Timer 1 "  + Time.time);
		c1Beats++;
		GameObject g = Instantiate (bar, this.transform.position, Quaternion.identity) as GameObject;
		if (c1Beats%(4) != 0)
			g.transform.localScale = new Vector3(4,0.15f,1);
		Destroy(g, 3);
	}

	private void tocks() {
		//print("Timer 2 " + Time.time);
		c2Beats++;
	}

	// Update is called once per frame
	void Update () {
		time = Time.time;
		beats = (int)(time/timeBetweenBeats);
		test1.Tick ();
		test1s.Tick ();
		if (Input.GetButtonDown("Fire1")) {
			float t = timeBetweenSubBeats - test1s.Value - lag;
			if (t <= timeBetweenSubBeats*window*3 || t >= timeBetweenSubBeats - timeBetweenSubBeats*window*1)
				print ("GREATE");
			else if(t <= timeBetweenSubBeats*window*4 || t >= timeBetweenSubBeats - timeBetweenSubBeats*window*3)
				print ("ok");
			else
				print ("fail");
		}
	}
	void FixedUpdate() {
		fixedTime = Time.fixedTime;
		ftBeats = (int)(fixedTime/timeBetweenBeats);
		test2.Tick();
		test2s.Tick ();
	}


	//------------

	public void timescale(float i) {
		if (!adio)
		    return;
		float s = i+0.5f;
		Time.timeScale = s;
		adio.pitch = s;
	}

	void OnGUI() {
		if (!adio)
			return;
		GUI.Label(new Rect(10, 500, 150, 100), "Scale: " + adio.pitch);
	}
}
