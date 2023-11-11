using NUnit.Framework;
using Unityroom.Internal;

namespace Unityroom.Tests.Editor
{
    public class TimeKeeperTest
    {
        [Test]
        public void TimeKeeperTestSimplePasses()
        {
            var timeKeeper = new TimeKeeper(3);
            Assert.False(timeKeeper.IsBusy(100));
            timeKeeper.Reset(110);
            Assert.True(timeKeeper.IsBusy(111));
            Assert.True(timeKeeper.IsBusy(112));
            Assert.False(timeKeeper.IsBusy(113));
            Assert.False(timeKeeper.IsBusy(114));
        }
    }
}