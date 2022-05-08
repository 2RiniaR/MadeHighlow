using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class Vector3DTest
    {
        #region Zero

        [Test]
        public void Zero_Always_ReturnsAllZero()
        {
            var actual = Vector3D.Zero;

            Assert.That(actual, Is.EqualTo(new Vector3D(0, 0, 0)));
        }

        #endregion

        #region XPositive

        [Test]
        public void XPositive_Always_ReturnsUnit()
        {
            var actual = Vector3D.XPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D(1, 0, 0)));
        }

        #endregion

        #region XNegative

        [Test]
        public void XNegative_Always_ReturnsUnit()
        {
            var actual = Vector3D.XNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D(-1, 0, 0)));
        }

        #endregion

        #region YPositive

        [Test]
        public void YPositive_Always_ReturnsUnit()
        {
            var actual = Vector3D.YPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D(0, 1, 0)));
        }

        #endregion

        #region YNegative

        [Test]
        public void YNegative_Always_ReturnsUnit()
        {
            var actual = Vector3D.YNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D(0, -1, 0)));
        }

        #endregion

        #region ZPositive

        [Test]
        public void ZPositive_Always_ReturnsUnit()
        {
            var actual = Vector3D.ZPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D(0, 0, 1)));
        }

        #endregion

        #region ZNegative

        [Test]
        public void ZNegative_Always_ReturnsUnit()
        {
            var actual = Vector3D.ZNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D(0, 0, -1)));
        }

        #endregion

        #region AddOperator

        [Test]
        public void AddOperator_Always_ReturnsAllAdded()
        {
            var vector1 = new Vector3D(1, 2, 1);
            var vector2 = new Vector3D(3, 4, 1);

            var actual = vector1 + vector2;

            Assert.That(actual, Is.EqualTo(new Vector3D(4, 6, 2)));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Always_ReturnsAllSubtracted()
        {
            var vector1 = new Vector3D(1, 2, 1);
            var vector2 = new Vector3D(3, 4, 1);

            var actual = vector1 - vector2;

            Assert.That(actual, Is.EqualTo(new Vector3D(-2, -2, 0)));
        }

        #endregion

        #region InverseOperator

        [Test]
        public void InverseOperator_Always_ReturnsAllInversed()
        {
            var vector = new Vector3D(1, 2, 1);

            var actual = -vector;

            Assert.That(actual, Is.EqualTo(new Vector3D(-1, -2, -1)));
        }

        #endregion

        #region MultipleOperator

        [Test]
        public void MultipleOperator_Always_ReturnsAllMultiple()
        {
            var vector = new Vector3D(1, 2, 1);
            var multiplier = 2;

            var actual = vector * multiplier;

            Assert.That(actual, Is.EqualTo(new Vector3D(2, 4, 2)));
        }

        #endregion

        #region ExtendTo

        [Test]
        public void ExtendTo_Always_ReturnsExtended()
        {
            var vector = new Vector3D(1, 2, 1);
            var direction = Direction3D.XPositive;
            var distance = new Distance(3);

            var actual = vector.ExtendTo(direction, distance);

            Assert.That(actual, Is.EqualTo(new Vector3D(4, 2, 1)));
        }

        #endregion
    }
}
