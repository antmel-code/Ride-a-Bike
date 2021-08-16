using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// My Strange Noise Function (can be better)
/// </summary>
[CreateAssetMenu(fileName = "New Noise", menuName = "Noise")]
public class MyNoise : ScriptableObject
{
    public int levels = 3;
    public float vertncalScale = 1;
    public float horizontalScale = 1;
    public int seed = 999;
    public float smoothness = 0.2f;

    float[] offsets;

    void InitOffsets()
    {
        Random.seed = seed;
        offsets = new float[levels];
        for (int i = 0; i < levels; i ++)
        {
            offsets[i] = Random.Range(0f, 3.14f);
        }
    }
    
    public float Get(float param)
    {
        if (offsets == default || offsets.Length != levels)
        {
            InitOffsets();
        }

        float value = 0;
        for (int i = 0; i < levels; i++)
        {
            // Ahalaymahalay
            value += Mathf.Sin((Mathf.Lerp(param, param * (i + 1), smoothness) / horizontalScale) + offsets[i]) * (vertncalScale / (i + 1));
        }
        return value;
    }
}
