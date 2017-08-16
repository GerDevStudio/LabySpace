using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainUi
{
	public class MainUI : MonoBehaviour
	{
		public const int DECREASE_ANIMATION_SECONDS = 1;

		public GameManager gameManager;

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

		/* PLAY */ 
		public Component panelPlay;
		public Component score;
		public Component scoreImage;
		private Text scoreText;
		public Component scoreBarComponent;

		private CountDownAnimation countDownAnimationInstance;
		private bool countDownAnimated;

		public GameManager GameManager { get; set; }

		void Start ()
		{	
			panelPlay.gameObject.SetActive (false);
			panelPause.gameObject.SetActive (false);
			panelStartLevel.gameObject.SetActive (true);

			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			countDownAnimationInstance = countStartLevel.GetComponent<CountDownAnimation> ();
			countDownAnimationInstance.gameObject.SetActive (true);
			scoreText = score.GetComponent<Text> ();
		}

		void OnGUI ()
		{
			if (scoreText != null) {
				scoreText.text = gameManager.GetStarsTaken ().ToString ();
			}
		}

		public void OnPauseTouched ()
		{
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
			buttonPause.gameObject.SetActive (false);
			panelPause.gameObject.SetActive (true);
			Time.timeScale = 0.0001f;

			panelPlay.gameObject.SetActive (false);
		}

		public void OnPlayTouched ()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			panelPause.gameObject.SetActive (false);
			panelPlay.gameObject.SetActive (true);
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
			if (gameManager == null) {
				gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
			}

			OnPlayTouched ();
			panelStartLevel.gameObject.SetActive (true);
			panelPlay.gameObject.SetActive (false);
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

			panelPlay.gameObject.SetActive (true);

			GameManager.prepareGeneration ();
		}

		public void initScoreBar (int steps, int stars)
		{
			ScoreBar scoreBar = scoreBarComponent.GetComponent<ScoreBar> ();
			scoreBar = scoreBarComponent.GetComponent<ScoreBar> ();
			scoreBar.init (steps,stars);
		}

		private void startDecreaseAnimation ()
		{
			countDownAnimationInstance.gameObject.SetActive (true);
		}
	}
}