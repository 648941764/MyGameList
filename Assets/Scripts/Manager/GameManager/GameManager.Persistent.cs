using Newtonsoft.Json.Linq;

public partial class GameManager : IPersistent
{
    private const string GAMETIME_KEY = "gameTime";

    public void Load(JObject jsonObject)
    {
        if (jsonObject.ContainsKey(GAMETIME_KEY))
        {
            _gameTime = (int)jsonObject[GAMETIME_KEY];
        }
    }

    public void Save(JObject jsonObject)
    {
        jsonObject.Add(GAMETIME_KEY, _gameTime);
    }
}
