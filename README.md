
# LeoConsole-external-scripts

This plugin enables you to run own scripts or external programs from the
[LeoConsole](https://github.com/BoettcherDasOriginal/LeoConsole).

## Installation

External-scripts is in the main repository. Just type

```
apkg get external-scripts
```

## Usage

This plugin adds following commands to your console:

### `<script-name> [args...]`

Runs a script. It can be a Shell, Python, ... script or even a binary. Every
executable in `$SAVEPATH/share/scripts` is considered a script.

The plugin passes the data it can get from LeoConsole in json format encoded
in base64 as the first command-line argument, following the arguments the user
entereddin LeoConsole.Example:

```text
<plugin path> eyJVc2VybmFtZSI6InVzZXIwMSIsIlNhdmVQYXRoIjoiL2hvbWUvdXNlcjAxL0xlb0NvbnNvbGUvZGF0YSIsIkRvd25sb2FkUGF0aCI6Ii9ob21lL3VzZXIwMS9MZW9Db25zb2xlL2RhdGEvdG1wIiwiVmVyc2lvbiI6IjIuMC4wIn0K arg1 arg2 arg2
```

This base64 string decodes to

```json
{"Username":"user01","SavePath":"/home/user01/LeoConsole/data","DownloadPath":"/home/user01/LeoConsole/data/tmp","Version":"2.0.0"}
```

**Warning**: if one of the variables username, savepath or downloadpath has
spaces, these will be replaced with dashes (`-`). This is also the case if the
variable is empty or not defined.

### `scripts-list`

Lists all scripts (executables) from `$SAVEPATH/share/scripts`.

### `exec <program> [args...]`

Executes an arbitrary binary (from your `$PATH`).

