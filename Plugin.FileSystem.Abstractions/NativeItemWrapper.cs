using System;

namespace Plugin.FileSystem.Abstractions
{
    public abstract class NativeItemWrapper<T> : INativeItemWrapper<T> where T : class
    {
        private readonly T nativeItem;

        public T NativeItem => nativeItem;

        public NativeItemWrapper(T nativeItem)
        {
            this.nativeItem = nativeItem ?? throw new ArgumentNullException();
        }
    }
}
