public class MinMax
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMax()
    {
        // Inizializziamo con valori limite invertiti
        Min = float.MaxValue;
        Max = float.MinValue;
    }

    public void AddValue(float v)
    {
        if (v > Max)
        {
            Max = v;
        }
        if (v < Min)
        {
            Min = v;
        }
    }
}