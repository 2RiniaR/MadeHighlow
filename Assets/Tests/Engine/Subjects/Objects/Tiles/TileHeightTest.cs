using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class TileHeightTest
    {
        #region Tower

        [Test]
        public void Tower_Always_ReturnsCounterpartType()
        {
            var actual = Elevation.Tower;

            Assert.That(actual.Type, Is.EqualTo(TileHeightType.Tower));
        }

        [Test]
        public void Tower_Always_ReturnsNoPlaceable()
        {
            var actual = Elevation.Tower;

            Assert.That(actual.Placeable, Is.False);
        }

        #endregion

        #region Abyss

        [Test]
        public void Abyss_Always_ReturnsNoPlaceable()
        {
            var actual = Elevation.Abyss;

            Assert.That(actual.Type, Is.EqualTo(TileHeightType.Abyss));
        }

        [Test]
        public void Abyss_Always_ReturnsCounterpartType()
        {
            var actual = Elevation.Abyss;

            Assert.That(actual.Placeable, Is.False);
        }

        #endregion
    }
}