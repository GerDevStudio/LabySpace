using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonus : Bonus
{

	private GameManager gameManager;

	protected override void Init ()
	{
		GameObject goGameManager = GameObject.Find ("GameManager");
		gameManager = goGameManager.GetComponent<GameManager> ();
	}

	protected override void DoAction (Ball ball)
	{
		gameManager.OnBonusTaken (this);
	}

	protected override float getRotationSpeed ()
	{
		return 0;
	}
}
