using System.Text.Json;
using EccSDK.models;

namespace CustomBlockChainLab.Keys.Models;

public class KeyData
{
    public KeyPair? KeyPair { get; set; }
    public SessionKey? SessionKey { get; set; }

    public void SaveKeys(string path)
    {
        var keyWithJson= JsonSerializer.Serialize(new KeyData {KeyPair = KeyPair, SessionKey = SessionKey});
        
        File.WriteAllText(path, keyWithJson);
    }
}