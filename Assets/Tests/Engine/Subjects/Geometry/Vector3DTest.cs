using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class Vector3DTest
    {
        #region Zero

        [Test]
        public void Zero_Get_ReturnsThatAllAxesAreZero()
        {
            var actual = Vector3D.Zero;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 0, Y = 0, Z = 0 }));
        }

        #endregion

        #region XPositive

        [Test]
        public void XPositive_Get_ReturnsThatXIsPositiveUnitAndOthersAreZero()
        {
            var actual = Vector3D.XPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 1, Y = 0, Z = 0 }));
        }

        #endregion

        #region XNegative

        [Test]
        public void XNegative_Get_ReturnsThatXIsNegativeUnitAndOthersAreZero()
        {
            var actual = Vector3D.XNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = -1, Y = 0, Z = 0 }));
        }

        #endregion

        #region YPositive

        [Test]
        public void YPositive_Get_ReturnsThatYIsPositiveUnitAndOthersAreZero()
        {
            var actual = Vector3D.YPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 0, Y = 1, Z = 0 }));
        }

        #endregion

        #region YNegative

        [Test]
        public void YNegative_Get_ReturnsThatYIsNegativeUnitAndOthersAreZero()
        {
            var actual = Vector3D.YNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 0, Y = -1, Z = 0 }));
        }

        #endregion

        #region ZPositive

        [Test]
        public void ZPositive_Get_ReturnsThatZIsPositiveUnitAndOthersAreZero()
        {
            var actual = Vector3D.ZPositive;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 0, Y = 0, Z = 1 }));
        }

        #endregion

        #region ZNegative

        [Test]
        public void ZNegative_Get_ReturnsThatZIsNegativeUnitAndOthersAreZero()
        {
            var actual = Vector3D.ZNegative;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 0, Y = 0, Z = -1 }));
        }

        #endregion

        #region AddOperator

        [Test]
        public void AddOperator_Vector3D_ReturnsThatAllAxesAreAddedValue()
        {
            var vector1 = new Vector3D { X = 1, Y = 2, Z = 1 };
            var vector2 = new Vector3D { X = 3, Y = 4, Z = 1 };

            var actual = vector1 + vector2;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 4, Y = 6, Z = 2 }));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Vector3D_ReturnsThatAllAxesAreSubtractedValue()
        {
            var vector1 = new Vector3D { X = 1, Y = 2, Z = 1 };
            var vector2 = new Vector3D { X = 3, Y = 4, Z = 1 };

            var actual = vector1 - vector2;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = -2, Y = -2, Z = 0 }));
        }

        #endregion

        #region InverseOperator

        [Test]
        public void InverseOperator_Vector3D_ReturnsThatAllAxesAreInversedValue()
        {
            var vector = new Vector3D { X = 1, Y = 2, Z = 1 };

            var actual = -vector;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = -1, Y = -2, Z = -1 }));
        }

        #endregion

        #region MultipleOperator

        [Test]
        public void MultipleOperator_Int_ReturnsThatAllAxesAreMultipleValue()
        {
            var vector = new Vector3D { X = 1, Y = 2, Z = 1 };
            var multiplier = 2;

            var actual = vector * multiplier;

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 2, Y = 4, Z = 2 }));
        }

        #endregion

        #region ExtendTo

        [Test]
        public void ExtendTo_Values_ReturnsThatExtendedVector()
        {
            var vector = new Vector3D { X = 1, Y = 2, Z = 1 };
            var direction = Direction3D.XPositive;
            var distance = new Distance(3);

            var actual = vector.ExtendTo(direction, distance);

            Assert.That(actual, Is.EqualTo(new Vector3D { X = 4, Y = 2, Z = 1 }));
        }

        #endregion
    }
}