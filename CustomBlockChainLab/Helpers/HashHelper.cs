using System.Security.Cryptography;
using System.Text;

namespace CustomBlockChainLab.Helpers;

public static class HashHelper
{
    public static string ToSha256(string data)
    {
        var sha256Hash = SHA256.Create();
        var builder = new StringBuilder();
        
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    } 
}