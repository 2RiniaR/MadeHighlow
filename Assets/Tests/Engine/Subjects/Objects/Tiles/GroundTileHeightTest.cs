using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles
{
    public class GroundTileHeightTest
    {
        #region Constructor

        [Test]
        public void Constructor_Get_ReturnsThatTypeIsCounterpart()
        {
            var height = new GroundTileHeight();

            var actual = height.Type;

            Assert.That(actual, Is.EqualTo(TileHeightType.Ground));
        }

        #endregion
    }
}