using System.Collections.Generic;

public class PickupModel : GameModel
{
    private int _currentScore, _highestScore;

    public int CurrentScore => _currentScore;
    public int HighestScore => _highestScore;

    public override string GetSceneName()
    {
        return string.Empty;
    }
}
