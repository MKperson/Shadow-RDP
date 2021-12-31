
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace ShadowClassLibrary
{

    public class ProxMox
    {
        public ProxMoxTicket Ticket = null;
        private static string BaseUrl = @"https://10.70.1.1:8006/api2/json";
        public ProxMox(string username, string password)
        {
            Ticket = GetTicket(username, password);
        }

        public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public bool Login
        {
            get
            {
                if (DateTime.Now < Ticket.Timeout)
                {
                    return true;
                }
                return false;
            }
        }
        private ProxMoxTicket GetTicket(string username, string password)
        {
            
            string URL = BaseUrl + "/access/ticket";

            HttpWebRequest Req = (HttpWebRequest)HttpWebRequest.Create(URL);
            Req.KeepAlive = true;
            Req.Method = "POST";
            Req.ContentType = "application/x-www-form-urlencoded";
            Req.Referer = URL;
            Req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
            Req.Timeout = 30000;
            Req.Accept = "text/json";
            Req.ServerCertificateValidationCallback = ValidateCertificate;

            string PostData = "username=" + username + "@pam&password=" + password;
            byte[] BA = Encoding.UTF8.GetBytes(PostData);

            Req.ContentLength = BA.Length;

            try
            {
                using (Stream PStrm = Req.GetRequestStream())
                {
                    PStrm.Write(BA, 0, BA.Length);
                    PStrm.Close();

                    using (HttpWebResponse Resp = (HttpWebResponse)Req.GetResponse())
                    {
                        using (Stream RStrm = Resp.GetResponseStream())
                        {
                            using (StreamReader SR = new StreamReader(RStrm))
                            {
                                string JSON = SR.ReadToEnd();

                                using (MemoryStream MemStr = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
                                {
                                    DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(ProxMoxTicket));
                                    Ticket = (ProxMoxTicket)Ser.ReadObject(MemStr);
                                    Ticket.Timeout = DateTime.Now.AddHours(2);
                                    Ser = null/* TODO Change to default(_) if this is not a reference type */;

                                    MemStr.Close();
                                }

                                SR.Close();
                            }
                            RStrm.Close();
                        }
                        Resp.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // could not retrieve new ticket
                if (Ticket == null)
                    Ticket = new ProxMoxTicket();
                Ticket.Error = true;
                Ticket.Message = ex.Message;
            }
            Req = null;
            return Ticket;
        }

        public static HttpWebResponse RestRequest(string method, string url, ProxMoxTicket ticket)
        {
            HttpWebResponse Response = null;
            CookieContainer Cookies = new CookieContainer();
            Cookies.Add(new Cookie() { Name = "PVEAuthCookie", Value = ticket.data.ticket, Domain = "10.70.1.1" });

            ServicePointManager.Expect100Continue = false;

            HttpWebRequest Req = null;
            try
            {
                Req = (HttpWebRequest)HttpWebRequest.Create(url);
                Req.KeepAlive = true;
                Req.Method = method;
                Req.ContentType = "application/x-www-form-urlencoded";
                Req.Referer = url;
                Req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11";
                Req.Timeout = 30000;
                Req.Accept = "text/json";
                Req.CookieContainer = Cookies;
                Req.Headers.Add("CSRFPreventionToken", ticket.data.CSRFPreventionToken);
                Req.ServerCertificateValidationCallback = ValidateCertificate;
                Response = (HttpWebResponse)Req.GetResponse();
                
                
            }
            catch (Exception ex)
            {
            }

            Req = null;
            Cookies = null;
            return Response;
        }

        public NodesResponse GetNodes()
        {
            string APIUrl = BaseUrl + "/nodes";
            NodesResponse NodeList = null;

            try
            {
                using (HttpWebResponse Response = RestRequest("GET", APIUrl, Ticket))
                {
                    using (Stream RespStrm = Response.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStrm))
                        {
                            string JSON = SR.ReadToEnd();

                            using (MemoryStream Mem = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
                            {
                                DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(NodesResponse));
                                NodeList = (NodesResponse)Ser.ReadObject(Mem);
                                Ser = null;

                                Mem.Close();
                            }
                            SR.Close();
                        }
                        RespStrm.Close();
                    }
                    Response.Close();
                }
                return NodeList;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public VMsResponse GetQemu(NodeData Node)
        {
            string APIUrl = BaseUrl + "/nodes/" + Node.node + "/qemu";
            VMsResponse VMs = null;

            try
            {
                using (HttpWebResponse Response = RestRequest("GET", APIUrl, Ticket))
                {
                    using (Stream RespStrm = Response.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStrm))
                        {
                            string JSON = SR.ReadToEnd();

                            using (MemoryStream Mem = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
                            {
                                DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(VMsResponse));
                                VMs = (VMsResponse)Ser.ReadObject(Mem);
                                Ser = null;

                                Mem.Close();
                            }

                            SR.Close();
                        }

                        RespStrm.Close();
                    }

                    Response.Close();
                }
                return VMs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public GetUsers GetUsers(NodeData Node, VMData VM)
        {
            string APIUrl = BaseUrl + "/nodes/" + Node.node + "/qemu/" + VM.vmid + "/agent/get-users";
            GetUsers Users = null;

            try
            {
                using (HttpWebResponse Response = RestRequest("GET", APIUrl, Ticket))
                {
                    using (Stream RespStrm = Response.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStrm))
                        {
                            string JSON = SR.ReadToEnd();

                            using (MemoryStream Mem = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
                            {
                                DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(GetUsers));

                                Users = (GetUsers)Ser.ReadObject(Mem);
                                Ser = null;

                                Mem.Close();
                            }

                            SR.Close();
                        }

                        RespStrm.Close();
                    }

                    Response.Close();
                }
                return Users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// CommandVM sends a command to a VM
        /// </summary>
        /// <param name="node">Node Name Ex:node01</param>
        /// <param name="vmid">VM ID</param>
        /// <param name="command">Command List:"current","reboot","reset","resume","shutdown","start","stop","suspend"</param>
        /// <returns>Success</returns>
        public bool CommandVM(string node, string vmid, string command)
        {
            bool Success = false;
            string APIUrl = BaseUrl + "/nodes/[NODE]/qemu/[VMID]/status/" + command;

            APIUrl = APIUrl.Replace("[NODE]", node).Replace("[VMID]", vmid);

            try
            {
                using (HttpWebResponse Resp = RestRequest("POST", APIUrl, Ticket))
                {
                    using (Stream RespStrm = Resp.GetResponseStream())
                    {
                        using (StreamReader SR = new StreamReader(RespStrm))
                        {
                            string JSON = SR.ReadToEnd();

                            using (MemoryStream Mem = new MemoryStream(Encoding.UTF8.GetBytes(JSON)))
                            {
                                DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(VMActionResponse));

                                VMActionResponse VMResponse = (VMActionResponse)Ser.ReadObject(Mem);

                                if (!string.IsNullOrEmpty(VMResponse.data))
                                    Success = true;

                                Mem.Close();
                            }

                            SR.Close();
                        }
                        RespStrm.Close();
                    }
                    Resp.Close();
                }

            }
            catch (Exception ex)
            {
            }

            return Success;
        }

        
    }
}

