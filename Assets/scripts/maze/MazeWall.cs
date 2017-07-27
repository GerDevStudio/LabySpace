using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge
{
	public MazeWallWood woodWall;
	public Dissapear dissapear;

	private Rigidbody rigidBody;

	void Start () {
		rigidBody = woodWall.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = true;
	}

	public void Explose(MazeDirection direction)
	{

		Vector3 force = 25 * (MazeDirections.toVector3 (direction).normalized + 0.5f*transform.up);

		ExplodeThenDissapear (this,force);

		if (secondCell != null) {
			MazeCellEdge secondEdge = secondCell.GetEdge (direction.GetOpposite ());
			if (secondEdge != null && secondEdge is MazeWall) {
				MazeWall secondWall = (MazeWall)secondEdge;
				ExplodeThenDissapear (secondWall, force);
			}
		}
	}

	private void ExplodeThenDissapear(MazeWall wall,Vector3 force)
	{
		//decrease wall length to fix overlapping and force
		Vector3 localScale = wall.transform.localScale;
		localScale.x = 0.8f;
		wall.transform.localScale = localScale;

		wall.rigidBody.isKinematic = false;
		wall.rigidBody.AddForce(force);

		wall.rigidBody.AddTorque (100, 100, 100);

		wall.dissapear.Perform ();
	}
}
