// sysreadern.cpp : Defines the exported functions for the DLL application.
// 100402010 - bug dies on 16703 packet eqsept.pkt 
//							128439 packet with 3 packets , code updated is mrked by NEW 100402010

#include "stdafx.h"
#include "sysreadern.h"
#include "include/lzo1z.h"
#include "winsock.h"
#include <time.h>
#include <cstdlib>
#include <iostream>
#include <fstream>
#include <windows.h>
#include <winnt.h>
#include <stdio.h>
#include <stdlib.h>
#include "tri_logger.hpp"
// bost 
// Link all libraries dynamically
//#define BOOST_ALL_DYN_LINK
// For linking with boost.thread not supported by ‘BOOST_ALL_DYN_LINK’
//#define BOOST_THREAD_USE_DLL
// Verbose
//#define BOOST_LIB_DIAGNOSTIC

//****************************************************************************************************************
//Commented By:SSP On:1/10/2010
//Desc: This line gives me error even we includes all the necessary directories including following
//\boost_1_42_0\boost_1_42_0\boost
//\ThreadPool\threadpool-0_2_5-src\threadpool-0_2_5-src\threadpool\boost
//#include "threadpool.hpp"
//****************************************************************************************************************
 

#define IP_ADD_MEMBERSHIP 12
#define IP_MULTICAST_TTL    10      /* set/get IP multicast timetolive  */ 
#define IP_MULTICAST_IF     9  
#pragma comment(lib, "ws2_32.lib")


//extern CRITICAL_SECTION csSync; 


// For linking with boost.thread not supported by ‘BOOST_ALL_DYN_LINK’
//#define BOOST_THREAD_USE_DLL
// Verbose
//#define BOOST_LIB_DIAGNOSTIC

// This is an example of an exported variable
SYSREADERN_API int nsysreadern=0;
int DEBUGTEST =1;
int testInt=0;
BOOL socketInit=false;
// This is an example of an exported function.
SYSREADERN_API int fnsysreadern(void)
{
	int errcode1= lzo1z_decompress((const unsigned char *)NULL,1,NULL,NULL,NULL);
	    
	return 42;
}

// This is the constructor of a class that has been exported.
// see sysreadern.h for the class definition
Csysreadern::Csysreadern()
{
	return;
}

// in, out buffer this used ****** 
SYSREADERN_API int ProcessMBP(char* streamBuffer, char *unCompbuffer, int filterTrCode,TKTABLE * tktable,long filterTkn, short saveFlg)
{

	//EnterCriticalSection(&csSync);

	if (DEBUGTEST)
	printf("\nProcessMBP:START\n");
	// for testing only need to dlete 
	//char tmpCompbuffer[1024];
	//unCompbuffer = (unsigned char *)&tmpCompbuffer[0];
	// delete above

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	//pac.iCompLen =0;
	//converttolittle(pkt1437,512);
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	tktable->noOfRecords = 0; // initialize pump the data
	
	if (DEBUGTEST)
	printf("\tProcessMBP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	// 
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
        //char *tmpPtr = pac->cPackData;
		//tmpPtr+= i*(2+bufLen); old
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\tProcessMBP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\tProcessMBP: SKIPPING UNcompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struc
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\tProcessMBP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\tProcessMBP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\tProcessMBP:FOUND trcode %i\n", trCode);
			PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\tProcessMBP:differnt trcode %i == %i\n", trCode,filterTrCode);
		}
		
	
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	//LeaveCriticalSection(&csSync);



	return ssize;
}



void PrintMsgHeader(MessageHeader *msgHdr)
{
	/*printf ("\t\tMsg HDR start %c ",msgHdr->iApiTcode);
	printf ("%c ",msgHdr->iApiFuncId);*/
	printf ("%l ",ntohl(msgHdr->LogTime));
	long ltime= ntohl(msgHdr->LogTime);
/*
	time_t msgTime =  time( (time_t *)&ltime );
	struct tm *t;
	t = localtime ( &msgTime );
   printf ( "%d:%d:%d\n", t->tm_hour, t->tm_min, t->tm_sec );
   printf("%s",asctime(t));
*/

	printf ("%c%c ",msgHdr->AlphaChar[0], msgHdr->AlphaChar[1]);
	short trCode = ntohs(msgHdr->TransactionCode);
	printf ("%i ",trCode);

	printf ("%i ",ntohs(msgHdr->ErrorCode));
	printf ("Msg HDR LEN %i  END \n",ntohs(msgHdr->MessageLength));

}

//** this is used 
void PrintBroadCastOnlyMBP(BroadCastOnlyMBP *onlyMBP, short trCode, TKTABLE *tktable,long filterTkn, short saveFlg)
{
	//PrintMsgHeader(&(onlyMBP->msgHeader));
	int noOfrec = ntohs(onlyMBP->noOfRecords);
	if (DEBUGTEST)
		printf ("\t\tPrintBroadCastOnlyMBP:noOfRecords %i \n",noOfrec);

	// test byte swap
	//short bigNoOfRecords=onlyMBP->noOfRecords;
	//byteSwap(&bigNoOfRecords,sizeof(short));
	//printf ("bigNoOfRecords %i \n",bigNoOfRecords);

		
	for (int i=0;i<noOfrec;i++)
	{
		int tokenCnt = tktable->noOfRecords;
		tktable->onlyMPBData[tokenCnt].trCode=trCode; // pump data
		long token = ntohl(onlyMBP->onlyMPBData[i].Token);
		if (DEBUGTEST)
			printf ("\t\t\tPrintBroadCastOnlyMBP:token %i \n",token);

		//// test byte swap
		//long bigtoken=onlyMBP->onlyMPBData[i].Token;
		//byteSwap(&bigtoken,sizeof(long));
		//printf ("bigtoken %i \n",bigtoken);
		
		
		tktable->onlyMPBData[tokenCnt].Token=token;
		tktable->onlyMPBData[tokenCnt].BookType = ntohs(onlyMBP->onlyMPBData[i].BookType);
		tktable->onlyMPBData[tokenCnt].TradingStatus = ntohs(onlyMBP->onlyMPBData[i].TradingStatus);
		tktable->onlyMPBData[tokenCnt].VolumeTradedToday = ntohl(onlyMBP->onlyMPBData[i].VolumeTradedToday);
		tktable->onlyMPBData[tokenCnt].LastTradedPrice = ntohl(onlyMBP->onlyMPBData[i].LastTradedPrice);
		tktable->onlyMPBData[tokenCnt].NetChangeIndicator = (onlyMBP->onlyMPBData[i].NetChangeIndicator);
		tktable->onlyMPBData[tokenCnt].NetPriceChangeFromClosingPrice =  ntohl(onlyMBP->onlyMPBData[i].NetPriceChangeFromClosingPrice);
		tktable->onlyMPBData[tokenCnt].LastTradeQuantity = ntohl(onlyMBP->onlyMPBData[i].LastTradeQuantity);
		tktable->onlyMPBData[tokenCnt].LastTradeTime= ntohl(onlyMBP->onlyMPBData[i].LastTradeTime);
		tktable->onlyMPBData[tokenCnt].AverageTradePrice = ntohl(onlyMBP->onlyMPBData[i].AverageTradePrice);
		tktable->onlyMPBData[tokenCnt].AuctionNumber = ntohs(onlyMBP->onlyMPBData[i].AuctionNumber);
		tktable->onlyMPBData[tokenCnt].AuctionStatus = ntohs(onlyMBP->onlyMPBData[i].AuctionStatus);
		tktable->onlyMPBData[tokenCnt].InitiatorType= ntohs(onlyMBP->onlyMPBData[i].InitiatorType);
		tktable->onlyMPBData[tokenCnt].InitiatorPrice= ntohl(onlyMBP->onlyMPBData[i].InitiatorPrice);
		tktable->onlyMPBData[tokenCnt].InitiatorQuantity = ntohl(onlyMBP->onlyMPBData[i].InitiatorQuantity);
		tktable->onlyMPBData[tokenCnt].AuctionPrice = ntohl(onlyMBP->onlyMPBData[i].AuctionPrice);
		tktable->onlyMPBData[tokenCnt].AuctionQuantity = ntohl(onlyMBP->onlyMPBData[i].AuctionQuantity);
		//char RecordBuffer [ sizeof ( MBPInformation ) * 10 ];
		// convert mbinfo struct also 
		for (int m=0;m<10;m++)
		{
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].Quantity = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].Price = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[m].Price);
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].NumberOfOrders = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[m].NumberOfOrders);
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].BbBuySellFlag = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[m].BbBuySellFlag);
			printf ("\t\t\tPrintBroadCastOnlyMBP:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].Quantity);
			printf ("\t\t\tPrintBroadCastOnlyMBP:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].Price);
			printf ("\t\t\tPrintBroadCastOnlyMBP:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].NumberOfOrders);
			printf ("\t\t\tPrintBroadCastOnlyMBP:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].BbBuySellFlag);

		
		}
		tktable->onlyMPBData[tokenCnt].BbTotalBuyFlag = ntohs(onlyMBP->onlyMPBData[i].BbTotalBuyFlag);
		tktable->onlyMPBData[tokenCnt].BbTotalSellFlag = ntohs(onlyMBP->onlyMPBData[i].BbTotalSellFlag);
		double TotalBuyQuantity;
		double TotalSellQuantity;
		//StIndicator stInicator;
		tktable->onlyMPBData[tokenCnt].ClosingPrice = ntohl(onlyMBP->onlyMPBData[i].ClosingPrice);
		tktable->onlyMPBData[tokenCnt].OpenPrice= ntohl(onlyMBP->onlyMPBData[i].OpenPrice);
		tktable->onlyMPBData[tokenCnt].HighPrice= ntohl(onlyMBP->onlyMPBData[i].HighPrice);
		tktable->onlyMPBData[tokenCnt].LowPrice= ntohl(onlyMBP->onlyMPBData[i].LowPrice);
		
		// test byte swap
		double bigTotalBuyQuantity=onlyMBP->onlyMPBData[i].TotalBuyQuantity;
		byteSwap(&bigTotalBuyQuantity,sizeof(double));
		bool print=false;
		if(filterTkn==0)
			print=true;
		else 
			if (filterTkn==token)
			{
				printf("\t\t\tPrintBroadCastOnlyMBP:printing only %i\n",filterTkn);
				print=true;
			}
		if (print)
		{
			if (DEBUGTEST)
			printf ("\t\t\tPrintBroadCastOnlyMBP:bigTotalBuyQuantity %g \n",bigTotalBuyQuantity);



			printf("\t\t\tPrintBroadCastOnlyMBP:token :\t>>>>>> %i <<<<<<<<<< \n",token);
			if (DEBUGTEST)
			{
			printf ("  \t\t\tPrintBroadCastOnlyMBP:BookType :\t%i \n",tktable->onlyMPBData[tokenCnt].BookType);
			printf ("  \t\t\tPrintBroadCastOnlyMBP:TradingStatus :\t%i \n",tktable->onlyMPBData[tokenCnt].TradingStatus);
			printf ("  \t\t\tPrintBroadCastOnlyMBP:ClosingPrice :\t%i \n",tktable->onlyMPBData[tokenCnt].ClosingPrice);
			printf ("  \t\t\tPrintBroadCastOnlyMBP:OpenPrice :\t%i \n",tktable->onlyMPBData[tokenCnt].OpenPrice);
			printf ("  \t\t\tPrintBroadCastOnlyMBP:VolumeTradedToday :\t%i \n",tktable->onlyMPBData[tokenCnt].VolumeTradedToday);
			}
		}
		
	 //   // print mbpinfo 
  //      for (int j=0;j<10;j++)
		//{
		//	long Quantity = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].Quantity);
		//	float Price = (float)ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].Price);
		//	long NumberOfOrders= ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].NumberOfOrders);
		//	short BbBuySellFlag = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[j].BbBuySellFlag);

		//	printf("\tMBPinfo[j].Quantity %i \n",Quantity);
		//	printf("\tMBPinfo[j].Price %f \n",Price);
		//	printf("\tMBPinfo[j].NumberOfOrders %i \n",NumberOfOrders);
		//	printf("\tMBPinfo[j].BbBuySellFlag %i \n",BbBuySellFlag);
		//}




		tktable->noOfRecords=tktable->noOfRecords+1; // increse cnt to move to next token
	}// end for 

}



//*********************  CM EQUITY 


// in, out buffer this used ****** CM EQUITY 
SYSREADERN_API int ProcessMBP_CM(char* streamBuffer, char *unCompbuffer, int filterTrCode,TKTABLE * tktable,long filterTkn, short saveFlg)
{

	//EnterCriticalSection(&csSync);

	if (DEBUGTEST)
	printf("\nrocessMBP_CM:START\n");
	// for testing only need to dlete 
	//char tmpCompbuffer[1024];
	//unCompbuffer = (unsigned char *)&tmpCompbuffer[0];
	// delete above

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	//pac.iCompLen =0;
	//converttolittle(pkt1437,512);
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	tktable->noOfRecords = 0; // initialize pump the data
	
	if (DEBUGTEST)
	printf("\trocessMBP_CM: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	short j=0; // if buffer needs to be incremented  NEW 100402010
	// 
	
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
        //char *tmpPtr = pac->cPackData;
		//tmpPtr+= i*(2+bufLen); old
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\trocessMBP_CM: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\trocessMBP_CM: SKIPPING UNcompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struc
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\tProcessMBP_CM:lzoing.. ");

		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\tProcessMBP_CM:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\tProcessMBP_CM:FOUND trcode %i\n", trCode);
			PrintBroadCastOnlyMBP_CM(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\tProcessMBP_CM:differnt trcode %i == %i\n", trCode,filterTrCode);
		}
		
	
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP_CM:final uncompress size%i \n",ssize);
		printf("ProcessMBP_CM:DONE\n");
	}
	//LeaveCriticalSection(&csSync);



	return ssize;
}

// this used ****** CM EQUITY 
void PrintMsgHeader_CM(CMMessageHeader *msgHdr)
{
	printf ("\t\tMsg HDR start %c ",msgHdr->iApiTcode);
	printf ("%c ",msgHdr->iApiFuncId);
	printf ("%l ",ntohl(msgHdr->LogTime));
	long ltime= ntohl(msgHdr->LogTime);
/*
	time_t msgTime =  time( (time_t *)&ltime );
	struct tm *t;
	t = localtime ( &msgTime );
   printf ( "%d:%d:%d\n", t->tm_hour, t->tm_min, t->tm_sec );
   printf("%s",asctime(t));
*/

	printf ("%c%c ",msgHdr->AlphaChar[0], msgHdr->AlphaChar[1]);
	short trCode = ntohs(msgHdr->TransactionCode);
	printf ("%i ",trCode);

	printf ("%i ",ntohs(msgHdr->ErrorCode));
	printf ("Msg HDR LEN %i  END \n",ntohs(msgHdr->MessageLength));

}


//** this is used  CM EQUITY 
void PrintBroadCastOnlyMBP_CM(CMBroadCastOnlyMBP *onlyMBP, short trCode, TKTABLE *tktable,long filterTkn, short saveFlg)
{
	//PrintMsgHeader(&(onlyMBP->msgHeader));
	int noOfrec = ntohs(onlyMBP->noOfRecords);
	if (DEBUGTEST)
		printf ("\t\tPrintBroadCastOnlyMBP_CM:noOfRecords %i \n",noOfrec);

	// test byte swap
	//short bigNoOfRecords=onlyMBP->noOfRecords;
	//byteSwap(&bigNoOfRecords,sizeof(short));
	//printf ("bigNoOfRecords %i \n",bigNoOfRecords);

		
	for (int i=0;i<noOfrec;i++)
	{
		int tokenCnt = tktable->noOfRecords;
		tktable->onlyMPBData[tokenCnt].trCode=trCode; // pump data
		long token = ntohs(onlyMBP->onlyMPBData[i].Token); // for short token 
		if (DEBUGTEST)
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:token %i \n",token);

		//// test byte swap
		//long bigtoken=onlyMBP->onlyMPBData[i].Token;
		//byteSwap(&bigtoken,sizeof(long));
		//printf ("bigtoken %i \n",bigtoken);

		
		tktable->onlyMPBData[tokenCnt].Token=token;
		tktable->onlyMPBData[tokenCnt].BookType = ntohs(onlyMBP->onlyMPBData[i].BookType);
		tktable->onlyMPBData[tokenCnt].TradingStatus = ntohs(onlyMBP->onlyMPBData[i].TradingStatus);
		tktable->onlyMPBData[tokenCnt].VolumeTradedToday = ntohl(onlyMBP->onlyMPBData[i].VolumeTradedToday);
		tktable->onlyMPBData[tokenCnt].LastTradedPrice = ntohl(onlyMBP->onlyMPBData[i].LastTradedPrice);
		tktable->onlyMPBData[tokenCnt].NetChangeIndicator = (onlyMBP->onlyMPBData[i].NetChangeIndicator);
		tktable->onlyMPBData[tokenCnt].NetPriceChangeFromClosingPrice =  ntohl(onlyMBP->onlyMPBData[i].NetPriceChangeFromClosingPrice);
		tktable->onlyMPBData[tokenCnt].LastTradeQuantity = ntohl(onlyMBP->onlyMPBData[i].LastTradeQuantity);
		tktable->onlyMPBData[tokenCnt].LastTradeTime= ntohl(onlyMBP->onlyMPBData[i].LastTradeTime);
		tktable->onlyMPBData[tokenCnt].AverageTradePrice = ntohl(onlyMBP->onlyMPBData[i].AverageTradePrice);
		tktable->onlyMPBData[tokenCnt].AuctionNumber = ntohs(onlyMBP->onlyMPBData[i].AuctionNumber);
		tktable->onlyMPBData[tokenCnt].AuctionStatus = ntohs(onlyMBP->onlyMPBData[i].AuctionStatus);
		tktable->onlyMPBData[tokenCnt].InitiatorType= ntohs(onlyMBP->onlyMPBData[i].InitiatorType);
		tktable->onlyMPBData[tokenCnt].InitiatorPrice= ntohl(onlyMBP->onlyMPBData[i].InitiatorPrice);
		tktable->onlyMPBData[tokenCnt].InitiatorQuantity = ntohl(onlyMBP->onlyMPBData[i].InitiatorQuantity);
		tktable->onlyMPBData[tokenCnt].AuctionPrice = ntohl(onlyMBP->onlyMPBData[i].AuctionPrice);
		tktable->onlyMPBData[tokenCnt].AuctionQuantity = ntohl(onlyMBP->onlyMPBData[i].AuctionQuantity);
		//char RecordBuffer [ sizeof ( MBPInformation ) * 10 ];
		// convert mbinfo struct also 
		for (int m=0;m<10;m++)
		{
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].Quantity = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].Price = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[m].Price);
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].NumberOfOrders = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[m].NumberOfOrders);
			tktable->onlyMPBData[tokenCnt].MBPinfo[m].BbBuySellFlag = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[m].BbBuySellFlag);
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].Quantity);
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].Price);
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].NumberOfOrders);
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:mbinfo %i \n",tktable->onlyMPBData[i].MBPinfo[m].BbBuySellFlag);

		
		}
		tktable->onlyMPBData[tokenCnt].BbTotalBuyFlag = ntohs(onlyMBP->onlyMPBData[i].BbTotalBuyFlag);
		tktable->onlyMPBData[tokenCnt].BbTotalSellFlag = ntohs(onlyMBP->onlyMPBData[i].BbTotalSellFlag);
		double TotalBuyQuantity;
		double TotalSellQuantity;
		//StIndicator stInicator;
		tktable->onlyMPBData[tokenCnt].ClosingPrice = ntohl(onlyMBP->onlyMPBData[i].ClosingPrice);
		tktable->onlyMPBData[tokenCnt].OpenPrice= ntohl(onlyMBP->onlyMPBData[i].OpenPrice);
		tktable->onlyMPBData[tokenCnt].HighPrice= ntohl(onlyMBP->onlyMPBData[i].HighPrice);
		tktable->onlyMPBData[tokenCnt].LowPrice= ntohl(onlyMBP->onlyMPBData[i].LowPrice);
		
		// test byte swap
		double bigTotalBuyQuantity=onlyMBP->onlyMPBData[i].TotalBuyQuantity;
		byteSwap(&bigTotalBuyQuantity,sizeof(double));
		bool print=false;
		if(filterTkn==0)
			print=true;
		else 
			if (filterTkn==token)
			{
				printf("\t\t\tPrintBroadCastOnlyMBP_CM:printing only %i\n",filterTkn);
				print=true;
			}
		if (print)
		{
			if (DEBUGTEST)
			printf ("\t\t\tPrintBroadCastOnlyMBP_CM:bigTotalBuyQuantity %g \n",bigTotalBuyQuantity);



			printf("\t\t\tPrintBroadCastOnlyMBP_CM:token :\t>>>>>> %i <<<<<<<<<< \n",token);
			if (DEBUGTEST)
			{
			printf ("  \t\t\tPrintBroadCastOnlyMBP_CM:BookType :\t%i \n",tktable->onlyMPBData[tokenCnt].BookType);
			printf ("  \t\t\tPrintBroadCastOnlyMBP_CM:TradingStatus :\t%i \n",tktable->onlyMPBData[tokenCnt].TradingStatus);
			printf ("  \t\t\tPrintBroadCastOnlyMBP_CM:ClosingPrice :\t%i \n",tktable->onlyMPBData[tokenCnt].ClosingPrice);
			printf ("  \t\t\tPrintBroadCastOnlyMBP_CM:OpenPrice :\t%i \n",tktable->onlyMPBData[tokenCnt].OpenPrice);
			printf ("  \t\t\tPrintBroadCastOnlyMBP:VolumeTradedToday :\t%i \n",tktable->onlyMPBData[tokenCnt].VolumeTradedToday);
			}
		}
		
	 //   // print mbpinfo 
  //      for (int j=0;j<10;j++)
		//{
		//	long Quantity = ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].Quantity);
		//	float Price = (float)ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].Price);
		//	long NumberOfOrders= ntohl(onlyMBP->onlyMPBData[i].MBPinfo[j].NumberOfOrders);
		//	short BbBuySellFlag = ntohs(onlyMBP->onlyMPBData[i].MBPinfo[j].BbBuySellFlag);

		//	printf("\tMBPinfo[j].Quantity %i \n",Quantity);
		//	printf("\tMBPinfo[j].Price %f \n",Price);
		//	printf("\tMBPinfo[j].NumberOfOrders %i \n",NumberOfOrders);
		//	printf("\tMBPinfo[j].BbBuySellFlag %i \n",BbBuySellFlag);
		//}




		tktable->noOfRecords=tktable->noOfRecords+1; // increse cnt to move to next token
	}// end for 

}




#define IP_ADD_MEMBERSHIP 12
#define IP_MULTICAST_TTL    10      /* set/get IP multicast timetolive  */ 
#define IP_MULTICAST_IF     9  
#pragma comment(lib, "ws2_32.lib")

using namespace std;

char* recvData2(int nport, char *strIP, int bug)
{
	char *RecvBuf=NULL; 
	if (DEBUGTEST)
		printf(" in rcvData 2>%i ->%s",nport,strIP);

// need to chnage ip and port from param - TODO 
if (bug==1)
{

	//RecvBuf = &pkt1437[0]; good
	RecvBuf = &pkt89[0];
	
	//strcpy(
	if (DEBUGTEST)
		printf("in debugtest %c", RecvBuf[0]);
	return RecvBuf;
}


	   WSADATA wsaData;
	  SOCKET RecvSocket;
	  sockaddr_in RecvAddr;
	  int Port = nport;
	  //char RecvBuf[1024];
	  int  BufLen = 1024;
	  sockaddr_in SenderAddr;
	  int SenderAddrSize = sizeof(SenderAddr);

	  //-----------------------------------------------
	  // Initialize Winsock
	  WSAStartup(MAKEWORD(2,2), &wsaData);

	  //-----------------------------------------------
	  // Create a receiver socket to receive datagrams
	  RecvSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

	  //-----------------------------------------------
	  // Bind the socket to any address and the specified port.
	  RecvAddr.sin_family = AF_INET;
	  //RecvAddr.sin_port = htons(Port);
	  RecvAddr.sin_port = htons(Port);
	 // RecvAddr.sin_addr.s_addr = htonl(0x7F000001);//old line
	  RecvAddr.sin_addr.s_addr = INADDR_ANY;//tonl(0x7F000001);
	 
	  char tru = TRUE;
	  //int tru = 1;
	  if (setsockopt(RecvSocket, SOL_SOCKET, SO_REUSEADDR, &tru, sizeof(tru))==SOCKET_ERROR) cout << "setsockopt 1" << endl; 
	  
	 struct ip_mreq {
	   struct in_addr imr_multiaddr;
	   struct in_addr imr_interface;
	 };
	  
	  if (bind(RecvSocket, (SOCKADDR *) &RecvAddr, sizeof(RecvAddr))==SOCKET_ERROR) cout << "bind" << endl;
	  
	  struct ip_mreq mreq;
	  mreq.imr_multiaddr.s_addr = inet_addr(strIP);//("233.1.2.5");
	  //mreq.imr_multiaddr.s_addr = inet_addr(strIP);
	  
	  mreq.imr_interface.s_addr = INADDR_ANY;//htonl(0x7F000001);
	  if(setsockopt(RecvSocket, IPPROTO_IP, IP_ADD_MEMBERSHIP,
					  (char*)&mreq, sizeof(mreq))==SOCKET_ERROR) cout << "sockopt 2" << endl;
	                  
	int TTL = 7; // Restrict to our  network, for example
	  if (setsockopt(RecvSocket, IPPROTO_IP, IP_MULTICAST_TTL,
			   (char *)&TTL, sizeof(TTL))==SOCKET_ERROR) cout << "sockopt 3" << endl;

	// Set the local interface from which multicast is to be transmitted
	unsigned long addr = inet_addr("127.0.0.1");
	if (setsockopt(RecvSocket, IPPROTO_IP, IP_MULTICAST_IF, (char *)&addr,
	sizeof(addr))==SOCKET_ERROR) cout << "sockopt 4" << endl; 

	//-----------------------------------------------
	// Call the recvfrom function to receive datagrams
	// on the bound socket.
	if (DEBUGTEST)
		printf("Receiving datagrams...\n");
	
	if(recvfrom(RecvSocket, RecvBuf, BufLen, 0, (SOCKADDR *)&SenderAddr, &SenderAddrSize)==SOCKET_ERROR) 
		cout << "recvfrom" << endl;

	cout << RecvBuf << endl;

	return RecvBuf;


}

SOCKET RecvSocket=NULL;
 sockaddr_in SenderAddr;
int SenderAddrSize =0;

int recvData(int nport, char *strIP,char* RecvBuf, int bug)
{

	if (DEBUGTEST)
		printf(" in rcvData >%i ->%s",nport,strIP);

// need to chnage ip and port from param - TODO 
if (bug==1)
{

	//RecvBuf = &pkt1437[0];

	//strcpy(
	//memcpy(RecvBuf,&pkt1437[0],512);
	memcpy(RecvBuf,&pkt89[0],512);
	
	printf("in debugtest %c", RecvBuf[0]);
	return 0;
}

	int  BufLen = 1024*10;
	// initialize only once
	if (socketInit==false)
	{

		  WSADATA wsaData;
		  //SOCKET RecvSocket;
		  sockaddr_in RecvAddr;
		  int Port = nport;//34330;
		  //char RecvBuf[1024];
		  
		  //sockaddr_in SenderAddr;
		  SenderAddrSize = sizeof(SenderAddr);

		  //-----------------------------------------------
		  // Initialize Winsock
		  WSAStartup(MAKEWORD(2,2), &wsaData);

		  //-----------------------------------------------
		  // Create a receiver socket to receive datagrams
		  RecvSocket = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);

		  //-----------------------------------------------
		  // Bind the socket to any address and the specified port.
		  RecvAddr.sin_family = AF_INET;
		  //RecvAddr.sin_port = htons(Port);
		  RecvAddr.sin_port = htons(Port);
		 // RecvAddr.sin_addr.s_addr = htonl(0x7F000001);//old line
		  RecvAddr.sin_addr.s_addr = INADDR_ANY;//tonl(0x7F000001);
		 
		  char tru = TRUE;
		  //int tru = 1;
		  if (setsockopt(RecvSocket, SOL_SOCKET, SO_REUSEADDR, &tru, sizeof(tru))==SOCKET_ERROR) cout << "setsockopt 1" << endl; 
		  
		 struct ip_mreq {
		   struct in_addr imr_multiaddr;
		   struct in_addr imr_interface;
		 };
		  
		  if (bind(RecvSocket, (SOCKADDR *) &RecvAddr, sizeof(RecvAddr))==SOCKET_ERROR) cout << "bind" << endl;
		  
		  struct ip_mreq mreq;
		  mreq.imr_multiaddr.s_addr = inet_addr(strIP);//("233.1.2.5");
		  //mreq.imr_multiaddr.s_addr = inet_addr(strIP);
		  
		  mreq.imr_interface.s_addr = INADDR_ANY;//htonl(0x7F000001);
		  if(setsockopt(RecvSocket, IPPROTO_IP, IP_ADD_MEMBERSHIP,
						  (char*)&mreq, sizeof(mreq))==SOCKET_ERROR) cout << "sockopt 2" << endl;
		                  
		int TTL = 7; // Restrict to our  network, for example
		  if (setsockopt(RecvSocket, IPPROTO_IP, IP_MULTICAST_TTL,
				   (char *)&TTL, sizeof(TTL))==SOCKET_ERROR) cout << "sockopt 3" << endl;

		// Set the local interface from which multicast is to be transmitted
		unsigned long addr = inet_addr("127.0.0.1");
		if (setsockopt(RecvSocket, IPPROTO_IP, IP_MULTICAST_IF, (char *)&addr,
		sizeof(addr))==SOCKET_ERROR) cout << "sockopt 4" << endl; 
		
		socketInit=true;
	}// end of socketInit

	//-----------------------------------------------
	// Call the recvfrom function to receive datagrams
	// on the bound socket.
	if (DEBUGTEST)
	printf("Receiving datagrams...\n");
	
	int recvRet = 0;
	if((recvRet = recvfrom(RecvSocket, RecvBuf, BufLen, 0, (SOCKADDR *)&SenderAddr, &SenderAddrSize))==SOCKET_ERROR) 
		printf( "recvfrom <*ERROR*> RET %d",  recvRet );

	printf( "recvfrom RET %d",  recvRet );

	return 1;
}


SYSREADERN_API int RecvDataAndProcessMBP(int nport, char *strIP, int nTrCode, TKTABLE * tktable, int bug,long filterTkn, short saveFlg)
{

		time_t rawtime;
        struct tm * timeinfo;
        time(&rawtime );
        timeinfo = localtime ( &rawtime );
		if (DEBUGTEST)
		printf ( "\nCurrent local time and date: %s \n", asctime (timeinfo) );
		//char timeformat1[50]; // YYYY/MM/DD HH:MM:SS
		//sprintf(timeformat1, "%04d/%02d/%2d %02d:%02d:%02d", (1900+timeinfo->tm_year), (timeinfo->tm_mon+1), timeinfo->tm_mday, timeinfo->tm_hour, timeinfo->tm_min,timeinfo->tm_sec);
		//printf("\nTime in format 1 %s",timeformat1);
 
		//char timeformat2[50]; // YYYYMMDDHHMMSS
		//sprintf(timeformat2, "%04d%02d%2d%02d%02d%02d", (1900+timeinfo->tm_year), (timeinfo->tm_mon+1), timeinfo->tm_mday, timeinfo->tm_hour, timeinfo->tm_min,timeinfo->tm_sec);
		//printf("\nTime in format 1 %s",timeformat2);

// logging 
		 TRI_LOG_ON();
		TRI_LOG_STR ("Trivial logger in main().");


	clock_t start, finish1, finish2;
	double duration;
	start = clock();
 
	if (DEBUGTEST)
		printf("RecvDataAndProcessMBP START \n");
	//TKTABLE tktable;
	char RecvBuf[1024*10];
	int ret = recvData(nport, strIP, RecvBuf, bug);
	finish1 =clock();
	duration = (double)(finish1 - start) / CLOCKS_PER_SEC;
	if (DEBUGTEST)
		printf("\nrecvData DONE %i in time %2.2f Sec\n", ret, duration);

	char unCompbuffer[3072];
	ret = ProcessMBP(RecvBuf, unCompbuffer,nTrCode,tktable,filterTkn, saveFlg);
	finish2 =clock();

	duration = (double)(finish2 - start) / CLOCKS_PER_SEC;
    //printf( "%2.1f seconds\n", duration );

	if (DEBUGTEST)
		printf("RecvDataAndProcessMBP DONE %i in time %2.2f Sec\n", ret, duration);
	return ret;
	
}

//****************************************************************************************************************
//Commented By:SSP On:1/10/2010
//Desc: Since we commented threadpool.h we have to comment the functions which uses threadpool.h
//extern "C"  SYSREADERN_API  int getIt();
//
//SYSREADERN_API int getIt()
//{
//
//	char *RecvBuf;
//	char *unCompbuff=NULL;
//	boost::threadpool::pool processorPool(5);
//
//	RecvBuf = (char *)malloc( MAX_MESSAGE_SIZE*2 );
//	if (RecvBuf==NULL) 
//		printf("Buy some RAM@, need to exit now- TODO");
//
//	int ret;
//	int rcCnt=0;
//	while (1)
//	{
//		printf("recv data calling %i",rcCnt); 
//		ret =  recvData(34330, "233.1.2.5",RecvBuf, 1);
//		// chnage it letter processorPool.schedule( boost::bind(&ProcessMBP,RecvBuf,unCompbuff));
//		//boost::this_thread::sleep(boost::posix_time::milliseconds(9)); 
//		Sleep(10);
//		printf("recv data called %i\n",rcCnt);
//		rcCnt++;
//	}
//	return 1;
//}


//static int count=0;
//using namespace boost::threadpool;
//
//void first_task(int n);
// extern "C"  SYSREADERN_API  int initAll();
// extern "C"  SYSREADERN_API  int  initAllBooster();
//void first_task(int n)
//  {
//	
//	//int cnt=++::count; 
//	int cnt=n;
//	
//	  printf("count=%i",cnt );
//	  int loop=30;
//	  if (cnt==1)
//		  loop=100;
//	  if (cnt==2)
//		  loop=500;
//	  printf("loop=%i\n",loop);
//	  for (int j=0;j<loop;j++)
//	  {
//		  printf("\tThread %i loop %i\n", cnt,j);
//		
//		boost::this_thread::sleep(boost::posix_time::milliseconds(3)); 
//	  }
//		printf("\n");
//	  
//  }
//
//
//SYSREADERN_API  int  initAll()
//{
//	// Create a thread pool.
//     pool tp(2);
//int c =0;
//	 printf("start --> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//
//	 // Add some tasks to the pool.
//	 tp.schedule(boost::bind(&first_task,c++));
//	printf("start 1--> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//    tp.schedule(boost::bind(&first_task,c++));
//    printf("start 2--> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//    
//	tp.schedule(boost::bind(&first_task,c++));
//	printf("start 3--> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//    tp.schedule(boost::bind(&first_task,c++));
//	printf("start 4--> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//	//tp.schedule(&first_task);
//	//tp.schedule(&first_task);
//	//tp.schedule(&first_task);
////	tp.schedule(&first_task);
//	//tp.wait();
//	tp.schedule(boost::bind(&first_task,c++));
//	printf("start 5--> %i %i %i\n", tp.size(),tp.active(), tp.pending());
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//	tp.schedule(boost::bind(&first_task,c++));
//
//
//	return 1;	
//}
//
//#include <boost/thread/thread.hpp>
//#include <boost/thread/mutex.hpp>
//#include <boost/thread/condition.hpp>
//#include <iostream>
//
//
//SYSREADERN_API  int  initAllBooster()
//{
//  boost::thread thrd1(boost::bind(&first_task,1));
//boost::thread thrd2(boost::bind(&first_task,2));
//
//  
//  
//  thrd1.join();
//  thrd2.join();
//
//
//	
//
//	return 1;	
//}
//
//SYSREADERN_API  void  setTestInit(int i)
//{
//	testInt=i;
//	printf("setting to %i",i);
//}
//
//SYSREADERN_API  int  getTestInit()
//{
//	return testInt;
//}
//****************************************************************************************************************

SYSREADERN_API  void   setPrintDebug(short debug)
{
	DEBUGTEST=debug;
}

SYSREADERN_API    long getVersion()
{
	return 062610001; // cm - equity struct updated
	//return 103009001;
	//return 81209001;

}


//****************************************************************************************************************
//Added By:SSP On:1/10/2010
//Adding three functions declarations here

//1]GetLtp:
//Desc: This Functions are used to retrive F&O Token and LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetLTP(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,FutureRate * futrt)
{
	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				futrt->token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				futrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for


	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	return intIndex;
}

//2]GetLtpEQ:
//Desc: This Functions are used to retrive EQ Token and LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetLTPEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,EQRate * eqrt)
{
	if (DEBUGTEST)
	printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				eqrt->token[intIndex]=ntohs(broadcastOnlyMBP->onlyMPBData[j].Token);
				eqrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}

//3]GetLTP2:
//Desc: This Functions are used to retrive both F&O / EQ Token and LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetLTP2(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,FutureRate * futrt,EQRate * eqrt ,int intEQFlag)
{
	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	short trCode;
	//int errcode1=0;

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		tmpPtr+= (i*2)+bufLen;
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		bufLen+=compBytes;
		
		if(intEQFlag==0)
		{
			//map tp msg header ie set the pointer to future value since lzo will decode it
			msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
			// point broadcastOnlyMBP to data struct
			broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
			if (DEBUGTEST)
				printf("\t\t GetLTP:lzoing.. ");
			errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
			if (DEBUGTEST)
				printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
			sizeCtr += ssize;
			//print msge header

			if (DEBUGTEST)
				PrintMsgHeader(msgHdr);
			// filer unwanted tr code
			trCode = ntohs(msgHdr->TransactionCode);
		
			if (trCode==filterTrCode)// we re looking for 7208
			{
				//if (DEBUGTEST)
				printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
				//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
				noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
				for(int j=0;j<noOfrec;j++)
				{
					futrt->token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
					futrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
					intIndex=intIndex+1;
				}
			}
			else
			{
				//if (DEBUGTEST)
				printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
			}
		}
		else
		{
			//map tp msg header ie set the pointer to future value since lzo will decode it
			cmmsgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
			//point broadcastOnlyMBP to data struct
			cmbroadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);

			if (DEBUGTEST)
				printf("\t\t GetLTP:lzoing.. ");
			errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
			if (DEBUGTEST)
				printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
			sizeCtr += ssize;
			//print msge header

			if (DEBUGTEST)
				PrintMsgHeader_CM(cmmsgHdr);
			// filer unwanted tr code
			trCode = ntohs(cmmsgHdr->TransactionCode);
		
			if (trCode==filterTrCode)// we re looking for 7208
			{
				//if (DEBUGTEST)
				printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
				//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
				noOfrec = ntohs(cmbroadcastOnlyMBP->noOfRecords);
				for(int j=0;j<noOfrec;j++)
				{
					eqrt->token[intIndex]=ntohs(cmbroadcastOnlyMBP->onlyMPBData[j].Token);
					eqrt->ltp[intIndex] = ntohl(cmbroadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
					intIndex=intIndex+1;
				}
			}
			else
			{
				//if (DEBUGTEST)
				printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
			}
		}
		
	}  // end for


	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	return intIndex;
}

//****************************************************************************************************************

//****************************************************************************************************************
//Added By:SSP On:1/12/2010
//Adding three functions declarations here

//4]GetBuySell:
//Desc: This Functions are used to retrive F&O Token, LTP, Buy LTP and Sell LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetBuySell(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,BuySellRate * futrt)
{

	//Added By HVB For testing of packet reachability
	//>>>START
	char tempBuffer[99999];
	char fileName[1024] = "PacketTraceLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");
	//<<<END

	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	//>>LOG
	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	WriteDetailLog(fileName,1,"",tempBuffer);
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

	strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
	WriteDetailLog(fileName,1,"","Input Buffer : ");
	WriteDetailLog(fileName,1,"",tempBuffer);
	
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

	sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
	WriteDetailLog(fileName,1,"",tempBuffer);
	
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
	WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
	WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
	//<<LOG

	//>>LOG
	sprintf(tempBuffer,"\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);	
	WriteDetailLog(fileName,1,"",tempBuffer);
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
	//<<LOG

	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		//>>LOG
		WriteDetailLog(fileName,1,"","Decompression completed");
		//<<LOG

		if (DEBUGTEST)
			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
			
			//>>LOG
			sprintf(tempBuffer,"\t\t GetLTP:FOUND trcode %i\n", trCode);
			WriteDetailLog(fileName,1,"",tempBuffer);
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			//<<LOG

			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			
			//>>LOG
			sprintf(tempBuffer,"\t\t GetMBP:FOUND Records %i\n", noOfrec);
			WriteDetailLog(fileName,1,"",tempBuffer);
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			//<<LOG

			for(int j=0;j<noOfrec;j++)
			{
				futrt->token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				
				//>>LOG
				sprintf(tempBuffer,"\t\t GetMBP:FOUND Token %ld\n", ntohl(broadcastOnlyMBP->onlyMPBData[j].Token));
				WriteDetailLog(fileName,1,"",tempBuffer);
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
				//<<LOG
				
				futrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				futrt->buyrate[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[0].Price);
				futrt->sellrate[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[5].Price);

				//>>LOG
				sprintf(tempBuffer,"\t\t GetMBP:FOUND LTP %i\n", ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice));
				WriteDetailLog(fileName,1,"",tempBuffer);
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
				//<<LOG

				//Added NYN 14/11/2013
				futrt->buyqty[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[0].Quantity);
				futrt->sellqty[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[5].Quantity);

				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
			
			//>>LOG
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"Skipped as different tr code was recieved ::: trCode[%i]\tfilterTrCode[%i]\n", trCode,filterTrCode);
			WriteDetailLog(fileName,1,"",tempBuffer);
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			//<<LOG

		}
		
	}  // end for






	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	
	//>>LOG
	WriteDetailLog(fileName,1,"","GetMBP Done closing method now!!");
	//<<LOG

	return intIndex;
}


SYSREADERN_API int _stdcall AddTwoNos(int num1)
{
	
	return (num1);
}

//****************************************************************************************************************
//Added By:SSP On:27/05/2011
//Adding three functions declarations here

//5]GetLtpLtqVtt:
//Desc: This Functions are used to retrive F&O Token, LTP, LTQ and VTT
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetLtpLtqVtt(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,LtpLtqVtt * eqrt)
{
	if (DEBUGTEST)
	printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				eqrt->token[intIndex]=ntohs(broadcastOnlyMBP->onlyMPBData[j].Token);
				eqrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				eqrt->ltq[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				eqrt->vtt[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);

				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}


/* ********************************
		Added By : HVB 
		Date     : 24/06/2014
		Desc.    : Method to write log file
   *********************************** */

void AppendCurrentDateTime(char * FileName,short AppendTime)
{
	time_t rawtime;
	struct tm* timeinfo;
	
	time( &rawtime );
	timeinfo = localtime( &rawtime );
	char timeChar[25];
	
	// Format Date Time As Char * DD/MM/YYYY HH:MM:SS
	if(AppendTime == 1)
		strftime (timeChar,25,"%d-%m-%y_%H-%M-%S",timeinfo);
	else
		strftime (timeChar,25,"%d-%m-%y",timeinfo);
	printf ("Current Time: %s",timeChar);
	strncat(FileName,timeChar,strlen(FileName)+strlen(timeChar));
	printf("File Name: %s",FileName);
}

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Truncates while space from source string and gives pointer to new location
				   CODE taken from http://stackoverflow.com/questions/122616/how-do-i-trim-leading-trailing-whitespace-in-a-standard-way?page=1&tab=votes#tab-top
   *********************************** */

// Note: This function returns a pointer to a substring of the original string.
// If the given string was allocated dynamically, the caller must not overwrite
// that pointer with the returned value, since the original pointer must be
// deallocated using the same allocator with which it was allocated.  The return
// value must NOT be deallocated using free() etc.
/*char * Trim(char *str)
{
  char *end;

  // Trim leading space
  while(isspace((unsigned char)*str)) str++;

  if(*str == 0)  // All spaces?
    return str;

  // Trim trailing space
  end = str + strlen(str) - 1;
  while(end > str && isspace((unsigned char)*end)) end--;

  // Write new null terminator
  *(end+1) = 0;

  return str;
}*/

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Converts char* to uppercase char*
   *********************************** */
/*char * UCase(char *str)
{
    int i = 0;
    char c;
    while (str[i])
    {
        c=str[i];
        str[i]=toupper(c);
        i++;
    }
    return str;
}*/

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Removes all blank space from text and gives upper case for text
   *********************************** */
char* UCaseWsTrim(char* str)
{
    int i = 0;
    int j = 0;
    char t[21] = "";
    char c;
    while (str[i])
    {
        c=str[i];
        if(c!=' ')
            t[j++]=toupper(c);
        i++;
    }
    memset(str,'\0',21);
    strcpy(str,t);
    return str;
}

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Deletes file by checking if file exists already
   *********************************** */
void DeleteFileSafely(char * FileName)
{
	if (FILE *file = fopen(FileName, "r")) {
        fclose(file);
        remove(FileName);
    } 
	//else
		//File doesn't exist no need to delete
}



void WriteDetailLog(char * FileName, char * Module,char * Message)
{
	FILE * pFileTXT;
	time_t rawtime;
	struct tm* timeinfo;
	
	time( &rawtime );
	timeinfo = localtime( &rawtime );
	char timeChar[25];
	
	// Format Date Time As Char * DD/MM/YYYY HH:MM:SS
	strftime (timeChar,25,"%d/%m/%y %H:%M:%S",timeinfo);

	pFileTXT = fopen(FileName,"a");
	fprintf(pFileTXT,"==============================\n");
	fprintf(pFileTXT,"Log Time    : %s\n",timeChar);
	if (Module != "")
		fprintf(pFileTXT,"Module Name : %s\n",Module);
	fprintf(pFileTXT,"---> Log Message  \n");
	//fprintf(pFileTXT,Message);
	fwrite(Message,1,sizeof(Message)+1,pFileTXT);
	fprintf(pFileTXT,"\n");
	fprintf(pFileTXT,"<--- Log Message  \n");
	fprintf(pFileTXT,"==============================\n");
	fclose (pFileTXT); // must close after opening
}

void WriteDetailLog(char * FileName,int PrintOnlyMsg, char * Module,char * Message)
{
	FILE * pFileTXT;
	pFileTXT = fopen(FileName,"a");

	if(PrintOnlyMsg == 0)
	{
		time_t rawtime;
		struct tm* timeinfo;
		
		time( &rawtime );
		timeinfo = localtime( &rawtime );
		char timeChar[25];
		
		// Format Date Time As Char * DD/MM/YYYY HH:MM:SS
		strftime (timeChar,25,"%d/%m/%y %H:%M:%S",timeinfo);

		
		fprintf(pFileTXT,"==============================\n");
		fprintf(pFileTXT,"Log Time    : %s\n",timeChar);
		if (Module != "")
			fprintf(pFileTXT,"Module Name : %s\n",Module);
		fprintf(pFileTXT,"---> Log Message  \n");
	}
	else if(PrintOnlyMsg == 2)
	{
		time_t rawtime;
		struct tm* timeinfo;
		
		time( &rawtime );
		timeinfo = localtime( &rawtime );
		char timeChar[25];
		
		// Format Date Time As Char * DD/MM/YYYY HH:MM:SS
		strftime (timeChar,25,"%d/%m/%y %H:%M:%S",timeinfo);

		
		fprintf(pFileTXT,"==============================\n");
		fprintf(pFileTXT,"Log Time    : %s\n",timeChar);
	}

	//fprintf(pFileTXT,Message);
	fwrite(Message,1,strlen(Message)+1,pFileTXT);
	fprintf(pFileTXT,"\n");
	
	if(PrintOnlyMsg == 0)
	{
		fprintf(pFileTXT,"<--- Log Message  \n");
		fprintf(pFileTXT,"==============================\n");
	}
	fclose (pFileTXT); // must close after opening
}

/* ********************************
		Added By : HVB 
		Date     : 24/06/2014
		Desc.    : Method to get trcodes from Broadcast data in log file
				   Returns 1 if every thing went fine
   *********************************** */

SYSREADERN_API int _stdcall GetTrCodes(char * FileNameForTr,char * streamBuffer,char * unCompbuffer)
{

	char tempBuffer[1024];

	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode != 0)
		{
			int TrCode = trCode; 
			sprintf(tempBuffer,"Found TRCODE = %d",TrCode);
			//WriteDetailLog(FileNameForTr,"",tempBuffer);
		}
		else
			printf("Nothing");
			//WriteDetailLog(FileNameForTr,"","TR_CODE WAS ZERO !!!!");
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	return 1;
}

SYSREADERN_API int _stdcall GetTrCodesNew(char * FileNameForTr,char * streamBuffer,char * unCompbuffer,char * retBuffer)
{

	char tempBuffer[1024];
	//char retBuffer[99999];

	if (DEBUGTEST)
	printf("\n GetTrCodesNew:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetTrCodesNew: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	sprintf(tempBuffer,"No Of Packets = %d;",noOfpacket);
	strncpy(retBuffer,tempBuffer,sizeof(tempBuffer));
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetTrCodesNew: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetTrCodesNew: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetTrCodesNew:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetTrCodesNew:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode != 0)
		{
			int TrCode = trCode; 
			sprintf(tempBuffer,"Found TRCODE = %d;",TrCode);
			strncat(retBuffer,tempBuffer,strlen(tempBuffer));
			//WriteDetailLog(FileNameForTr,"",tempBuffer);
		}
		else
		{
			sprintf(tempBuffer,"TRCODE Was %d;",0);
			strncat(retBuffer,tempBuffer,strlen(tempBuffer));
			//strcat(retBuffer,"TR_CODE WAS ZERO !!!!");
			//WriteDetailLog(FileNameForTr,"","TR_CODE WAS ZERO !!!!");
		}
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tGetTrCodesNew:final uncompress size%i \n",ssize);
		printf("GetTrCodesNew:DONE\n");
	}
	//WriteDetailLog(FileNameForTr,"",retBuffer);
	return 1;
}

/* ********************************
		Added By : HVB 
		Date     : 23/06/2014
		Desc.    : Method to decode structure for ticker and market
				   Returns number of tokens it had
   *********************************** */

SYSREADERN_API int _stdcall GetTickerAndMarket(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,BCAST_TICKER_AND_MKT_INDEX * ticker) // mbp
{
	char tempBuffer[99999];
	char fileName[1024] = "TickerDataLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");

	//WriteDetailLog(fileName,0,"","Starting Get Ticker");

	//if (DEBUGTEST)
		printf("\n GetTicker:Start\n");
	
	printf("rec buf len : %d",strlen(streamBuffer));

	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

	strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"","Input Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	//printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);
	
	sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);
	
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	//printf("Recieved UnComp Buffer (At Start)= %s\n",tempBuffer);
	
	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
	
	// Added By HVB
	BCAST_TICKER_AND_MKT_INDEX * tickerData;
    
	pac = (BcastPackData *)&streamBuffer[0];

	short noOfpacket = htons(pac->iNoPackets);

	int intIndex=0;
	int noOfrec=0;
	
	//if (DEBUGTEST)
	//printf("\t GetTicker: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	
	char *tmpPtr = pac->cPackData;

	
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	
	
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;

		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		//if (DEBUGTEST)
		printf("\t\ GetTicker: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetTicker: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		// changed as unsigned char * from char *
		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);

		//if (DEBUGTEST)
			printf("\t\t GetTicker:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		//if (DEBUGTEST)
			printf("\t\t GetTicker:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		//if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code

		
		short trCode = ntohs(msgHdr->TransactionCode);

		printf("Found TrCode = %hd",msgHdr->TransactionCode);

		if (trCode==filterTrCode)// we re looking for 7202
		{
			ticker->msgHeader.AlphaChar[0] = msgHdr->AlphaChar[0];
			ticker->msgHeader.AlphaChar[1] = msgHdr->AlphaChar[1];
			ticker->msgHeader.ErrorCode = ntohs(msgHdr->ErrorCode);
			ticker->msgHeader.iApiFuncId = msgHdr->iApiFuncId;
			ticker->msgHeader.iApiFuncId2 = msgHdr->iApiFuncId2;
			ticker->msgHeader.iApiFuncId3 = msgHdr->iApiFuncId3;
			ticker->msgHeader.iApiTcode = msgHdr->iApiTcode;
			ticker->msgHeader.LogTime = ntohl(msgHdr->LogTime);
			ticker->msgHeader.MessageLength  = ntohs(msgHdr->MessageLength);
			for (int iN=0;iN<8;iN++)
			{
				ticker->msgHeader.Timestamp[iN] = msgHdr->Timestamp[iN];
				ticker->msgHeader.TimeStamp1[iN] = msgHdr->TimeStamp1[iN];
				ticker->msgHeader.TimeStamp2[iN] = msgHdr->TimeStamp2[iN];
			}
			ticker->msgHeader.TransactionCode = ntohs(msgHdr->TransactionCode);

			ticker->noOfRecords = ntohs(tickerData->noOfRecords);
			
			//if (DEBUGTEST)
				printf("\t\t GetTicker:FOUND trcode %i\n", trCode);
			noOfrec = ntohs(tickerData->noOfRecords);

			for(int k=0;k < noOfrec;k++)
			{
				//if (DEBUGTEST)
				printf("Token[%i] = [%i]",k,tickerData->tickerInfo[k].Token);
				
				sprintf(tempBuffer,"Token[%d] = %d",k,ntohl(tickerData->tickerInfo[k].Token));
				//WriteDetailLog(fileName,1,"",tempBuffer);

				ticker->tickerInfo[k].Token = ntohl(tickerData->tickerInfo[k].Token);

				sprintf(tempBuffer,"MarketType[%d] = %d",k,ntohs(tickerData->tickerInfo[k].MarketType));
				//WriteDetailLog(fileName,1,"",tempBuffer);

				ticker->tickerInfo[k].MarketType = ntohs(tickerData->tickerInfo[k].MarketType);
				
				ticker->tickerInfo[k].FillPrice = ntohl(tickerData->tickerInfo[k].FillPrice);
				ticker->tickerInfo[k].FillVolume = ntohl(tickerData->tickerInfo[k].FillVolume);

				ticker->tickerInfo[k].OpenInterest = ntohl(tickerData->tickerInfo[k].OpenInterest);
				ticker->tickerInfo[k].DayHiOI = ntohl(tickerData->tickerInfo[k].DayHiOI);
				ticker->tickerInfo[k].DayLoOI = ntohl(tickerData->tickerInfo[k].DayLoOI);

				intIndex=intIndex+1;

			}

		}
		else
		{
			//if (DEBUGTEST)
				printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
	
	}  // end for
	

	if (DEBUGTEST)
	{
		printf("\t\GetTicker:final uncompress size%i \n",ssize);
		printf("GetTicker:DONE\n");
	}

	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"","Input Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);
	
	sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);

	//WriteDetailLog(fileName,1,"","Get Ticker Over\n*****************");
	return intIndex;	
}

SYSREADERN_API  int _stdcall GetTickerAndMarketNew(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnly * bticker)
{
	char tempBuffer[99999];
	char fileName[1024] = "TickerDataLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");
	
	if(saveFlg == 1)
		WriteDetailLog(fileName,0,"","Starting Get Ticker");

	if (DEBUGTEST)
		printf("\n GetTicker:Start\n");

	if (DEBUGTEST)
		printf("rec buf len : %d",strlen(streamBuffer));
	
	if(saveFlg == 1)
	{
		sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
	
		strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
		WriteDetailLog(fileName,1,"","Input Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	}

	if (DEBUGTEST)
		printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);
	
	if(saveFlg == 1)
	{
		sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

		strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	}

	if (DEBUGTEST)
		printf("Recieved UnComp Buffer (At Start)= %s\n",tempBuffer);
	
	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
	
	// Added By HVB
	BCAST_TICKER_AND_MKT_INDEX * tickerData;
    
	pac = (BcastPackData *)&streamBuffer[0];

	short noOfpacket = htons(pac->iNoPackets);

	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetTicker: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	
	char *tmpPtr = pac->cPackData;
	
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	
	if(noOfpacket <= 0)
	{
		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"No of packets = %d",noOfpacket);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}
	}
	
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;

		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
			printf("\t\ GetTicker: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetTicker: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		// changed as unsigned char * from char *
		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);

		if (DEBUGTEST)
			printf("\t\t GetTicker:lzoing.. ");
		
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetTicker:ssize %i errcode %i\n",ssize,errcode1);

		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code

		
		short trCode = ntohs(msgHdr->TransactionCode);

		if (DEBUGTEST)
			printf("Found TrCode = %hd",msgHdr->TransactionCode);

		if (trCode==filterTrCode)// we re looking for 7202
		{
			
			//if (DEBUGTEST)
				printf("\t\t GetTicker:FOUND trcode %i\n", trCode);
				noOfrec = ntohs(tickerData->noOfRecords);

			for(int k=0;k < noOfrec;k++)
			{
				//it sud b intIndex on place of k at left side
				if (DEBUGTEST)
					printf("Token[%i] = [%i]",k,tickerData->tickerInfo[k].Token);
				
				if(saveFlg == 1)
				{
					memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
					sprintf(tempBuffer,"Token[%d] = %ld",k,ntohl(tickerData->tickerInfo[k].Token));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

				bticker->Token[k] = ntohl(tickerData->tickerInfo[k].Token);
				
				if(saveFlg == 1)
				{
					memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
					sprintf(tempBuffer,"MarketType[%d] = %d",k,ntohs(tickerData->tickerInfo[k].MarketType));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

				bticker->MarketType[k] = ntohs(tickerData->tickerInfo[k].MarketType);
				
				bticker->FillPrice[k] = ntohl(tickerData->tickerInfo[k].FillPrice);
				bticker->FillVolume[k] = ntohl(tickerData->tickerInfo[k].FillVolume);

				bticker->OpenInterest[k] = ntohl(tickerData->tickerInfo[k].OpenInterest);
				bticker->DayHiOI[k] = ntohl(tickerData->tickerInfo[k].DayHiOI);
				bticker->DayLoOI[k] = ntohl(tickerData->tickerInfo[k].DayLoOI);

				intIndex=intIndex+1;

			}

		}
		else
		{
			if (DEBUGTEST)
				printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
	
	}  // end for
	

	if (DEBUGTEST)
	{
		printf("\t\GetTicker:final uncompress size%i \n",ssize);
		printf("GetTicker:DONE\n");
	}
	
	if(saveFlg == 1)
	{
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
		sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);

		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
		strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
		WriteDetailLog(fileName,1,"","Input Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	}

	if (DEBUGTEST)
		printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);

	if(saveFlg == 1)
	{
		sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
		strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
		WriteDetailLog(fileName,1,"","Get Ticker Over\n*****************");
	}

	return intIndex;	
}

SYSREADERN_API  int _stdcall GetTickerAndMarketNewCD(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnly * bticker)
{
	char tempBuffer[99999];
	char fileName[1024] = "TickerDataLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");

	if(saveFlg == 1)
	{
		WriteDetailLog(fileName,0,"","Starting Get Ticker");
	}
	
	if (DEBUGTEST)
	{
		printf("\n GetTicker:Start\n");
		printf("rec buf len : %d",strlen(streamBuffer));
	}
	
	if(saveFlg == 1)
	{
		sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

		strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
		WriteDetailLog(fileName,1,"","Input Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	
		sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
	
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

		strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	}

	if (DEBUGTEST)
			printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);

	if (DEBUGTEST)
			printf("Recieved UnComp Buffer (At Start)= %s\n",tempBuffer);
	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	//MessageHeader *msgHdr;
	Message_HeaderCD *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
	
	// Added By HVB
	BCAST_TICKER_AND_MKT_INDEX * tickerData;
    
	pac = (BcastPackData *)&streamBuffer[0];

	short noOfpacket = htons(pac->iNoPackets);

	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetTicker: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	
	char *tmpPtr = pac->cPackData;
	
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	
	if(noOfpacket <= 0)
	{
		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"No of packets = %d",noOfpacket);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}
	}

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;

		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		
		if (DEBUGTEST)
			printf("\t\ GetTicker: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes==0)
		{
			printf("\t\ GetTicker: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (Message_HeaderCD *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		// changed as unsigned char * from char *
		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);

		if (DEBUGTEST)
			printf("\t\t GetTicker:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetTicker:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		//if (DEBUGTEST)
		//	PrintMsgHeader(msgHdr);
		// filer unwanted tr code

		
		short trCode = ntohs(msgHdr->TransactionCode);

		if (DEBUGTEST)
			printf("Found TrCode = %hd",msgHdr->TransactionCode);

		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"\nFound TrCode = %hd\n",trCode);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}

		if (trCode==filterTrCode)// we re looking for 7202
		{
			
			if (DEBUGTEST)
				printf("\t\t GetTicker:FOUND trcode %i\n", trCode);

			noOfrec = ntohs(tickerData->noOfRecords);

			for(int k=0;k < noOfrec;k++)
			{
				//it sud b intIndex on place of k at left side
				if (DEBUGTEST)
					printf("Token[%i] = [%i]",k,tickerData->tickerInfo[k].Token);
				
				if(saveFlg == 1)
				{
					sprintf(tempBuffer,"Token[%d] = %ld",k,ntohl(tickerData->tickerInfo[k].Token));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

				bticker->Token[k] = ntohl(tickerData->tickerInfo[k].Token);

				if(saveFlg == 1)
				{
					sprintf(tempBuffer,"MarketType[%d] = %d",k,ntohs(tickerData->tickerInfo[k].MarketType));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

				bticker->MarketType[k] = ntohs(tickerData->tickerInfo[k].MarketType);
				
				bticker->FillPrice[k] = ntohl(tickerData->tickerInfo[k].FillPrice);
				bticker->FillVolume[k] = ntohl(tickerData->tickerInfo[k].FillVolume);

				bticker->OpenInterest[k] = ntohl(tickerData->tickerInfo[k].OpenInterest);
				bticker->DayHiOI[k] = ntohl(tickerData->tickerInfo[k].DayHiOI);
				bticker->DayLoOI[k] = ntohl(tickerData->tickerInfo[k].DayLoOI);

				intIndex=intIndex+1;

			}

		}
		else
		{
			if (DEBUGTEST)
				printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
	
	}  // end for
	

	if (DEBUGTEST)
	{
		printf("\t\GetTicker:final uncompress size%i \n",ssize);
		printf("GetTicker:DONE\n");
	}

	if(saveFlg == 1)
	{
		sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

		strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
		WriteDetailLog(fileName,1,"","Input Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);
	
		sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"",tempBuffer);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

		strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
		WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
		WriteDetailLog(fileName,1,"",tempBuffer);

		WriteDetailLog(fileName,1,"","Get Ticker Over\n*****************");
	}

	if (DEBUGTEST)
		printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);

	return intIndex;	
}


SYSREADERN_API  int _stdcall WriteTickerAndMarket(char * FileNameToWrite,char * streamBuffer,char * unCompbuffer)
{
	char tempBuffer[99999];
	char fileName[1024] = "TickerDataLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");

	//WriteDetailLog(fileName,0,"","Starting Get Ticker");

	//if (DEBUGTEST)
		printf("\n GetTicker:Start\n");
	
	printf("rec buf len : %d",strlen(streamBuffer));

	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));

	strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"","Input Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);
	
	sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);
	
	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	printf("Recieved UnComp Buffer (At Start)= %s\n",tempBuffer);
	
	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
	
	// Added By HVB
	BCAST_TICKER_AND_MKT_INDEX * tickerData;
    
	pac = (BcastPackData *)&streamBuffer[0];

	short noOfpacket = htons(pac->iNoPackets);

	int intIndex=0;
	int noOfrec=0;
	
	//if (DEBUGTEST)
	printf("\t GetTicker: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	
	char *tmpPtr = pac->cPackData;

	
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	
	
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;

		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		//if (DEBUGTEST)
		printf("\t\ GetTicker: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetTicker: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		// changed as unsigned char * from char *
		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);

		//if (DEBUGTEST)
			printf("\t\t GetTicker:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		//if (DEBUGTEST)
			printf("\t\t GetTicker:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		//if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code

		
		short trCode = ntohs(msgHdr->TransactionCode);

		printf("Found TrCode = %hd",msgHdr->TransactionCode);

		int filterTrCode = 7202;
		
		WriteDetailLog(fileName,2,"","Start reading packet for TR-CODE 7202");
		if (trCode==filterTrCode)// we re looking for 7202
		{
			
			//if (DEBUGTEST)
				printf("\t\t GetTicker:FOUND trcode %i\n", trCode);
				noOfrec = ntohs(tickerData->noOfRecords);

			for(int k=0;k < noOfrec;k++)
			{
				//if (DEBUGTEST)
				printf("Token[%i] = [%i]",k,tickerData->tickerInfo[k].Token);
				
				long test = ntohl(tickerData->tickerInfo[k].Token);
				short t2 = ntohs(tickerData->tickerInfo[k].Token);

				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"Token[%i] = %ld\n",k,ntohl(tickerData->tickerInfo[k].Token));
				WriteDetailLog(fileName,1,"",tempBuffer);
				
				//Writing only log so ommited
				//bticker->Token[k] = ntohl(tickerData->tickerInfo[k].Token);
				
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"MarketType[%i] = %i\n",k,ntohs(tickerData->tickerInfo[k].MarketType));
				WriteDetailLog(fileName,1,"",tempBuffer);

				//bticker->MarketType[k] = ntohs(tickerData->tickerInfo[k].MarketType);
				
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"FillPrice[%i] = %ld\n",k,ntohl(tickerData->tickerInfo[k].FillPrice));
				WriteDetailLog(fileName,1,"",tempBuffer);

				//bticker->FillPrice[k] = ntohl(tickerData->tickerInfo[k].FillPrice);

				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"FillVolume[%d] = %ld\n",k,ntohl(tickerData->tickerInfo[k].FillVolume));
				WriteDetailLog(fileName,1,"",tempBuffer);

				//bticker->FillVolume[k] = ntohl(tickerData->tickerInfo[k].FillVolume);
				
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"OpenInterest[%d] = %ld\n",k,ntohl(tickerData->tickerInfo[k].OpenInterest));
				WriteDetailLog(fileName,1,"",tempBuffer);

				//bticker->OpenInterest[k] = ntohl(tickerData->tickerInfo[k].OpenInterest);

				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"DayHiOI[%d] = %ld\n",k,ntohl(tickerData->tickerInfo[k].DayHiOI));
				WriteDetailLog(fileName,1,"",tempBuffer);
				
				//bticker->DayHiOI[k] = ntohl(tickerData->tickerInfo[k].DayHiOI);

				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer,"DayLoOI[%d] = %ld\n",k,ntohl(tickerData->tickerInfo[k].DayLoOI));
				WriteDetailLog(fileName,1,"",tempBuffer);

				//bticker->DayLoOI[k] = ntohl(tickerData->tickerInfo[k].DayLoOI);

				intIndex=intIndex+1;

			}

		}
		else
		{
			//if (DEBUGTEST)
				printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );
				sprintf(tempBuffer ,"GetTicker:- Different TRCode [%i] / Expecting = [%i] .... Skipped \n", trCode,filterTrCode);
				WriteDetailLog(fileName,1,"",tempBuffer);
		}
	
	}  // end for
	

	if (DEBUGTEST)
	{
		printf("\t\GetTicker:final uncompress size%i \n",ssize);
		printf("GetTicker:DONE\n");
	}

	sprintf(tempBuffer,"Length Of Input Buffer With StrLen = %d",strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,streamBuffer,strlen(streamBuffer));
	//WriteDetailLog(fileName,1,"","Input Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);
	printf("Recieved Comp Buffer (At Start)= %s\n",tempBuffer);
	
	sprintf(tempBuffer,"Length Of Input UnComp Buffer With StrLen = %d",strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"",tempBuffer);

	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer) );

	strncpy(tempBuffer,unCompbuffer,strlen(unCompbuffer));
	//WriteDetailLog(fileName,1,"","Uncomp Buffer : ");
	//WriteDetailLog(fileName,1,"",tempBuffer);

	//WriteDetailLog(fileName,1,"","Get Ticker Over\n*****************");
	return intIndex;	
}

//SYSREADERN_API int _stdcall GetTickerAndMarket(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,BCAST_TICKER_AND_MKT_INDEX * ticker) // mbp
//{
//	char tempBuffer[99999];
//	char fileName[1024] = "TickerDataLog_";
//	AppendCurrentDateTime(fileName,0);
//	strcat(fileName,".log");
//	//char retBuffer[99999];
//
//	if (DEBUGTEST)
//	printf("\n GetLTP:Start\n");
//	
//	WriteDetailLog(fileName,0,"GetTickerAndMarket Method From Dll","Method Execution Started");
//	
//	sprintf(tempBuffer,"Recieved Comp Buffer (1st Param)= %s",streamBuffer);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//	
//	sprintf(tempBuffer,"Recieved UnCompBuffer (2nd Param)= %s",unCompbuffer);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//
//	unsigned  short errcode1=0;
//	unsigned long ssize=0;
//
//	BcastPackData *pac;
//	BcastCmpPacket *compPac;
//	MessageHeader *msgHdr;
//	CMMessageHeader *cmmsgHdr;
//	BroadCastOnlyMBP * broadcastOnlyMBP;
//	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
//	
//	// Added By HVB
//	BCAST_TICKER_AND_MKT_INDEX * tickerData;
//
//	WriteDetailLog(fileName,1,"","Casting stream buffer to broadcast packet\n ******************");
//	sprintf(tempBuffer,"Pointer Address Of Stream Buffer = %d\nPointer Address Of Braodcast Packet = %d",&streamBuffer[0],(BcastPackData *)&streamBuffer[0]);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//    
//	pac = (BcastPackData *)&streamBuffer[0];
//	
//	sprintf(tempBuffer,"Casted Pointer Address Of Braodcast Packet = %d",&pac);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//	
//	WriteDetailLog(fileName,1,"","Casting Done\n ******************");
//
//	short noOfpacket = htons(pac->iNoPackets);
//
//	sprintf(tempBuffer,"Number Of Packets from broadcast packet = %i",noOfpacket);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//
//	int intIndex=0;
//	int noOfrec=0;
//	
//	if (DEBUGTEST)
//	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
//	
//	sprintf(tempBuffer,"From broadcast packet cNetId = %c , No Of Packets = %c\n",pac->cNetId,pac->iNoPackets);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//
//	
//	sprintf(tempBuffer,"From broadcast packet Packet Data = %s",pac->cPackData);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//
//	char *tmpPtr = pac->cPackData;
//
//	sprintf(tempBuffer,"Saved Data To Local Variable = %s",tmpPtr);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//	
//	short bufLen=0; 
//	short sizeCtr=0;
//	long token;
//	
//	short j=0; // if buffer needs to be incremented  NEW 100402010
//	
//	WriteDetailLog(fileName,1,"","Looping for packets\n ******************");
//	for(int i=0;i<noOfpacket;i++)
//	{
//		short compBytes=0;
//		noOfrec=0;
//		tmpPtr = pac->cPackData;
//
//		sprintf(tempBuffer,"Saved Broadcast Data To Local Variable = %s",tmpPtr);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		WriteDetailLog(fileName,1,"","<---- Variable Check Block ---->");
//		
//		sprintf(tempBuffer,"bufLen = %i\ntmpPtr = %i\nj = %i\ncompPac Address = %i",bufLen,tmpPtr,j,&compPac);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		//sprintf(tempBuffer,"compPac->iCompLen = %i\ncompBytes = %i",htons(compPac->iCompLen),compBytes);
//		//WriteDetailLog(fileName,1,"",tempBuffer);
//
//		//tmpPtr+= (i*2)+bufLen;
//		if (bufLen>0) //NEW 100402010
//			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//		compPac = (BcastCmpPacket *)tmpPtr;
//		compBytes = htons(compPac->iCompLen);
//		if (DEBUGTEST)
//		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
//		if (compBytes==0)
//		{
//			WriteDetailLog(fileName,1,"","SKIPPING Uncompresed packets");
//			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
//			continue; 
//		}
//		j++;//NEW 100402010
//		bufLen+=compBytes;
//		
//		sprintf(tempBuffer,"bufLen = %i\ntmpPtr = %i\nj = %i\ncompPac Address = %i",bufLen,tmpPtr,j,&compPac);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		sprintf(tempBuffer,"compPac->iCompLen = %i\ncompBytes = %i",htons(compPac->iCompLen),compBytes);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		WriteDetailLog(fileName,1,"","<---- Variable Check Block ---->");
//
//		WriteDetailLog(fileName,1,"","<---- LZO ---->");
//
//		WriteDetailLog(fileName,1,"","map tp msg header ie set the pointer to future value since lzo will decode it");
//		
//		sprintf(tempBuffer,"unCompbuffer = %d\nsizeCtr = %i",unCompbuffer,sizeCtr);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		//map tp msg header ie set the pointer to future value since lzo will decode it
//		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//		
//		sprintf(tempBuffer,"Message Header Pointer Address = %i",&msgHdr);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		// point broadcastOnlyMBP to data struct
//		// broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
//
//		// point broadcastOnlyMBP to data struct
//		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);
//
//		sprintf(tempBuffer,"7202 Structure Pointer Address = %i",&tickerData);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		WriteDetailLog(fileName,1,"","lzoing.. ");
//
//		if (DEBUGTEST)
//			printf("\t\t GetLTP:lzoing.. ");
//		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//		
//		sprintf(tempBuffer,"unCompbuffer = %d\nsizeCtr = %i\nerrcode1 = %d\nssize = %i",unCompbuffer,sizeCtr,errcode1,ssize);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		if (DEBUGTEST)
//			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
//		sizeCtr += ssize;
//		// print msge header
//
//		if (DEBUGTEST)
//			PrintMsgHeader(msgHdr);
//		// filer unwanted tr code
//
//		
//		short trCode = ntohs(msgHdr->TransactionCode);
//		
//		sprintf(tempBuffer,"msgHdr->TransactionCode = %d\ntrCode = %i",ntohs(msgHdr->TransactionCode),trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		WriteDetailLog(fileName,1,"","<---- LZO ---->");
//
//		if (trCode==filterTrCode)// we re looking for 7202
//		{
//			WriteDetailLog(fileName,1,"","Found Filtering TRCODE");
//			
//			ticker->msgHeader.AlphaChar[0] = msgHdr->AlphaChar[0];
//			ticker->msgHeader.AlphaChar[1] = msgHdr->AlphaChar[1];
//			ticker->msgHeader.ErrorCode = ntohs(msgHdr->ErrorCode);
//			ticker->msgHeader.iApiFuncId = msgHdr->iApiFuncId;
//			ticker->msgHeader.iApiFuncId2 = msgHdr->iApiFuncId2;
//			ticker->msgHeader.iApiFuncId3 = msgHdr->iApiFuncId3;
//			ticker->msgHeader.iApiTcode = msgHdr->iApiTcode;
//			ticker->msgHeader.LogTime = ntohl(msgHdr->LogTime);
//			ticker->msgHeader.MessageLength  = ntohs(msgHdr->MessageLength);
//			for (int iN=0;iN<8;iN++)
//			{
//				ticker->msgHeader.Timestamp[iN] = msgHdr->Timestamp[iN];
//				ticker->msgHeader.TimeStamp1[iN] = msgHdr->TimeStamp1[iN];
//				ticker->msgHeader.TimeStamp2[iN] = msgHdr->TimeStamp2[iN];
//			}
//			ticker->msgHeader.TransactionCode = ntohs(msgHdr->TransactionCode);
//
//			ticker->noOfRecords = ntohs(tickerData->noOfRecords);
//			//if (DEBUGTEST)
//			printf("\t\t GetTicker:FOUND trcode %i\n", trCode);
//			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
//			noOfrec = ntohs(tickerData->noOfRecords);
//
//			sprintf(tempBuffer,"tickerData->noOfRecords = %d\nnoOfrec = %i",ntohs(tickerData->noOfRecords),noOfrec);
//			WriteDetailLog(fileName,1,"",tempBuffer);
//
//			sprintf(tempBuffer," ^^^^^^^ Looping For %d Records ^^^^^^^ ",noOfrec);
//			WriteDetailLog(fileName,1,"",tempBuffer);
//			
//			for(int k=0;j < noOfrec;k++)
//			{
//				ticker->tickerInfo[intIndex].Token = ntohl(tickerData->tickerInfo[k].Token);
//				ticker->tickerInfo[intIndex].MarketType = ntohs(tickerData->tickerInfo[k].MarketType);
//				
//				ticker->tickerInfo[intIndex].FillPrice = ntohl(tickerData->tickerInfo[k].FillPrice);
//				ticker->tickerInfo[intIndex].FillVolume = ntohl(tickerData->tickerInfo[k].FillVolume);
//
//				ticker->tickerInfo[intIndex].OpenInterest = ntohl(tickerData->tickerInfo[k].OpenInterest);
//				ticker->tickerInfo[intIndex].DayHiOI = ntohl(tickerData->tickerInfo[k].DayHiOI);
//				ticker->tickerInfo[intIndex].DayLoOI = ntohl(tickerData->tickerInfo[k].DayLoOI);
//
//				//printf("\t \t ClosingPrice %i\n",mbp->CP[intIndex]);
//				intIndex=intIndex+1;
//
//			}
//
//			WriteDetailLog(fileName,1,""," ^^^^^^^ Looping For Records ^^^^^^^ ");
//		}
//		else
//		{
//			sprintf(tempBuffer,"Different TRCode %i == %i ",trCode,filterTrCode);
//			WriteDetailLog(fileName,1,"",tempBuffer);
//			//if (DEBUGTEST)
//			printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
//		}
//	
//	}  // end for
//	
//	WriteDetailLog(fileName,1,"","Looping for packets\n ******************");
//
//	if (DEBUGTEST)
//	{
//		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
//		printf("ProcessMBP:DONE\n");
//	}
//	return intIndex;
//	/*
//	if (DEBUGTEST)
//	printf("\n GetTicker:Start\n");
//
//	unsigned  short errcode1=0;
//	unsigned long ssize=0;
//
//	BcastPackData *pac;
//	BcastCmpPacket *compPac;
//	MessageHeader *msgHdr;
//	//CMMessageHeader *cmmsgHdr;
//	//BroadCastOnlyMBP * broadcastOnlyMBP;
//	BCAST_TICKER_AND_MKT_INDEX * tickerData;
//	//CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
//    pac = (BcastPackData *)&streamBuffer[0];
//	short noOfpacket = htons(pac->iNoPackets);
//	int intIndex=0;
//	int noOfrec=0;
//	
//	if (DEBUGTEST)
//		printf("\t GetTicker: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
//	
//	char *tmpPtr = pac->cPackData;
//	short bufLen=0; 
//	short sizeCtr=0;
//	long token;
//	
//	short j=0; // if buffer needs to be incremented  NEW 100402010
//	for(int i=0;i<noOfpacket;i++)
//	{
//		short compBytes=0;
//		noOfrec=0;
//		tmpPtr = pac->cPackData;
//		//tmpPtr+= (i*2)+bufLen;
//		if (bufLen>0) //NEW 100402010
//			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//		compPac = (BcastCmpPacket *)tmpPtr;
//		compBytes = htons(compPac->iCompLen);
//		if (DEBUGTEST)
//			printf("\t\ GetTicker: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
//		if (compBytes==0)
//		{
//			printf("\t\ GetTicker: SKIPPING Uncompresed packets\n");
//			continue; 
//		}
//		j++;//NEW 100402010
//		bufLen+=compBytes;
//		
//		//map tp msg header ie set the pointer to future value since lzo will decode it
//		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//		
//		// point broadcastOnlyMBP to data struct
//		tickerData = (BCAST_TICKER_AND_MKT_INDEX *)((char *)unCompbuffer+sizeCtr+8);
//		if (DEBUGTEST)
//			printf("\t\t GetTicker:lzoing.. ");
//		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//		
//		if (DEBUGTEST)
//			printf("\t\t GetTicker:ssize %i errcode %i\n",ssize,errcode1);
//		sizeCtr += ssize;
//		// print msge header
//
//		if (DEBUGTEST)
//			PrintMsgHeader(msgHdr);
//		// filer unwanted tr code
//		short trCode = ntohs(msgHdr->TransactionCode);
//		
//		if (trCode==filterTrCode)// we re looking for 7202
//		{
//			//if (DEBUGTEST)
//			printf("\t\t GetTicker:FOUND trcode %i\n", trCode);
//			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
//			noOfrec = ntohs(tickerData->noOfRecords);
//			for(int j=0;j < noOfrec;j++)
//			{
//				ticker->tickerInfo[intIndex].Token = ntohl(tickerData->tickerInfo[j].Token);
//				ticker->tickerInfo[intIndex].MarketType = ntohl(tickerData->tickerInfo[j].MarketType);
//				
//				ticker->tickerInfo[intIndex].FillPrice = ntohl(tickerData->tickerInfo[j].FillPrice);
//				ticker->tickerInfo[intIndex].FillVolume = ntohl(tickerData->tickerInfo[j].FillVolume);
//
//				ticker->tickerInfo[intIndex].OpenInterest = ntohl(tickerData->tickerInfo[j].OpenInterest);
//				ticker->tickerInfo[intIndex].DayHiOI = ntohl(tickerData->tickerInfo[j].DayHiOI);
//				ticker->tickerInfo[intIndex].DayLoOI = ntohl(tickerData->tickerInfo[j].DayLoOI);
//
//				//printf("\t \t ClosingPrice %i\n",mbp->CP[intIndex]);
//				intIndex=intIndex+1;
//
//			}
//		}
//		else
//		{
//			//if (DEBUGTEST)
//			printf("\t\t  GetTicker:- Different TRCode %i == %i \n", trCode,filterTrCode);
//		}
//	
//	}  // end for
//
//
//
//
//
//
//	if (DEBUGTEST)
//	{
//		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
//		printf("ProcessMBP:DONE\n");
//	}
//	return intIndex;*/
//} 



//****************************************************************************************************************
//Added By:SSP On:1/06/2011
//Adding three functions declarations here

//1]GetMBP:
//Desc: This Functions are used to retrive F&O Token and LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetMBP(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,MBP * mbp)
{

	

	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	

	

	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		

		if (DEBUGTEST)
			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
			
			

			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);

			

			for(int j=0;j<noOfrec;j++)
			{
				mbp->Token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);

				

				mbp->VTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				mbp->LTP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				
				

				mbp->NCI[intIndex]=broadcastOnlyMBP->onlyMPBData[j].NetChangeIndicator;
				mbp->NPCFCP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].NetPriceChangeFromClosingPrice);
				mbp->LTQ[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				mbp->LTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeTime);
//Commented 18/11/2013
				//mbp->AverageTradePrice[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].AverageTradePrice);

				//mbp->ShortAuctionNumber[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].AuctionNumber);
	
				mbp->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);

				for (int m=0;m<10;m++)
				{
					mbp->MBPI[intIndex].Quantity[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
					mbp->MBPI[intIndex].Price[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Price);
				}
				

				
			//	mbp->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);


				//printf("\t\t GetLTP:FOUND trcode %i\n", trCode);


				//mbp->TBQ[intIndex]=broadcastOnlyMBP->onlyMPBData[j].TotalBuyQuantity;//Addded NYN 31

				//printf("\t \t TBQ %i\n",mbp->TBQ[intIndex]);

				///*mbp->TSQ[intIndex]=broadcastOnlyMBP->onlyMPBData[j].TotalSellQuantity;
				//prin*/tf("\t \t TSQ %i\n",mbp->TSQ[intIndex]);

				//mbp->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);

				printf("\t \t ClosingPrice %i\n",mbp->CP[intIndex]);
				mbp->OP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].OpenPrice);
				mbp->HP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].HighPrice);
				mbp->LP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LowPrice);
				//mbp->LTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeTime);//Added NYN 16/07/2013 for Last Traded Time
				//mbp->LTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeTime);//Added NYN 16/07/2013 for Last Traded Time		
				//mbp->LTT[intIndex]=ntohl(0);//Added NYN 16/07/2013 for Last Traded Time
				



				//for (int m=0;m<10;m++)
				//{
				//	mbp->MBPI[intIndex].Quantity[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
				//	mbp->MBPI[intIndex].Price[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Price);
				//}

				/*long token = ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
		
				char charToken[15];
				char srToken[100]= "";
				_ltoa(token,charToken,10);
				sprintf(srToken,"%s.txt",charToken);

				FILE *fToken=fopen(srToken,"w");
				if(fToken==NULL)
					printf("Unable to open file");
				fprintf(fToken, "j=%i",j);
				fprintf(fToken, "i=%i",intIndex);
				fprintf(fToken, "Ltp2=%i",mbp->LTP[intIndex]);
				fprintf(fToken, "vtt2=%i",mbp->VTT[intIndex]);
				fclose(fToken);*/
				intIndex=intIndex+1;

				
				
				//mbp->Token[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				//mbp->VTT[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				//mbp->LTP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				//mbp->NCI[j]=broadcastOnlyMBP->onlyMPBData[j].NetChangeIndicator;
				//mbp->NPCFCP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].NetPriceChangeFromClosingPrice);
				//mbp->LTQ[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				//mbp->TBQ[j]=broadcastOnlyMBP->onlyMPBData[j].TotalBuyQuantity;
				//mbp->TSQ[j]=broadcastOnlyMBP->onlyMPBData[j].TotalSellQuantity;
				//mbp->CP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);
				//mbp->OP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].OpenPrice);
				//mbp->HP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].HighPrice);
				//mbp->LP[j]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LowPrice);
				//for (int m=0;m<10;m++)
				//{
				//	mbp->MBPI[intIndex].Quantity[m] = ntohl(broadcastOnlyMBP->onlyMPBData[i].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
				//	mbp->MBPI[intIndex].Price[m] = ntohl(broadcastOnlyMBP->onlyMPBData[i].MBPinfo[m].Price);
				//}

			}
		}
		else
		{
			//if (DEBUGTEST)

			printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
	
	}  // end for



	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}

	return intIndex;
}

//7]GetBuySellEQ:
//Desc: This Functions are used to retrive EQ Token and LTP, BBP & BSP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetBuySellEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode, long filterTkn, short saveFlg, EQBuySellRate * eqrt)
{
	if (DEBUGTEST)
	printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				eqrt->token[intIndex]=ntohs(broadcastOnlyMBP->onlyMPBData[j].Token);
				eqrt->ltp[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);

				eqrt->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);//Added Nyn 29/10/2013
				eqrt->buyrate[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[0].Price);
				eqrt->sellrate[intIndex]= ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[5].Price);
				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}

//8]GetIvAnalysis:
//Desc: This Functions are used to retrive F&O Token and LTP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetIvAnalysis(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg, LiveIvAnalysis * mbp)
{
	if (DEBUGTEST)
	printf("\n GetLTP:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTP: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;
	
	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		// point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTP:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTP:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		// print msge header

		if (DEBUGTEST)
			PrintMsgHeader(msgHdr);
		// filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTP:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				mbp->Token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				mbp->VTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				mbp->LTP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				mbp->LTQ[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				mbp->BuyP[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[0].Price); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
				mbp->SellP[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[5].Price);


				intIndex=intIndex+1;

			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTP:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
	
	}  // end for






	if (DEBUGTEST)
	{
		printf("\t\tProcessMBP:final uncompress size%i \n",ssize);
		printf("ProcessMBP:DONE\n");
	}
	return intIndex;
}

//Desc: This Functions are used to retrive EQ Token and LTP,Volume and market index
//Its return value is consider as a number of tokens it had
SYSREADERN_API  int _stdcall GetTickerAndMarketNewCM(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnlyCM * bticker)
{

	char tempBuffer[99999];
	char fileName[1024] = "TickerDataLog_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");

	if(saveFlg == 1)
	{
		WriteDetailLog(fileName,0,"","Starting Get Ticker");
	}

	if (DEBUGTEST)
		printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	BCAST_TICKER_AND_MKT_INDEX_CM * broadcastOnlyMBP;
	BCAST_TICKER_AND_MKT_INDEX_CM * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010

	if(noOfpacket <= 0)
	{
		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"No of packets = %d",noOfpacket);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}
	}

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);

		if (DEBUGTEST)
			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BCAST_TICKER_AND_MKT_INDEX_CM *)((char *)unCompbuffer+sizeCtr+8);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7202
		{
			if (DEBUGTEST)
				printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				bticker->Token[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].Token);
				bticker->MarketType[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].MarketType);
				bticker->FillPrice[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillPrice);
				bticker->FillVolume[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillVolume);
				bticker->MarketIndexValue[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].MarketIndexValue);

			    intIndex=intIndex+1;
			}
		}
		else
		{
			if (DEBUGTEST)
				printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}

//Memory corruption error...commented on 06/10/2016
/*SYSREADERN_API  int _stdcall GetTrCodesLzo(char * streamBuffer,char * unCompbuffer)
{
	int saveFlg = 1;
	char tempBuffer[99999];
	char fileName[1024] = "TrCodesLogCPP_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");

	if(saveFlg == 1)
	{
		WriteDetailLog(fileName,0,"","Starting Get TrCode Reader");
	}

	if (DEBUGTEST)
		printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	MessageHeader *msgHdr;
	//CMMessageHeader *cmmsgHdr;
	BCAST_INDICES * broadcastOnlyMBP;
	BCAST_INDICES * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010

	if(noOfpacket <= 0)
	{
		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			sprintf(tempBuffer,"No of packets = %d",noOfpacket);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}
	}

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);

		if (DEBUGTEST)
			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (BCAST_INDICES *)((char *)unCompbuffer+sizeCtr+8);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		
		sizeCtr += ssize;
		//print msge header
		
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
		sprintf(tempBuffer,"\t\t Found TRCode %i \n", trCode);
		WriteDetailLog(fileName,1,"",tempBuffer);
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return 1;
}*/

SYSREADERN_API  int _stdcall GetTrCodesLzo(char * streamBuffer,char * unCompbuffer)
{
	char tempBuffer[99999];
	char fileName[1024] = "SpotPrice7207_";
	AppendCurrentDateTime(fileName,0);
	strcat(fileName,".log");
	int saveFlg = 1;

	if(saveFlg == 1)
	{
		WriteDetailLog(fileName,0,"","Starting Get Ticker");
	}

	if (DEBUGTEST)
		printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	//MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	//BCAST_INDICES * broadcastOnlyMBP;
	CMBcast_Indices * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010

	if(noOfpacket > 0)
	{
		if(saveFlg == 1)
		{
			memset( tempBuffer, '\0', sizeof(tempBuffer));
			sprintf(tempBuffer,"No of packets = %d\n",noOfpacket);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}
	}

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);

		if (DEBUGTEST)
			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes>0)
		{
			printf("\t\ GetLTP: SKIPPING compresed packets\n");
			continue; 
		}

		j++;//NEW 100402010
		//bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		//msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		cmmsgHdr = (CMMessageHeader *)((unsigned char *)tmpPtr+sizeCtr+10); // added 2 more bytes for mapping un comp buffer
		

		//point broadcastOnlyMBP to data struct
		//broadcastOnlyMBP = (BCAST_INDICES *)((char *)unCompbuffer+sizeCtr+8);
		cmbroadcastOnlyMBP = (CMBcast_Indices *)((char *)tmpPtr+sizeCtr+10); // added 2 more bytes for mapping un comp buffer
		
		bufLen += sizeof(cmbroadcastOnlyMBP); // Add current size for next mapping added by hvb

		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		
		//LZO Skipped by hvb as the data decompressed by default
		//int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		
		//Note required commented by hvb this was used by lzo
		//sizeCtr += ssize;
		//print msge header

		/*if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);*/
		
		//filer unwanted tr code
		short trCode = ntohs(cmmsgHdr->TransactionCode);
		//short trCode = ntohs(msgHdr->TransactionCode);
		
		/*if(saveFlg == 1)
		{
			//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
			memset( tempBuffer, '\0', sizeof(tempBuffer));
			sprintf(tempBuffer,"Got TrCode = %i",trCode);
			WriteDetailLog(fileName,1,"",tempBuffer);
		}*/
		
		if(trCode == 7207)
		{
			if(saveFlg == 1)
			{
				//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
				memset( tempBuffer, '\0', sizeof(tempBuffer));
				sprintf(tempBuffer,"Got TrCode = %i",trCode);
				WriteDetailLog(fileName,1,"",tempBuffer);
			}
			
			noOfrec = ntohs(cmbroadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				/*bticker->Token[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].Token);
				bticker->MarketType[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].MarketType);
				bticker->FillPrice[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillPrice);
				bticker->FillVolume[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillVolume);
				bticker->MarketIndexValue[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].MarketIndexValue);*/

				if(saveFlg == 1)
				{
					//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
					memset( tempBuffer, '\0', sizeof(tempBuffer));
					sprintf(tempBuffer,"%s = %ld", cmbroadcastOnlyMBP->Indices_Data[j].IndexName,ntohl(cmbroadcastOnlyMBP->Indices_Data[j].IndexValue));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

			    intIndex=intIndex+1;
			}

		}

		/*
		if(trCode == 7202)
		{
			noOfrec = ntohs(cmbroadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{

				if(saveFlg == 2)
				{
					//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
					memset( tempBuffer, '\0', sizeof(tempBuffer));
					sprintf(tempBuffer,"TOKEN = %i\n",ntohs(cmbroadcastOnlyMBP->tickerInfo[j].Token));
					WriteDetailLog(fileName,1,"",tempBuffer);

					memset( tempBuffer, '\0', sizeof(tempBuffer));
					sprintf(tempBuffer,"INDEX Val = %ld\n",ntohl(cmbroadcastOnlyMBP->tickerInfo[j].MarketIndexValue));
					WriteDetailLog(fileName,1,"",tempBuffer);
				}

			    intIndex=intIndex+1;
			}
		}*/
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Returns number of records dumped if there are 7207 records
   *********************************** */
SYSREADERN_API  int _stdcall DumpSpotPrice(char * FilePath,char * streamBuffer,char * unCompbuffer)
{
	char tempBuffer[99999];
	char fileName[1024] = "SpotData.txt";
	

	const int fileExtensionSize = 4;//.txt = 4

	if(FilePath != "")
	{
		if(strlen(FilePath) > fileExtensionSize)
		{
			if(strcmp(FilePath + strlen(FilePath) - fileExtensionSize,".txt") == 0)        
			{
				//File path has file name so change file name to file path
				memset(fileName,'\0',sizeof(fileName));
				strcpy(fileName,FilePath);
				//fileName = FilePath;
			}
			else
			{
				//set file name to file path + SpotData.txt
				memset(fileName,'\0',sizeof(fileName));
				strcpy(fileName,FilePath);
				if(strcmp(FilePath + strlen(FilePath) - 1,"\\") != 0) // path don't have ending slash
					strcat(fileName,"\\");
				strcat(fileName,"SpotData.txt");
			}
		}
		//Not proper path so not updating file name
		/*else
		{
			//Not a proper path don't change file name
		}*/
	}
	//else // FilePath not specified so don't bother changing filename

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	//MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	//BCAST_INDICES * broadcastOnlyMBP;
	CMBcast_Indices * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 

	short j=0; // if buffer needs to be incremented  NEW 100402010

	int fileDeletedForPacket = 0;

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);

		if (DEBUGTEST)
			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes>0)
		{
			printf("\t\ GetLTP: SKIPPING compresed packets\n");
			//Added by hvb on 15/10/2016
			//if current packet is compressed we need to move to next packet
			//so add the size of current packet to bufferlen and next loop will read packet from that len
			bufLen+=compBytes;
			continue; 
		}

		j++;//NEW 100402010
		
		//map to msg header ie set the pointer to future value since lzo will decode it
		//msgHdr = (MessageHeader *)((unsigned char *)tmpPtr+sizeCtr+10);
		cmmsgHdr = (CMMessageHeader *)((unsigned char *)tmpPtr+10); // added 2 more bytes for mapping un comp buffer

		//point broadcastOnlyMBP to data struct
		//broadcastOnlyMBP = (BCAST_INDICES *)((char *)tmpPtr+sizeCtr+10);
		cmbroadcastOnlyMBP = (CMBcast_Indices *)((char *)tmpPtr+10); // added 2 more bytes for mapping un comp buffer
		
		bufLen += sizeof(cmbroadcastOnlyMBP); // Add current size for next mapping added by hvb

		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		
		//LZO Skipped by hvb as the data un compressed by default
		//int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		
		//print msge header
		/*if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);*/
		
		//filer unwanted tr code
		short trCode = ntohs(cmmsgHdr->TransactionCode);
		//short trCode = ntohs(msgHdr->TransactionCode);
		
		if(trCode == 7207) //We are looking for 7207
		{
			if(fileDeletedForPacket == 0)
			{
				//Modified by hvb on 17/10/2016 for testing at client side
				//Remove once testing done
				DeleteFileSafely(fileName);
				//uncomment above lines^^^ and comment below line
				//WriteDetailLog(fileName,0,"","Starting spot price reading");
				fileDeletedForPacket = 1;
			}
			
			noOfrec = ntohs(cmbroadcastOnlyMBP->noOfRecords);
			
			//Modified loop counter by hvb on 15/10/2016
			//as j was already used
			for(int k=0;k<noOfrec;k++)
			{
				//21 is character length set for index name by nse
				memset( tempBuffer, '\0', sizeof(tempBuffer));
				char indexName[32] = "";
				strcpy(indexName,cmbroadcastOnlyMBP->Indices_Data[k].IndexName);
				UCaseWsTrim(&indexName[0]);
				sprintf(tempBuffer,"%s=%ld",  indexName,ntohl(cmbroadcastOnlyMBP->Indices_Data[k].IndexValue));
				WriteDetailLog(fileName,1,"",tempBuffer);
				
			    intIndex=intIndex+1;
			}

		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}

SYSREADERN_API  int _stdcall TextAppendTest(LPSTR psrcStr)
{
	//char srcStr[99999] = "";
	//memset(psrcStr,'\0',99999);
	char srcStr[99999] = "";
	strcat(srcStr,"Test1,");
	AppendCurrentDateTime(srcStr,1);
	strcat(srcStr,",Test2,");
	strcat(srcStr,"Test3,");
	strcat(srcStr,"Test4,");
	strcat(srcStr,"Test5,");
	strcat(srcStr,"Test6,");
	AppendCurrentDateTime(srcStr,1);
	strcat(srcStr,",Done");
	strcpy(psrcStr,srcStr);
	//memset(psrcStr,'\0',strlen(psrcStr));
	//WriteDetailLog("D:\\Packet Test\\Debug.txt",0,"og",psrcStr);
	//WriteDetailLog("D:\\Packet Test\\Debug.txt",0,"method",srcStr);
	//strcpy(psrcStr,srcStr);
	//WriteDetailLog("D:\\Packet Test\\Debug.txt",0,"og",psrcStr);
	//WriteDetailLog("D:\\Packet Test\\Debug.txt",0,"method",srcStr);
	return strlen(psrcStr);
}

/* ********************************
		Added By   : HVB 
		Date       : 17/10/2016
		Desc.      : Decodes packet for 7207 and returns length of string after modification
		Parameters : (1) LPSTR outString    : String which will have output
		             (2) char* streamBuffer : Source buffer
					 (3) char* unCompbuffer : Source unCompbuffer
   *********************************** */
SYSREADERN_API  int _stdcall ReadSpotPrice(LPSTR outString,char * streamBuffer,char * unCompbuffer)
{
	char tempBuffer[99999];

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	//MessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	//BCAST_INDICES * broadcastOnlyMBP;
	CMBcast_Indices * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 

	short j=0; // if buffer needs to be incremented  NEW 100402010

	char strCombinedOutput[99999] = "";

	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);

		if (DEBUGTEST)
			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);

		if (compBytes>0)
		{
			printf("\t\ GetLTP: SKIPPING compresed packets\n");
			//Added by hvb on 15/10/2016
			//if current packet is compressed we need to move to next packet
			//so add the size of current packet to bufferlen and next loop will read packet from that len
			bufLen+=compBytes;
			continue; 
		}

		j++;//NEW 100402010
		
		//map to msg header ie set the pointer to future value since lzo will decode it
		//msgHdr = (MessageHeader *)((unsigned char *)tmpPtr+sizeCtr+10);
		cmmsgHdr = (CMMessageHeader *)((unsigned char *)tmpPtr+10); // added 2 more bytes for mapping un comp buffer

		//point broadcastOnlyMBP to data struct
		//broadcastOnlyMBP = (BCAST_INDICES *)((char *)tmpPtr+sizeCtr+10);
		cmbroadcastOnlyMBP = (CMBcast_Indices *)((char *)tmpPtr+10); // added 2 more bytes for mapping un comp buffer
		
		bufLen += sizeof(cmbroadcastOnlyMBP); // Add current size for next mapping added by hvb

		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		
		//LZO Skipped by hvb as the data un compressed by default
		//int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		
		//print msge header
		/*if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);*/
		
		//filer unwanted tr code
		short trCode = ntohs(cmmsgHdr->TransactionCode);
		//short trCode = ntohs(msgHdr->TransactionCode);
		
		if(trCode == 7207) //We are looking for 7207
		{
			noOfrec = ntohs(cmbroadcastOnlyMBP->noOfRecords);
			
			//Modified loop counter by hvb on 15/10/2016
			//as j was already used
			for(int k=0;k<noOfrec;k++)
			{
				//21 is character length set for index name by nse
				memset( tempBuffer, '\0', sizeof(tempBuffer));
				char indexName[32] = "";
				strcpy(indexName,cmbroadcastOnlyMBP->Indices_Data[k].IndexName);
				UCaseWsTrim(&indexName[0]);
				sprintf(tempBuffer,"%s=%ld,",  indexName,ntohl(cmbroadcastOnlyMBP->Indices_Data[k].IndexValue));
				strcat(strCombinedOutput,tempBuffer);

				//sprintf(tempBuffer,"%s=%ld",  indexName,ntohl(cmbroadcastOnlyMBP->Indices_Data[k].IndexValue));
				//WriteDetailLog(fileName,1,"",tempBuffer);
				
			    intIndex=intIndex+1;
			}

		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}

	strcpy(outString,strCombinedOutput);

	return intIndex;
}

/*SYSREADERN_API  int _stdcall CheckCreation(char * FilePath)
{
	char tempBuffer[99999];
	char fileName[1024] = "SpotData.txt";
	
	const int fileExtensionSize = 4;//.txt = 4

	if(FilePath != "")
	{
		if(strlen(FilePath) > fileExtensionSize)
		{
			if(strcmp(FilePath + strlen(FilePath) - fileExtensionSize,".txt") == 0)        
			{
				//File path has file name so change file name to file path
				memset(fileName,'\0',sizeof(fileName));
				strcpy(fileName,FilePath);
				//fileName = FilePath;
			}
			else
			{
				//set file name to file path + SpotData.txt
				memset(fileName,'\0',sizeof(fileName));
				strcpy(fileName,FilePath);
				if(strcmp(FilePath + strlen(FilePath) - 1,"\\") != 0) // path don't have ending slash
					strcat(fileName,"\\");
				strcat(fileName,"SpotData.txt");
			}
		}
		//else //Not proper path so not updating file name
	}
	
	WriteDetailLog(fileName,1,"","File has been created");
	
	char * filePathIgot = "D:\\Hitesh\\spotfile\\debug.txt";
	WriteDetailLog(filePathIgot,0,"File Name",fileName);

	WriteDetailLog(filePathIgot,0,"File Path",FilePath);
	return 1;
}*/

//For F&O...Issues commented
//SYSREADERN_API  int _stdcall GetTrCodesWithoutLzo(char * streamBuffer,char * unCompbuffer)
//{
//	char tempBuffer[99999];
//	char fileName[1024] = "TrCodeCapturesWComp_";
//	AppendCurrentDateTime(fileName,0);
//	strcat(fileName,".log");
//
//	WriteDetailLog(fileName,0,"","Starting Get Ticker");
//	
//	unsigned  short errcode1=0;
//	unsigned long ssize=0;
//
//	BcastPackData *pac;
//	BcastCmpPacket *compPac;
//	MessageHeader *msgHdr;
//
//	//BCAST_TICKER_AND_MKT_INDEX_CM * broadcastOnlyMBP;
//	//BCAST_TICKER_AND_MKT_INDEX_CM * cmbroadcastOnlyMBP;
//
//	pac = (BcastPackData *)&streamBuffer[0];
//	
//	short noOfpacket = htons(pac->iNoPackets);
//	int intIndex=0;
//	int noOfrec=0;
//	
//	
//	char *tmpPtr = pac->cPackData;
//	short bufLen=0; 
//	short sizeCtr=0;
//	long token;
//
//	short j=0; // if buffer needs to be incremented  NEW 100402010
//	
//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//	sprintf(tempBuffer,"No of packets = %d",noOfpacket);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//
//	for(int i=0;i<noOfpacket;i++)
//	{
//		short compBytes=0;
//		noOfrec=0;
//		//tmpPtr = pac->cPackData;
//		//tmpPtr+= (i*2)+bufLen;
//		tmpPtr = pac->cPackData;
//		if (bufLen>0) //NEW 100402010
//			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//
//		compPac = (BcastCmpPacket *)tmpPtr;
//		compBytes = htons(compPac->iCompLen);
//
//		if (DEBUGTEST)
//			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
//
//		if (compBytes==0)
//		{
//			WriteDetailLog(fileName,1,"","Uncompressed  packet found going for 7207/03");	
//			//Set buf lenght to size of packet
//			//ie., chars
//			compBytes = strlen(pac->cPackData);
//		}
//		else
//		{
//			WriteDetailLog(fileName,1,"","Compressed packet found skipping");
//			continue; 
//		}
//
//		j++;//NEW 100402010
//		bufLen+=compBytes;
//		
//		//map tp msg header ie set the pointer to future value since lzo will decode it
//		//msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//		//Commented above style of decode for testing by hvb on 12/07/2016
//		msgHdr = (MessageHeader *)(tmpPtr+sizeCtr+8);
//		
//		short trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(tmpPtr+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [((unsigned char *)unCompbuffer+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr);
//
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(unsigned char *)unCompbuffer+sizeCtr] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		msgHdr = (MessageHeader *)(tmpPtr+sizeCtr);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		
//		msgHdr = &(((BCAST_INDICES *)(tmpPtr+sizeCtr+8))->msgHeader);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [Direct_Convert(tmpPtr+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		msgHdr = &(((BCAST_INDICES *)(tmpPtr+sizeCtr))->msgHeader);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [Direct_Convert(tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		//point broadcastOnlyMBP to data struct
//		//broadcastOnlyMBP = (BCAST_TICKER_AND_MKT_INDEX_CM *)((char *)unCompbuffer+sizeCtr+8);
//		
//		//if (DEBUGTEST)
//		//	printf("\t\t GetLTPEQ:lzoing.. ");
//		
//		//int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//		
//		//if (DEBUGTEST)
//		//	printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
//		
//		//sizeCtr += bufLen;
//
//		//if (DEBUGTEST)
//		//	PrintMsgHeader_CM(msgHdr);
//		
//		//filer unwanted tr code
//		//short trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer,"Found trCode without compression = %d",noOfpacket);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//	}  // end for
//
//	if (DEBUGTEST)
//	{
//		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
//		printf("ProcessMBPEQ:DONE\n");
//	}
//	return 1;
//}

//SYSREADERN_API  int _stdcall GetTrCodesWithoutLzo(char * streamBuffer,char * unCompbuffer)
//{
//	char tempBuffer[99999];
//	char fileName[1024] = "CMTrCodeCapturesWComp_";
//	AppendCurrentDateTime(fileName,0);
//	strcat(fileName,".log");
//
//	WriteDetailLog(fileName,0,"","Starting Get TrCodeCM_Spot");
//	
//	unsigned  short errcode1=0;
//	unsigned long ssize=0;
//
//	BcastPackData *pac;
//	BcastCmpPacket *compPac;
//	CMMessageHeader *msgHdr;
//
//	//BCAST_TICKER_AND_MKT_INDEX_CM * broadcastOnlyMBP;
//	//BCAST_TICKER_AND_MKT_INDEX_CM * cmbroadcastOnlyMBP;
//
//	pac = (BcastPackData *)&streamBuffer[0];
//	
//	short noOfpacket = htons(pac->iNoPackets);
//	int intIndex=0;
//	int noOfrec=0;
//	
//	
//	char *tmpPtr = pac->cPackData;
//	short bufLen=0; 
//	short sizeCtr=0;
//	long token;
//
//	short j=0; // if buffer needs to be incremented  NEW 100402010
//	
//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//	sprintf(tempBuffer,"No of packets = %d",noOfpacket);
//	WriteDetailLog(fileName,1,"",tempBuffer);
//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//
//	for(int i=0;i<noOfpacket;i++)
//	{
//		short compBytes=0;
//		noOfrec=0;
//		//tmpPtr = pac->cPackData;
//		//tmpPtr+= (i*2)+bufLen;
//		tmpPtr = pac->cPackData;
//		if (bufLen>0) //NEW 100402010
//			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//
//		compPac = (BcastCmpPacket *)tmpPtr;
//		compBytes = htons(compPac->iCompLen);
//
//		if (DEBUGTEST)
//			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
//
//		if (compBytes==0)
//		{
//			WriteDetailLog(fileName,1,"","Uncompressed  packet found going for 7207/03");	
//			//Set buf lenght to size of packet
//			//ie., chars
//			compBytes = strlen(pac->cPackData);
//		}
//		else
//		{
//			WriteDetailLog(fileName,1,"","Compressed packet found skipping");
//			continue; 
//		}
//
//		j++;//NEW 100402010
//		bufLen+=compBytes;
//		
//		//map tp msg header ie set the pointer to future value since lzo will decode it
//		//msgHdr = (MessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//		//Commented above style of decode for testing by hvb on 12/07/2016
//		msgHdr = (CMMessageHeader *)((unsigned char *)tmpPtr+sizeCtr+8);
//		
//		short trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [((unsigned char *)tmpPtr+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		msgHdr = (CMMessageHeader *)((unsigned char *)tmpPtr+sizeCtr);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [((unsigned char *)tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		msgHdr = (CMMessageHeader *)((char *)tmpPtr+sizeCtr);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [((char *)tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		msgHdr = (CMMessageHeader *)(tmpPtr);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(char *)tmpPtr] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		/*msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [((unsigned char *)unCompbuffer+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr);
//
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(unsigned char *)unCompbuffer+sizeCtr] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		msgHdr = (CMMessageHeader *)(tmpPtr+sizeCtr);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [(tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//
//		
//		msgHdr = &(((CMBcast_Indices *)(tmpPtr+sizeCtr+8))->msgHeader);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [Direct_Convert(tmpPtr+sizeCtr+8)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);
//		
//		msgHdr = &(((CMBcast_Indices *)(tmpPtr+sizeCtr))->msgHeader);
//		
//		trCode = ntohs(msgHdr->TransactionCode);
//
//		memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer," [Direct_Convert(tmpPtr+sizeCtr)] Found trCode without compression = %d",trCode);
//		WriteDetailLog(fileName,1,"",tempBuffer);*/
//
//		//point broadcastOnlyMBP to data struct
//		//broadcastOnlyMBP = (BCAST_TICKER_AND_MKT_INDEX_CM *)((char *)unCompbuffer+sizeCtr+8);
//		
//		//if (DEBUGTEST)
//		//	printf("\t\t GetLTPEQ:lzoing.. ");
//		
//		//int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//		
//		//if (DEBUGTEST)
//		//	printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
//		
//		//sizeCtr += bufLen;
//
//		//if (DEBUGTEST)
//		//	PrintMsgHeader_CM(msgHdr);
//		
//		//filer unwanted tr code
//		//short trCode = ntohs(msgHdr->TransactionCode);
//
//		/*memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//		sprintf(tempBuffer,"Found trCode without compression = %d",noOfpacket);
//		WriteDetailLog(fileName,1,"",tempBuffer);*/
//		
//	}  // end for
//
//	if (DEBUGTEST)
//	{
//		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
//		printf("ProcessMBPEQ:DONE\n");
//	}
//	return 1;
//}

//SYSREADERN_API  int _stdcall GetUncompIndexData(char * streamBuffer,char * unCompbuffer)
//{
//	char tempBuffer[99999];
//	char fileName[1024] = "SpotDebugWComp_";
//	AppendCurrentDateTime(fileName,0);
//	strcat(fileName,".log");
//	int saveFlg = 1;
//	//WriteDetailLog(fileName,0,"","Starting Get Spot UnCompressed");
//	//
//	//unsigned  short errcode1=0;
//	//unsigned long ssize=0;
//
//	//BcastPackData *pac;
//	//CMBcast_Indices *UncompPac;
//	//CMMessageHeader *msgHdr;
//
//	////BCAST_TICKER_AND_MKT_INDEX_CM * broadcastOnlyMBP;
//	////BCAST_TICKER_AND_MKT_INDEX_CM * cmbroadcastOnlyMBP;
//
//	//pac = (BcastPackData *)&streamBuffer[0];
//	//
//	//short noOfpacket = htons(pac->iNoPackets);
//	//int intIndex=0;
//	//int noOfrec=0;
//	//
//	//
//	//char *tmpPtr = pac->cPackData;
//	//short bufLen=0; 
//	//short sizeCtr=0;
//	//long token;
//
//	//short j=0; // if buffer needs to be incremented  NEW 100402010
//	//
//	//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//	//sprintf(tempBuffer,"No of packets = %d",noOfpacket);
//	//WriteDetailLog(fileName,1,"",tempBuffer);
//	//memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//
//	//for(int i=0;i<noOfpacket;i++)
//	//{
//	//	//short trCode=0;
//	//	noOfrec=0;
//	//	//tmpPtr = pac->cPackData;
//	//	//tmpPtr+= (i*2)+bufLen;
//	//	tmpPtr = pac->cPackData;
//	//	if (bufLen>0) //NEW 100402010
//	//		tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//
//	//	UncompPac = (CMBcast_Indices *)tmpPtr;
//	//	//trCode = htons(compPac->iCompLen);
//
//	//	/*if (DEBUGTEST)
//	//		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);*/
//
//	//	/*if (compBytes==0)
//	//	{
//	//		WriteDetailLog(fileName,1,"","Uncompressed  packet found going for 7207/03");	
//	//		//Set buf lenght to size of packet
//	//		//ie., chars
//	//		compBytes = strlen(pac->cPackData);
//	//	}
//	//	else
//	//	{
//	//		WriteDetailLog(fileName,1,"","Compressed packet found skipping");
//	//		continue; 
//	//	}*/
//
//	//	j++;//NEW 100402010
//	//	bufLen+=strlen(tmpPtr);
//	//	
//	//	//map tp msg header ie set the pointer to future value since lzo will decode it
//	//	msgHdr = &(UncompPac->msgHeader;
//	//	
//	//	//point broadcastOnlyMBP to data struct
//	//	//broadcastOnlyMBP = (BCAST_TICKER_AND_MKT_INDEX_CM *)((char *)unCompbuffer+sizeCtr+8);
//	//	
//	//	/*if (DEBUGTEST)
//	//		printf("\t\t GetLTPEQ:lzoing.. ");
//	//	
//	//	int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//	//	
//	//	if (DEBUGTEST)
//	//		printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);*/
//	//	
//	//	//sizeCtr += bufLen;
//
//	//	/*if (DEBUGTEST)
//	//		PrintMsgHeader_CM(msgHdr);*/
//	//	
//	//	//filer unwanted tr code
//	//	short trCode = ntohs(msgHdr->TransactionCode);
//
//	//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//	//	sprintf(tempBuffer,"Found trCode without compression = %d",trCode);
//	//	WriteDetailLog(fileName,1,"",tempBuffer);
//	//	
//	//}  // end for
//
//	//if (DEBUGTEST)
//	//{
//	//	printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
//	//	printf("ProcessMBPEQ:DONE\n");
//	//}
//
//	if(saveFlg == 1)
//	{
//		WriteDetailLog(fileName,0,"","Starting Get Spot Compressed");
//	}
//
//	if (DEBUGTEST)
//		printf("\n GetLTPEQ:Start\n");
//
//	unsigned  short errcode1=0;
//	unsigned long ssize=0;
//
//	BcastPackData *pac;
//	BcastCmpPacket *compPac;
//	CMMessageHeader *msgHdr;
//	//CMMessageHeader *cmmsgHdr;
//	CMBcast_Indices * broadcastOnlyMBP;
//	//CMMs_Indices * cmbroadcastOnlyMBP;
//    pac = (BcastPackData *)&streamBuffer[0];
//	short noOfpacket = htons(pac->iNoPackets);
//	int intIndex=0;
//	int noOfrec=0;
//	
//	if (DEBUGTEST)
//		printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
//	
//	char *tmpPtr = pac->cPackData;
//	short bufLen=0; 
//	short sizeCtr=0;
//	long token;
//
//	short j=0; // if buffer needs to be incremented  NEW 100402010
//
//	if(noOfpacket > 0)
//	{
//		if(saveFlg == 1)
//		{
//			memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//			sprintf(tempBuffer,"No of packets = %d",noOfpacket);
//			WriteDetailLog(fileName,1,"",tempBuffer);
//		}
//	}
//
//	for(int i=0;i<noOfpacket;i++)
//	{
//		short compBytes=0;
//		noOfrec=0;
//		//tmpPtr = pac->cPackData;
//		//tmpPtr+= (i*2)+bufLen;
//		tmpPtr = pac->cPackData;
//		if (bufLen>0) //NEW 100402010
//			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
//		compPac = (BcastCmpPacket *)tmpPtr;
//		compBytes = htons(compPac->iCompLen);
//
//		if (DEBUGTEST)
//			printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
//
//		if (compBytes==0)
//		{
//			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
//			continue; 
//		}
//		j++;//NEW 100402010
//		bufLen+=compBytes;
//		
//		//map tp msg header ie set the pointer to future value since lzo will decode it
//		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
//		
//		//point broadcastOnlyMBP to data struct
//		broadcastOnlyMBP = (CMBcast_Indices *)((char *)unCompbuffer+sizeCtr+8);
//		
//		if (DEBUGTEST)
//			printf("\t\t GetLTPEQ:lzoing.. ");
//		
//		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
//		
//		if (DEBUGTEST)
//			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
//		
//		sizeCtr += ssize;
//		//print msge header
//
//		if (DEBUGTEST)
//			PrintMsgHeader_CM(msgHdr);
//		
//		//filer unwanted tr code
//		short trCode = ntohs(msgHdr->TransactionCode);
//		
//		if (trCode==7207)// we re looking for 7202
//		{
//			if (DEBUGTEST)
//				printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
//
//			//if (saveFlg == 1)
//			//{
//				//printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
//				WriteDetailLog(fileName,1,"","Found trCode 7207");
//			//}
//
//			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
//			//noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
//			//for(int j=0;j<noOfrec;j++)
//			//{
//			//	/*bticker->Token[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].Token);
//			//	bticker->MarketType[intIndex]=ntohs(broadcastOnlyMBP->tickerInfo[j].MarketType);
//			//	bticker->FillPrice[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillPrice);
//			//	bticker->FillVolume[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].FillVolume);
//			//	bticker->MarketIndexValue[intIndex] = ntohl(broadcastOnlyMBP->tickerInfo[j].MarketIndexValue);
//
//			//    intIndex=intIndex+1;*/
//			//	
//			//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//			//	sprintf(tempBuffer,"\t\t  Got Script :- %s \n", broadcastOnlyMBP->IndiceData[j].IndexName);
//			//	WriteDetailLog(fileName,1,"",tempBuffer);
//			//	
//			//	memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//			//	sprintf(tempBuffer,"\t\t  Got Index :- %ld \n", ntohl(broadcastOnlyMBP->IndiceData[j].IndexValue));
//			//	WriteDetailLog(fileName,1,"",tempBuffer);
//
//			//}
//		}
//		else
//		{
//			if (saveFlg == 1)
//			{
//				//printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
//				memset( tempBuffer, '\0', sizeof(char)*strlen(tempBuffer));
//				sprintf(tempBuffer,"\t\t  GetUnCompIndex:- Different TRCode %d == %d", trCode,7207);
//				WriteDetailLog(fileName,1,"",tempBuffer);
//			}
//		}
//		
//	}  // end for
//
//	if (DEBUGTEST)
//	{
//		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
//		printf("ProcessMBPEQ:DONE\n");
//	}
//	//return intIndex;
//
//	return 1;
//}

//9]GetMBPEQ:
//Desc: This Functions are used to retrive EQ Token and LTP, BBP & BSP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetMBPEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,MBP * eqrt)
{
	if (DEBUGTEST)
	printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);

			
			
			

			for(int j=0;j<noOfrec;j++)
			{
				eqrt->Token[intIndex]=ntohs(broadcastOnlyMBP->onlyMPBData[j].Token);
				eqrt->VTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				eqrt->LTP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				eqrt->NCI[intIndex]=broadcastOnlyMBP->onlyMPBData[j].NetChangeIndicator;
				eqrt->NPCFCP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].NetPriceChangeFromClosingPrice);
				eqrt->LTQ[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				//eqrt->TBQ[intIndex]=broadcastOnlyMBP->onlyMPBData[j].TotalBuyQuantity;
				//eqrt->TSQ[intIndex]=broadcastOnlyMBP->onlyMPBData[j].TotalSellQuantity;
				eqrt->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);
				eqrt->OP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].OpenPrice);
				eqrt->HP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].HighPrice);
				eqrt->LP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LowPrice);
				for (int m=0;m<10;m++)
				{
					eqrt->MBPI[intIndex].Quantity[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
					eqrt->MBPI[intIndex].Price[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Price);
				}

				////FILE *fp;
				////char fName[128]="UDP.txt";
				////int numwritten =0;
				////fp=fopen(fName, "a+");
				////sprintf(fName,"                                                                      ");
				////sprintf(fName,"\n Token=%d",eqrt->Token[intIndex]);
				////fwrite(fName,1,128,fp);
				////fclose(fp);

				////eqrt->HP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].HighPrice);
				////eqrt->CP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].ClosingPrice);
				////eqrt->LP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LowPrice);
				////eqrt->LTP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				////eqrt->LTQ[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				////eqrt->NCI[intIndex]=broadcastOnlyMBP->onlyMPBData[j].NetPriceChangeFromClosingPrice;
				////eqrt->NPCFCP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].NetPriceChangeFromClosingPrice);
				////eqrt->OP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].OpenPrice);
				////eqrt->Token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				////eqrt->VTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				////
				////for (int m=0;m<10;m++)
				////{
				////	eqrt->MBPI[intIndex].Quantity[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Quantity); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
				////	eqrt->MBPI[intIndex].Price[m] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[m].Price);
				////}
				
				intIndex=intIndex+1;
			}
			
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}


//9]GetIvAnalysisEQ:
//Desc: This Functions are used to retrive EQ Token and LTP, BBP & BSP
//Its return value is consider as a number of tokens it had
SYSREADERN_API int _stdcall GetIvAnalysisEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,LiveIvAnalysis * eqrt)
{
	if (DEBUGTEST)
	printf("\n GetLTPEQ:Start\n");

	unsigned  short errcode1=0;
	unsigned long ssize=0;

	BcastPackData *pac;
	BcastCmpPacket *compPac;
	CMMessageHeader *msgHdr;
	CMMessageHeader *cmmsgHdr;
	CMBroadCastOnlyMBP * broadcastOnlyMBP;
	CMBroadCastOnlyMBP * cmbroadcastOnlyMBP;
    pac = (BcastPackData *)&streamBuffer[0];
	short noOfpacket = htons(pac->iNoPackets);
	int intIndex=0;
	int noOfrec=0;
	
	if (DEBUGTEST)
	printf("\t GetLTPEQ: %c %i  packets %i\n",pac->cNetId, pac->iNoPackets, noOfpacket);
	
	char *tmpPtr = pac->cPackData;
	short bufLen=0; 
	short sizeCtr=0;
	long token;

	short j=0; // if buffer needs to be incremented  NEW 100402010
	for(int i=0;i<noOfpacket;i++)
	{
		short compBytes=0;
		noOfrec=0;
		//tmpPtr = pac->cPackData;
		//tmpPtr+= (i*2)+bufLen;
		tmpPtr = pac->cPackData;
		if (bufLen>0) //NEW 100402010
			tmpPtr+= (j*2)+bufLen; //NEW-replace 100402010
		compPac = (BcastCmpPacket *)tmpPtr;
		compBytes = htons(compPac->iCompLen);
		if (DEBUGTEST)
		printf("\t\ GetLTP: compresed len should be > 0 %i %i\n", compBytes,compPac->iCompLen);
		if (compBytes==0)
		{
			printf("\t\ GetLTP: SKIPPING Uncompresed packets\n");
			continue; 
		}
		j++;//NEW 100402010
		bufLen+=compBytes;
		
		//map tp msg header ie set the pointer to future value since lzo will decode it
		msgHdr = (CMMessageHeader *)((unsigned char *)unCompbuffer+sizeCtr+8);
		
		//point broadcastOnlyMBP to data struct
		broadcastOnlyMBP = (CMBroadCastOnlyMBP *)((char *)unCompbuffer+sizeCtr+8);
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:lzoing.. ");
		int errcode1= lzo1z_decompress((const unsigned char *)compPac->cCompData,compBytes,(unsigned char *)unCompbuffer+sizeCtr,(lzo_uint *)&ssize,NULL);
		
		if (DEBUGTEST)
			printf("\t\t GetLTPEQ:ssize %i errcode %i\n",ssize,errcode1);
		sizeCtr += ssize;
		//print msge header

		if (DEBUGTEST)
			PrintMsgHeader_CM(msgHdr);
		//filer unwanted tr code
		short trCode = ntohs(msgHdr->TransactionCode);
		
		if (trCode==filterTrCode)// we re looking for 7208
		{
			//if (DEBUGTEST)
			printf("\t\t GetLTPEQ:FOUND trcode %i\n", trCode);
			//PrintBroadCastOnlyMBP(broadcastOnlyMBP,trCode,tktable, filterTkn,  saveFlg);
			noOfrec = ntohs(broadcastOnlyMBP->noOfRecords);
			for(int j=0;j<noOfrec;j++)
			{
				
				eqrt->Token[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].Token);
				eqrt->VTT[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].VolumeTradedToday);
				eqrt->LTP[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradedPrice);
				eqrt->LTQ[intIndex]=ntohl(broadcastOnlyMBP->onlyMPBData[j].LastTradeQuantity);
				eqrt->BuyP[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[0].Price); //chnage index to i for  onlyMBP->onlyMPBData[i] instead of tokenCnt vers 103009001
				eqrt->SellP[intIndex] = ntohl(broadcastOnlyMBP->onlyMPBData[j].MBPinfo[5].Price);

				intIndex=intIndex+1;
			}
		}
		else
		{
			//if (DEBUGTEST)
			printf("\t\t  GetLTPEQ:- Different TRCode %i == %i \n", trCode,filterTrCode);
		}
		
	}  // end for

	if (DEBUGTEST)
	{
		printf("\t\tProcessMBPEQ:final uncompress size%i \n",ssize);
		printf("ProcessMBPEQ:DONE\n");
	}
	return intIndex;
}