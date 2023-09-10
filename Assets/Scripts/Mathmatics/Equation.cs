public interface IEquation
{
    float Map(float x);
}

/// <summary> 一元一次方程 // </summary>
public struct LinearEquation : IEquation
{
    public float k, b;

    public float Map(float x)
    {
        return k * x + b;
    }
}