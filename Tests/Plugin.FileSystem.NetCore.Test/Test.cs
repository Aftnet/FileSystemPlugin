using Plugin.FileSystem.Test;

namespace Plugin.FileSystem.NetCore.Test
{
    public class Test : TestBase
    {
        public Test() : base(CrossFileSystem.Current)
        {
        }
    }
}
