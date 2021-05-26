using System.Text.Json;

namespace FieldEffect.VCL.CommunicationProtocol.Helpers
{
    public static class Serialization
    {
        public static string Serialize(object serializableObject)
        {
            return JsonSerializer.Serialize(serializableObject);
        }

        public static TSerializableType Deserialize<TSerializableType>(string serializedObject)
        {
            return JsonSerializer.Deserialize<TSerializableType>(serializedObject);
        }
    }
}
