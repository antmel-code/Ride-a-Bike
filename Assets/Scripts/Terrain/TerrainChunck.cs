using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class TerrainChunck : MonoBehaviour
{
    /// <summary>
    /// Number of segments
    /// </summary>
    [SerializeField]
    int stepsCount = 10;

    /// <summary>
    /// Size of sigments
    /// </summary>
    [SerializeField]
    float stepSize = 1;

    /// <summary>
    /// 1D Noise
    /// </summary>
    [SerializeField]
    MyNoise noise;

    EdgeCollider2D collider;
    LineRenderer renderer;

    public float ChunckWidth
    {
        get => stepSize * stepsCount;
    }
    private void Awake()
    {
        collider = GetComponent<EdgeCollider2D>();
        renderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateTerrain();
    }

    void CreateTerrain()
    {
        List<Vector2> points = new List<Vector2>();
        List<Vector3> pointsV3 = new List<Vector3>();
        for (int i = 0; i <= stepsCount; i++)
        {
            Vector2 newPoint = new Vector2(i * stepSize, noise.Get(i * stepSize + transform.position.x));
            points.Add(newPoint);
            pointsV3.Add(newPoint);
        }
        collider.points = points.ToArray();
        renderer.positionCount = stepsCount + 1;
        renderer.useWorldSpace = false;
        renderer.SetPositions(pointsV3.ToArray());
    }
}
