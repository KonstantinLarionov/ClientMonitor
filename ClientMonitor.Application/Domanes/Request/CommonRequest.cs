using ClientMonitor.Application.Domanes.Objects.Regru;

using JetBrains.Annotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace ClientMonitor.Application.Domanes.Request
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class CommonRequest
    {
        [NotNull]
        public abstract string EndPoint { get; }

        public abstract RequestMethod Method { get; }
        [CanBeNull]
        public virtual IDictionary<string, string> Properties => null;
    }
}
