using Confluent.Kafka;
using System.Text;
using Newtonsoft.Json;

namespace Carting.Infrastructure.Kafka.Services
{
    public class KafkaMessageDeserializer<TMessage> : IDeserializer<TMessage>
    {
        public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
                return default;

            var serializedMessage = Encoding.UTF8.GetString(data.ToArray());

            return JsonConvert.DeserializeObject<TMessage>(serializedMessage);
        }
    }
}

