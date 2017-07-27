using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainUi
{
	public class MainUI : MonoBehaviour
	{
		public const int DECREASE_ANIMATION_SECONDS = 1;

		/* PAUSE */
		public Component panelPause;
		public Component textPause;
		public Component buttonPause;
		public Component buttonRestart;
		public Component buttonPlay;
		public Component buttonGivup;
        
		/* START LEVEL*/
		public Component panelStartLevel;
		public Component countStartLevel;
		public Component textStartLevel;

		public Component score;
		private Text scoreText;

		private CountDownAnimation countDownAnimationInstance;
		private bool countDownAnimated;

		public GameManager GameManager { get; set; }

		void Start ()
		{
			panelPause.gameObject.SetActive (false);
			panelStartLevel.gameObject.SetActive (false);
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			countDownAnimationInstance = countStartLevel.GetComponent<CountDownAnimation> ();
			countDownAnimationInstance.gameObject.SetActive (false);
			scoreText = score.GetComponent<Text> ();
		}

		void OnGUI ()
		{
			scoreText.text = "Score : " + GameManager.StarsTaken;
		}

		public void OnPauseTouched ()
		{
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
			buttonPause.gameObject.SetActive (false);
			panelPause.gameObject.SetActive (true);
			Time.timeScale = 0.0001f;
		}

		public void OnPlayTouched ()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			panelPause.gameObject.SetActive (false);
			buttonPause.gameObject.SetActive (true);

			Time.timeScale = 1f;
		}

		public void OnRestartTouched ()
		{
			OnPlayTouched ();
			if (GameManager != null) {
				GameManager.RestartGame ();
			}
		}

		public void OnStartLevel (int level)
		{
			OnPlayTouched ();
			panelStartLevel.gameObject.SetActive (true);
			score.gameObject.SetActive (false);
			Text levelTxt = textStartLevel.GetComponent<Text> ();
			if (levelTxt != null)
				levelTxt.text = "Level " + level;
			StartCoroutine (OnStartLevelFinished ());
		}

		private IEnumerator OnStartLevelFinished ()
		{
			yield return new WaitForSeconds (3);
			panelStartLevel.gameObject.SetActive (false);
			buttonPause.gameObject.SetActive (true);
			score.gameObject.SetActive (true);
			GameManager.prepareGeneration ();
		}

		private void startDecreaseAnimation ()
		{
			countDownAnimationInstance.gameObject.SetActive (true);

		}
	}
}