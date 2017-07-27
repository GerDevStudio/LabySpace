using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownAnimation : MonoBehaviour {

	public  const float MAX_SCALE = 3;
	public  const float MIN_SCALE = 2;

	private Text targetText;
	private float startTime;
	private float scale;

	void Start () {
		targetText = GetComponent<Text> ();
		StartCoroutine (CountDown());
	}

	void OnEnable()
	{
		StartCoroutine (CountDown ());
	}

	void OnDisable()
	{
		StopCoroutine (CountDown ());
	}

	private IEnumerator CountDown()
	{
		scale = MAX_SCALE;
		WaitForSeconds wait = new WaitForSeconds (1);
		for (int i = 3; i > 0; i--) {
			if (targetText != null) {
				targetText.text = i.ToString ();
				yield return wait;
			}
		}
	}

	void Update () {
		if (scale < MIN_SCALE)
			scale = MAX_SCALE;
		
		scale -= Time.deltaTime;

		gameObject.transform.localScale = new Vector3 (scale,scale,1);
	}
}
