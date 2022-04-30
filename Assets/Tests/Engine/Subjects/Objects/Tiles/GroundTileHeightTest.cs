using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class GroundTileHeightTest
    {
        #region Constructor

        [Test]
        public void Constructor_Always_ReturnsCounterpartType()
        {
            var actual = new GroundTileHeight();

            Assert.That(actual.Type, Is.EqualTo(TileHeightType.Ground));
        }

        #endregion
    }
}