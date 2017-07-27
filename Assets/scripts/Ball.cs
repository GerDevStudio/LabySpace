using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float size;

    private MainCamera mainCamera;

	public BallSphere childBallSphere {
		get;
		set;
	}

    void Start()
    {
		name = "Ball";

		mainCamera = GameObject.Find("MainCamera").GetComponent<MainCamera>();
        mainCamera.Target = gameObject;

        gameObject.transform.localScale = new Vector3(size, size, size);
        gameObject.transform.position += Vector3.up * size / 2;

		this.childBallSphere = GetComponentInChildren<BallSphere> ();
    }

    void Update()
    {
		
    }

    public void Move(MazeDirection direction, float force)
    {
        if (gameObject != null)
        {
            Vector3 vectorForce = force * MazeDirections.toVector3(direction);
            GetComponent<Rigidbody>().AddForce(vectorForce);
        }
    }
}
