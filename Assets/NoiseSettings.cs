using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    // 1. Definiamo i tipi di filtro disponibili
    public enum FilterType { Simple, Rigid }
    public FilterType filterType;

    public float strength = 1f;
    [Range(1, 8)]
    public int numLayers = 1;
    public float baseRoughness = 1f;
    public float roughness = 2f;
    public float persistence = 0.5f;
    public Vector3 centre;
    // ... tutte le tue variabili precedenti (strength, numLayers, ecc.)
    public float minValue;

    // Nuovo parametro per controllare la nitidezza delle valli nel Rigid Noise
    public float weightMultiplier = 0.8f;
}
