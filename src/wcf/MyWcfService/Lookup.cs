using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MyWcfService
{
    public class Lookup
    {
         [DataMember]
    public int Lookup_Id { get; set; }

    [DataMember]
    public int Lookup_Code { get; set; }

    [DataMember]
    public string Lookup_Type { get; set; }

    [DataMember]
    public string Lookup_Desc { get; set; }

    [DataMember]
    public string Is_Active { get; set; }
    }
}