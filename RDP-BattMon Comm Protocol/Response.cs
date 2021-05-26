using System;
using System.Collections.Generic;
using FieldEffect.VCL.CommunicationProtocol.Exceptions;
using FieldEffect.VCL.CommunicationProtocol.Helpers;

namespace FieldEffect.VCL.CommunicationProtocol
{
    [Serializable]
    public class Response : IResponse
    {
        public Dictionary<string, object> Value { get; private set; }

        public Response()
        {
            Value = new Dictionary<string, object>();
        }

        public string Serialize()
        {
            return Serialization.Serialize(this) + '\0';
        }

        public static Response Deserialize(string value)
        {
            if (!value.EndsWith("\0"))
                throw new BattMonCommunicationException("Response text was garbled");

            value = value[..^1];

            return Serialization.Deserialize<Response>(value);
        }
    }
}
