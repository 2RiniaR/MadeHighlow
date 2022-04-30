using System;
using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class Direction2DTest
    {
        #region FromVector

        [Test]
        public void FromVector_XPositiveVector_ReturnsXPositive()
        {
            var vector = Vector2D.XPositive;

            var actual = Direction2D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction2D.XPositive));
        }

        [Test]
        public void FromVector_XNegativeVector_ReturnsXNegative()
        {
            var vector = Vector2D.XNegative;

            var actual = Direction2D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction2D.XNegative));
        }

        [Test]
        public void FromVector_YPositiveVector_ReturnsYPositive()
        {
            var vector = Vector2D.YPositive;

            var actual = Direction2D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction2D.YPositive));
        }

        [Test]
        public void FromVector_YNegativeVector_ReturnsYNegative()
        {
            var vector = Vector2D.YNegative;

            var actual = Direction2D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction2D.YNegative));
        }

        [Test]
        public void FromVector_InvalidVector_ThrowsArgumentException()
        {
            Assert.That(
                () => Direction2D.FromVector(new Vector2D { X = 1, Y = 1 }),
                Throws.TypeOf<ArgumentException>()
            );
        }

        #endregion

        #region ToVector

        [Test]
        public void ToVector_XPositive_ReturnsXPositiveVector()
        {
            var direction = Direction2D.XPositive;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector2D.XPositive));
        }

        [Test]
        public void ToVector_XNegative_ReturnsXNegativeVector()
        {
            var direction = Direction2D.XNegative;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector2D.XNegative));
        }

        [Test]
        public void ToVector_YPositive_ReturnsYPositiveVector()
        {
            var direction = Direction2D.YPositive;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector2D.YPositive));
        }

        [Test]
        public void ToVector_YNegative_ReturnsYNegativeVector()
        {
            var direction = Direction2D.YNegative;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector2D.YNegative));
        }

        #endregion

        #region Backward

        [Test]
        public void Backward_XPositive_ReturnsXNegative()
        {
            var direction = Direction2D.XPositive;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction2D.XNegative));
        }

        [Test]
        public void Backward_XNegative_ReturnsXPositive()
        {
            var direction = Direction2D.XNegative;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction2D.XPositive));
        }

        [Test]
        public void Backward_YPositive_ReturnsYNegative()
        {
            var direction = Direction2D.YPositive;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction2D.YNegative));
        }

        [Test]
        public void Backward_YNegative_ReturnsYPositive()
        {
            var direction = Direction2D.YNegative;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction2D.YPositive));
        }

        #endregion

        #region RightSide

        [Test]
        public void RightSide_XPositive_ReturnsYNegative()
        {
            var direction = Direction2D.XPositive;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction2D.YNegative));
        }

        [Test]
        public void RightSide_YNegative_ReturnsXNegative()
        {
            var direction = Direction2D.YNegative;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction2D.XNegative));
        }

        [Test]
        public void RightSide_XNegative_ReturnsYPositive()
        {
            var direction = Direction2D.XNegative;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction2D.YPositive));
        }

        [Test]
        public void RightSide_YPositive_ReturnsXPositive()
        {
            var direction = Direction2D.YPositive;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction2D.XPositive));
        }

        #endregion

        #region LeftSide

        [Test]
        public void LeftSide_XPositive_ReturnsYPositive()
        {
            var direction = Direction2D.XPositive;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction2D.YPositive));
        }

        [Test]
        public void LeftSide_YPositive_ReturnsXNegative()
        {
            var direction = Direction2D.YPositive;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction2D.XNegative));
        }

        [Test]
        public void LeftSide_XNegative_ReturnsYNegative()
        {
            var direction = Direction2D.XNegative;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction2D.YNegative));
        }

        [Test]
        public void LeftSide_YNegative_ReturnsXPositive()
        {
            var direction = Direction2D.YNegative;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction2D.XPositive));
        }

        #endregion
    }
}