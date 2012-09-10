@echo off
SET DIR=%1

FOR /D %%i IN (%DIR%\*) DO du -s %%i\meta 
FOR /D %%j IN (%DIR%\*) DO du -s %%j\representations

@echo on