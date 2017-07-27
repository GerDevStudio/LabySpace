using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellFinish : MazeCell {

    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision col)
	{        
		GameObject collider = col.collider.gameObject;
		if (collider.GetComponent<BallSphere> () != null) {
			gm.LevelFinished ();
		}
	}
}
