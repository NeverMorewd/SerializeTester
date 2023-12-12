using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeTester
{
    [ProtoContract]
    public class Organization
    {
        [ProtoMember(1)] public int Id { get; set; }

        [ProtoMember(2)] public string[]? Tags { get; set; }

        [ProtoMember(3)] public List<Member>? Members { get; set; }
    }

    [ProtoContract]
    public class Member
    {
        [ProtoMember(1)] public int Id { get; set; }

        [ProtoMember(2)] public string? Name { get; set; }

        [ProtoMember(3)] public string? Description { get; set; }

        [ProtoMember(4)] public string? Address { get; set; }

        [ProtoMember(5)] public double Value { get; set; }

        [ProtoMember(6)] public long UpdateTime { get; set; }
    }


}
