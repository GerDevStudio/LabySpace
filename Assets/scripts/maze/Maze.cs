using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
	public IntVector2 mazeSize;

	public MazePassage passagePrefab;

	public MazeWall wallPrefab;
	public MazeCell cellPrefab;
	public MazeCellFinish cellFinishPrefab;
	public MazeCellHole cellHolePrefab;

	public BombBonus bombBonusPrefab;
	public OvalBonus ovalBonusPrefab;
	public GhostBonus ghostBonusPrefab;
	public StarBonus starBonusPrefab;

	public BonusRates bonusRates;
	public CellRates mazeRates;

	private MazeCell[,] cells;

	public void Generate (IntVector2 size, Action<int> onGenerationFinished)
	{
		this.mazeSize = size;
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell> ();
		DoFirstGenerationStep (activeCells);
		int steps = 1;
		while (activeCells.Count > 0) {
			DoNextGenerationStep (activeCells);
			steps++;
		}
		onGenerationFinished (steps);
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells)
	{
		MazeCell startCell = CreateCell (RandomCoordinates);
		activeCells.Add (startCell);
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells)
	{
		//narrow paths
		int currentIndex = activeCells.Count - 1;

		//random paths
		//int currentIndex = Random.Range(0,activeCells.Count - 1);

		MazeCell currentCell = activeCells [currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt (currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
		if (ContainsCoordinates (coordinates)) {
			MazeCell neighbor = GetCell (coordinates);
			if (neighbor == null) {
				neighbor = CreateCell (coordinates);
				CreatePassage (currentCell, neighbor, direction);
				activeCells.Add (neighbor);
			} else {
				CreateWall (currentCell, neighbor, direction);
			}
		} else {
			CreateWall (currentCell, null, direction);
		}
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction.GetOpposite ());
	}

	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			MazeWall otherWall = Instantiate (wallPrefab) as MazeWall;
			otherWall.Initialize (otherCell, cell, direction.GetOpposite ());
		}
	}

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2 (UnityEngine.Random.Range (0, mazeSize.x), UnityEngine.Random.Range (0, mazeSize.z));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate)
	{
		return coordinate.x >= 0 && coordinate.x < mazeSize.x && coordinate.z >= 0 && coordinate.z < mazeSize.z;
	}

	private MazeCell CreateCell (IntVector2 coordinates)
	{
		MazeCell newCell;

		if (coordinates.x == 0 && coordinates.z == 0) {
			newCell = Instantiate (cellPrefab) as MazeCell;
		} else if (coordinates.x == mazeSize.x - 1 && coordinates.z == mazeSize.z - 1) {
			newCell = Instantiate (cellFinishPrefab) as MazeCellFinish;
		} else {
			float random = UnityEngine.Random.Range (0, mazeRates.holeWeight + mazeRates.bonusWeight + mazeRates.emptyWeight);
			if (random <= mazeRates.holeWeight) {
				newCell = Instantiate (cellHolePrefab) as MazeCellHole;
				Quaternion random90degreesRotation = Quaternion.AngleAxis (90 * UnityEngine.Random.Range (0, 4), Vector3.up);
				((MazeCellHole)newCell).ground.transform.rotation = random90degreesRotation;
			} else {
				newCell = Instantiate (cellPrefab) as MazeCell;

				if (random >= mazeRates.holeWeight && random <= mazeRates.holeWeight + mazeRates.bonusWeight)
					CreateBonus (newCell);
			}
		}

		cells [coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = toVector3Centered (coordinates);
		return newCell;
	}

	private void CreateBonus (MazeCell cell)
	{
		IntVector2 coordinates = cell.coordinates;
		Bonus bonus = Bonus.BonusFactory.newBonus (cell, bonusRates, bombBonusPrefab, ovalBonusPrefab, ghostBonusPrefab, starBonusPrefab);
		bonus.name = (bonus is BombBonus) ? "BombBonus" : "OvalBonus " + coordinates.x + ", " + coordinates.z;
		bonus.transform.localPosition += 0.1f * Vector3.up;

		// put in a random corner
		MazeDirection direction = (MazeDirection)UnityEngine.Random.Range (0, 4);
		MazeDirection rightDirection = (MazeDirection)(((int)direction + 1) % 4);
		bonus.transform.localPosition += 0.23f * (MazeDirections.toVector3 (direction) + (MazeDirections.toVector3 (rightDirection)));
	}

	public MazeCell GetCell (IntVector2 coordinates)
	{
		return cells [coordinates.x, coordinates.z];
	}

	void Start ()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MainCamera> ().Target = gameObject;
	}

	void Update ()
	{

	}

	private Vector3 toVector3Centered (IntVector2 coordinates)
	{
		return new Vector3 (coordinates.x - mazeSize.x * 0.5f + 0.5f, 0f, coordinates.z - mazeSize.z * 0.5f + 0.5f);
	}
}
