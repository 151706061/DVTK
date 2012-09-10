@echo off
REM DicomTerrier version 1.0
REM
REM The Original Code is desktop_terrier.bat
REM 
REM The Initial Developer of the Original Code is the University of Glasgow.
REM Portions created by The Initial Developer are Copyright (C) 2004, 2005
REM the initial Developer. All Rights Reserved.
REM
REM Author Gerald van Veldhuijsen


if "Windows_NT"=="%OS%" setlocal

rem keep %0 in case we overwrite
SET PROGRAM=%0
rem SCRIPT contains the full path filename of this script
SET SCRIPT=%~f0
rem BIN contains the path of the BIN folder
SET BIN=%~dp0

set COLLECTIONPATH=%~f1

REM --------------------------
REM Derive DICOMTERRIER_HOME, DICOMTERRIER_ETC, DICOMTERRIER_LIB
REM --------------------------

if defined DICOMTERRIER_HOME goto dicomterrier_etc
CALL "%BIN%\fq.bat" "%BIN%\.."
SET DICOMTERRIER_HOME=%FQ%
echo Set DICOMTERRIER_HOME to be %DICOMTERRIER_HOME%

:dicomterrier_etc
if defined DICOMTERRIER_ETC goto dicomterrier_lib
SET DICOMTERRIER_ETC=%DICOMTERRIER_HOME%\etc

:dicomterrier_lib
if defined DICOMTERRIER_LIB goto classpath
SET DICOMTERRIER_LIB=%DICOMTERRIER_HOME%\lib

:classpath

REM ------------------------
REM -- Build up class path 
REM ------------------------
call "%BIN%\lcp.bat" %CLASSPATH%
SET LOCALCLASSPATH=
FOR %%i IN ("%DICOMTERRIER_LIB%\*.jar") DO call "%BIN%\lcp.bat" "%%i"


set JAVA_BIN=java

IF "%1"=="--debug" set JAVA_BIN=java

IF "%1"=="--DEBUG" set JAVA_BIN=java

IF "%1"=="-debug" set JAVA_BIN=java

IF "%1"=="-DEBUG" set JAVA_BIN=java


REM ------------------------
REM -- Run DesktopTerrier
REM ------------------------

%JAVA_BIN% -Xss1M -Xmx350M -Dterrier.home="%DICOMTERRIER_HOME%" -Dterrier.setup="%DICOMTERRIER_HOME%\etc\terrier.properties" -cp %LOCALCLASSPATH% uk.ac.gla.terrier.applications.desktop.DesktopTerrier %*

if "Windows_NT"=="%OS%" endlocal
