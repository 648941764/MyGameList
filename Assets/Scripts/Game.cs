using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    protected int _point;
    protected int _maxPoint;

    public int Point => _point;
    public int MaxPoint => _maxPoint;

    protected virtual void Awake() { }
    protected virtual void Start() { }
    public virtual void Update() { GameLogics(); }

    protected abstract void CalcPoint();

    protected abstract void GameLogics();

    /* Flyppy Brid
    1 制作主角（小鸟），动画
    2 让背景开始移动或者让小鸟进行移动
    3 制作障碍物柱子,每当小鸟碰到柱子的时候游戏就结束，
    4 分数管理，每次当小鸟穿过柱子的时候分数加一。当游戏结束的时候检测当前分数是否大于之前记录的最大分数，如果大于，那么就进行覆盖
    

    */
}
public class FlappyBird : Game
{
    protected override void CalcPoint()
    {
        throw new System.NotImplementedException();
    }

    protected override void GameLogics()
    {

    }
}
