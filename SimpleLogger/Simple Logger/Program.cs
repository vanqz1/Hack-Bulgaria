using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace second
{   
    /// Declaring an interface MyLogger with the only metof log
    
    interface Mylogger
    {
        void log(int level, string message);
    }

    /// Make a class ConsoleLogger that implements MyLogger
    /// The ckass ConsoleLogger would write the result on the console
    class ConsoleLogger : Mylogger
    {
        public void log(int level, string message)
        {
            string date = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
            string result = null;
            switch (level)
            {
                case 1:
                    {
                        result = "INFO" + "::" + date + "::" + message;
                        break;
                    }

                case 2:
                    {
                        result = "WARNING" + "::" + date + "::" + message;
                        break;
                    }

                case 3:
                    {
                        result = "PLSCHECKFFS" + "::" + date + "::" + message;
                        break;
                    }
            }

            Console.WriteLine(result);
        }
    }

    /// Make a class FileLogger that implements MyLogger
    /// The ckass ConsoleLogger would write the result on the File
    /// The directory of the file is /Simple Logger /Bin/Release/fileLogger.txt
    class FileLogger : Mylogger
    {
        public void log(int level, string message)
        {
            string FileName = "fileLogger.txt";
            StreamWriter writer = new StreamWriter(FileName,true);
            string date = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
            string result = null;

            using (writer)
            {
                switch (level)
                {
                    case 1:
                        {
                            result = "INFO" + "::" + date + "::" + message;
                            break;
                        }

                    case 2:
                        {
                            result = "WARNING" + "::" + date + "::" + message;
                            break;
                        }

                    case 3:
                        {
                            result = "PLSCHECKFFS" + "::" + date + "::" + message;
                            break;
                        }
                }

                writer.WriteLine(result);
             }
        }
    }
    class HTTPLogger : Mylogger
    {
        public void log(int level, string message)
        {
      
            string date = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
            string result = null;

           
                switch (level)
                {
                    case 1:
                        {
                            result = "INFO" + "::" + date + "::" + message;
                            break;
                        }

                    case 2:
                        {
                            result = "WARNING" + "::" + date + "::" + message;
                            break;
                        }

                    case 3:
                        {
                            result = "PLSCHECKFFS" + "::" + date + "::" + message;
                            break;
                        }
                }
               
                //Creating a request using a URL 
                WebRequest request = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ");
                
                // Set the Method property of the request to POST.
                request.Method = "POST";
               
                // Creating a POST data and convert it to a byte array.
                string postData = result;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
               
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/x-www-form-urlencoded";
               
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
               
                // Close the Stream object.
                dataStream.Close();
                
                // Get the response.
                WebResponse response = request.GetResponse();
               
                // Testing whether the data was successfull sent
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
             
           }
    }

    class Program
    {
        static void Main(string[] args)
        {

            ConsoleLogger test11 = new ConsoleLogger();
            test11.log(1, "Hello");

            ConsoleLogger test12 = new ConsoleLogger();
            test12.log(2, "Moha");

            ConsoleLogger test13 = new ConsoleLogger();
            test13.log(3, "hallo");

            FileLogger test21 = new FileLogger();
            test21.log(1, "Hello");

            FileLogger test22 = new FileLogger();
            test22.log(2, "Moha");

            FileLogger test23 = new FileLogger();
            test23.log(3, "Hallo");
            
           
            HTTPLogger test31 = new HTTPLogger();
            test31.log(1, "Hello");

            HTTPLogger test32 = new HTTPLogger();
            test32.log(3, "Moha");

        }
    }





}



