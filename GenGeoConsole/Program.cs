using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Net.Http;
using System.Web;



namespace GenGeoConsole
{
    class Program
    {
        [DataContract]
        class GEO
        {
            [DataMember]
            internal string nameID;
            [DataMember]
            internal string geonamedevice;
            [DataMember]
            internal double X;
            [DataMember]
            internal double Y;

            //GEO()
            //{
            //    X = 100;
            //    Y = 200;
            //    nameID = "test00";
            //    geonamedevice = "dev00";
            //}

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Start HTTP request");

            string url = "https://localhost:44359/home/inputgeoJSON";
            Console.WriteLine("url : {0}",url);

            Console.Write("Количество отправлений ===> ");
            int n = int.Parse(Console.ReadLine());

            Random rnd = new Random();

            for (int i = 0; i <= n; i++)
            {
            GEO testgeo = new GEO();


            testgeo.nameID = "test00";
            testgeo.geonamedevice = "dev00";
            //testgeo.X=78.12;
            testgeo.X = rnd.Next(99);
            //testgeo.Y = 980.12;
            testgeo.Y = rnd.Next(99);

            //MemoryStream stream1 = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(GEO));
            //ser.WriteObject(stream1, testgeo);

            string json = "{"
                          + "\""
                          +"nameID"
                          + "\""
                          +": "
                          + "\""
                          + testgeo.nameID
                          + "\""
                          + ","
                          + "\""
                          + "geonamedevice"
                          + "\""
                          + ": "                          
                          + "\""
                          + testgeo.geonamedevice
                          + "\""
                          + ","
                          + "\""
                          + "X"
                          + "\""
                          + ": "
                          + "\""
                          + Convert.ToString(testgeo.X)
                          + "\""
                          + ","
                          + "\""
                          + "Y"
                          + "\""
                          + ": "
                          + "\""
                          + Convert.ToString(testgeo.Y)
                          + "\""
                          + "}";

            Console.WriteLine("JSON: {0}",json);

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var requestStream = httpRequest.GetRequestStream())
            using (var writer = new StreamWriter(requestStream))
            {
                writer.Write(json);
            }
            using (var httpResponse = httpRequest.GetResponse())
            using (var responseStream = httpResponse.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            {
                string response = reader.ReadToEnd();
            }

            }
            Console.WriteLine("===>OK<===");
            Console.ReadLine();
        }
    }
}
