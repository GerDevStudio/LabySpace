using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OvalEffect : MonoBehaviour
{

	private const float DURATION = 5;
	private const float MIN_SCALE = 0.76f;
	private Ball ball;
	float ballScale = MIN_SCALE;

	public void Restart ()
	{
		ballScale = MIN_SCALE;
	}

	void Start ()
	{
		ball = GetComponentInParent<Ball> ();
		StartCoroutine (Resize ());
	}

	private IEnumerator Resize ()
	{
		ballScale = MIN_SCALE;
		int steps = 10;
		WaitForSeconds firstWait = new WaitForSeconds (0.9f*DURATION);
		WaitForSeconds wait = new WaitForSeconds (0.1f * DURATION / (steps));
		setBallScale (ballScale);
		yield return firstWait;
		while (ballScale < 1.0f) {
			if (ball != null && ball.childBallSphere != null) {
				ballScale += (1 - MIN_SCALE) / steps;
				setBallScale (ballScale);
				yield return wait;
			}
		}
		setBallScale (1);
		Destroy (this);
	}

	private void setBallScale(float scale)
	{
		ball.childBallSphere.transform.localScale = new Vector3 (ballScale, 1, ballScale);
	}
}
