// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the SYSREADERN_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// SYSREADERN_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef SYSREADERN_EXPORTS
#define SYSREADERN_API __declspec(dllexport)
#else
#define SYSREADERN_API __declspec(dllimport)
#endif

#include "util.h"
#include "winsock.h"
#include "include/lzo1z.h "
#include "stdio.h"
#include <time.h>
#include "intrin.h"


// This class is exported from the sysreadern.dll
class SYSREADERN_API Csysreadern {
public:
	Csysreadern(void);
	// TODO: add your methods here.
};

extern SYSREADERN_API int nsysreadern;

SYSREADERN_API int fnsysreadern(void);





#pragma pack(2)

typedef struct 
{
char cNetId [2];
short iNoPackets;
char cPackData [512];
} BcastPackData;

typedef struct {
	short iCompLen;
	char cCompData[MAX_MESSAGE_SIZE];
}BcastCmpPacket;

typedef struct 
{
	//BYTE Reserved : 4;
	char iApiTcode;
	char iApiFuncId;
	char iApiFuncId2; // extra 2 byte for this header 12 20 2010 change in
	char iApiFuncId3; 
	long LogTime;
	char AlphaChar [2];
	short TransactionCode;
	short ErrorCode;
	char Timestamp [8];
	char TimeStamp1 [8];
	char TimeStamp2 [8];
	short MessageLength;
}MessageHeader; 

typedef struct
{
	long Quantity;
	long Price;
	short NumberOfOrders;// this is now chnaged to short from long, i thnk this was issue 
	short BbBuySellFlag;
}MBPInformation;

 typedef struct 
{
	BYTE Reserved : 4;
	BYTE Sell : 1 ;
	BYTE Buy : 1 ;
	BYTE Last_Trade_Less : 1 ;
	BYTE Last_Trade_More : 1 ;
	BYTE Reserved_byte : 8 ;
} StIndicator;

typedef struct 
{
	long Token;
	short BookType;
	short TradingStatus;
	long VolumeTradedToday;
	long LastTradedPrice;
	char NetChangeIndicator;
	long NetPriceChangeFromClosingPrice;
	long LastTradeQuantity;
	long LastTradeTime;
	long AverageTradePrice;
	short AuctionNumber;
	short AuctionStatus;
	short InitiatorType;
	long InitiatorPrice;
	long InitiatorQuantity;
	long AuctionPrice;
	long AuctionQuantity;
	//char RecordBuffer[120]; // removed and added struc since data is required 
	//char RecordBuffer [ sizeof ( MBPInformation ) * 10 ];
	MBPInformation MBPinfo[10];
	short BbTotalBuyFlag;
	short BbTotalSellFlag;
	double TotalBuyQuantity;
	double TotalSellQuantity;
	StIndicator stInicator;
	long ClosingPrice;
	long OpenPrice;
	long HighPrice;
	long LowPrice;
}
InteractiveOnlyMBPData; // for f&o


typedef struct 
{
		MessageHeader msgHeader;
		short noOfRecords;
		InteractiveOnlyMBPData  onlyMPBData[2];

}
BroadCastOnlyMBP;


// **********************  for equity 

typedef struct 
{
	char iApiTcode;
	char iApiFuncId;
	char iApiFuncId2; // extra 2 byte for this header 
	char iApiFuncId3;
	long LogTime;
	char AlphaChar [2];
	short TransactionCode;
	short ErrorCode;
	char Timestamp [8];
	char TimeStamp1 [8];
	char TimeStamp2 [8];
	short MessageLength;
} CMMessageHeader; 


typedef struct 
{
	short Token; // for fno its long 
	short BookType;
	short TradingStatus;
	long VolumeTradedToday;
	long LastTradedPrice;
	char NetChangeIndicator;
	long NetPriceChangeFromClosingPrice;
	long LastTradeQuantity;
	long LastTradeTime;
	long AverageTradePrice;
	short AuctionNumber;
	short AuctionStatus;
	short InitiatorType;
	long InitiatorPrice;
	long InitiatorQuantity;
	long AuctionPrice;
	long AuctionQuantity;
	//char RecordBuffer[120]; // removed and added struc since data is required 
	//char RecordBuffer [ sizeof ( MBPInformation ) * 10 ];
	MBPInformation MBPinfo[10];
	short BbTotalBuyFlag;
	short BbTotalSellFlag;
	double TotalBuyQuantity;
	double TotalSellQuantity;
	StIndicator stInicator;  // it should be MBP_indicator but stindicator seems to have save struch structure 
	long ClosingPrice;
	long OpenPrice;
	long HighPrice;
	long LowPrice;


}
CMInteractiveOnlyMBPData; // for equity 

typedef struct 
{
		CMMessageHeader msgHeader;
		short noOfRecords;
		CMInteractiveOnlyMBPData  onlyMPBData[2];

}
CMBroadCastOnlyMBP;



// ******** generic structure for o/p 
typedef struct 
{
	short trCode;
	long Token;
	short BookType;
	short TradingStatus;
	long VolumeTradedToday;
	long LastTradedPrice;
	char NetChangeIndicator;
	long NetPriceChangeFromClosingPrice;
	long LastTradeQuantity;
	long LastTradeTime;
	long AverageTradePrice;
	short AuctionNumber;
	short AuctionStatus;
	short InitiatorType;
	long InitiatorPrice;
	long InitiatorQuantity;
	long AuctionPrice;
	long AuctionQuantity;
	MBPInformation MBPinfo[10];
	short BbTotalBuyFlag;
	short BbTotalSellFlag;
	double TotalBuyQuantity;
	double TotalSellQuantity;
	long ClosingPrice;
	long OpenPrice;
	long HighPrice;
	long LowPrice;
}TKROW;

typedef struct 
{
		
		short noOfRecords;
		TKROW  onlyMPBData[30];

}TKTABLE;

//****************************************************************************************************************
//Added By:SSP On:1/10/2010
//Desc: Adding two structures one for F&O and Second for EQ
//Since we need only LTP and Token we added two elements to the both structure as
//token and Ltp
//Another reason is for adding two elements here is that VB can not handles whole TKTable therefore
//we decided to took some part of it i.e. LTP and Token
typedef struct 
{
	int token[30];
	int ltp[30];
}FutureRate;


typedef struct 
{
	int token[30];
	int ltp[30];
}EQRate;

typedef struct 
{
	int token[30];
	int ltp[30];
	int buyrate[30];
	int sellrate[30];
	int CP[30];//Added NYN 29/10/2013
}EQBuySellRate; //Added By SSP 31/07/2012


//****************************************************************************************************************

/* ********************************
		Added By : HVB 
		Date     : 23/06/2014
		Desc.    : Base / Child Structure for Ticker and Market Index Packet Structure (F&O)
   *********************************** */
typedef struct 
{
	long Token;
	short MarketType;
	long FillPrice;
	long FillVolume;
	long OpenInterest;
	long DayHiOI;
	long DayLoOI;
}
ST_TICKER_INDEX_INFO; 

/* ********************************
		Added By : HVB 
		Date     : 23/06/2014
		Desc.    : Ticker and Market Index Packet Structure that contains Open Intrest
				   TRCODE_BCAST_TICKER_AND_MKT_INDEX	7202 (F&O)
		
   *********************************** */
typedef struct 
{
		MessageHeader msgHeader;
		short noOfRecords;
		ST_TICKER_INDEX_INFO  tickerInfo[17];
}
BCAST_TICKER_AND_MKT_INDEX;

typedef struct 
{
	long Token[30];
	short MarketType[30];
	long FillPrice[30];
	long FillVolume[30];
	long OpenInterest[30];
	long DayHiOI[30];
	long DayLoOI[30];
}
St_TickerDataOnly; 


/* ********************************
		Added By : HVB 
		Date     : 27/06/2016
		Desc.    : Base / Child Structure for Ticker and Market Index Packet Structure (EQ)
   *********************************** */
typedef struct 
{
	short Token;
	short MarketType;
	long FillPrice;
	long FillVolume;
	long MarketIndexValue;
}
ST_TICKER_INDEX_INFO_CM; 

/* ********************************
		Added By : HVB 
		Date     : 27/06/2014
		Desc.    : Ticker and Market Index Packet Structure that contains Open Intrest
				   TRCODE_BCAST_TICKER_AND_MKT_INDEX	7202 (EQ)
		
   *********************************** */
typedef struct 
{
		CMMessageHeader msgHeader;
		short noOfRecords;
		ST_TICKER_INDEX_INFO_CM  tickerInfo[28];
}
BCAST_TICKER_AND_MKT_INDEX_CM;

typedef struct 
{
	short Token[30];
	short MarketType[30];
	long FillPrice[30];
	long FillVolume[30];
	long MarketIndexValue[30];
}
St_TickerDataOnlyCM; 

/* ********************************
		Added By : HVB 
		Date     : 28/06/2014
		Desc.    : Ticker and Market Index Packet Structure that contains Open Intrest
				   TRCODE_BCAST_TICKER_AND_MKT_INDEX	7202 (CD)
		
   *********************************** */

//This message header i can't find so added for testing.
//Refereing document of nse for cd
typedef struct
{
	//2 Reserved Bytes
	char Reservered1;
	char Reservered2;
	char Reservered3;//Extra 2 more bytes
	char Reservered4;
	long LogTime;
	char AlphaChar[2];
	short TransactionCode;
	short ErrorCode;
	char Reserved[8];
	char TimeStamp1[8];
	char TimeStamp2[8];
	short MessageLength;
}
Message_HeaderCD;

typedef struct 
{
		Message_HeaderCD msgHeader;
		short noOfRecords;
		ST_TICKER_INDEX_INFO  tickerInfo[17];
}
BCAST_TICKER_AND_MKT_INDEXCD;

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Spot price packet having market index 7207 (FO)
		
   *********************************** */

typedef struct
{
	char	IndexName[21];
	long	IndexValue;
	long	HighIndexValue;
	long	LowIndexValue;
	long	OpeningIndex;
	long	ClosingIndex;
	long	PercentChange;
	long	YearlyHigh;
	long	YearlyLow;
	long	NoOfUpmoves;
	long	NoOfDownmoves;
	double	MarketCaptitalization;
	char	NetChangeIndicator;
	char	Reserverd1;
}
MS_Indices;

typedef struct
{
	MessageHeader msgHeader;
	short		  noOfRecords;
	MS_Indices	  IndiceData[15];
}
BCAST_INDICES;

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Spot price packet having market index 7207 (CM)
		
   *********************************** */

typedef struct
{
	char IndexName [21];
	long IndexValue;
	long HighIndexValue;
	long LowIndexValue;
	long OpeningIndex;
	long ClosingIndex;
	long PercentChange;
	long YearlyHigh;
	long YearlyLow;
	long NoOfUpmoves;
	long NoOfDownmoves;
	double MarketCapitalisation;
	char NetChangeIndicator;
	char FILLER;
}
CMMs_Indices;

typedef struct
{
	CMMessageHeader msgHeader;
	short			noOfRecords;
	CMMs_Indices	Indices_Data[6];
}
CMBcast_Indices;
//****************************************************************************************************************


#pragma pack(2)

/* ********************************
		Added By : HVB 
		Date     : 23/06/2014
		Desc.    : Method to decode structure for ticker and market for F&O ... Added Method For CM ON 27/06/2016
   *********************************** */
void WriteDetailLog(char * FileName, char * Module,char * Message);
void WriteDetailLog(char * FileName,int PrintOnlyMsg, char * Module,char * Message);
void AppendCurrentDateTime(char * FileName,short AppendTime);
extern "C" SYSREADERN_API  int _stdcall GetTrCodes(char * FileNameForTr,char * streamBuffer,char * unCompbuffer);
extern "C" SYSREADERN_API  int _stdcall GetTrCodesNew(char * FileNameForTr,char * streamBuffer,char * unCompbuffer,char * retBuffer);
extern "C" SYSREADERN_API  int _stdcall GetTickerAndMarket(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,BCAST_TICKER_AND_MKT_INDEX * bticker);
extern "C" SYSREADERN_API  int _stdcall GetTickerAndMarketNew(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnly * bticker);
extern "C" SYSREADERN_API  int _stdcall GetTickerAndMarketNewCD(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnly * bticker);
extern "C" SYSREADERN_API  int _stdcall GetTickerAndMarketNewCM(char * streamBuffer,char * unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,St_TickerDataOnlyCM * bticker);

//Added By Hvb On 09-07-2016 to get spot price
extern "C" SYSREADERN_API  int _stdcall GetTrCodesLzo(char * streamBuffer,char * unCompbuffer);
//extern "C" SYSREADERN_API  int _stdcall GetUncompIndexData(char * streamBuffer,char * unCompbuffer);

/* ********************************
		Added By : HVB 
		Date     : 23/06/2016
		Desc.    : Method to decode structure for ticker and market 7202 And write to text file
   *********************************** */
extern "C" SYSREADERN_API  int _stdcall WriteTickerAndMarket(char * FileNameToWrite,char * streamBuffer,char * unCompbuffer);

/* ********************************
		Added By : HVB 
		Date     : 15/10/2016
		Desc.    : Decodes packet for 7207 and creates spot price text file in key val form
   *********************************** */
extern "C" SYSREADERN_API  int _stdcall DumpSpotPrice(char * FilePath,char * streamBuffer,char * unCompbuffer);

//Code added by hvb to see text modification
extern "C" SYSREADERN_API  int _stdcall TextAppendTest(LPSTR srcStr);

/* ********************************
		Added By   : HVB 
		Date       : 17/10/2016
		Desc.      : Decodes packet for 7207 and returns length of string after modification
		Parameters : (1) LPSTR outString    : String which will have output
		             (2) char* streamBuffer : Source buffer
					 (3) char* unCompbuffer : Source unCompbuffer
   *********************************** */
extern "C" SYSREADERN_API  int _stdcall ReadSpotPrice(LPSTR outString,char * streamBuffer,char * unCompbuffer);


//Added by hvb on 16/10/2016 to check file path recieved
//extern "C" SYSREADERN_API  int _stdcall CheckCreation(char * FilePath);


extern "C"  void PrintMsgHeader(MessageHeader *msgHdr);
extern "C"  void PrintBroadCastOnlyMBP(BroadCastOnlyMBP *onlyMBP, short trCode, TKTABLE * tktable,long filterTkn, short saveFlg);
extern "C"  void PrintMsgHeader_CM(CMMessageHeader *msgHdr);
extern "C"  void PrintBroadCastOnlyMBP_CM(CMBroadCastOnlyMBP *onlyMBP, short trCode, TKTABLE * tktable,long filterTkn, short saveFlg);
extern "C" SYSREADERN_API int ProcessMBP_CM(char* streamBuffer, char *unCompbuffer, char *strTrCode,TKTABLE * tktable,long filterTkn, short saveFlg);
extern "C" SYSREADERN_API  int recvData(int nport, char *strIP,char* RecvBuf, int bug);
extern "C" SYSREADERN_API int ProcessMBP(char* streamBuffer, char *unCompbuffer, char *strTrCode,TKTABLE * tktable,long filterTkn, short saveFlg);
extern "C" SYSREADERN_API  char* recvData2(int nport, char *strIP, int bug);
extern "C" SYSREADERN_API int RecvDataAndProcessMBP(int nport, char *strIP,  int nTrCode, TKTABLE * tktable, int bug,long filterTkn, short saveFlg);
extern "C" SYSREADERN_API  void  setTestInit(int i);
extern "C" SYSREADERN_API  int  getTestInit();
extern "C" SYSREADERN_API  void  setPrintDebug(short debug);
extern "C" SYSREADERN_API  long getVersion();
extern "C" SYSREADERN_API  int _stdcall AddTwoNos(int num1);

//****************************************************************************************************************
//Added By:SSP On:1/10/2010
//Adding three functions declarations here
//1]GetLtp
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//FutureRate * futrt
extern "C" SYSREADERN_API int _stdcall GetLTP(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,FutureRate * futrt);

//2]GetLtpEQ
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//EQRate * eqrt
extern "C" SYSREADERN_API int _stdcall GetLTPEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,EQRate * eqrt);

//3]GetLtp2
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//FutureRate * futrt
//EQRate * eqrt
//int intEQFlag
extern "C" SYSREADERN_API int _stdcall GetLTP2(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,FutureRate * futrt,EQRate * eqrt ,int intEQFlag);
//****************************************************************************************************************


//Added By:SSP On:1/12/2010

typedef struct 
{
	int token[30];
	int ltp[30];
	int buyrate[30];
	int sellrate[30];
	int buyqty[30];//Added NYN 14/11/2013
	int sellqty[30];//Added NYN 14/11/2013
}BuySellRate;

//Adding additional functions declarations here
//4]GetBuySell
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//BuySellRate * futrt
extern "C" SYSREADERN_API int _stdcall GetBuySell(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,BuySellRate * futrt);
//****************************************************************************************************************

//Added By:SSP On:27/5/2011

typedef struct 
{
	int token[30];
	int ltp[30];
	int ltq[30];
	int vtt[30];
}LtpLtqVtt;

//Adding additional functions declarations here
//5]GetLtpLtqVtt
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//LtpLtqVtt * futrt
extern "C" SYSREADERN_API int _stdcall GetLtpLtqVtt(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,LtpLtqVtt * futrt);
//****************************************************************************************************************


//Added By:SSP On:1/6/2011
////Public Type MBPInfo
////    Qty(0 To 9) As Long
////    Price(0 To 9) As Long
////End Type
////
////Public Type MBP
////    Token(30) As Long
////    VTT(30) As Long 'VolumeTradedToday
////    LTP(30) As Long 'LastTradedPrice
////    NCI(30) As Byte 'NetChangeIndicator
////    NPCFCP(30) As Long 'NetPriceChangeFromClosingPrice
////    LTQ(30) As Long 'LastTradedQuatity
////    TBQ(30) As Double 'TotalBuyQuatity
////    TSQ(30) As Double 'TotalSellQuantity
////    CP(30) As Long  'ClosingPrice
////    OP(30) As Long 'OpenPrice
////    HP(30) As Long 'HighPrice
////    LP(30) As Long 'LowPrice
////    MBPI(30) As MBPInfo 'MbpInformation
////End Type

typedef struct
{
	long Quantity[10];
	long Price[10];
}MBPInfo;

typedef struct 
{
	int Token[30]; 
	int VTT[30];
	int LTP[30];

	//char NCI[30];
	int NPCFCP[30];
	int LTQ[30];
	int LTT[30];//Added NYN 16/07/2013 For Adding the Last Traded Time for Talkdelta Opportunity
	//Commented NYN 19/11/2013
	/*int AverageTradePrice [30];
    int ShortAuctionNumber [30];
	float TBQ[30];
	float TSQ[30];*/
	int CP[30];
	int OP[30];
	int HP[30];
	int LP[30];
	MBPInfo MBPI[30];
	char NCI[30];


}MBP;



//Adding additional functions declarations here
//6]GetMBP
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//MBP * futrt
extern "C" SYSREADERN_API int _stdcall GetMBP(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,MBP * mbp);
//****************************************************************************************************************




//Adding additional functions declarations here
//7]GetBuySellEQ
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//EQBuySellRate * futrt
extern "C" SYSREADERN_API int _stdcall GetBuySellEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,EQBuySellRate * eqrt);

typedef struct 
{
	int Token[30];
	int LTP[30];
	int LTQ[30];
	int BuyP[30];
	int SellP[30];
	int VTT[30];
}LiveIvAnalysis;

//Adding additional functions declarations here
//8]GetIvAnalysis
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//MBP * futrt
extern "C" SYSREADERN_API int _stdcall GetIvAnalysis(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,LiveIvAnalysis * mbp);
//****************************************************************************************************************



//9]GetMBPEQ
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//MBP * futrt
//Added By SSP 11/12/2012
extern "C" SYSREADERN_API int _stdcall GetMBPEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,MBP * eqrt);

//10]GetIvAnalysisEQ
//Parameters:
//char* streamBuffer
//char *unCompbuffer
//int filterTrCode
//long filterTkn
//short saveFlg
//MBP * futrt
//Added By SSP 11/12/2012
extern "C" SYSREADERN_API int _stdcall GetIvAnalysisEQ(char* streamBuffer, char *unCompbuffer, int filterTrCode,long filterTkn, short saveFlg,LiveIvAnalysis * eqrt);


char pkt1437[] = {
0x02, 0x20, 0x00, 0x02, 0x00, 0xef, 
0x2d, 0x02, 0x59, 0x5a, 0x2a, 0x2a, 0x31, 0x32, 
0x33, 0x00, 0x00, 0x36, 0xfe, 0x39, 0x0a, 0x20, 
0x20, 0x1c, 0x28, 0x00, 0x00, 0x00, 0x06, 0x43, 
0x6b, 0x2c, 0x69, 0x62, 0x72, 0xa0, 0x4c, 0x01, 
0xb2, 0xb2, 0x32, 0x20, 0xa0, 0x00, 0x07, 0x01, 
0xd4, 0x00, 0x02, 0x00, 0x00, 0x94, 0x3e, 0x00, 
0x01, 0x60, 0x1e, 0x02, 0xbc, 0x00, 0x0f, 0x2e, 
0xe0, 0x20, 0x40, 0xba, 0x2e, 0xe0, 0x40, 0x11, 
0x14, 0x00, 0xae, 0x38, 0x07, 0x00, 0x1b, 0x32, 
0xdd, 0x00, 0x36, 0x00, 0x01, 0xc8, 0x00, 0x0a, 
0x2f, 0x49, 0x40, 0xec, 0x60, 0x01, 0x14, 0x00, 
0x0a, 0x2b, 0x5c, 0x28, 0x00, 0x2e, 0x2a, 0xf8, 
0x29, 0x00, 0x2d, 0x94, 0x28, 0x00, 0x2e, 0x27, 
0x15, 0xdc, 0x40, 0xee, 0x30, 0x9d, 0x28, 0x00, 
0x5e, 0x32, 0xc8, 0x28, 0x00, 0x2e, 0x38, 0xa4, 
0x28, 0x00, 0x2e, 0x39, 0xcb, 0xdc, 0x43, 0x16, 
0x39, 0xd0, 0x62, 0x94, 0x60, 0x03, 0x40, 0x98, 
0x10, 0xa0, 0x1d, 0x7e, 0xc0, 0x3c, 0xc2, 0xcc, 
0x60, 0x0e, 0x39, 0x6c, 0xa0, 0x1e, 0x95, 0x5d, 
0xa3, 0x56, 0x01, 0xc2, 0x00, 0x0f, 0x08, 0x07, 
0x2b, 0x40, 0x86, 0x07, 0xd0, 0x60, 0x34, 0x01, 
0x36, 0xfe, 0x26, 0xe6, 0x60, 0x44, 0x36, 0x03, 
0x50, 0x80, 0xbd, 0xa2, 0xa1, 0xd6, 0x08, 0xca, 
0x40, 0x0d, 0x48, 0xa0, 0x2c, 0x60, 0x5e, 0x07, 
0x12, 0x29, 0x00, 0x2d, 0x08, 0xbe, 0x03, 0x84, 
0x45, 0x79, 0x0e, 0x28, 0x00, 0xbe, 0x09, 0x65, 
0x28, 0x00, 0x8e, 0x0a, 0xb4, 0x29, 0x00, 0x2d, 
0xf0, 0xbc, 0x38, 0x00, 0x03, 0x40, 0xbd, 0xe2, 
0xa0, 0x1e, 0xa8, 0x9c, 0x9d, 0x80, 0xc2, 0xcc, 
0x62, 0xac, 0xa0, 0x0c, 0x11, 0x00, 0x00, 0x00, 
0xe4, 0x2d, 0x02, 0x59, 0x5a, 0x2a, 0x2a, 0x31, 
0x32, 0x33, 0x00, 0x00, 0x36, 0xfe, 0x39, 0x0a, 
0x20, 0x20, 0x1c, 0x28, 0x00, 0x00, 0x00, 0x06, 
0x43, 0x6c, 0x3b, 0x69, 0x62, 0x72, 0xa0, 0x4c, 
0x01, 0xb2, 0xba, 0x32, 0x20, 0xa0, 0x00, 0x07, 
0x01, 0xd4, 0x00, 0x02, 0x00, 0x00, 0x95, 0x61, 
0x00, 0x01, 0x60, 0x1e, 0x01, 0xc2, 0x00, 0x0f, 
0x13, 0x88, 0x2d, 0x40, 0xba, 0x16, 0xc1, 0x60, 
0x34, 0x01, 0x36, 0xfe, 0x35, 0xdb, 0x60, 0x45, 
0x00, 0x35, 0x00, 0x02, 0x08, 0xca, 0x40, 0x75, 
0xf1, 0x40, 0xec, 0x40, 0x02, 0x0f, 0xd2, 0x00, 
0x0e, 0x10, 0xcc, 0xa0, 0x2c, 0x61, 0x1e, 0x0f, 
0xa0, 0x28, 0x00, 0x2e, 0x0b, 0xb8, 0xbe, 0x03, 
0x84, 0x42, 0x21, 0x36, 0xa0, 0x2c, 0x60, 0xee, 
0x15, 0x63, 0x28, 0x00, 0x8e, 0x17, 0x0c, 0x28, 
0x00, 0x2e, 0x1b, 0x58, 0xbc, 0x38, 0x00, 0x03, 
0x40, 0xbf, 0xa4, 0xa0, 0x1e, 0xa8, 0x9c, 0xbc, 
0xc2, 0xcc, 0x62, 0xac, 0xe0, 0x0e, 0x95, 0x68, 
0xa3, 0x56, 0x07, 0x08, 0x00, 0x0e, 0x0a, 0x8c, 
0x63, 0x54, 0x42, 0x1c, 0x83, 0x56, 0x32, 0x6f, 
0x40, 0x2d, 0x1e, 0x3a, 0x03, 0x56, 0x0b, 0x22, 
0x28, 0x02, 0x36, 0x09, 0xc9, 0x29, 0x00, 0x2d, 
0xc4, 0x28, 0x00, 0x2e, 0x08, 0x34, 0x28, 0x00, 
0x2e, 0x07, 0xda, 0x28, 0x03, 0x56, 0x0c, 0x44, 
0x28, 0x00, 0x5e, 0x10, 0x04, 0x28, 0x00, 0x2e, 
0x13, 0x1a, 0x29, 0x00, 0x2d, 0x88, 0x32, 0x03, 
0x87, 0x40, 0xc0, 0xb3, 0xa0, 0x1e, 0xac, 0x20, 
0x9d, 0x80, 0xc2, 0xce, 0x0d, 0xac, 0xa0, 0x0e, 
0x09, 0xf1, 0x11, 0x00, 0x00, 0x00, 0x2c, 0xfe, 
0x12, 0x00, 0xbc, 0xc2, 0xce, 0x31, 0x2c, 0xfe, 
0x12, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x54, 0x62, 0xce, 0x0f, 0x3c, 0x63, 
0x04, 0x60, 0x1c, 0x11, 0x00, 0x00, 0x11, 0x00, 
0x00, 0xad };

char pkt89[] = {
 0x02, 0x20, 0x00, 0x01, 0x01, 0x06, 
0x2d, 0x02, 0x59, 0x5a, 0x2a, 0x2a, 0x31, 0x32, 
0x33, 0x00, 0x00, 0x36, 0xfe, 0x39, 0x07, 0x20, 
0x20, 0x1c, 0x28, 0x00, 0x00, 0x00, 0x06, 0x43, 
0x2b, 0x2a, 0x69, 0x62, 0x72, 0xa0, 0x4c, 0x01, 
0xc4, 0x3a, 0x32, 0x20, 0xa0, 0x00, 0x07, 0x01, 
0xd4, 0x00, 0x02, 0x00, 0x00, 0xac, 0x74, 0x00, 
0x01, 0x40, 0x1f, 0x04, 0x19, 0x6a, 0x00, 0x2f, 
0x26, 0x48, 0x2b, 0x40, 0xba, 0x26, 0x25, 0x40, 
0x11, 0x32, 0x00, 0xae, 0x37, 0x0f, 0x00, 0x1b, 
0x25, 0x78, 0x00, 0x35, 0x00, 0x02, 0x01, 0xf4, 
0x40, 0x75, 0x8a, 0x40, 0xec, 0x60, 0x01, 0x64, 
0x40, 0x2d, 0x85, 0xbc, 0x80, 0x5d, 0x21, 0x29, 
0x00, 0x5d, 0x1c, 0x61, 0x94, 0x40, 0x5d, 0xc2, 
0x00, 0x0e, 0x24, 0x45, 0xc0, 0x5d, 0x32, 0x00, 
0x0a, 0x28, 0xdc, 0x29, 0x00, 0x2d, 0xf0, 0x28, 
0x00, 0xbe, 0x29, 0x04, 0x28, 0x00, 0x5e, 0x29, 
0x9f, 0x28, 0x00, 0x2e, 0x2a, 0x1c, 0xdc, 0x02, 
0x00, 0x40, 0xd4, 0x11, 0x80, 0x80, 0x1f, 0xe2, 
0xa0, 0xc0, 0x9c, 0xc2, 0xce, 0x25, 0x80, 0x80, 
0xf7, 0x00, 0x23, 0x2d, 0x43, 0x55, 0x75, 0x9f, 
0x00, 0x0b, 0x22, 0x00, 0x0e, 0x73, 0xa0, 0x40, 
0x43, 0x00, 0x77, 0xe2, 0x61, 0x7c, 0x05, 0x36, 
0xfe, 0x36, 0x44, 0x00, 0x00, 0x76, 0x8d, 0x36, 
0x03, 0x56, 0x07, 0x3a, 0x40, 0xbd, 0x50, 0x28, 
0x03, 0x26, 0x73, 0x28, 0xa0, 0x2e, 0x11, 0x94, 
0x00, 0x0e, 0x70, 0x8a, 0x28, 0x00, 0x5e, 0x61, 
0xa8, 0x28, 0x02, 0x66, 0x5b, 0xcc, 0x28, 0x00, 
0x2e, 0x79, 0x90, 0xdd, 0xc8, 0x5d, 0xe0, 0xbc, 
0x63, 0xe6, 0x7d, 0x0f, 0xa0, 0x2e, 0x03, 0xb6, 
0x00, 0x0e, 0x7e, 0x8b, 0xe3, 0x24, 0x2a, 0x00, 
0x03, 0x40, 0xbf, 0x72, 0xa0, 0x1e, 0x99, 0xc8, 
0xbc, 0xc2, 0xcd, 0x7d, 0x40, 0x1e, 0x8a, 0x3e, 
0x42, 0x25, 0x4b, 0x11, 0x00, 0x00, 0x1e, 0x9e, 
0xfc, 0x00, 0x0e, 0x92, 0xe0, 0x11, 0x00, 0x00, 
0x11, 0x00, 0x00, 0xd4, 0x00, 0x02, 0x00, 0x00, 
0xac, 0x51, 0x00, 0x01, 0x60, 0x1e, 0x38, 0x40, 
0x00, 0x0f, 0x0d, 0xac, 0x2d, 0x40, 0xba, 0x0f, 
0x3c, 0x00, 0x0e, 0x01, 0x90, 0x00, 0xae, 0x2b, 
0x6d, 0x00, 0x1f, 0x0e, 0xc4, 0x00, 0x36, 0x00, 
0x01, 0x32, 0x00, 0x0a, 0x0c, 0x8a, 0x40, 0xec, 
0x40, 0x02, 0x04, 0x1a, 0x00, 0x0e, 0x0b, 0xea, 
0xa0, 0x2e, 0x00, 0x64, 0x5d, 0xb8, 0x61, 0x44, 
0xa0, 0x8e, 0x0a, 0xf5, 0xa0, 0x5e, 0x0c, 0x1c, 
0x40, 0x2d, 0xf0, 0x28, 0x00, 0x8e, 0x0d, 0xde, 
0xa0, 0x2e, 0x03, 0xb6, 0x41, 0x95, 0x06, 0xc0, 
0x2d, 0xe8, 0x5d, 0x6a, 0x28, 0x00, 0x2e, 0x0f, 
0x32, 0xbe, 0x02, 0x58, 0x00, 0x0e, 0x10, 0x5e, 
0xc0, 0xec, 0x02, 0x00, 0x40, 0xf2, 0x84, 0xa0, 
0x80, 0x1e, 0xb9, 0x32, 0x27, 0x02, 0x30, 0x62, 
0xce, 0x0e, 0x10, 0xa0, 0x1e, 0x0c, 0xe4, 0x43, 
0x55, 0x53, 0xbd, 0x32, 0x40, 0x8b, 0xf6, 0x18, 
0x2b, 0x40, 0x86, 0xf4, 0x0b, 0x62, 0x3c, 0x05, 
0x36, 0xfe, 0x34, 0xfa, 0x00, 0x00, 0xf8, 0x2f, 
0x36, 0x03, 0x52, 0x02, 0xbc, 0x00, 0x0e, 0xeb, 
0xfa, 0xc1, 0xa4, 0x40, 0xee, 0xcf, 0x6c, 0x29, 
0x00, 0x2d, 0x08, 0x28, 0x00, 0x2e, 0xcd, 0x14, 
0x28, 0x00, 0x2e, 0xc8, 0x64, 0x28, 0x02, 0xc4, 
0x41, 0x70, 0x27, 0x00, 0x2e, 0xfb, 0x40, 0x36, 
0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2c, 0xfe, 
0x12, 0x00, 0x00, 0x5e, 0xff, 0xdc, 0x2c, 0xfe, 
0x12, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0xf6, 0x18, 0x40, 0x95, 0x78, 0x00, 
0x0e, 0xf0, 0x3c, 0x11, 0x00, 0x00, 0x27, 0x29, 
0x6c, 0xad };

#pragma pack(2)

