using UnityEngine;

public class ColorGenerator
{
    private ColorSettings settings;
    private Texture2D texture;
    private const int textureResolution = 50;

    public void UpdateSettings(ColorSettings settings)
    {
        this.settings = settings;
        if (texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        // Invia i valori di quota minima e massima allo Shader
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColors()
    {
        Color[] colours = new Color[textureResolution];

        for (int i = 0; i < textureResolution; i++)
        {
            // 1. Calcola la percentuale t tra 0 e 1 per il pixel corrente
            float t = i / (textureResolution - 1f);
            // 2. Estrai il colore dal gradiente usando settings.gradient.Evaluate(t)
            Color colors = settings.gradient.Evaluate(t);

            // 3. Salva il colore nell'array colours[i]
            colours[i] = colors;
        }

        texture.SetPixels(colours);
        texture.Apply();

        // Passa la texture generata alla proprietà "_texture" del materiale
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}