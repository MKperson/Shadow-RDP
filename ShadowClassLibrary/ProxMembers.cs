
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ShadowClassLibrary
{

    [DataContract]
    public class ProxMoxTicket
    {
        [DataMember]
        public TicketData data { get; set; } = new TicketData();

        public System.DateTime Timeout;
        public bool Error;
        public string Message;
    }

    [DataContract]
    public class TicketData
    {


        [DataMember]
        public string ticket { get; set; } = "";
        [DataMember]
        public string CSRFPreventionToken { get; set; } = "";
        [DataMember]
        public string username { get; set; } = "";
        [DataMember]
        public string clustername { get; set; } = "";
        [DataMember]
        public TicketCap cap { get; set; } = new TicketCap();

    }

    [DataContract]
    public class TicketCap
    {

        [DataMember]
        public TicketVMs vms { get; set; } = new TicketVMs();
        [DataMember]
        public TicketNodes nodes { get; set; } = new TicketNodes();
        [DataMember]
        public TicketStorage storage { get; set; } = new TicketStorage();
        [DataMember]
        public TicketDC dc { get; set; } = new TicketDC();
        [DataMember]
        public TicketAccess access { get; set; } = new TicketAccess();


    }

    [DataContract]
    public class TicketVMs
    {


        [DataMember(Name = "VM.Config.Options")]
        public int? ConfigOptions { get; set; }
        [DataMember(Name = "VM.Clone")]
        public int? Clone { get; set; }
        [DataMember(Name = "VM.Config.Disk")]
        public int? ConfigDisk { get; set; }
        [DataMember(Name = "VM.Snapshot.Rollback")]
        public int? SnapshotRollback { get; set; }
        [DataMember(Name = "VM.Snapshot")]
        public int? Snapshot { get; set; }
        [DataMember(Name = "VM.Audit")]
        public int? Audit { get; set; }
        [DataMember(Name = "VM.Migrate")]
        public int? Migrate { get; set; }
        [DataMember(Name = "VM.Config.CPU")]
        public int? ConfigCPU { get; set; }
        [DataMember(Name = "VM.PowerMgmt")]
        public int? PowerMgmt { get; set; }
        [DataMember(Name = "VM.Config.HWType")]
        public int? ConfigHWType { get; set; }
        [DataMember(Name = "VM.Monitor")]
        public int? Monitor { get; set; }
        [DataMember(Name = "VM.Config.Memory")]
        public int? ConfigMemory { get; set; }
        [DataMember(Name = "VM.Allocate")]
        public int? Allocate { get; set; }
        [DataMember(Name = "VM.Config.CDROM")]
        public int? ConfigCDROM { get; set; }
        [DataMember(Name = "VM.Config.Network")]
        public int? ConfigNetwork { get; set; }
        [DataMember(Name = "Permissions.Modify")]
        public int? PermissionsModify { get; set; }
        [DataMember(Name = "VM.Backup")]
        public int? Backup { get; set; }
        [DataMember(Name = "VM.Console")]
        public int? Console { get; set; }


    }

    [DataContract]
    public class TicketNodes
    {


        [DataMember(Name = "Permissions.Modify")]
        public int? PermissionsModify { get; set; }
        [DataMember(Name = "Sys.Audit")]
        public int? SysAudit { get; set; }
        [DataMember(Name = "Sys.PowerMgmt")]
        public int? SysPowerMgmt { get; set; }
        [DataMember(Name = "Sys.Console")]
        public int? SysConsole { get; set; }
        [DataMember(Name = "Sys.Modify")]
        public int? SysModify { get; set; }
        [DataMember(Name = "Sys.Syslog")]
        public int? SysSyslog { get; set; }


    }

    [DataContract]
    public class TicketStorage
    {
        [DataMember(Name = "Permissions.Modify")]
        public int? PermissionsModify { get; set; }
        [DataMember(Name = "Datastore.Audit")]
        public int? DatastoreAudit { get; set; }
        [DataMember(Name = "Datastore.AllocateTemplate")]
        public int? DatastoreAllocateTemplate { get; set; }
        [DataMember(Name = "Datastore.AllocateSpace")]
        public int? DatastoreAllocateSpace { get; set; }
        [DataMember(Name = "Datastore.Allocate")]
        public int? DatastoreAllocate { get; set; }
    }

    [DataContract]
    public class TicketDC
    {
        [DataMember(Name = "Sys.Audit")]
        public int? SysAudit { get; set; }
    }

    [DataContract]
    public class TicketAccess
    {


        [DataMember(Name = "Permissions.Modify")]
        public int? PermissionsModify { get; set; }
        [DataMember(Name = "Group.Allocate")]
        public int? GroupAllocate { get; set; }
        [DataMember(Name = "User.Modify")]
        public int? UserModify { get; set; }


    }

    [DataContract]
    public class NodesResponse
    {


        [DataMember]
        public List<NodeData> data { get; set; } = new List<NodeData>();


    }

    [DataContract]
    public class NodeData
    {

        [DataMember]
        public string level { get; set; } = "";
        [DataMember]
        public long? maxmem { get; set; } = 0;
        [DataMember]
        public long? mem { get; set; } = 0;
        [DataMember]
        public string node { get; set; } = "";
        [DataMember]
        public long? uptime { get; set; } = 0;
        [DataMember]
        public long? disk { get; set; } = 0;
        [DataMember]
        public string ssl_fingerprint { get; set; } = "";
        [DataMember]
        public string id { get; set; } = "";
        [DataMember]
        public long? maxdisk { get; set; } = 0;
        [DataMember]
        public double? cpu { get; set; } = 0;
        [DataMember]
        public string status { get; set; } = "";


    }

    [DataContract]

    public class VMsResponse
    {


        [DataMember]
        public List<VMData> data { get; set; } = new List<VMData>();


    }

    [DataContract]
    public class VMData
    {


        [DataMember]
        public decimal? cpu { get; set; }
        [DataMember]
        public string name { get; set; } = "";
        [DataMember]
        public string status { get; set; } = "";
        [DataMember]
        public long? netout { get; set; }
        [DataMember]
        public long? mem { get; set; }
        [DataMember]
        public string template { get; set; } = "";
        [DataMember]
        public long? uptime { get; set; }
        [DataMember]
        public long? disk { get; set; }
        [DataMember]
        public long? diskwrite { get; set; }
        [DataMember]
        public string pid { get; set; } = "";
        [DataMember]
        public long? maxdisk { get; set; }
        [DataMember]
        public long? maxmem { get; set; }
        [DataMember]
        public long? diskread { get; set; }
        [DataMember]
        public string vmid { get; set; } = "";
        [DataMember]
        public long? netin { get; set; }
        [DataMember]
        public int? cpus { get; set; }


    }

    [DataContract]
    public class VMActionResponse
    {
        [DataMember]
        public string data = "";
    }

    [DataContract]
    public class GetActiveAgentsResponse
    {


        [DataMember]
        public List<RemoteRecord> ActiveUsers { get; set; } = new List<RemoteRecord>();


    }

    [DataContract]
    public class RemoteRecord
    {


        [DataMember]
        public string ECSUser { get; set; }
        [DataMember]
        public string Workstation { get; set; }
        [DataMember]
        public string IP { get; set; }


    }

    [DataContract]
    public class GetUsersData
    {


        [DataMember(Name = "result")]
        public List<GetUsersResult> result { get; set; } = new List<GetUsersResult>();


    }
    [DataContract]
    public class GetUsersResult
    {


        [DataMember(Name = "domain")]
        public string domain { get; set; } = "";
        [DataMember(Name = "user")]
        public string user { get; set; } = "";
        [DataMember(Name = "login-time")]
        public double? login_time { get; set; }


    }

    [DataContract]
    public class GetUsers
    {


        [DataMember(Name = "data")]
        public GetUsersData data { get; set; } = new GetUsersData();


    }


}
