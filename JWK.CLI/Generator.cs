using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace JWK.CLI;

public static class Generator
{
    private static readonly Dictionary<KeyUsage, string> UsageToString = new()
    {
        { KeyUsage.Encryption, "enc" },
        { KeyUsage.Signature, "sig" }
    };
    
    private static readonly Dictionary<string, int> AlgorithmToKeySize = new()
    {
        { SecurityAlgorithms.RsaSha256, 512 }
    };

    private static RsaSecurityKey GenerateRsaKey(Options options)
    {
        using var rsa = RSA.Create();
        rsa.KeySize = options.KeySize;
        var parameters = rsa.ExportParameters(true);
        return new RsaSecurityKey(parameters) { KeyId = options.KeyId };
    }

    private static JsonWebKey ConvertFromRsaKey(RsaSecurityKey key, Options options)
    {
        var withPrivateKey = !options.HidePrivateKey;
        var parameters = key.Rsa?.ExportParameters(withPrivateKey) ?? key.Parameters;
        var result = new JsonWebKey
        {
            Kty = JsonWebAlgorithmsKeyTypes.RSA,
            Kid = key.KeyId,
            Use = UsageToString[options.KeyUsage],
            Alg = options.Algorithm,
            N = parameters.Modulus == null ? null : Base64UrlEncoder.Encode(parameters.Modulus),
            E = parameters.Exponent == null ? null : Base64UrlEncoder.Encode(parameters.Exponent)
        };

        if (!withPrivateKey)
        {
            return result;
        }

        result.P = parameters.P == null ? null : Base64UrlEncoder.Encode(parameters.P);
        result.Q = parameters.Q == null ? null : Base64UrlEncoder.Encode(parameters.Q);
        result.D = parameters.D == null ? null : Base64UrlEncoder.Encode(parameters.D);
        result.DQ = parameters.DQ == null ? null : Base64UrlEncoder.Encode(parameters.DQ);
        result.DP = parameters.DP == null ? null : Base64UrlEncoder.Encode(parameters.DP);
        result.QI = parameters.InverseQ == null ? null : Base64UrlEncoder.Encode(parameters.InverseQ);
        return result;
    }

    public static JsonWebKey Generate(Options options)
    {
        // TODO: support other algorithms
        if (!options.Algorithm.Equals(SecurityAlgorithms.RsaSha256, StringComparison.OrdinalIgnoreCase))
        {
            throw new NotSupportedException("Only RSA with Sha256 is supported at the moment!");
        }

        var expectedKeyLength = AlgorithmToKeySize[options.Algorithm];
        if (options.KeySize < expectedKeyLength)
        {
            throw new NotSupportedException(
                $"This algorithm requires a key length of at least {expectedKeyLength}!");
        }

        var rsa = GenerateRsaKey(options);
        return ConvertFromRsaKey(rsa, options);
    }
}