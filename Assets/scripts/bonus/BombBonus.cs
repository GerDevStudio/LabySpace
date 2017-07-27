using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBonus : Bonus
{
	public FireEffect firePrefab;

	protected override void DoAction (Ball ball)
	{
		Vector3 direction = (transform.position - ball.transform.position).normalized;
		Vector3 force = -direction * 400;

		prepareFire (ball);
		if (cell != null) {
			cell.ExplodeWalls ();
		}

		Rigidbody body = ball.GetComponent<Rigidbody> ();
		if (body != null) {
			body.AddForce (force);
		}
	}

	private void prepareFire (Ball ball)
	{
		FireEffect fire = ball.GetComponent<FireEffect> ();

		if (fire != null)
			Destroy (fire.gameObject);
		
		fire = Instantiate (firePrefab, ball.transform) as FireEffect;
	}
}
