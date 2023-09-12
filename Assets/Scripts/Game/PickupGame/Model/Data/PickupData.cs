using TMPro.EditorUtilities;
using UnityEngine;

public class AppleData
{
    private static int[] AppleScore = new int[] { 3, 5, 7 };
    private static float[] AppleSpeed = new float[] { 8f, 10f, 13f };
    private static Color[] AppleColor = new Color[] { Color.yellow, Color.cyan, Color.red };

    private int _apple;

    public AppleData() => _apple = Random.Range(0, 3);

    public int Score => AppleScore[_apple];
    public float Speed => AppleSpeed[_apple];
    public Color Color => AppleColor[_apple];
}

public class PickupBucketData
{
    private static float[] StageSpeed = new float[]
    {
        8f, 10f, 12f, 14f, 17f
    };

    /// <summary> ºÁÃë // </summary>
    private static int[] StageInterval = new int[]
    {
        1500, 1300, 1200, 1000, 900, 800, 700
    };

    private static int[] StageScroe = new int[]
    {
        20, 30, 40, 50, 60, 70
    };

    /// <summary> Ë÷Òý /// </summary>
    private int _stage;
    public float Speed => StageSpeed[_stage >= StageSpeed.Length ? StageSpeed.Length - 1 : _stage];
    public int Interval => StageInterval[_stage >= StageInterval.Length ? StageInterval.Length - 1 : _stage];

    public void UpState(int score)
    {
        int i = -1;
        if (score <= StageScroe[StageScroe.Length - 1])
        {
            while (++i < StageScroe.Length)
            {
                if (score - StageScroe[i] <= 0)
                {
                    _stage = i;
                    break;
                }
            }
        }
        else
        {
            ++_stage;
        }
    }

    public void Reset() => _stage = 0;
}