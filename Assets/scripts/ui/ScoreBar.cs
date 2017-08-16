using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {

	private Slider starSlider; 

	private int count;
	private int max;
	private int steps;

	private int stepNotified;

	private Listener listener;

	void Start () {
	}
	
	void Update () {
		
	}

	void OnGui () {
		
	}

	public void SetListener(Listener listener)
	{
		this.listener = listener;
	}

	public void Add(int count)
	{
		this.count += count;

		if (listener != null && count>= (max/steps)*(stepNotified+1))
		{
			listener.OnScoreStep(stepNotified++);
		}

		float ratio = (float)count / (float)max;

		starSlider.value = ratio;
	}

	public void init(int steps, int max)
	{
		starSlider = GetComponent<Slider> ();
		this.max = max;
		this.steps = steps;
		count = 0;
		stepNotified = 0;
		starSlider.value = 0;
	}

	public interface Listener
	{
		void OnScoreStep(int step);
	}
}
