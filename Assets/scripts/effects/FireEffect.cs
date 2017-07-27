using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{

	private const float DURATION = 5;
	private float startTime;

    public bool Firing { get; set; }

    public FireParticleSystem fireParticle;

    private Ball ball;

    void Start()
    {
        ball = GetComponentInParent<Ball>();
		startTime = Time.time;
	}

    void Update()
    {
		if (ball != null) {
			transform.position = ball.transform.position + Vector3.up * ball.size / 3;
			transform.LookAt (Vector3.up);
		}

		if (Time.time - startTime > DURATION)
			Destroy (gameObject);
    }
}
