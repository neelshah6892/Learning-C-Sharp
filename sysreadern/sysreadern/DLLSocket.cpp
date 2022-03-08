
#include "stdafx.h"
#include "DLLSocket.h"
// Receiving Socket Class implementation



// UdpReceiveSocket
UdpReceiveSocket::UdpReceiveSocket()
{
      // Just specify input PORT#, local machine is assumed
      BOOL bRet = Create(9122,SOCK_DGRAM,FD_READ);
      if (bRet != TRUE)
      {
             UINT uErr = GetLastError();
             TCHAR szError[256];
             wsprintf(szError, "Server Receive Socket Create() failed: %d", uErr);
             AfxMessageBox(szError);
      }
}

UdpReceiveSocket::~UdpReceiveSocket()
{
}

// UdpReceiveSocket member functions
void UdpReceiveSocket::OnReceive(int nErrorCode)   
{
  static int i=0;

  i++;

  TCHAR buff[4096];
  int nRead;

  CString strSendersIp;

  UINT uSendersPort;

  // Could use Receive here if you don't need the senders address & port
  nRead = ReceiveFromEx(buff, 4096, strSendersIp, uSendersPort); 

  switch (nRead)
  {
  case 0:       // Connection was closed.
     Close();      
     break;
  case SOCKET_ERROR:
     if (GetLastError() != WSAEWOULDBLOCK) 
     {
        AfxMessageBox ("Error occurred");
        Close();
     }
     break;
  default: // Normal case: Receive() returned the # of bytes received.
     buff[nRead] = 0; //terminate the string (assuming a string for this example)
     CString strReceivedData(buff);       // This is the input data    
  }
  CAsyncSocket::OnReceive(nErrorCode);
}