@echo off
REM Wrapper for Unity Smart Merge (Windows)

set UNITY_MERGE="C:\Program Files\Unity\Hub\Editor\6000.0.27f1\Editor\Data\Tools\UnityYAMLMerge.exe"
%UNITY_MERGE% merge -p %1 %2 %3 %4