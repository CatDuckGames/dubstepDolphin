using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour {

	public float speed;
	public BeatMaster beats;

	public float method = 1;
	public bool moveOnBeat = false;

	public AudioSource bullet;
	private bool bSounds = true;

	private Rigidbody2D rBody;
	private Weapon[] guns;
	private Note hitNote;
	private Note hitNoteBomb;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		guns = GetComponentsInChildren<Weapon>();
		hitNote = new Note(0,0);
		hitNoteBomb = new Note(0,0);
	}

	public bool Quarter() {
		beatInfo beat = beats.OnBeat(Time.time);
		if (!Application.isEditor) {
			beat.subBeat++;
		}
		int val = beats.isQuarter(beat.subBeat);
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
			return false;
		}
		if (beat.value < 4) {
			hitNote = new Note(beat.subBeat, 4);
			return true;
		}
		return false;
	}
	public bool Half() {
		beatInfo beat = beats.OnBeat(Time.time);
		if (!Application.isEditor) {
			beat.subBeat++;
		}
		int val = beats.isHalf(beat.subBeat);
		beat.value += val * 2;
		if (hitNoteBomb.IsInNote(beat.subBeat)) {
			beat.value = 8;
			return false;
		}
		if (beat.value < 4) {
			hitNoteBomb = new Note(beat.subBeat, 8);
			return true;
		}
		return false;
	}

	public void setGuns(float f){
		method = f;
	}
	public void setMove(){
		moveOnBeat = !moveOnBeat;
	}
	public void setS(){
		bSounds = !bSounds;
	}
	// Update is called once per frame
	void Update () {
		float x = Input.GetAxisRaw("Horizontal") * speed;
		float y = Input.GetAxisRaw("Vertical") * speed;
		if (!moveOnBeat)
			rBody.velocity = new Vector2(x,y);
		if (moveOnBeat && Quarter() ) {
			Vector3 v = transform.localPosition;
			v.x += x/speed;
			v.y += y/speed;
			transform.localPosition = v;
		}

		if (method == 0) {
			if (Input.GetButtonDown("Fire1")) {
				foreach(Weapon w in guns) {
					w.ShootBullet();
					if (bSounds)
						bullet.Play();
				}
			}
			if (Input.GetButtonDown("Fire2")) {
				foreach(Weapon w in guns) {
					w.ShootBomb();
				}
			}
		}
		if (Mathf.Approximately(method,1f/3f)) {
			if (Input.GetButtonDown("Fire1") && Quarter()) {
				foreach(Weapon w in guns) {
					w.ShootBullet();
					if (bSounds)
						bullet.Play();
				}
			}
			if (Input.GetButtonDown("Fire2") && Half()) {
				foreach(Weapon w in guns) {
					w.ShootBomb();
				}
			}
		}
		if (Mathf.Approximately(method,2f/3f)) {
			if (Input.GetButton("Fire1") && Quarter()) {
				foreach(Weapon w in guns) {
					w.ShootBullet();
					if (bSounds)
						bullet.Play();
				}
			}
			if (Input.GetButton("Fire2") && Half()) {
				foreach(Weapon w in guns) {
					w.ShootBomb();
				}
			}
		}
		if (method == 1) {
			if (Input.GetButtonDown("Fire1")) {
				foreach(Weapon w in guns) {
					w.ShootBullet(Quarter());
					if (bSounds)
						bullet.Play();
				}
			}
			if (Input.GetButtonDown("Fire2")) {
				foreach(Weapon w in guns) {
					w.ShootBomb(Half());
				}
			}



		}

	}
}
