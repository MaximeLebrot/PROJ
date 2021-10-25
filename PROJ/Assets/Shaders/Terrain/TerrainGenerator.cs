using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BiomeSetting
{
    public float maxHeight;
    public FastNoiseLite.NoiseType noiseType;
    public float noiseFrequency;
    public FastNoiseLite.FractalType fractalType;
    public int octaves;
    public float lacunarity;
    public float gain;
    public float weightedStrength;
}

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class TerrainGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] [Range(1, 256)] private int chunkSize;
    [SerializeField] private float scale = 1;
    [SerializeField] private Vector2 offset;
    [SerializeField] private string seed;
    [SerializeField] private bool randomSeed;

    public BiomeSetting settings;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public MeshFilter MeshFilter
    {
        get
        {
            if (meshFilter == null)
            {
                meshFilter = GetComponent<MeshFilter>();
            }
            return meshFilter;
        }
    }

    public MeshCollider MeshCollider
    {
        get
        {
            if (meshCollider == null)
            {
                meshCollider = GetComponent<MeshCollider>();
            }
            return meshCollider;
        }
    }

    Color32[] colors32;

    public void Start()
    {
        GenerateMap();
    }

    [ContextMenu("Generate")]
    public void GenerateMap()
    {    
        float[] noise = new float[chunkSize * chunkSize];       
        noise = GenerateNoise(settings);

        Mesh mesh = MeshGenerator.GenerateTerrainMesh(chunkSize, scale, noise).GenerateMesh();

        mesh.colors32 = colors32;
        MeshFilter.sharedMesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    private float[] GenerateNoise(BiomeSetting biomeSetting)
    {
        FastNoiseLite noiseGenerator = new FastNoiseLite();
        
        noiseGenerator.SetSeed(randomSeed ? Random.Range(-1000000, 1000000) : seed.GetHashCode());
        noiseGenerator.SetNoiseType(biomeSetting.noiseType);
        noiseGenerator.SetFrequency(biomeSetting.noiseFrequency);
        noiseGenerator.SetFractalType(biomeSetting.fractalType);
        noiseGenerator.SetFractalOctaves(biomeSetting.octaves);
        noiseGenerator.SetFractalLacunarity(biomeSetting.lacunarity);
        noiseGenerator.SetFractalGain(biomeSetting.gain);
        noiseGenerator.SetFractalWeightedStrength(biomeSetting.weightedStrength);

        float[] noise = new float[chunkSize * chunkSize];
        int i = 0;
        for (float y = 0; y < chunkSize; y++)
        {
            for (float x = 0; x < chunkSize; x++)
            {
                noise[i++] += noiseGenerator.GetNoise(x + offset.x, y + offset.y) * biomeSetting.maxHeight;
            }
        }
        return noise;
    }
}