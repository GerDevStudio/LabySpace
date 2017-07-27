using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalBonus : Bonus {

	public OvalEffect ovalEffectPrefab;

	protected override void DoAction (Ball ball)
	{
		OvalEffect ovalEffect = ball.GetComponentInChildren<OvalEffect> ();

		if (ovalEffect != null)
			ovalEffect.Restart ();
		else {
			ovalEffect = Instantiate (ovalEffectPrefab, ball.transform) as OvalEffect;
		}
	}
}
