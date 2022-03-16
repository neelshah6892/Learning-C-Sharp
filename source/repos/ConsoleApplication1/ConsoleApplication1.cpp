#include <winsock2.h>
#include <WS2tcpip.h>
#include <stdio.h>

#define SERVER "233.2.1.5"
#define BUFLEN 512
#define DEFAULT_PORT "6790"

#pragma comment(lib, "Ws2_32.lib")

SOCKET s;
struct sockaddr_in si_other, server;
int slen, recv_len;
char buf[BUFLEN];
WSADATA wsa;

struct addrinfo* result = NULL, * ptr = NULL, hints;

int main() {
	

	slen = sizeof(si_other);

	//Initialise winsock
	printf("\n Initialising winsock...");
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0) {
		printf("Failed, Error Code: %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	printf("Initialised.\n");

	//Create a socket
	if ((s = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP)) == INVALID_SOCKET) {
		printf("Could not create socket: %d", WSAGetLastError());
	}
	printf("Socket Created.\n");

	//Bind Socket
	if (bind(s, (struct sockaddr*)&server, sizeof(server)) == SOCKET_ERROR) {
		printf("Bind failed with error code : %d", WSAGetLastError());
		exit(EXIT_FAILURE);
	}
	printf("Bind Successful.\n");

	//Listen to the data
	while (1) {
		printf("Waiting for data...");
		fflush(stdout);

		//Clear buffer by filling null, it might have previously received data
		memset(buf, '\0', BUFLEN);

		//Try to receive some data, this is a blocking call
		if ((recv_len = recvfrom(s, buf, BUFLEN, 0, (struct sockaddr*)&si_other, &slen)) == SOCKET_ERROR) {
			printf("recvfrom() failed with error code : %d", WSAGetLastError());
			exit(EXIT_FAILURE);
		}

		//Print details of data received
		//printf("Received packet from %s:%d\n", inet_ntoa(si_other.sin_addr), ntohs(si_other.sin_port));
		printf("Data: %s\n", buf);
	}
	closesocket(s);
	WSACleanup();

	return 0;
}