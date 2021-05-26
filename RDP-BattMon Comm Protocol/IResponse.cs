using System.Collections.Generic;

namespace FieldEffect.VCL.CommunicationProtocol
{
    public interface IResponse
    {
        Dictionary<string, object> Value { get; }

        string Serialize();
    }
}