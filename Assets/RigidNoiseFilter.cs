using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    private NoiseSettings settings;
    private SimplexNoise noise = new SimplexNoise();

    public RigidNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            // 1. Estrazione del rumore base in range [-1, 1]
            float v = noise.Evaluate(point * frequency + settings.centre);

            // 2. Formule di Musgrave (Creste e pendenze ripide)
            v = 1 - Mathf.Abs(v);
            v *= v;

            // 3. Applicazione del peso atmosferico
            v *= weight;

            // 4. Calcolo del nuovo peso per lo strato successivo (dettagli soffocati nelle valli)
            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            // 5. Accumulo nella sommatoria
            noiseValue += v * amplitude;

            // Aggiornamento spazio-frequenza per fBm
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}