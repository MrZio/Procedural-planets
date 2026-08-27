using UnityEngine;

public interface INoiseFilter
{
    // Chiunque firmi questo contratto deve avere un metodo Evaluate
    float Evaluate(Vector3 point);
}