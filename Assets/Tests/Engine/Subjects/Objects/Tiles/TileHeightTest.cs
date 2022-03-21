using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Objects.Tiles
{
    public class TileHeightTest
    {
        #region Tower

        [Test]
        public void Tower_Get_ReturnsThatTypeIsCounterpart()
        {
            var height = TileHeight.Tower;

            var actual = height.Type;

            Assert.That(actual, Is.EqualTo(TileHeightType.Tower));
        }

        [Test]
        public void Tower_Get_ReturnsThatPlaceableIsFalse()
        {
            var height = TileHeight.Tower;

            var actual = height.Placeable;

            Assert.That(actual, Is.False);
        }

        #endregion

        #region Abyss

        [Test]
        public void Abyss_Get_ReturnsThatPlaceableIsFalse()
        {
            var height = TileHeight.Abyss;

            var actual = height.Type;

            Assert.That(actual, Is.EqualTo(TileHeightType.Abyss));
        }

        [Test]
        public void Abyss_Get_ReturnsThatTypeIsCounterpart()
        {
            var height = TileHeight.Abyss;

            var actual = height.Placeable;

            Assert.That(actual, Is.False);
        }

        #endregion
    }
}