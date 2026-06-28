#! /bin/sh

MOD_DIR="$HOME/.config/unity3d/Freehold Games/CavesOfQud/Mods/vifix"

mkdir -p "$MOD_DIR"
cp -r ./common "$MOD_DIR"
cp -r ./current "$MOD_DIR"
cp ./vifix_icon.png "$MOD_DIR"
cp ./manifest.json "$MOD_DIR"
cp ./workshop.json "$MOD_DIR"

