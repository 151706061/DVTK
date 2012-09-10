// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//  Emulator Test Session class.

#ifndef EMULATOR_SESSION_H
#define EMULATOR_SESSION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Inetwork.h"
#include "session.h"		// Base Session include

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_SCP_EMULATOR_CLASS;

//>>***************************************************************************

class EMULATOR_SESSION_CLASS : public BASE_SESSION_CLASS

    //  DESCRIPTION     : Emulator Test Session Class.
    //  INVARIANT       :
    //  NOTES           :
    //<<***************************************************************************
{
private:
    // DVT ACSE Properties
    string							dvtAeTitleM;
    UINT32							dvtMaximumLengthReceivedM;
    string							dvtImplementationClassUidM;
    string							dvtImplementationVersionNameM;

    // SUT ACSE Properties
    string							sutAeTitleM;
    UINT32							sutMaximumLengthReceivedM;
    string							sutImplementationClassUidM;
    string							sutImplementationVersionNameM;
    UP_ENUM                         sutRoleM;

    // Test Session Properties - via Socket Parameters
    SOCKET_PARAMETERS	socketParametersM;

    // Test Properties
    bool				logScpThreadM;
    bool				autoType2AttributesM;
    bool				defineSqLengthM;
    bool				addGroupLengthM;
    ARRAY<string>		supportedTransferSyntaxM;

    ARRAY<BASE_SCP_EMULATOR_CLASS*> scpEmulatorThreadM;
    BASE_SOCKET_CLASS			*serverSocketM_ptr;

    void cleanup();

protected:
    SCP_EMULATOR_ENUM			scpEmulatorTypeM;
    SCU_EMULATOR_ENUM			scuEmulatorTypeM;

public:
    EMULATOR_SESSION_CLASS();

    ~EMULATOR_SESSION_CLASS();

    bool serialise(FILE*);

    //
    // deprecated methods
    //
    void setProductRoleIsAcceptor(bool productRoleIsAcceptor);
    bool getProductRoleIsAcceptor();

    void setProductRoleIsRequestor(bool productRoleIsRequestor);
    bool getProductRoleIsRequestor();

    void setCalledAeTitle(char *calledAeTitle_ptr);
    const char *getCalledAeTitle();

    void setCallingAeTitle(char *callingAeTitle_ptr);
    const char *getCallingAeTitle();

    void setMaximumLengthReceived(int maximumLengthReceived);
    UINT32 getMaximumLengthReceived();

    void setImplementationClassUid(char *implementationClassUid_ptr);
    const char *getImplementationClassUid();

    void setImplementationVersionName(char *implementationVersionName_ptr);
    const char *getImplementationVersionName();

    void setRemoteHostname(char *remoteHostname_ptr);
    const char *getRemoteHostname();

    void setRemoteConnectPort(UINT16 remoteConnectPort);
    int getRemoteConnectPort();

    void setLocalListenPort(UINT16 localListenPort);
    int getLocalListenPort();

    void setSocketTimeout(int socketTimeout);
    int getSocketTimeout();
    //
    // end of deprecated methods
    //


    // DVT session properties
    void setDvtAeTitle(char *dvtAeTitle_ptr);
    const char *getDvtAeTitle();

    void setDvtMaximumLengthReceived(int maximumLengthReceived);
    UINT32 getDvtMaximumLengthReceived();

    void setDvtImplementationClassUid(char *implementationClassUid_ptr);
    const char *getDvtImplementationClassUid();

    void setDvtImplementationVersionName(char *implementationVersionName_ptr);
    const char *getDvtImplementationVersionName();

    void setDvtPort(UINT16 port);
    UINT16 getDvtPort();

    void setDvtSocketTimeout(int socketTimeout);
    int getDvtSocketTimeout();

    // SUT session properties
    void setSutAeTitle(char *sutAeTitle_ptr);
    const char *getSutAeTitle();

    void setSutMaximumLengthReceived(int maximumLengthReceived);
    UINT32 getSutMaximumLengthReceived();

    void setSutImplementationClassUid(char *implementationClassUid_ptr);
    const char *getSutImplementationClassUid();

    void setSutImplementationVersionName(char *implementationVersionName_ptr);
    const char *getSutImplementationVersionName();

    void setSutHostname(char *hostname_ptr);
    const char *getSutHostname();

    void setSutPort(UINT16 port);
    UINT16 getSutPort();

    void setSutRole(UP_ENUM role);
    UP_ENUM getSutRole();

    void setUseSecureSockets(bool useSecureSockets)
    {
        socketParametersM.useSecureSocketsM = useSecureSockets;
    }

    void setTlsPassword(const char* password)
    {
        socketParametersM.certificateFilePasswordM = password;
    }

    bool isTlsPasswordValid(bool& unencryptedKeyFound) 
    { 
        return socketParametersM.isTlsPasswordValid(unencryptedKeyFound); 
    }

    void setTlsVersion(const char* tlsVersion)
    {
        socketParametersM.tlsVersionM = tlsVersion;
    }

    void setCheckRemoteCertificate(bool checkRemoteCertificate)
    { 
        socketParametersM.checkRemoteCertificateM = checkRemoteCertificate; 
    }

    void setCipherList(const char* cipherList)
    { 
        socketParametersM.cipherListM = cipherList; 
    }

    void setCacheTlsSessions(bool cacheTlsSessions)
    { 
        socketParametersM.cacheTlsSessionsM = cacheTlsSessions; 
    }

    void setTlsCacheTimeout(int tlsCacheTimeout)
    { 
        socketParametersM.tlsCacheTimeoutM = tlsCacheTimeout; 
    }

    void setCredentialsFilename(const char* credentialsFilename)
    {
        string filename = credentialsFilename;
        socketParametersM.credentialsFilenameM = generateFullPath(filename, sessionDirectoryM); 
    }

    void setCertificateFilename(const char* certificateFilename)
    {
        string filename = certificateFilename;
        socketParametersM.certificateFilenameM = generateFullPath(filename, sessionDirectoryM); 
    }

    void setSocketParametersChanged(bool changed) 
    { 
        socketParametersM.contentsChangedM = changed; 
    }

    SOCKET_PARAMETERS& getSocketParameters()
    { 
        return socketParametersM; 
    }

    bool getUseSecureSockets()
    { 
        return socketParametersM.useSecureSocketsM; 
    }

    const char *getTlsPassword()
    { 
        return socketParametersM.certificateFilePasswordM.c_str(); 
    }

    const char *getTlsVersion()
    { 
        return socketParametersM.tlsVersionM.c_str(); 
    }

    bool getCheckRemoteCertificate()
    { 
        return socketParametersM.checkRemoteCertificateM; 
    }

    const char *getCipherList()
    { 
        return socketParametersM.cipherListM.c_str(); 
    }

    bool getCacheTlsSessions()
    { 
        return socketParametersM.cacheTlsSessionsM; 
    }

    int getTlsCacheTimeout()
    { 
        return socketParametersM.tlsCacheTimeoutM; 
    }

    string getCredentialsFilename()
    { 
        return generateFullPath(socketParametersM.credentialsFilenameM, sessionDirectoryM); 
    }

    string getCertificateFilename()
    { 
        return generateFullPath(socketParametersM.certificateFilenameM, sessionDirectoryM); 
    }

    void setLogScpThread(bool flag)
    { 
        logScpThreadM = flag; 
    }

    void setStrictValidation(bool);

    void setAutoType2Attributes(bool flag)
    { 
        autoType2AttributesM = flag; 
    }

    void setDefineSqLength(bool flag)
    { 
        defineSqLengthM = flag; 
    }

    void setAddGroupLength(bool flag)
    { 
        addGroupLengthM = flag; 
    }

    void deleteSupportedTransferSyntaxes() 
    {	
        while (supportedTransferSyntaxM.getSize())
        {
            supportedTransferSyntaxM.removeAt(0); 
        }
    }

    void addSupportedTransferSyntax(char *transferSyntax_ptr)
    { 
        string transferSyntax = transferSyntax_ptr;
        supportedTransferSyntaxM.add(transferSyntax); 
    }

    int noSupportedTransferSyntaxes()
    { 
        return supportedTransferSyntaxM.getSize(); 
    }

    const char *getSupportedTransferSyntax(int i) 
    { 
        return supportedTransferSyntaxM[i].c_str(); 
    }

    bool getLogScpThread()
    { 
        return logScpThreadM; 
    }

    bool getStrictValidation()
    { 
        return strictValidationM; 
    }

    bool getAutoType2Attributes()
    { 
        return autoType2AttributesM; 
    }

    bool getDefineSqLength()
    { 
        return defineSqLengthM; 
    }

    bool getAddGroupLength()
    { 
        return addGroupLengthM; 
    }

    void setScpEmulatorType(SCP_EMULATOR_ENUM scpEmulatorType);
	
	SCP_EMULATOR_ENUM getScpEmulatorType();

    void setScuEmulatorType(SCU_EMULATOR_ENUM scuEmulatorType);
	
	SCU_EMULATOR_ENUM getScuEmulatorType();

    bool emulateSCP();

    bool emulateStorageSCU(vector<string>* filenames, UINT options, UINT nr_repetitions = 1);

    bool emulateVerificationSCU();

    bool sendStatusEvent();

    bool terminateConnection();

    void registerEmulateSCPThread(BASE_SCP_EMULATOR_CLASS*);

    void unRegisterEmulateSCPThead(BASE_SCP_EMULATOR_CLASS*);

    void setLogger(LOG_CLASS*);
};

#endif /* EMULATOR_SESSION_H */


