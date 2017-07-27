using System;

[System.Serializable]
public struct CellRates
{
	public int emptyWeight,holeWeight,bonusWeight;

	public CellRates(int emptyWeight,int holeWeight, int bonusWeight)
	{
		this.emptyWeight = emptyWeight;
		this.holeWeight = holeWeight;
		this.bonusWeight = bonusWeight;
	}
}
