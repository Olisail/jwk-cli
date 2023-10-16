using CommandLine;
using JWK.CLI;
using JWK.CLI.Extensions;
using Microsoft.IdentityModel.Tokens;

Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
{
    JsonWebKey? jwk = null;
    if (!o.HidePrivateKey)
    {
        jwk = Generator.Generate(o);
        Console.WriteLine("JWK w/ Private Data:");
        Console.WriteLine(jwk.ToIndentedJson());
        Console.WriteLine("\n");
    }
    
    Console.WriteLine("Public JWK:");
    var publicJwk = jwk?.ToPublicKey() ?? Generator.Generate(o);
    Console.WriteLine(publicJwk.ToIndentedJson());
});