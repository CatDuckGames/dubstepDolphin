using UnityEngine;
//using System.Collections;

public delegate void cTimerExspired();

[System.Serializable]
public class cTimer {

	public event cTimerExspired OnExpired;
	public bool Enabled = true;
	public float Value = 0;

	private float interval = 1;
	private float startDelay = 0;
	private int timesToTrigger = -1;	//-1 = infinate
	private bool ignoreTimeScale = false;

	public cTimer(float timeInterval, float initialDelay = 0, int numberOfRepeats = -1, bool ignoreTimeScaling = false, bool enabled = true ) {
		interval = timeInterval;
		timesToTrigger = numberOfRepeats;
		startDelay = initialDelay;
		Enabled = enabled;
		ignoreTimeScale = ignoreTimeScaling;
	}

	/// <summary>
	/// Start/resume this instance.
	/// </summary>
	public void Start() {
		Enabled = true;
	}
	/// <summary>
	/// Stop this instance and reset its counter to 0.
	/// </summary>
	public void Stop() {
		Enabled = false;
		Value = 0;
	}
	/// <summary>
	/// Pause this instance. Call 'Start()' to resume.
	/// </summary>
	public void Pause() {
		Enabled = false;
	}
	/// <summary>
	/// Restart this instance.
	/// </summary>
	public void Restart() {
		Stop();
		Start();
	}


	//must call in the parent's Update function
	public void Tick() {
		//if we have reached our number of triggers or cTimer is off, return
		if (!Enabled || timesToTrigger == 0)
			return;
		//set time Delta based on scaled or unscaled time
		float deltaTime = 0;
		if (ignoreTimeScale)
			deltaTime = Time.unscaledDeltaTime;
		else
			deltaTime = Time.deltaTime;

		//has initial delay passed?
		if (startDelay > 0) {
			startDelay -= deltaTime;
			return;
		} else if (startDelay < 0) {
			Value -= startDelay; 
			startDelay = 0;
		}

		//count time
		Value += deltaTime;
		//if time > target
		if (Value >= interval) {
			//decrement number of times to trigger (-1 = trigger infinatly)
			if (timesToTrigger > 0)
				timesToTrigger--;
			//reset to 0 and include any extra time we used
			Value -= interval;
			//run delegates if they exist
			if (OnExpired != null)
				OnExpired();
		}
	}

}
