using System.Collections;
using System.Collections.Generic;
using MainUi;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public const string LEVEL = "level";

	public SwipeDetector swipeDetectorPrefab;
	public Maze mazePrefab;
	public Ball ballPrefab;
	public MainUI mainUIPrefab;

	private SwipeDetector swipeDetectorInstance;
	private Ball ballInstance;
	private Maze mazeInstance;
	private MainUI mainUIInstance;

	private int starsTaken;
	private int starsMax;

	public int GetStarsTaken()
	{
		return starsTaken;
	}

	public void SetStarsTaken(int stars)
	{
		starsTaken = stars;
		ScoreBar scoreBar = mainUIInstance.scoreBarComponent.GetComponent<ScoreBar>();
		if (scoreBar != null) {
			scoreBar.Add (stars);
		}
	}

	public int level = 1;

	public static bool finished = false;

	void Start ()
	{
		#if !UNITY_EDITOR
		loadLevel();
		#endif

		swipeDetectorInstance = Instantiate (swipeDetectorPrefab) as SwipeDetector;
		mainUIInstance = Instantiate (mainUIPrefab) as MainUI;
		mainUIInstance.GameManager = this;

		Time.timeScale = 1.0f;

		BeginGame ();
	}

	private void loadLevel ()
	{
		if (PlayerPrefs.GetInt (LEVEL) > 0) {
			level = PlayerPrefs.GetInt (LEVEL);
		}
	}

	void Update ()
	{
		if (ballInstance != null && ballInstance.transform.position.y < -5) {
			finished = true;
		}

		if (finished) {
			RestartGame ();
		}
	}

	private void BeginGame ()
	{
		mainUIInstance.OnStartLevel (level);
	}

	public void prepareGeneration ()
	{
		mazeInstance = Instantiate (mazePrefab) as Maze;
		IntVector2 size = GetMazeSizeForNextLevel (level, mazeInstance.mazeSize);
		mazeInstance.Generate (size, onGenerationFinished);
		SetStarsTaken (0);
	}

	public void OnBonusTaken(Bonus bonus)
	{
		if (bonus is StarBonus) {
			starsTaken += 1;
			SetStarsTaken (starsTaken);
		}
	}

	public void RestartGame ()
	{
		finished = false;
		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);
		if (ballInstance != null)
			Destroy (ballInstance.gameObject);
		BeginGame ();
	}

	IntVector2 GetMazeSizeForNextLevel (int level, IntVector2 mazeSize)
	{

		int x = 3;
		int z = 3;
		int i = 1;
		while (i < level) {
			if (x < z)
				x++;
			else
				z++;
			int nbOfTimeTurnPlayedForThisSize = Mathf.RoundToInt ((float)x / 3f);
			i += nbOfTimeTurnPlayedForThisSize;
		}
		Debug.Log ("Level : " + level + "/ x : " + x + " / z : " + z + ". Next Change at level : " + i);
		return new IntVector2 (x, z);
	}

	private void onGenerationFinished (int steps, int stars)
	{
		this.starsMax = stars;

		Debug.Log ("Generation finished in " + steps + " steps");
		PopBall ();

		mainUIInstance.initScoreBar (steps,stars);
	}

	public void LevelFinished ()
	{
		level++;
		PlayerPrefs.SetInt(LEVEL,level);
		PlayerPrefs.Save ();
		RestartGame ();
	}

	private void PopBall ()
	{
		Ball newBall = Instantiate (ballPrefab) as Ball;

		int x = mazeInstance.mazeSize.x;
		int z = mazeInstance.mazeSize.z;

		newBall.transform.position = new Vector3 (-x * 0.5f + 0.5f, 0f, -z * 0.5f + 0.5f);
		newBall.name = "Ball";

		this.ballInstance = newBall;
		swipeDetectorInstance.SetsListener (ballInstance);
	}
}
