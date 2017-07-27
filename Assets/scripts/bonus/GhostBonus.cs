using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBonus : Bonus {

	public GhostEffect ghostEffectPrefab;

	protected override void DoAction (Ball ball)
	{
		GhostEffect ghostEffectInstance = Instantiate (ghostEffectPrefab, ball.transform) as GhostEffect;
	}
}
