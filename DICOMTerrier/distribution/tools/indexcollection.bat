@echo off
IF EXIST index1.log (call index.bat %1 > index2.log && del index1.log) ELSE (call index.bat %1 > index1.log && del index2.log)
pause