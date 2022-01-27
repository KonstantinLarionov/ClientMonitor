using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientMonitor.Application.Handler.JsonHandlers
{
    public class BaseJsonHandler<T> : CommonJsonHandler, IDataHandler<T>
    {
        public T HandleSingle(string message)
        {
            try
            {
                return Build(JToken.Parse(message));
            }
            catch (JsonException ex)
            {
                throw NewFormatException(message, ex);
            }
        }

        protected virtual T Build(JToken token)
        {
            using (var jsonReader = new JTokenReader(token))
            {
                return Serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
