#if !defined(AFX_MULTICASTSOCKET_H__269E2C7F_2037_11D3_8EF3_0000C0FD25F8__INCLUDED_)
#define AFX_MULTICASTSOCKET_H__269E2C7F_2037_11D3_8EF3_0000C0FD25F8__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000
// MulticastSocket.h : header file
//
/////////////////////////////////////////////////////////////////////////////
// CMulticastSocket command target
#include <afxsock.h>

class CMulticastSocket : public CAsyncSocket
{
// Attributes
public:
	long m_counter;
	long m_sysreadcounter;
	long m_tokencounter;
	bool m_readFast;

	LPVOID tktable ;

// Operations
public:
	CMulticastSocket();
	virtual ~CMulticastSocket();
	void setReadFast(bool fst);
	bool getReadFast();
	bool ProcessRecvBuffer();
	bool ProcessRecvBufferTest();
	long AnalyzeUDP();
	
// Overrides
public:
	BOOL bDataReceived;
	BOOL JoinGroup(CString, UINT, UINT, BOOL);
	BOOL LeaveGroup();
	BOOL SendTo(const void*, int);
	void SetLoopBack(BOOL);
	BOOL SetTTL(UINT nTTL);
	BOOL CreateSendingSocket(UINT, BOOL);
	BOOL CreateReceivingSocket(LPCTSTR, UINT);
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMulticastSocket)
	public:
	virtual void OnReceive(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CMulticastSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
public:
	char m_strBuffer[32000];	// Receiving buffer for the packet that has arrived
	SOCKADDR_IN m_saHostGroup;	// SOCKADDR structure to hold IP/Port of the Host group to send data to it
	ip_mreq m_mrMReq;			// Contains IP and interface of the host group
	UINT m_nSendersPort;		// Holds Port No. of the socket from which last packet was received
	CString m_strSendersIP;		// Hold IP of the socket from which the last packet was received
	UINT m_nLocalPort;			// Ephemeral port number of the sending port
	CString m_strLocalIP;		// IP Address of the local host or your machine
	BOOL bForceNoLoopback;		// If interface does not support lopback and the service is required, the bool is set to true
	CAsyncSocket m_SendSocket;	// Socket for sending data to the host group
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MULTICASTSOCKET_H__269E2C7F_2037_11D3_8EF3_0000C0FD25F8__INCLUDED_)
