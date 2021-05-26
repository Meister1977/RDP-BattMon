using System;
using System.Collections.Generic;
using FieldEffect.VCL.CommunicationProtocol.Exceptions;
using FieldEffect.VCL.CommunicationProtocol.Helpers;
using FieldEffect.VCL.CommunicationProtocol.Interfaces;

namespace FieldEffect.VCL.CommunicationProtocol
{
    [Serializable]
    public class Request : IRequest
    {
        public List<string> Value { get; private set; }

        public Request()
        {
            Value = new List<string>();
        }

        public string Serialize()
        {
            return Serialization.Serialize(this) + '\0';
        }

        public static Request Deserialize(string value)
        {
            if (!value.EndsWith("\0"))
                throw new BattMonCommunicationException("Request text was garbled");

            value = value[..^1];

            return Serialization.Deserialize<Request>(value);
        }
    }
}
