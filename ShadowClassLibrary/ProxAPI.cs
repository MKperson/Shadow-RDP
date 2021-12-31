using Rijndael256;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ShadowClassLibrary
{
    public class ProxAPI
    {

        private string proxCipher = Properties.Settings.Default.proxCipher;
        private string key = Properties.Settings.Default.key;

        ProxMox client;

        public ProxAPI()
        {
            client = new ProxMox("root", Rijndael.Decrypt(proxCipher, key, KeySize.Aes256));

        }



        public ListViewItem[] GetVms
        {
            get
            {
                List<ListViewItem> lv = new List<ListViewItem>();
                if (client.Login)
                {
                    NodesResponse nodesResp = client.GetNodes();

                    if (nodesResp != null && nodesResp.data != null)
                    {
                        List<NodeData> nodes = nodesResp.data;

                        foreach (NodeData node in nodes)
                        {
                            VMsResponse Resp = client.GetQemu(node);

                            if (Resp != null && Resp.data != null)
                            {
                                List<VMData> vms = Resp.data;
                                foreach (VMData vm in vms)
                                {
                                    if (int.Parse(vm.vmid) >= 600 && int.Parse(vm.vmid) <= 649)
                                    {
                                        TimeSpan timeSpan = TimeSpan.FromSeconds(vm.uptime == null ? 0 : (long)vm.uptime);
                                        string uptime = string.Format("{0:D2}d+{1:D2}h:{2:D2}m:{3:D2}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                                        ListViewItem lvi = new ListViewItem(new string[] { vm.name, vm.vmid, node.node, vm.status, uptime });
                                        lv.Add(lvi);
                                    }
                                }
                                vms.Clear();
                            }
                        }
                        nodes.Clear();
                    }
                }
                return lv.ToArray();
            }
        }

        public List<string> GetAActiveVms
        {
            get
            {
                List<string> lv = new List<string>();

                if (client.Login)
                {
                    NodesResponse nodesResponse = client.GetNodes();

                    if (nodesResponse != null && nodesResponse.data != null)
                    {
                        List<NodeData> nodes = nodesResponse.data;

                        foreach (NodeData node in nodes)
                        {
                            VMsResponse vMsResponse = client.GetQemu(node);

                            if (vMsResponse.data != null)
                            {
                                List<VMData> vms = vMsResponse.data;

                                foreach (VMData vm in vms)
                                {
                                    if (int.Parse(vm.vmid) >= 600 && int.Parse(vm.vmid) <= 649)
                                    {
                                        GetUsers getUsersResp = client.GetUsers(node, vm);

                                        if (getUsersResp != null)
                                        {
                                            List<GetUsersResult> agentlist = getUsersResp.data.result;   //Nodes[node.node].Qemu[vm.vmid].Agent.Get_Users.GetRest().Response.data;

                                            if (agentlist != null && agentlist.Count > 0)
                                            {
                                                lv.Add(vm.vmid);
                                            }
                                            agentlist.Clear();
                                        }
                                    }
                                }
                                vms.Clear();
                            }

                        }
                        nodes.Clear();
                    }
                }
                return lv;
            }
        }
        public bool Reboot(string node, string vmid)
        {
            bool Success = false;
            if (client.Login)
            {
                Success = client.CommandVM(node, vmid, "reboot");
                //client.Nodes[node].Qemu[vmid].Status.Reboot.VmReboot();
            }

            return Success;
        }
        public bool Start(string node, string vmid)
        {
            bool Success = false;
            if (client.Login)
            {
                Success = client.CommandVM(node, vmid, "start");
                
                //client.Nodes[node].Qemu[vmid].Status.Reboot.VmReboot();
            }

            return Success;
        }
        public bool ValidTicket
        {
            get
            { 
                bool result = client.Login;
                return result;
            }
        }
    }

}

