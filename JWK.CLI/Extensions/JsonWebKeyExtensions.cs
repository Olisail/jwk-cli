using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace JWK.CLI.Extensions;

public static class JsonWebKeyExtensions
{
    private static readonly JsonSerializerOptions IndentedJsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public static JsonWebKey ToPublicKey(this JsonWebKey keyWithPrivateData) =>
        new()
        {
            Kty = keyWithPrivateData.Kty,
            Kid = keyWithPrivateData.Kid,
            Use = keyWithPrivateData.Use,
            Alg = keyWithPrivateData.Alg,
            N = keyWithPrivateData.N,
            E = keyWithPrivateData.E
        };

    public static string ToIndentedJson(this JsonWebKey key) =>
        JsonSerializer.Serialize(key, IndentedJsonSerializerOptions);
}