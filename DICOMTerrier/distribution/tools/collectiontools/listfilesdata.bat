@echo off
SET DIR=%1

FOR /D %%i IN (%DIR%\DATA*) DO @echo %%i

@echo on