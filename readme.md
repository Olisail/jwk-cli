# README for DotNet JWK Generator Tool

## Description
This DotNet tool is designed to generate a JSON Web Key (JWK). It offers several options to customize the key generation.

## Options
Here are the available options for this tool:

- `-s`, `--key-size` : Sets the key size. This option is required.
- `-i`, `--key-id` : Sets the key ID. This option is required and defaults to a new GUID if not provided.

- `-u`, `--key-usage` : Sets the key usage. This option is not required and defaults to `Signature` if not provided.
- `-a`, `--algorithm` : Sets the algorithm. This option is not required and defaults to `RS256` if not provided.
- `-n`, `--no-private-key` : If set, the private key will not be included in the output.

## Usage
To use this tool, run the following command with your desired options:

```bash
dotnet tool install --global JWK.CLI
JWK-CLI -s <key-size> -i <key-id> [-u <key-usage>] [-a <algorithm>] [-n]
```

Replace `<key-size>` and `<key-id>` with your desired key size and key ID. The other options are optional and can be omitted if you want to use the default values. If you want to hide the private key in the output, include the `-n` option.

## Example
Here is an example of how to use this tool:

```bash
JWK-CLI -s 2048 -i my-key-id -u Signature -a RS256
```

This will generate a JWK with a key size of 2048, a key ID of "my-key-id", a key usage of "Signature", and an algorithm of "RS256". The private key will be included in the output.
