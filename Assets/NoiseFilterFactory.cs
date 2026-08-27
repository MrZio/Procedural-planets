using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings settings)
    {
        // Controlliamo l'enum invece della stringa testuale
        if (settings.filterType == NoiseSettings.FilterType.Simple)
        {
            return new SimpleNoiseFilter(settings);
        }
        else if (settings.filterType == NoiseSettings.FilterType.Rigid)
        {
            return new RigidNoiseFilter(settings);
        }
        else
        {
            // Gestione dell'errore in console e ritorno nullo
            Debug.LogError("Tipo di filtro non riconosciuto o non supportato!");
            return null;
        }
    }
}