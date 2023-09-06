using Newtonsoft.Json.Linq;

public interface IPersistent
{
    void Load(JObject jsonObject);
    void Save(JObject jsonObject);
}

public sealed class Persistent : Singleton<Persistent>
{

}