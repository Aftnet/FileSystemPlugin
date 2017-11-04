using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Filesystem.Abstractions
{
    public interface INativeItemWrapper<T>
    {
        T NativeItem { get; }
    }
}
