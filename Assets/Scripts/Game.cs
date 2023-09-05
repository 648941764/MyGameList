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
    1 �������ǣ�С�񣩣�����
    2 �ñ�����ʼ�ƶ�������С������ƶ�
    3 �����ϰ�������,ÿ��С���������ӵ�ʱ����Ϸ�ͽ�����
    4 ��������ÿ�ε�С�񴩹����ӵ�ʱ�������һ������Ϸ������ʱ���⵱ǰ�����Ƿ����֮ǰ��¼����������������ڣ���ô�ͽ��и���
    

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
