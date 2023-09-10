public interface IEquation
{
    float Map(float x);
}

/// <summary> һԪһ�η��� // </summary>
public struct LinearEquation : IEquation
{
    public float k, b;

    public float Map(float x)
    {
        return k * x + b;
    }
}