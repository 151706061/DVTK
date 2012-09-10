@echo off
SET DIR=%1
SET BIN=%~dp0

FOR /D %%i IN (%DIR%\*) DO call "%BIN%print" "%%i\meta" %2
FOR /D %%j IN (%DIR%\*) DO call "%BIN%print" "%%j\representations" %2
