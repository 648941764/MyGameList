public struct LinearEquation
{
    public float k, b;

    public float Map(float x)
    {
        return k * x + b;
    }
}