using Newtonsoft.Json.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;

public partial class GameManager
{
    private readonly Dictionary<int, Timer> _scheduleList = new Dictionary<int, Timer>();;
    private readonly List<Timer> _addList = new List<Timer>();
    private readonly List<Timer> _delList = new List<Timer>();

    private const float ONE_MILLI_SEC = 0.001f;
    private const int ONE_SEC = 1;

    /// <summary> ��Ϸʱ��, ���룬��APP��һ���п�ʼ��¼���ᱣ�� /// </summary>
    private int _gameTime;
    private float _elapsed;

    private void _UpdateTime()
    {
        if (_addList.Count > 0)
        {
            int i = -1;
            while (++i < _addList.Count)
            {
                _scheduleList.Add(_addList[i].GetHashCode(), _addList[i]);
            }
            _addList.Clear();
        }

        if (_delList.Count > 0)
        {
            int i = -1;
            while (++i < _delList.Count)
            {
                _scheduleList.Remove(_delList[i].GetHashCode());
                TimerPool.ReleaseTimer(_delList[i]);
            }
            _delList.Clear();
        }

        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1����
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
            if (_scheduleList.Count > 0)
            {
                foreach (Timer timer in _scheduleList.Values)
                {
                    if (timer.delay == 0)
                    {
                        if (timer.onSchedule != null && timer.elapsed % timer.interval == 0)
                        {
                            timer.onSchedule.Invoke(timer.duration - timer.elapsed);
                        }
                        if (timer.elapsed == timer.duration)
                        {
                            timer.onComplete?.Invoke();
                            if (--timer.repeat == 0)
                            {
                                _delList.Add(timer);
                            }
                            timer.elapsed = 0;
                            continue;
                        }
                        timer.elapsed += ONE_SEC;
                    }
                    else
                    {
                        timer.delay -= ONE_SEC;
                    }
                }
            }
        }
    }
    
    /// <param name="duration">����ʱ�䣨���룩</param>
    /// <param name="onComplete">����¼�</param>
    /// <param name="onSchedule">ÿ�θ����¼���ʣ��ʱ��</param>
    /// <param name="repeat">�ظ�����</param>
    /// <param name="delay">�Ƴ�ʱ�䣨���룩</param>
    /// <param name="interval">���¼�������룩</param>
    public int Schedule(int duration, Action onComplete = default, Action<int> onSchedule = default, int repeat = 1, int interval = 1, int delay = 0)
    {
        Timer timer = TimerPool.GetTimer();
        timer.beginAt = _gameTime;
        timer.elapsed = 0;
        timer.finishAt = _gameTime + duration;
        timer.onComplete = onComplete;
        timer.onSchedule = onSchedule;
        timer.repeat = repeat;
        timer.delay = delay;
        timer.interval = interval;
        _addList.Add(timer);
        return timer.GetHashCode();
    }

    /// <summary>
    /// ���Ƴ٣�1������ʱ��;
    /// </summary>
    /// <param name="duration">����ʱ�䣨��)</param>
    /// <param name="onComplete"></param>
    /// <param name="onSchedule"></param>
    /// <returns></returns>
    public int Schedule(float duration, Action onComplete = default, Action<int> onSchedule = default, int repeat = 1)
    {
        return Schedule((int)(duration * 1000f), onComplete, onSchedule, repeat, 1000);
    }

    public void Unschedule(int timerIdentifier)
    {
        if (_scheduleList.ContainsKey(timerIdentifier))
        {
            _delList.Add(_scheduleList[timerIdentifier]);
        }
    }

    private class Timer
    {
        public int beginAt, finishAt, elapsed, duration, repeat, interval, delay;
        public Action<int> onSchedule;
        public Action onComplete;

        public override int GetHashCode()
        {
            int hash = 255 + beginAt + finishAt;
            if (onSchedule != null) { hash += onSchedule.GetHashCode(); }
            if (onComplete != null) { hash += onComplete.GetHashCode(); }
            return hash;
        }
    }

    private static class TimerPool
    {
        private static readonly ObjectPool<Timer> _pool = new ObjectPool<Timer>(150);

        public static Timer GetTimer()
        {
            return _pool.Get();
        }

        public static void ReleaseTimer(Timer timer)
        {
            _pool.Release(timer);
        }
    }
}