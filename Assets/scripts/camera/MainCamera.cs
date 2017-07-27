using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public int speed;
    public GameObject Target { get; set; }
    private Vector3 offsetDefault;
    private Quaternion defaultRotation = Quaternion.Euler(60, 0, 0);
    public GameManager gameManager;

    public Vector3 offsetForce;

    private Component lastComponentTargeted = null;

    void Start()
    {
        offsetForce = Vector3.zero;
        offsetDefault = new Vector3(0, 3.5f, -1.5f); ;
        transform.position = offsetDefault;
        transform.rotation = defaultRotation;
    }

    void Update()
    {

        if (Target != null)
        {
            Component ballComponent = Target.GetComponent<Ball>();
            Component mazeComponent = Target.GetComponent<Maze>();

            if (ballComponent != null)
            {
                Vector3 desiredPosition = Target.transform.position + offsetDefault + offsetForce;

                Vector3 direction = (desiredPosition - transform.position);
                Vector3 translation = (direction) * speed * Time.deltaTime;

                transform.Translate(translation);
                transform.rotation = defaultRotation;
            }
            else if (mazeComponent != null)
            {
                if (lastComponentTargeted != mazeComponent)
                {
                    Vector3 targetedPosition = (2 + gameManager.level * 0.2f) * offsetDefault;
                    transform.position = targetedPosition;
                }
                transform.LookAt(Target.transform);
            }
        }
    }
}
