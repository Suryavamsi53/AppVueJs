using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MyWcfService
{
    [DataContract]
    public class Account
    {
    [DataMember]
    public int AccId { get; set; }

    [DataMember]
    public string AccountNumber { get; set; }

    [DataMember]
    public int AccountStatus_id { get; set; }
    }
}