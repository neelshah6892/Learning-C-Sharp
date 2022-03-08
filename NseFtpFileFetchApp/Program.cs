using System;
using System.IO;
using System.Net;

namespace NseFtpFileFetchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            security_today_cm();
            participant_today_cm();
            nnf_security_today_cm();
            nnf_participant_today_cm();
            spd_contract_today_cm();
            contract_today_cm();
            cm_contract_stream_info();

            ael_today_fo();
            nsccl_today_fo();
            contracts_today_fo();
            fo_participant();
            //fo_marketreports();
            fo_spd_contract_stream_info();
            fo_contract_stream_info();
            fo_ael_nifty_options();

            nsccl_today_cd();
            cd_spd_contract();
            cd_participant();
            cd_contract();
            cd_spd_contract_stream_info();
            cd_contract_stream_info();
            //cd_marketreports();
            
            //List all Folders and files
            //StreamReader streamReader = new StreamReader(response.GetResponseStream());

            //List<string> directories = new List<string>();

            //string line = streamReader.ReadLine();
            //while (!string.IsNullOrEmpty(line))
            //{
            //    directories.Add(line);
            //    line = streamReader.ReadLine();
            //    Console.WriteLine(line);
            //}
        }

        private static void ael_today_fo()
        {
            string today = DateTime.Today.ToString("ddMMyyyy");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/Limit Files/ael_" + today + ".csv");

            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/Limit Files/ael_30042021.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/ael_" + today + ".csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void nsccl_today_fo()
        {
            string today = DateTime.Today.ToString("yyyyMMdd");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/Parameter/nsccl." + today + ".i01.spn.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/nsccl." + today + ".i01.spn.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void contracts_today_fo()
        {
            string today = DateTime.Today.ToString("ddMMyyyy");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/Limit Files/F_AEL_OTM_CONTRACTS_" + today + ".csv.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/F_AEL_OTM_CONTRACTS_" + today + ".csv.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void nsccl_today_cd()
        {
            string today = DateTime.Today.ToString("yyyyMMdd");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/Parameter/nsccl_x." + today + ".i01.spn.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/nsccl_x." + today + ".i01.spn.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void security_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/security.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/security.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void participant_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/participant.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/participant.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void nnf_security_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/nnf_security.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/nnf_security.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void nnf_participant_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/nnf_participant.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/nnf_participant.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void spd_contract_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/spd_contract.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/spd_contract.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void contract_today_cm()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/NTNEAT/contract.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/contract.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cm_contract_stream_info()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/common/tbt_masters/cm_contract_stream_info.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("ftpguest", "FTPGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cm_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void fo_participant()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/fo_participant.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/fo_participant.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void fo_marketreports()
        {
            string today = DateTime.Today.ToString("ddMMyyyy");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/MarketReports/F_CN01_NSE_" + today + ".csv.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/F_CN01_NSE_" + today + ".csv.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void fo_spd_contract_stream_info()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/tbt_masters/fo_spd_contract_stream_info.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/fo_spd_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void fo_contract_stream_info()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/tbt_masters/fo_contract_stream_info.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/fo_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void fo_ael_nifty_options()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/faoftp/FaoCommon/Limit Files/ael_NIFTY_Options.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("faoguest", "FAOGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/ael_NIFTY_Options.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_spd_contract()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/cd_spd_contract.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_spd_contract.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_participant()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/cd_participant.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_participant.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_contract()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/cd_contract.gz");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_contract.gz", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_spd_contract_stream_info()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/tbt_masters/cd_spd_contract_stream_info.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_spd_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_contract_stream_info()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/tbt_masters/cd_contract_stream_info.csv");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }

        private static void cd_marketreports()
        {
            string today = DateTime.Today.ToString("ddMMyyyy");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.connect2nse.com/%2F/cdsftp/cdscommon/MarketReports/X_CN01_NSE_" + today +".CSV.GZ");

            request.UseBinary = true;

            request.Credentials = new NetworkCredential("cdsguest", "CDSGUEST");

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            FileStream writeStream = new FileStream("D:/nsefiles/cd_contract_stream_info.csv", FileMode.Create);

            int Length = 4096;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();


            Console.WriteLine($"Download complete, status {response.StatusDescription}");

            reader.Close();
            response.Close();
        }
    }
}