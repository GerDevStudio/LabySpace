using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dissapear : MonoBehaviour
{
	
	public void Perform ()
	{
		StartCoroutine (FadeTo (0.0f, 3.0f, FadeFinish));
	}

	private void FadeFinish (int actionCode)
	{
		Destroy (this.gameObject);
	}

	IEnumerator FadeTo (float aValue, float aTime, Action<int> finishListener)
	{
		MazeWall wall = gameObject.GetComponent<MazeWall> ();
		if (wall!=null) {
			float alpha = wall.woodWall.gameObject.GetComponent<Renderer>().material.color.a;

			yield return new WaitForSeconds(aTime * 0.66f);

			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / (aTime*0.33f)) {
				Color newColor = new Color (1, 1, 1, Mathf.Lerp (alpha, aValue, t));
				wall.woodWall.gameObject.GetComponent<Renderer> ().material.color = newColor;
				yield return null;
			}

			FadeFinish (0);
		}
	}
}
