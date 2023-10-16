using CommandLine;
using Microsoft.IdentityModel.Tokens;

namespace JWK.CLI;

[Verb("generate", HelpText = "Run JWK Generation")]
public class Options
{
    [Option('s', "key-size", Required = true)]
    public required int KeySize { get; set; }

    [Option('i', "key-id", Required = true)]
    public required string KeyId { get; set; } = Guid.NewGuid().ToString();
    
    [Option('u', "key-usage", Required = false)]
    public KeyUsage KeyUsage { get; set; } = KeyUsage.Signature;

    [Option('a', "algorithm", Required = false)]
    public string Algorithm { get; set; } = SecurityAlgorithms.RsaSha256;

    [Option('n', "no-private-key", Required = false)]
    public bool HidePrivateKey { get; set; }
}