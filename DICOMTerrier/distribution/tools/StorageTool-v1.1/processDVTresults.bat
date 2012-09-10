@echo off
SET PROGRAM=%0
SET BIN=%~dp0

del %2\*.pix
del %2\*.idx

"%BIN%resources\Java\bin\java.exe" -cp "%BIN%resources\utility.jar;%BIN%resources\xercesImpl.jar;%BIN%resources\xercesSamples.jar;%BIN%resources\xml-apis.jar" dicomxml.XMLRenamer %1 %2