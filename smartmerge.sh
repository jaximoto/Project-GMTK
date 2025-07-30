#!/bin/bash
# Unity Smart Merge wrapper for macOS/Linux/Git Bash

# Change this to your Unity version folder if needed
UNITY_VERSION="6000.0.27f1"
UNITY_MERGE="/Applications/Unity/Hub/Editor/$UNITY_VERSION/Unity.app/Contents/Tools/UnityYAMLMerge"

# Call UnityYAMLMerge with merge -p and all arguments passed by Git
"$UNITY_MERGE" merge -p "$1" "$2" "$3" "$4"