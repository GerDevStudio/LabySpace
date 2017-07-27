using System;

[System.Serializable]
public struct BonusRates
{
	public int ghostWeight,bombWeight,ovalWeight,starWeight;

	public BonusRates(int ghostWeight,int bombWeight, int ovalWeight, int starWeight)
	{
		this.ghostWeight = ghostWeight;
		this.bombWeight = bombWeight;
		this.ovalWeight = ovalWeight;
		this.starWeight = starWeight;
	}
}
