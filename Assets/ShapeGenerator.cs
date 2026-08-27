using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings settings;
    private INoiseFilter noiseFilter;

    public MinMax elevationMinMax;

    public ShapeGenerator(ShapeSettings settings)
    {
        this.settings = settings;
        this.noiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.noiseSettings);

        elevationMinMax = new MinMax();
    }





    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.Evaluate(pointOnUnitSphere);

        float unscaledElevation = settings.planetRadius * (1 + elevation);
        return pointOnUnitSphere * unscaledElevation;
    }
}
