using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class PickupModel : GameModel
{
    private const string
        CURRENT_KEY = "PickupCurrent",
        HIGHEST_KEY = "PickupHigh";

    private int _currentScore, _highestScore;
    PickupBucketData _bucketData = new PickupBucketData();
    PickupPlayerData _playerData = new PickupPlayerData();

    public int CurrentScore => _currentScore;
    public int HighestScore => _highestScore;
    public float BucketSpeed => _bucketData.Speed;
    public int ThrowInterval => _bucketData.Interval;

    public override void OnEstablished()
    {
        EnrollEvents(_ =>
        {
            switch (_.eventName)
            {
                case EventName.PickupAppleEscape:
                    {
                        _playerData.SetHealth(_playerData.currentHealth - 1);
                        if (_playerData.currentHealth == 0)
                        {
                            Broadcast(ParamPool.Get(EventName.PickupGameOver));
                        }
                        break;
                    }
            }
        });
        base.OnEstablished();
    }

    public override string GetSceneName()
    {
        return "Pickup";
    }

    public void AddScore(AppleData apple)
    {
        _currentScore += apple.Score;
        if (_currentScore >= _highestScore)
        {
            _highestScore = _currentScore;
            Broadcast(ParamPool.Get(EventName.PickupHighestScoreChange).Push(_highestScore.ToString()));
        }
        _bucketData.UpState(_currentScore);
        Broadcast(ParamPool.Get(EventName.PickupScoreChange).Push(_currentScore.ToString()));
    }
    public void ResetScore() 
    {
        _currentScore = 0; _bucketData.Reset();
        Broadcast(ParamPool.Get(EventName.PickupHighestScoreChange).Push(_highestScore.ToString()));
        Broadcast(ParamPool.Get(EventName.PickupScoreChange).Push(_currentScore.ToString()));
    }
    public void ResetAllScore() 
    {
        _currentScore = 0; _highestScore = 0;
        Broadcast(ParamPool.Get(EventName.PickupHighestScoreChange).Push(_highestScore.ToString()));
        Broadcast(ParamPool.Get(EventName.PickupScoreChange).Push(_currentScore.ToString()));
    }

    public void ResetPlayer()
    {
        _playerData.SetHealth(PickupPlayerData.MAX_HEALTH);
    }

    public int GetPlayerHealth() 
    {
        return _playerData.currentHealth;
    }

    public override void Save(JObject jsonObject)
    {
        jsonObject.Add(CURRENT_KEY, _currentScore);
        jsonObject.Add(HIGHEST_KEY, _highestScore);
    }

    public override void Load(JObject jsonObject)
    {
        if (jsonObject.ContainsKey(CURRENT_KEY))
        {
            _currentScore = (int)jsonObject[CURRENT_KEY];
        }
        if (jsonObject.ContainsKey(HIGHEST_KEY))
        {
            _highestScore = (int)jsonObject[HIGHEST_KEY];
        }
    }
}
