﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.RabbitMQ.Serializers
{
    public sealed class NewtonsoftJsonRabbitMqSerializer : IRabbitMqSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonsoftJsonRabbitMqSerializer(JsonSerializerSettings settings = null)
        {
            _settings = settings ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public ReadOnlySpan<byte> Serialize(object value) => Encode(JsonConvert.SerializeObject(value, _settings));

        public object Deserialize(ReadOnlySpan<byte> value, Type type) => JsonConvert.DeserializeObject(Decode(value), type, _settings);

        public object Deserialize(ReadOnlySpan<byte> value) => JsonConvert.DeserializeObject(Decode(value), _settings);

        private static ReadOnlySpan<byte> Encode(string value) => Encoding.UTF8.GetBytes(value);

        private static string Decode(ReadOnlySpan<byte> value) => Encoding.UTF8.GetString(value);
    }
}
