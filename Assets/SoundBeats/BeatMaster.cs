using UnityEngine;
using System.Collections;

public struct beatInfo {
	public int value;
	public int subBeat;
}

public class Note {
	public int atSubBeat;
	public int length;

	public int beatInMeasure;

	public Note(int subBeat, int Length){
		atSubBeat = subBeat;
		length = Length;
		beatInMeasure = subBeat % 16;
	}

	public bool IsInNote(int subBeat) {
		subBeat = subBeat % 16;
		if (beatInMeasure + length > 15) {
			if (subBeat >= beatInMeasure || subBeat < (beatInMeasure + length)%16)
				return true;
		} else {
			if (subBeat >= beatInMeasure && subBeat < (beatInMeasure + length)%16)
				return true;
		}
		return false;
	}
}

[RequireComponent(typeof(AudioSource))]
public class BeatMaster : MonoBehaviour {

	public float bpm;
	[Header("Musical Info")]
	public int measure;
	public int beatInMeasure;
	[HideInInspector]
	public int subBeats;
	[Header("Calibration")]
	public float calibrationOffset = 0;

	public score info;

	private AudioSource sound_source;
	private float currentBeat = 0;
	private float nextBeat = 0;
	private float timeBetweenSubBeats;
	private int subdivisions = 4;

	// Use this for initialization
	void Start () {
		measure = -1;
		subBeats = -1;
		float timeBetweenBeats = 1f/(bpm/60f);
		timeBetweenSubBeats = timeBetweenBeats / subdivisions;
		sound_source = GetComponent<AudioSource>();
		sound_source.Play();
		nextBeat = timeBetweenSubBeats - Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CoreLogic();
	}

	/// <summary>
	/// Core logic.
	/// Counts subBeats
	/// </summary>
	void CoreLogic() {
		if (Time.time < nextBeat)
			return;
		subBeats++;
		//TODO account for delay
		currentBeat = nextBeat;
		nextBeat =  nextBeat + timeBetweenSubBeats;
		if ( isWhole(subBeats, window: 0) < 4 )
			measure++;
		beatInMeasure = subBeats % 16;
	}

	/// <summary>
	/// Raises the beat event.
	/// </summary>
	/// <param name="timestamp">Timestamp.</param>
	public beatInfo OnBeat(float timestamp) {
		beatInfo info = new beatInfo();
		info.subBeat = subBeats;
		info.value = 1;

		timestamp += calibrationOffset;
		float compareTo = currentBeat;
		// if closer to next beat
		if ( timestamp >= nextBeat-0.6f*timeBetweenSubBeats) {
			compareTo = nextBeat;
			info.subBeat++;
		}
		//float okLower = 	compareTo - 0.6f * timeBetweenSubBeats;
		float greatLower = 	compareTo - 0.3f * timeBetweenSubBeats;
		float greatUpper = 	compareTo + 0.2f * timeBetweenSubBeats;
		//float okUpper = 	compareTo + 0.4f * timeBetweenSubBeats;
		//if (timestamp >= okLower && timestamp < okUpper) {
		//	info.value = 1;
		//}
		if (timestamp >= greatLower && timestamp < greatUpper) {
			info.value = 0;
		}
		return info;
	}
	#region noteComparisons
	public int isWhole(int beatNumber, int offset = 0, int window = 1) {
		return isBeat(beatNumber, 16, offset, window);
	}

	public int isHalf(int beatNumber, int offset = 0, int window = 1) {
		return isBeat(beatNumber, 8, offset, window);
	}

	public int isQuarter(int beatNumber, int offset = 0, int window = 1) {
		return isBeat(beatNumber, 4, offset, window);
	}

	public int isEighth(int beatNumber, int offset = 0) {
		return isBeat(beatNumber, 2, offset, 0);
	}

	public int isSixteenth(int beatNumber, int offset = 0, int window = 1) {
		return 0;
	}

	public int isBeat(int beatNumber, int noteLength, int offset = 0, int window = 1) {
		//beatNumber += offset + window;
		int index = beatNumber % noteLength;
//		if (noteLength != 16)
//			info.info(string.Format("GOT: {0}%{1}={2}  REAL: {3}%{4}={5}", 
//			                     beatNumber, noteLength, index,
//			                     beatInMeasure, noteLength, beatInMeasure%noteLength));
		//if (index <= window)
		//	return index;
		return index;
	}
	#endregion
}
