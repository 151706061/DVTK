@echo off

time /T

echo "Stopping web domain..."
call asadmin stop-domain domain1

echo "Building up folderlist"
if exist %DICOMTERRIER_HOME%\\var\\desktop.spec del %DICOMTERRIER_HOME%\\var\\desktop.spec
call %DICOMTERRIER_HOME%/tools/collectiontools/listfilesforindexing "\\GVB\LocalImageDB" %DICOMTERRIER_HOME%\\var\\desktop.spec

echo "Creating direct index..."
call %DICOMTERRIER_HOME%/bin/desktop_dicomterrier.bat --rundirectindex

echo "Creating inverted index..."
call %DICOMTERRIER_HOME%/bin/desktop_dicomterrier.bat --runinvertedindex

echo "Starting web domain..."
call asadmin start-domain domain1

echo "Index finished"

time /T

pause