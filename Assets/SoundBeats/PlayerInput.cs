using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {

	public BeatMaster timer;
	public score visuals;

	private Note hitNote;
	// Update is called once per frame
	void Start () {
		hitNote = new Note(0,0);
	}

	public void FireQ() {
		beatInfo beat = timer.OnBeat(Time.time);
		if (!Application.isEditor) {
			beat.subBeat++;
			Debug.LogWarning("!!!!");
		}
		int val = timer.isQuarter(beat.subBeat);
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat, 2);
		visuals.scores(beat.value);
	}

	public void FireH() {
		beatInfo beat = timer.OnBeat(Time.time);
		if (!Application.isEditor) {
			beat.subBeat++;
			Debug.LogWarning("!!!!");
		}
		int val = timer.isHalf(beat.subBeat);
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat, 4);
		visuals.scores(beat.value);
	}

	public void FireQ2() {
		beatInfo beat = timer.OnBeat(Time.time);
		int val = timer.beatInMeasure % 4;
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat, 2);
		visuals.scores(beat.value);
	}
	
	public void FireH2() {
		beatInfo beat = timer.OnBeat(Time.time);
		int val = timer.beatInMeasure % 8;
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat,4);
		visuals.scores(beat.value);
	}


	public void FireQ3() {
		beatInfo beat = timer.OnBeat(Time.time);
		int val = timer.isQuarter(beat.subBeat);
		int val2 = timer.beatInMeasure % 4;
		if ( val2 < val)
			val = val2;
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat, 2);
		visuals.scores(beat.value);
	}
	
	public void FireH3() {
		beatInfo beat = timer.OnBeat(Time.time);
		int val = timer.isHalf(beat.subBeat);
		int val2 = timer.beatInMeasure % 8;
		if ( val2 < val)
			val = val2;
		beat.value += val * 2;
		if (hitNote.IsInNote(beat.subBeat)) {
			beat.value = 8;
		}
		hitNote = new Note(beat.subBeat, 4);
		visuals.scores(beat.value);
	}

}
