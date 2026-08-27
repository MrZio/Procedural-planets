using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    private NoiseSettings settings;
    private SimplexNoise noise = new SimplexNoise();

    public SimpleNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        // 1. Scrivi qui il ciclo for per accumulare il rumore (fBm)
        for(int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.centre);
            v = (v + 1) * 0.5f; // Normalizza il rumore tra 0 e 1
            noiseValue += v * amplitude;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }
        // 2. Scrivi qui la formula per applicare Mathf.Max e strength
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);

        // 3. Ritorna il valore finale
        return noiseValue * settings.strength; // Sostituisci con la tua variabile
    }
}