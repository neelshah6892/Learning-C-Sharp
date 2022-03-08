/****************************************************************************************
* 
*	Original Filename: 	MulticastSocket.cpp
*
*	History:
*	Created/Modified by				Date			Main Purpose/Changes
*	Rajesh Surve				2009/10/25			
*	
*	Comments:	
*   
****************************************************************************************/


#include "stdafx.h"
//#include "CMulticastSocket.h"
#include "MulticastSocket.h"
#include "sysreadern.h"


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//extern CRealTimeApp theApp;
// also defined in mainfrm.h
#define WM_USER_THREAD_UPDATE_RECV_PROGRESSV1 (WM_USER+0x104)
#define WM_USER_THREAD_UPDATE_DB_PROGRESSV1 (WM_USER+0x105) // for db to writning 
#define WM_USER_THREAD_INSERT_TKROW (WM_USER+0x101)
/////////////////////////////////////////////////////////////////////////////
// CMulticastSocket

CMulticastSocket::CMulticastSocket()
{
	m_counter=0;
	bForceNoLoopback = FALSE;
	bDataReceived = TRUE;		/* Variable defined for this project. Not necessarily part of CMulticastSocket */
	m_readFast = false;
	tktable = new TKTABLE;
	memset(tktable,0,sizeof(TKTABLE));
	m_sysreadcounter=0;
	m_tokencounter=0;

}

CMulticastSocket::~CMulticastSocket()
{
	//AsyncSelect(0);
	delete tktable;
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CMulticastSocket, CAsyncSocket)
	//{{AFX_MSG_MAP(CMulticastSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CMulticastSocket member functions

BOOL CMulticastSocket::CreateReceivingSocket(LPCTSTR strGroupIP, UINT nGroupPort)
{
	/* Create socket for receiving packets from multicast group */
	//if(!Create(nGroupPort, SOCK_DGRAM, FD_READ))
	//if (!((m_readFast==false)?Create(nGroupPort, SOCK_DGRAM, FD_READ):Create(nGroupPort, SOCK_RAW, FD_READ)))
	if (!((false==false)?Create(nGroupPort, SOCK_DGRAM, FD_READ):Create(nGroupPort, SOCK_RAW, FD_READ)))
	
	//if(!Create(nGroupPort, SOCK_RAW, FD_READ))
	{
		AfxMessageBox("Error in socket create");
		return FALSE;
	}
	BOOL bMultipleApps = TRUE;		/* allow reuse of local port if needed */
	SetSockOpt(SO_REUSEADDR, (void*)&bMultipleApps, sizeof(BOOL), SOL_SOCKET);

	/* Fill m_saHostGroup_in for sending datagrams */
	memset(&m_saHostGroup, 0, sizeof(m_saHostGroup));
	m_saHostGroup.sin_family = AF_INET;
	m_saHostGroup.sin_addr.s_addr = inet_addr(strGroupIP);
	m_saHostGroup.sin_port = htons((USHORT)nGroupPort);


	int buffer=12*1024;
  if (setsockopt(m_hSocket,SOL_SOCKET,SO_RCVBUF,(char *)&buffer,sizeof(buffer))== -1) {
    fprintf(stderr,"Error setting receive buffer size: %d\n",WSAGetLastError());
    exit(1);
  }

	DWORD         dwBytesRet;
	int ret;
   // Set the ioctl
    printf("Setting the ioctl...\n");
    bool bOpt = TRUE;
    ret = WSAIoctl(m_hSocket, SIO_ENABLE_CIRCULAR_QUEUEING, &bOpt, sizeof(bOpt), NULL, 0, &dwBytesRet, NULL, NULL);
    if (ret == SOCKET_ERROR)
    {
        AfxMessageBox(" WSAIoctl(SIO_ENABLE_CIRCULAR_QUEUEING) failed with error code %d\n", WSAGetLastError());
        return -1;
    }
    // Get the ioctl
    //AfxMessageBox("Getting the ioctl...\n");
    ret = WSAIoctl(m_hSocket, SIO_ENABLE_CIRCULAR_QUEUEING, NULL, 0, &bOpt, sizeof(bOpt), &dwBytesRet, NULL, NULL);
    if (ret == SOCKET_ERROR)
    {
        AfxMessageBox(" WSAIoctl(SIO_ENABLE_CIRCULAR_QUEUEING) failed with error code %d\n", WSAGetLastError());
        return -1;
    }
    /*if (bOpt == TRUE)
        AfxMessageBox("Circular queuing is TRUE!\n");
    else
        AfxMessageBox("Circular queuing is FALSE!\n");
    */


	/* Join the multicast group */
	m_mrMReq.imr_multiaddr.s_addr = inet_addr(strGroupIP);	/* group addr */ 
	m_mrMReq.imr_interface.s_addr = htons(INADDR_ANY);		/* use default */ 
	if(setsockopt(m_hSocket, IPPROTO_IP, IP_ADD_MEMBERSHIP, (char FAR *)&m_mrMReq, sizeof(m_mrMReq)) < 0)
		return FALSE;

	return TRUE;
}

BOOL CMulticastSocket::CreateSendingSocket(UINT nTTL, BOOL bLoopBack)
{
	if(!m_SendSocket.Create(0, SOCK_DGRAM, 0))		// Create an unconnected UDP socket
		return FALSE;

	if(!SetTTL(nTTL))								// Set Time to Live as specified by user
		AfxMessageBox("Warning! Error Setting TTL");

	SetLoopBack(bLoopBack);							// Enable/Disable Loopback

	return TRUE;
}

BOOL CMulticastSocket::SetTTL(UINT nTTL)
{
	/* Set Time to Live to parameter TTL */
	if(m_SendSocket.SetSockOpt(IP_MULTICAST_TTL, &nTTL, sizeof(int), IPPROTO_IP) == 0)
		return FALSE;		/* Error Setting TTL */
	else
		return TRUE;		/* else TTL set successfully */
}

void CMulticastSocket::SetLoopBack(BOOL bLoop)
{
	/* Set LOOPBACK option to TRUE OR FALSE according to IsLoop parameter */
	int nLoopBack = (int)bLoop;
	if(m_SendSocket.SetSockOpt(IP_MULTICAST_LOOP, &nLoopBack, sizeof(int), IPPROTO_IP) == 0)
	{
		if(!bLoop)						/* if required to stop loopback */
		{
			bForceNoLoopback = TRUE;		/* Internally making a note that loopback has to be disabled forcefilly */

			// Get IP/Port for send socket in order to disable loopback forcefully */
			char localHost[255];
			gethostname(localHost, 255);
			struct hostent *host = gethostbyname(localHost);	/* Get local host IP */
			m_strLocalIP = inet_ntoa (*(struct in_addr*)*host->h_addr_list);
			CString Dummy;			// Dummy string to be passed to the GetSockName function
			m_SendSocket.GetSockName(Dummy, m_nLocalPort);		/* Get Port Number for Sending Port */
		}
	}
}

void CMulticastSocket::OnReceive(int nErrorCode)
{
	int nError = ReceiveFrom (m_strBuffer, 1024, m_strSendersIP, m_nSendersPort);
	if(nError == SOCKET_ERROR)
		AfxMessageBox("Error receiving data from the host group");
	else
	{
		if (!bForceNoLoopback || (bForceNoLoopback && !(m_strSendersIP == m_strLocalIP && m_nSendersPort == m_nLocalPort)))
		{
			// 1. If loopbackback is not to be forced then interface handles the loopback itself
			// 2. If you have to loopback and SOCKOPT LOOPBACK fails, no problem, interfaces loopback by default
			// 3. If you have to stop loopback and SOCKOPT LOOPBACK fails, ignore messages coming from your own sending socket
	
			//if () // this flag is no long used its used for saving to DB 
			//{
			//	//long result = AnalyzeUDP();
			//	//if (result!=0) // since n udp
			//	{
			//		 bDataReceived = TRUE;	/* Making note that a message has arrived */
			//		 
			//		if (m_strBuffer[0]==2 && m_strBuffer[1]==32)
			//		{
			//			m_counter++;
			//		// TODO : Add your code for here. The packet received is in m_strBuffer
			//		// need to process it here and send to grid , may chnage it later if its lenthy processing 
			//		// test processing here if we can have good spped 
			//		ProcessRecvBufferTest();
			//		}

			//	}
			//}
			//else
			{
				bDataReceived = TRUE;	/* Making note that a message has arrived */
				m_counter++;
				// TODO : Add your code for here. The packet received is in m_strBuffer
				// need to process it here and send to grid , may chnage it later if its lenthy processing 
				// test processing here if we can have good spped 
				ProcessRecvBuffer();
			}

		}
	}

	CAsyncSocket::OnReceive(nErrorCode); 
}

bool CMulticastSocket::ProcessRecvBufferTest()
{

	//SYSREADERN_API int ProcessMBP(char* streamBuffer, char *unCompbuffer, int filterTrCode,TKTABLE * tktable,long filterTkn, short saveFlg)
	//ret = ProcessMBP(RecvBuf, unCompbuffer,nTrCode,tktable,filterTkn, saveFlg);
	int ipHeaderSize = 0;
	
	BOOL bRet=true;
	char unCompbuffer[3072];
	TKTABLE *ptktable = (TKTABLE *)tktable;
	memset(tktable,0,sizeof(TKTABLE));
	
	int ret = theApp.nseFeedDll.functionptrProcessBMP(m_strBuffer+ipHeaderSize,unCompbuffer, 7208, ptktable, (long)theApp.appParams.filtertoken,theApp.appParams.sveflag);

		for (int i =0;i<ptktable->noOfRecords;i++)
		{
			TKROW *feedTKRow = new TKROW;
			memcpy(feedTKRow,&ptktable->onlyMPBData[i], sizeof(TKROW));

			bRet=PostMessage(theApp.m_pMainWnd->m_hWnd,WM_USER_THREAD_UPDATE_RECV_PROGRESSV1,(WPARAM)m_counter,(LPARAM)feedTKRow);
			
			if(bRet==false)
				MessageBeep(0xFFFFFFFF);
			//if (delay>0)
			//Sleep(1); //***IMP dont use
			
		}
	
	return bRet; 	
}

// NEWEST WAY 
bool CMulticastSocket::ProcessRecvBuffer()
{

	//SYSREADERN_API int ProcessMBP(char* streamBuffer, char *unCompbuffer, int filterTrCode,TKTABLE * tktable,long filterTkn, short saveFlg)
	//ret = ProcessMBP(RecvBuf, unCompbuffer,nTrCode,tktable,filterTkn, saveFlg);
	int ipHeaderSize = 0;
	
	//if (to REAd  IP packets )
	//{
	//	ipHeaderSize = sizeof(IpHeader)+sizeof(UDPHeader);// ip
	//}
	
	BOOL bRet=true;
	char unCompbuffer[3072];
	TKTABLE *ptktable = (TKTABLE *)tktable;

	int ret = theApp.nseFeedDll.functionptrProcessBMP(m_strBuffer+ipHeaderSize,unCompbuffer, 7208, ptktable, (long)theApp.appParams.filtertoken,theApp.appParams.sveflag);

	if(ptktable->noOfRecords!=0)
		m_sysreadcounter++;	

		for (int i =0;i<ptktable->noOfRecords;i++)
		{
			TKROW *feedTKRow = new TKROW;

			memcpy(feedTKRow,&ptktable->onlyMPBData[i], sizeof(TKROW));
			//if (theApp.appParams.saveToDB)
			//{
			//	optionBIDB.insertOptionTokenRow(feedTKRow);
			//	//break;
			//}
			//else
			//{
				bRet=PostMessage(theApp.m_pMainWnd->m_hWnd,WM_USER_THREAD_UPDATE_RECV_PROGRESSV1,(WPARAM)m_counter,(LPARAM)feedTKRow);
			//}
			//-----------------------------------------
			//Change By: Sachin
			//Date: 25/02/2010

			//if (!theApp.appParams.saveToDB)// send msg to save to db else display on grid
				
			/*else
				bRet=PostMessage(theApp.m_pMainWnd->m_hWnd,WM_USER_THREAD_UPDATE_DB_PROGRESSV1,(WPARAM)m_counter,(LPARAM)feedTKRow);*/
			
			//bRet=PostMessage(tis->hWnd,WM_USER_THREAD_UPDATE_RECV_PROGRESSV1,(WPARAM)totPacket,(LPARAM)&tktable->onlyMPBData[i]);
			
			if(bRet==false)
				MessageBeep(0xFFFFFFFF);
			//if (delay>0)
			//Sleep(1); //***IMP dont use
			m_tokencounter++;
			//------------------------------------------
			
		}
	
	return bRet; 	
}

long CMulticastSocket::AnalyzeUDP()
{
	IpHeader ipHeader;
	UDPHeader udpHeader;

    	//find if its our packet 
		//Copy the header
		memcpy(&ipHeader,m_strBuffer,sizeof(IpHeader));

		if (ipHeader.Protocol!=17) //udp
			return 0;

		//Check the packet is addresses to us
			
		//First create the address
		in_addr addr;

		//Assign it
		addr.S_un.S_addr=ipHeader.sourceIPAddress;

		//Return the value
		LPSTR lpAddress= inet_ntoa(addr);

	//Read the UDP header
	
	memcpy(&udpHeader,m_strBuffer+sizeof(IpHeader),sizeof(UDPHeader));

	//Print out the code
	CString strUDP;

	//Convert to strings
	char cUDP[10];
	ltoa(htons(udpHeader.SourcePort),cUDP,10);

	strUDP="Source port: ";
	strUDP+=cUDP;

	//Convert again
	ltoa(htons(udpHeader.DestinationPort),cUDP,10);
	long size = htons(udpHeader.Length);

	strUDP+=", destination port: ";
	strUDP+=cUDP;

	//return atol(cUDP);
	if ((m_strBuffer[29]==32) && (m_strBuffer[28]==2))
		return 1;
	else
		return 0;
	//m_pList->AddString(strUDP);
}

BOOL CMulticastSocket::LeaveGroup()
{
	if(setsockopt (m_hSocket, IPPROTO_IP, IP_DROP_MEMBERSHIP, (char FAR *)&m_mrMReq, sizeof(m_mrMReq)) < 0)
		return FALSE;

	m_SendSocket.Close();		// Close sending socket
	Close();					// Close receving socket
	return TRUE;
}

BOOL CMulticastSocket::SendTo(const void* strMessage, int nSize)
{
	if(m_SendSocket.SendTo(strMessage, nSize, (SOCKADDR*)&m_saHostGroup, sizeof(SOCKADDR), 0) == SOCKET_ERROR)
		return FALSE;
	else
		return TRUE;
}

BOOL CMulticastSocket::JoinGroup(CString GroupIP, UINT nGroupPort, UINT nTTL, BOOL bLoopback)
{
		if(!CreateReceivingSocket(GroupIP, nGroupPort))		/* Create Socket for receiving and join the host group */
			return FALSE;
		//if(!CreateSendingSocket(nTTL, bLoopback))			/* Create Socket for sending */
		//	return FALSE;

		return TRUE;
}

void CMulticastSocket::setReadFast(bool fst)
{
	m_readFast=fst;
}
bool CMulticastSocket::getReadFast()
{
	return m_readFast;
}