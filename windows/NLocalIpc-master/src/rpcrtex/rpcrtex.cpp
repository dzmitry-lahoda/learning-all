// rpcrtex.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "rpcrtex.h"
#include "comutil.h"
#include <tchar.h>
#include <conio.h>
#include <stdio.h>

#define RPCRTEX_ERROR = 100;

#define LPC_COMMAND_REQUEST_NOREPLY  0x00000000
#define LPC_COMMAND_REQUEST_REPLY    0x00000001
#define LPC_COMMAND_STOP             0x00000002
#define LpcPortName "mylpcport"
// This is the data structure transferred through LPC.
// Every structure must begin with PORT_MESSAGE, and must NOT be
// greater that MAX_LPC_DATA


extern "C" RPCRTEX_API PSECURITY_DESCRIPTOR WINAPI secddd()
{
	PSECURITY_DESCRIPTOR sd = malloc(sizeof(SECURITY_DESCRIPTOR));
    InitializeSecurityDescriptor(sd, SECURITY_DESCRIPTOR_REVISION);
    SetSecurityDescriptorDacl(sd, TRUE, NULL, FALSE);
return sd;
}


typedef struct _TRANSFERRED_MESSAGE
{
    PORT_MESSAGE Header;

    ULONG   Command;
    WCHAR   MessageText[48];

} TRANSFERRED_MESSAGE, *PTRANSFERRED_MESSAGE;


DWORD WINAPI ServerThread1(LPVOID lpParameter);

 int LpcServerStart(const BSTR lpcPortName)
{	
    DWORD dwThreadId;
	auto lpcPortNameCopy = SysAllocString(lpcPortName);
	HANDLE hThread = CreateThread(NULL, 0, ServerThread1, lpcPortNameCopy, 0, &dwThreadId);
	return 42;
}

DWORD WINAPI ServerThread1(LPVOID lpParameter)
{
	BSTR portName = (BSTR)lpParameter;
    SECURITY_DESCRIPTOR sd;
    OBJECT_ATTRIBUTES ObjAttr;              // Object attributes for the name
    UNICODE_STRING PortName;
    NTSTATUS Status;
    HANDLE LpcPortHandle = NULL;
    BYTE RequestBuffer[sizeof(PORT_MESSAGE) + MAX_LPC_DATA];
    BOOL WeHaveToStop = FALSE;
    int nError;

    __try     // try-finally
    {
        //
        // Initialize security descriptor that will be set to
        // "Everyone has the full access"
        //

        if(!InitializeSecurityDescriptor(&sd, SECURITY_DESCRIPTOR_REVISION))
        {
            nError = GetLastError();
            __leave;
        }

        //
        // Set the empty DACL to the security descriptor
        //

        if(!SetSecurityDescriptorDacl(&sd, TRUE, NULL, FALSE))
        {
            nError = GetLastError();
            __leave;
        }

        //
        // Initialize attributes for the port and create it
        //

        RtlInitUnicodeString(&PortName, portName);
        InitializeObjectAttributes(&ObjAttr, &PortName, 0, NULL, &sd);

        Status = NtCreatePort(&LpcPortHandle,
                              &ObjAttr,
                               NULL,
                               sizeof(PORT_MESSAGE) + MAX_LPC_DATA,
                               0);
        _tprintf(_T("Server: NtCreatePort result 0x%08lX\n"), Status);

        if(!NT_SUCCESS(Status))
            __leave;

        //
        // Process all incoming LPC messages
        //

        while(WeHaveToStop == FALSE)
        {
            PTRANSFERRED_MESSAGE LpcMessage = NULL;
            HANDLE ServerHandle = NULL;

            //
            // Create the data buffer for the request
            //

            LpcMessage = (PTRANSFERRED_MESSAGE)RequestBuffer;
            _tprintf(_T("Server: ------------- Begin loop ----------------------\n"), Status);

            //
            // Listen to the port. This call is blocking, and cannot be interrupted,
            // even if the handle is closed. The only way how to stop the block is to send
            // an LPC request which will be recognized by server thread as "Stop" command
            //

            if(NT_SUCCESS(Status))
            {
                _tprintf(_T("Server: Listening to LPC port (NtListenPort) ...\n"), LpcPortName);
                Status = NtListenPort(LpcPortHandle,
                                     &LpcMessage->Header);
                _tprintf(_T("Server: NtListenPort result 0x%08lX\n"), Status);
            }

            //
            // Accept the port connection
            //

            if(NT_SUCCESS(Status))
            {
                _tprintf(_T("Server: Accepting LPC connection (NtAcceptConnectPort) ...\n"), LpcPortName);
                Status = NtAcceptConnectPort(&ServerHandle,
                                              NULL,
                                             &LpcMessage->Header,
                                              TRUE,
                                              NULL,
                                              NULL);
                _tprintf(_T("Server: NtAcceptConnectPort result 0x%08lX\n"), Status);
            }

            //
            // Complete the connection
            //

            if(NT_SUCCESS(Status))
            {
                _tprintf(_T("Server: Completing LPC connection (NtCompleteConnectPort) ...\n"), LpcPortName);
                Status = NtCompleteConnectPort(ServerHandle);
                _tprintf(_T("Server: NtCompleteConnectPort result 0x%08lX\n"), Status);
            }

            //
            // Now accept the data request coming from the port.
            //

            if(NT_SUCCESS(Status))
            {
                _tprintf(_T("Server: Receiving LPC data (NtReplyWaitReceivePort) ...\n"), LpcPortName);
                Status = NtReplyWaitReceivePort(ServerHandle,
                                                NULL,
                                                NULL,
                                               &LpcMessage->Header);
                _tprintf(_T("Server: NtReplyWaitReceivePort result 0x%08lX\n"), Status);
            }

            //
            // Get the data sent by the client 
            //

            if(NT_SUCCESS(Status))
            {
                // If a request has been received, answer to it.
                switch(LpcMessage->Command)
                {
                    case LPC_COMMAND_REQUEST_NOREPLY:
                        _tprintf(_T("Server: Received request \"%s\"\n"), LpcMessage->MessageText);
                        break;      // Nothing more to do

                    case LPC_COMMAND_REQUEST_REPLY:
                        _tprintf(_T("Server: Received request \"%s\"\n"), LpcMessage->MessageText);
                        _tprintf(_T("Server: Sending reply (NtReplyPort) ...\n"), LpcPortName);
                        Status = NtReplyPort(LpcPortHandle, &LpcMessage->Header);
                        _tprintf(_T("Server: NtReplyPort result 0x%08lX\n"), Status);
                        break;

                    case LPC_COMMAND_STOP:      // Stop the work
                        _tprintf(_T("Server: Stopping ...\n"));
                        WeHaveToStop = TRUE;
                        break;
                }
            }

            //
            // Close the server connection handle
            //

            if(ServerHandle != NULL)
            {
                _tprintf(_T("Server: Closing the request handle (NtClose) ...\n"), LpcPortName);
                Status = NtClose(ServerHandle);
                _tprintf(_T("Server: NtClose result 0x%08lX\n"), Status);
            }

            _tprintf(_T("Server: ------------- End loop ----------------------\n"), Status);
        }
    }

    __finally
    {
        if(LpcPortHandle != NULL)
            NtClose(LpcPortHandle);
    }
    
    return 0;
}

