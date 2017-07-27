using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
	public const int ROTATION_SPEED = 90;

	protected abstract void DoAction (Ball ball);
	protected virtual void Init()
	{
		
	}

	protected MazeCell cell;
	protected Ball ball;

	// Use this for initialization
	void Start ()
	{
		setCell (gameObject.GetComponentInParent<MazeCell> ());
		Init ();
	}

	public void setCell (MazeCell cell)
	{
		this.cell = cell;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (Vector3.up * getRotationSpeed() * Time.deltaTime);
	}

	protected virtual float getRotationSpeed()
	{
		return ROTATION_SPEED;
	}

	void OnTriggerEnter (Collider colider)
	{
		BallSphere ballSphere = colider.gameObject.GetComponent<BallSphere> ();
		if (ballSphere) {
			ball = ballSphere.GetComponentInParent<Ball> ();
			DoAction (ball);
			Destroy (this.gameObject);
		}
	}

	public static class BonusFactory
	{
		public static Bonus newBonus (MazeCell cell, 
										BonusRates bonusRates,
		                              BombBonus bombBonusPrefab,
		                              OvalBonus ovalBonusPrefab,
		                              GhostBonus ghostBonusPrefab,
			StarBonus starBonusPrefab)
		{
			int sum = bonusRates.bombWeight + bonusRates.ovalWeight + bonusRates.ghostWeight + bonusRates.starWeight;;
			if (sum != 0) {
				int random = Random.Range (0, sum);
				if (random < bonusRates.bombWeight) {
					return Instantiate (bombBonusPrefab, cell.transform) as BombBonus;
				} else if (random < bonusRates.bombWeight + bonusRates.ovalWeight) {
				return Instantiate (ovalBonusPrefab, cell.transform) as OvalBonus;} 
				else if (random < bonusRates.bombWeight + bonusRates.ovalWeight + bonusRates.starWeight) {
					return Instantiate (starBonusPrefab, cell.transform) as StarBonus;
				} else if (random < sum) {
					return Instantiate (ghostBonusPrefab, cell.transform) as GhostBonus;
				}
			}
			return null;
		}
	}
}


