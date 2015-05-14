using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class score : MonoBehaviour {

	public BeatMaster timer;
	public GameObject scoreTExt;

	public UILabel greatL;
	public UILabel okL;
	public UILabel poorL;
	public UILabel missL;
	public UILabel comboL;
	public UILabel bestL;
	public UILabel totalL;
	public UILabel cL;

	public List<float> inputs;
	public float average;
	
	private int greats = 0;
	private int ok = 0;
	private int miss = 0;
	private int poor = 0;
	private int combo = 0;
	private int bestCombo = 0;

	public UILabel consol;
	private string[] rotatingStrings;
	private int rotIndex = 0;

	// Use this for initialization
	void Start () {
		inputs = new List<float>();
		rotatingStrings = new string[8];
	}

	public void scores(int point) {
		GameObject g = Instantiate(scoreTExt) as GameObject;
		comboText text = g.GetComponent<comboText>();
		switch (point) {
		case 0:
			text.spawn("GREAT");
			greats++;
			combo++;
			break;
		case 1:
			text.spawn("Ok");
			ok++;
			combo++;
			break;
		case 2:
			text.spawn("Poor");
			poor++;
			combo++;
			break;
		case 8:
			text.spawn("collision");
			break;
		default:
			text.spawn("miss");
			miss++;
			combo = 0;
			break;
		}
		greatL.text = "Great: " + greats;
		okL.text = "OK: " + ok;
		poorL.text = "Poor: " + poor;
		missL.text = "Miss: " + miss;
		comboL.text = "Combo: " + combo;
		bestL.text = "Best: " + bestCombo;
		totalL.text = "Total: " + (greats + ok + miss + poor);
		if (combo > bestCombo)
			bestCombo = combo;
	}

	public void ResetScores() {
		greats = 0;
		ok = 0;
		poor = 0;
		miss = 0;
	}

	public void Calibrate() {
		inputs.Add(Time.time);
		float temp = 0;
		for (int i = 1; i < inputs.Count; i++) {
			temp += inputs[i]-inputs[i-1];
		}
		average = temp / (inputs.Count - 1);
		timer.calibrationOffset = 0.5f - average;
		cL.text = "C: " + timer.calibrationOffset;
	}
	
	public void reset() {
		inputs = new List<float>();
		average = 0;
		timer.calibrationOffset = 0;
		cL.text = "C: " + timer.calibrationOffset;
	}

	public void info(string s) {
		print (s);
		rotatingStrings[rotIndex] = s;
		rotIndex = (rotIndex + 1) %  rotatingStrings.Length;
		string content = "";
		for (int i = 0; i < rotatingStrings.Length; i++) {
			content += rotatingStrings[i] + "\n";
		}
		consol.text = content;
	}

	public void quit() {
		Application.Quit();
	}
}
