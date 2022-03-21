using System;
using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class Direction3DTest
    {
        #region FromVector

        [Test]
        public void FromVector_XPositiveVector_ReturnsXPositive()
        {
            var vector = Vector3D.XPositive;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.XPositive));
        }

        [Test]
        public void FromVector_XNegativeVector_ReturnsXNegative()
        {
            var vector = Vector3D.XNegative;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.XNegative));
        }

        [Test]
        public void FromVector_YPositiveVector_ReturnsYPositive()
        {
            var vector = Vector3D.YPositive;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.YPositive));
        }

        [Test]
        public void FromVector_YNegativeVector_ReturnsYNegative()
        {
            var vector = Vector3D.YNegative;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.YNegative));
        }

        [Test]
        public void FromVector_ZPositiveVector_ReturnsZPositive()
        {
            var vector = Vector3D.ZPositive;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.ZPositive));
        }

        [Test]
        public void FromVector_ZNegativeVector_ReturnsZNegative()
        {
            var vector = Vector3D.ZNegative;

            var actual = Direction3D.FromVector(vector);

            Assert.That(actual, Is.EqualTo(Direction3D.ZNegative));
        }

        [Test]
        public void FromVector_InvalidVector_ThrowsArgumentException()
        {
            Assert.That(
                () => Direction3D.FromVector(new Vector3D { X = 1, Y = 1 }),
                Throws.TypeOf<ArgumentException>()
            );
        }

        #endregion

        #region ToVector

        [Test]
        public void ToVector_XPositive_ReturnsXPositiveVector()
        {
            var direction = Direction3D.XPositive;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.XPositive));
        }

        [Test]
        public void ToVector_XNegative_ReturnsXNegativeVector()
        {
            var direction = Direction3D.XNegative;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.XNegative));
        }

        [Test]
        public void ToVector_YPositive_ReturnsYPositiveVector()
        {
            var direction = Direction3D.YPositive;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.YPositive));
        }

        [Test]
        public void ToVector_YNegative_ReturnsYNegativeVector()
        {
            var direction = Direction3D.YNegative;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.YNegative));
        }

        [Test]
        public void ToVector_ZPositive_ReturnsZPositiveVector()
        {
            var direction = Direction3D.ZPositive;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.ZPositive));
        }

        [Test]
        public void ToVector_ZNegative_ReturnsZNegativeVector()
        {
            var direction = Direction3D.ZNegative;

            var actual = direction.ToVector();

            Assert.That(actual, Is.EqualTo(Vector3D.ZNegative));
        }

        #endregion

        #region Backward

        [Test]
        public void Backward_XPositive_ReturnsXNegative()
        {
            var direction = Direction3D.XPositive;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.XNegative));
        }

        [Test]
        public void Backward_XNegative_ReturnsXPositive()
        {
            var direction = Direction3D.XNegative;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.XPositive));
        }

        [Test]
        public void Backward_YPositive_ReturnsYNegative()
        {
            var direction = Direction3D.YPositive;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.YNegative));
        }

        [Test]
        public void Backward_YNegative_ReturnsYPositive()
        {
            var direction = Direction3D.YNegative;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.YPositive));
        }

        [Test]
        public void Backward_ZPositive_ReturnsZPositive()
        {
            var direction = Direction3D.ZPositive;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.ZPositive));
        }

        [Test]
        public void Backward_ZNegative_ReturnsZNegative()
        {
            var direction = Direction3D.ZNegative;

            var actual = direction.Backward;

            Assert.That(actual, Is.EqualTo(Direction3D.ZNegative));
        }

        #endregion

        #region Inverse

        [Test]
        public void Inverse_XPositive_ReturnsXNegative()
        {
            var direction = Direction3D.XPositive;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.XNegative));
        }

        [Test]
        public void Inverse_XNegative_ReturnsXPositive()
        {
            var direction = Direction3D.XNegative;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.XPositive));
        }

        [Test]
        public void Inverse_YPositive_ReturnsYNegative()
        {
            var direction = Direction3D.YPositive;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.YNegative));
        }

        [Test]
        public void Inverse_YNegative_ReturnsYPositive()
        {
            var direction = Direction3D.YNegative;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.YPositive));
        }

        [Test]
        public void Inverse_ZPositive_ReturnsZNegative()
        {
            var direction = Direction3D.ZPositive;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.ZNegative));
        }

        [Test]
        public void Inverse_ZNegative_ReturnsZPositive()
        {
            var direction = Direction3D.ZNegative;

            var actual = direction.Inverse;

            Assert.That(actual, Is.EqualTo(Direction3D.ZPositive));
        }

        #endregion

        #region RightSide

        [Test]
        public void RightSide_XPositive_ReturnsYNegative()
        {
            var direction = Direction3D.XPositive;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.YNegative));
        }

        [Test]
        public void RightSide_YNegative_ReturnsXNegative()
        {
            var direction = Direction3D.YNegative;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.XNegative));
        }

        [Test]
        public void RightSide_XNegative_ReturnsYPositive()
        {
            var direction = Direction3D.XNegative;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.YPositive));
        }

        [Test]
        public void RightSide_YPositive_ReturnsXPositive()
        {
            var direction = Direction3D.YPositive;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.XPositive));
        }

        [Test]
        public void RightSide_ZPositive_ReturnsZPositive()
        {
            var direction = Direction3D.ZPositive;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.ZPositive));
        }

        [Test]
        public void RightSide_ZNegative_ReturnsZNegative()
        {
            var direction = Direction3D.ZNegative;

            var actual = direction.RightSide;

            Assert.That(actual, Is.EqualTo(Direction3D.ZNegative));
        }

        #endregion

        #region LeftSide

        [Test]
        public void LeftSide_XPositive_ReturnsYPositive()
        {
            var direction = Direction3D.XPositive;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.YPositive));
        }

        [Test]
        public void LeftSide_YPositive_ReturnsXNegative()
        {
            var direction = Direction3D.YPositive;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.XNegative));
        }

        [Test]
        public void LeftSide_XNegative_ReturnsYNegative()
        {
            var direction = Direction3D.XNegative;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.YNegative));
        }

        [Test]
        public void LeftSide_YNegative_ReturnsXPositive()
        {
            var direction = Direction3D.YNegative;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.XPositive));
        }

        [Test]
        public void LeftSide_ZPositive_ReturnsZPositive()
        {
            var direction = Direction3D.ZPositive;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.ZPositive));
        }

        [Test]
        public void LeftSide_ZNegative_ReturnsZNegative()
        {
            var direction = Direction3D.ZNegative;

            var actual = direction.LeftSide;

            Assert.That(actual, Is.EqualTo(Direction3D.ZNegative));
        }

        #endregion
    }
}