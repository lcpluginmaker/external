#!/bin/sh

echo "This is an example shell script"

username="$1"; shift
savepath="$1"; shift
dlpath="$1"; shift

cat <<EOF

---

Data I got from LeoConsole:
 - data.User.name: $username
 - data.SavePath: $savepath
 - data.DownloadPath: $dlpath
 - args: $@

EOF

