using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{	
	private const float DURATION = 2.2f;

	private Ball ball;
	List<MazeCellEdge> touchedEdges;

	void Start ()
	{
		StopAllCoroutines ();
		ball = GetComponentInParent<Ball> ();
		StartCoroutine (PerformGhost ());
	}

	void OnTriggerEnter (Collider collider)
	{
		MazeCellEdge touchedEdge = collider.gameObject.GetComponentInParent<MazeWall> ();
		if (touchedEdge != null) {
			touchedEdges.Add (touchedEdge);
			touchedEdge.GetComponentInChildren <BoxCollider> ().enabled = false;
		}
	}

	private IEnumerator PerformGhost ()
	{
		SetAlpha (0.3f);
		touchedEdges = new List<MazeCellEdge> ();
		WaitForSeconds wait = new WaitForSeconds (DURATION);
		yield return wait;
		foreach (MazeCellEdge edge in touchedEdges) {
			edge.GetComponentInChildren <Collider> ().enabled = true;
		}
		SetAlpha (1f);
		Destroy (this.gameObject);
	}

	private void SetAlpha (float alpha)
	{
		Renderer renderer = ball.childBallSphere.GetComponentInChildren<Renderer> ();

		Color newColor = new Color (1, 1, 1, alpha);

		renderer.material.color = newColor;

		//GetComponentInParent<Renderer> ().material.color = newColor;
	}
}
