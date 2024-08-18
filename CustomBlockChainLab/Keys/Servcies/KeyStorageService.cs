using System.Text.Json;
using CustomBlockChainLab.Keys.Models;
using EccSDK;

namespace CustomBlockChainLab.Keys.Servcies;

public class KeyStorageService
{
    private const string KeysPath = "~/keys.json";

    public KeyData LoadKeyPair()
    {
        if (File.Exists(KeysPath))
        {
            return RestoreKeys(KeysPath);
        }

        var keyData = new KeyData()
        {
            KeyPair = EccGenerator.GenerateKeyPair(256),
            SessionKey = SessionKeyGenerator.GenerateSessionKey()
        };
        keyData.SaveKeys(KeysPath);
        return keyData;
    }

    private KeyData RestoreKeys(string path)
    {
        var keyData = File.ReadAllText(path);
        return JsonSerializer.Deserialize<KeyData>(keyData)!;
    }
}