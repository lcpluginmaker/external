#!/bin/sh

echo "This is an example shell script for LeoConsole"

data="$1"; shift
echo "raw data: $data"
decoded_data="$(echo "$data" | base64 -d)"
echo "decoded data: $decoded_data"

username="$(echo "$decoded_data" | jq -r ".Username")"
savepath="$(echo "$decoded_data" | jq -r ".SavePath")"
downloadpath="$(echo "$decoded_data" | jq -r ".DownloadPath")"
version="$(echo "$decoded_data" | jq -r ".Version")"

cat <<EOF

Data I got from LeoConsole:

username:     '$username'
savepath:     '$savepath'
downloadpath: '$downloadpath'
version:      '$version'

EOF

