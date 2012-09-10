SET PROGRAM=%0
SET BIN=%~dp0
cd %BIN%resources
START %BIN%resources\Java\bin\javaw.exe -cp %BIN%resources\storage.jar;%BIN%resources\utility.jar;%BIN%resources\xercesImpl.jar;%BIN%resources\xercesSamples.jar;%BIN%resources\xml-apis.jar -Djava.util.logging.config.file=logging.properties Storage