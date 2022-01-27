using System;

using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ClientMonitor.Application.Handler.JsonHandlers
{
    public class CommonJsonHandler
    {
        [NotNull]
        protected static JsonSerializer Serializer { get; } = new JsonSerializer
        {
            Converters =
            {
                new StringEnumConverter()
            },
            //ContractResolver = new ConverterContractResolver()
        };

        [NotNull]
        protected FormatException NewFormatException([CanBeNull] string actualMessage) =>
            new FormatException($@"Unable to parse the message: {actualMessage}");

        [NotNull]
        protected FormatException NewFormatException([CanBeNull] string actualMessage, [NotNull] JsonException ex) =>
            new FormatException($@"Unable to parse the message: {actualMessage}", ex);
    }
}
