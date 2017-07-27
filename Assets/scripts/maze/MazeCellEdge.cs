using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {

    public MazeCell firstCell;
    public MazeCell secondCell;
	public MazeDirection direction;

    public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		this.firstCell = cell;
		this.secondCell = otherCell;
		this.direction = direction;
		cell.SetEdge (direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation ();
	}

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
