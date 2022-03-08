#pragma once



#include "stdafx.h"

// UdpReceiveSocket command target
class UdpReceiveSocket : public CAsyncSocket
{
      void OnReceive(int nErrorCode);
public:
      UdpReceiveSocket();
      virtual ~UdpReceiveSocket();
};