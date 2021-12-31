using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rijndael256;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShadowClassLibrary
{


    public class GuacAPI
    {
        private string GuacCipher = Properties.Settings.Default.guacCipher;
        private string key = Properties.Settings.Default.key;

        private string UserName = "Stephen";
        private string Password = "";

        public GuacAPI()
        {
            Password = Rijndael.Decrypt(GuacCipher, key, KeySize.Aes256);
        }

        [DataContract]
        public class GuacAuth
        {
            [DataMember] public string authToken { get; set; }
            [DataMember] public string username { get; set; }
            [DataMember] public string dataSource { get; set; }
            [DataMember] public List<string> availableDataSources { get; set; }

            public GuacAuth()
            {
                //Instantiator
            }
        }

        public List<string> GetConnections()
        {
            List<string> ConnectionNames = new List<string>();

            //Log In to Guacamole
            string URL = "https://tms-remote.systech.services/api/tokens?username=" + UserName + "&password=" + Password;

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(URL);
            WebReq.Method = "POST";
            WebReq.ContentType = "application/x-www-form-urlencoded";
            WebReq.Accept = "*/*";
            //WebReq.Referer = URL;
            WebReq.KeepAlive = true;
            WebReq.ContentLength = 0;

            GuacAuth AuthInfo = null;
            using(Stream ReqStream = WebReq.GetRequestStream())
            {
                byte[] BA = { };
                ReqStream.Write(BA, 0, BA.Length);
                ReqStream.Close();

                using (HttpWebResponse Resp = (HttpWebResponse)WebReq.GetResponse())
                {
                    using (Stream RespStream = Resp.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStream))
                        {
                            string RawJSON = SR.ReadToEnd();

                            using (MemoryStream Mem = new MemoryStream(Encoding.UTF8.GetBytes(RawJSON)))
                            {
                                DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(GuacAuth));
                                AuthInfo = (GuacAuth)Ser.ReadObject(Mem);
                                Ser = null;

                                Mem.Close();
                            }

                            SR.Close();
                        }
                    }
                    Resp.Close();
                }
            }
            

            if(AuthInfo != null)
            {
                //Guac Auth Info Received.  Now get the list of connections

                URL = "https://tms-remote.systech.services/api/session/data/" + AuthInfo.dataSource + "/connections?token=" + AuthInfo.authToken;

                WebReq = (HttpWebRequest)WebRequest.Create(URL);
                WebReq.Method = "GET";
                WebReq.Referer = URL;
                WebReq.KeepAlive = true;

                dynamic ConnectionList = null;
                using (HttpWebResponse Resp = (HttpWebResponse)WebReq.GetResponse())
                {
                    using (Stream RespStream = Resp.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStream))
                        {
                            string RawJSON = SR.ReadToEnd();

                            ConnectionList = JsonConvert.DeserializeObject(RawJSON);
                        }
                    }
                }

                if(ConnectionList != null)
                {
                    foreach(var Conn in ConnectionList){
                        ConnectionNames.Add(Conn.Name);
                    }
                }
            }

            return ConnectionNames;
        }
    }
}
