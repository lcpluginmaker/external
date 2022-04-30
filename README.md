
# LeoConsole-external-scripts

This plugin enables you to run own scripts or external programs from the
[LeoConsole](https://github.com/BoettcherDasOriginal/LeoConsole).

## Installation

### Default (with `pkg`)

In LeoConsole, type

```
pkg update
pkg get external-scripts
```

### Bleeding-edge (from git)

In LeoConsole, type

```
apkg update
apkg get https://github.com/alexcoder04/LeoConsole-external-scripts.git
```

## Usage

It adds following commands to your console:

### `script <name>`

Runs a script. It can be a Shell, Python, ... script or even a binary. Every
executable in `$SAVEPATH/script` is considered a script. Following arguments
are passed to the script (args are arguments you passed inside LeoConsole):

```text
username savepath downloadpath args...
```

### `scripts-list`

Lists all scripts (executables) from `$SAVEPATH/scripts`.

### `exec`

Executes an arbitrary binary (from your `$PATH`).

