using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.FileSystem.Abstractions
{
    public interface INativeItemWrapper<T>
    {
        T NativeItem { get; }
    }
}
