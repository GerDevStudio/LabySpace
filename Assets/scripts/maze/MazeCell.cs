using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    void Start()
    {
        //GetComponent<Renderer>().material.color = new Color(0, 255, 0);
    }
		
    void Update()
    {

    }

    private int initializedEdgeCount;

    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }


    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    //help choose path during generation algorithm. we need to call to choose direction each generation step
    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }

	public void ExplodeWalls()
	{
		for (int direction = 0; direction < MazeDirections.Count; direction++)
		{
			MazeCellEdge edge = edges [direction];
			if (edge is MazeWall) {
				MazeWall wall = (MazeWall)edge;
				wall.Explose ((MazeDirection)direction);
			}
		}
	}

}
