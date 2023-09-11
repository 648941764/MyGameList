using TMPro.EditorUtilities;
using UnityEngine;

public class AppleData
{
    private static int[] AppleScore = new int[] { 3, 5, 7 };
    private static float[] AppleSpeed = new float[] { 4f, 5f, 7f };
    private static Color[] AppleColor = new Color[] { Color.yellow, Color.cyan, Color.red };

    private int _apple;

    public AppleData() => RandomStage();

    public int Score => AppleScore[_apple];
    public float Speed => AppleSpeed[_apple];
    public Color Color => AppleColor[_apple];
    public void RandomStage() => _apple = Random.Range(0, 3);
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
        1300, 1100, 1000, 900, 800, 700
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
        int stage = _stage;
        if (score <= StageScroe[StageScroe.Length - 1])
        {
            while (++i < StageScroe.Length)
            {
                if (score - StageScroe[i] <= 0)
                {
                    stage = i;
                    break;
                }
            }
        }
        else
        {
            ++stage;
        }
        if (stage != _stage)
        {
            _stage = stage;
            EventManager.Instance.Broadcast(ParamPool.Get(EventName.PickupGameStageChange));
        }
    }

    public void Reset() => _stage = 0;
}

public class PickupPlayerData
{
    public const int MAX_HEALTH = 3;

    private int _current;
    public int currentHealth => _current;

    public void ResetHealth()
    {
        _current = MAX_HEALTH;
    }

    public void SetHealth(int health)
    {
        health = Mathf.Clamp(health, 0, MAX_HEALTH);
        _current = health;
    }
}