#include "stdafx.h"
#include "stdio.h"
#include "util.h"






void converttolittle(char *src, int len)
{
	char tmp;
	int i;
	for ( i=0;i<512;i+=2)
	{	
		tmp = src[i];
	    src[i]=src[i+1];
		src[i+1]=tmp;

	}
	printf("%i",i);
}


void byteSwap(void *data, int dataLength) 
{
    /*
      Swap byte-order, usage: 
      double a = 123.456; 
      byteSwap (&a, sizeof(double));
    */

    unsigned char *byte, temp;
    int i;
    double *b;
    
    byte = (unsigned char *) data; 
    for (i = 0; i<dataLength/2; i++) 
	{
		temp = byte[i];
		byte[i] = byte[dataLength-1-i];
		byte[dataLength-1-i] = temp;
    }
}