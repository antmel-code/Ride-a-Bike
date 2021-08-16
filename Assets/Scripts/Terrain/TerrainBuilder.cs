using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
    [SerializeField]
    TerrainChunck chunckPrefab;

    /// <summary>
    /// Controlled vehicle for centering terrain
    /// </summary>
    [SerializeField]
    VehicleController vehicle;

    [SerializeField]
    int chunckCount = 5;
    Deque<TerrainChunck> chuncks = new Deque<TerrainChunck>();

    int lastChunck = 0;
    int firstChunck = 0;

    private void Awake()
    {
        vehicle.OnUpdate += UpdateTerrain;
    }

    private void OnDestroy()
    {
        vehicle.OnUpdate -= UpdateTerrain;
    }

    private void Start()
    {
        CreateTerrain();
    }

    void CreateTerrain()
    {
        for (int i = 0; i < chunckCount; i ++)
        {
            TerrainChunck newChunck = Instantiate(chunckPrefab);
            newChunck.transform.position = new Vector2(i * chunckPrefab.ChunckWidth, 0f);
            newChunck.transform.SetParent(transform, false);
            chuncks.AddLast(newChunck);
        }
        firstChunck = 0;
        lastChunck = chunckCount - 1;
    }

    void UpdateTerrain()
    {
        CenterTerrain(vehicle.transform.position);
    }

    void CenterTerrain(Vector3 position)
    {
        int centerChunck = Mathf.RoundToInt(position.x / chunckPrefab.ChunckWidth);
        Debug.Log(centerChunck);
        int half = chunckCount / 2;
        int newFirstChunck = chunckCount % 2 != 0 ? centerChunck - half : centerChunck - (half - 1);
        int newLastChunck = centerChunck + half;
        while (lastChunck != newLastChunck)
        {
            if (lastChunck > newLastChunck)
            {
                lastChunck--;
                firstChunck--;
                Destroy(chuncks.RemoveLast().gameObject);
                TerrainChunck newChunck = Instantiate(chunckPrefab);
                newChunck.transform.position = new Vector2(firstChunck * chunckPrefab.ChunckWidth, 0f);
                newChunck.transform.SetParent(transform, false);
                chuncks.AddFirst(newChunck);
            }
            else
            {
                lastChunck++;
                firstChunck++;
                Destroy(chuncks.RemoveFirst().gameObject);
                TerrainChunck newChunck = Instantiate(chunckPrefab);
                newChunck.transform.position = new Vector2(lastChunck * chunckPrefab.ChunckWidth, 0f);
                newChunck.transform.SetParent(transform, false);
                chuncks.AddLast(newChunck);
            }
        }
    }
}
