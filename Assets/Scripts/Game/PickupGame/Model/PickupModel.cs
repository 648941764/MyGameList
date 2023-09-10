using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class PickupModel : GameModel
{
    private const string
        CURRENT_KEY = "PickupCurrent",
        HIGHEST_KEY  = "PickupHigh";

    private int _currentScore, _highestScore;

    public int CurrentScore => _currentScore;
    public int HighestScore => _highestScore;

    PickupBucketData _bucketData = new PickupBucketData();

    public override string GetSceneName()
    {
        return string.Empty;
    }

    public void AddScore(AppleData apple)
    {
        _currentScore += apple.Score;
        if (_currentScore >= _highestScore)
        {
            _highestScore = _currentScore;
        }
        _bucketData.UpState(_currentScore);
    }

    public void ResetScore() => _currentScore = 0;
    public void ResetAllScore() { _currentScore = 0; _highestScore = 0; }

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
