# Use Extism PDK for .Net to send HTTP(s) requests from inside WASM

This devcontainer is configured to provide you a DotNet SDK 8.0 and wasi-sdk 20.0.

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/wasm-outbound-http-examples/extism-dotnet-pdk)


## Instructions for this devcontainer

### Preparation

1. Open this repo in devcontainer, e.g. using Github Codespaces.
   Type or copy/paste following commands to devcontainer's terminal.


2. Install the WASI workload:

```sh
dotnet workload install wasi-experimental
```

3. Generate WASI-enabled .Net project:

```sh
dotnet new wasiconsole -o HTTPRequestingPlugin
```

4. `cd` into that project's directory:

```sh
cd HTTPRequestingPlugin
```

5. Add Extism PDK dependency to project:

```sh
dotnet add package Extism.Pdk --prerelease
```

6. Patch project configuration to produce solid standalone WASM file ( [info](https://github.com/extism/dotnet-pdk#installation) ):

```sh
sed -si.bak 's|<PublishTrimmed>true</PublishTrimmed>|& \n\n<WasmSingleFileBundle>true</WasmSingleFileBundle>\n<WasmBuildNative>true</WasmBuildNative>\n|' HTTPRequestingPlugin.csproj 
```

7. Copy HTTP example source code into project's folder to replace generated one:

```sh
cp ../Program.cs . 
```

### Building

1. Compile the example:

```sh
dotnet build
```

Build process creates 25M-sized `HTTPRequestingPlugin/bin/Debug/net8.0/wasi-wasm/AppBundle/HTTPRequestingPlugin.wasm` bundle.

### Test with Extism CLI

For testing purposes, you can invoke functions from Extism plugins with [Extism CLI](https://github.com/extism/cli).

1. Install `Extism CLI` from Github releases: 

```sh
wget https://github.com/extism/cli/releases/download/v0.3.0/extism-v0.3.0-linux-amd64.tar.gz
tar -xzf extism-v0.3.0-linux-amd64.tar.gz
```

And now you have `extism` binary in current folder.

2. Run default function (it's `_start`) from extism plugin with CLI, allowing outbound connections to all hosts:

```sh
./extism call ./bin/Debug/net8.0/wasi-wasm/AppBundle/HTTPRequestingPlugin.wasm _start --allow-host '*' --wasi
```

### Finish

Perform your own experiments if desired.

---

This devcontainer is based on [dev-wasm/dev-wasm-dotnet](https://github.com/dev-wasm/dev-wasm-dotnet).

<sub>Created for (wannabe-awesome) [list](https://github.com/vasilev/HTTP-request-from-inside-WASM)</sub>
