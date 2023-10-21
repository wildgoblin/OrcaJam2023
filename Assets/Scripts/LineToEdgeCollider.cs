using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LineToEdgeCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        if (lineRenderer == null || edgeCollider == null)
        {
            Debug.LogError("LineToEdgeCollider script requires LineRenderer and EdgeCollider2D components.");
            return;
        }

        UpdateEdgeCollider();
    }

    void UpdateEdgeCollider()
    {
        Vector2[] colliderPoints = new Vector2[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            colliderPoints[i] = lineRenderer.GetPosition(i);
        }

        edgeCollider.points = colliderPoints;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the edge collider whenever the line renderer's positions change
        //UpdateEdgeCollider();
    }
}
