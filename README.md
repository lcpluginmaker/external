
# LeoConsole-external-scripts

This plugin enables you to run own scripts or external programs from the
[LeoConsole](https://github.com/BoettcherDasOriginal/LeoConsole).

## Installation

External is in the main repository. Just type

```
apkg get external
```

## Usage

This plugin adds following commands to your console:

### `<plugin-name> [args...]`

Runs a Go plugin. Any executable in `$SAVEPATH/share/go-plugin` is considered a
Go plugin. Use the [gilc](https://github.com/alexcoder04/gilc) library for
writing Go plugins.

### `<script-name> [args...]`

Runs a script. It can be a Shell, Python, ... script or even a binary. Every
executable in `$SAVEPATH/share/scripts` is considered a script.

The plugin passes the data it can get from LeoConsole in json format encoded
in base64 as the first command-line argument, following the arguments the user
entereddin LeoConsole. Example:

```text
$SAVEPATH/share/scripts/<script-name> eyJVc2VybmFtZSI6InVzZXIwMSIsIlNhdmVQYXRoIjoiL2hvbWUvdXNlcjAxL0xlb0NvbnNvbGUvZGF0YSIsIkRvd25sb2FkUGF0aCI6Ii9ob21lL3VzZXIwMS9MZW9Db25zb2xlL2RhdGEvdG1wIiwiVmVyc2lvbiI6IjIuMC4wIn0K [args...]
```

This base64 string decodes to

```json
{"Username":"user01","SavePath":"/home/user01/LeoConsole/data","DownloadPath":"/home/user01/LeoConsole/data/tmp","Version":"2.0.0"}
```

### `exec <program> [args...]`

Executes an arbitrary binary (from your `$PATH`) or using an absolute path.

