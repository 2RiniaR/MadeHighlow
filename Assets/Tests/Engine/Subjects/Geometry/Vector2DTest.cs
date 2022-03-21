using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class Vector2DTest
    {
        #region Zero

        [Test]
        public void Zero_Get_ReturnsThatAllAxesAreZero()
        {
            var actual = Vector2D.Zero;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 0, Y = 0 }));
        }

        #endregion

        #region XPositive

        [Test]
        public void XPositive_Get_ReturnsThatXIsPositiveUnitAndOthersAreZero()
        {
            var actual = Vector2D.XPositive;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 1, Y = 0 }));
        }

        #endregion

        #region XNegative

        [Test]
        public void XNegative_Get_ReturnsThatXIsNegativeUnitAndOthersAreZero()
        {
            var actual = Vector2D.XNegative;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = -1, Y = 0 }));
        }

        #endregion

        #region YPositive

        [Test]
        public void YPositive_Get_ReturnsThatYIsPositiveUnitAndOthersAreZero()
        {
            var actual = Vector2D.YPositive;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 0, Y = 1 }));
        }

        #endregion

        #region YNegative

        [Test]
        public void YNegative_Get_ReturnsThatYIsNegativeUnitAndOthersAreZero()
        {
            var actual = Vector2D.YNegative;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 0, Y = -1 }));
        }

        #endregion

        #region AddOperator

        [Test]
        public void AddOperator_Vector2D_ReturnsThatAllAxesAreAddedValue()
        {
            var vector1 = new Vector2D { X = 1, Y = 2 };
            var vector2 = new Vector2D { X = 3, Y = 4 };

            var actual = vector1 + vector2;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 4, Y = 6 }));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Vector2D_ReturnsThatAllAxesAreSubtractedValue()
        {
            var vector1 = new Vector2D { X = 1, Y = 2 };
            var vector2 = new Vector2D { X = 3, Y = 4 };

            var actual = vector1 - vector2;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = -2, Y = -2 }));
        }

        #endregion

        #region InverseOperator

        [Test]
        public void InverseOperator_Vector2D_ReturnsThatAllAxesAreInversedValue()
        {
            var vector = new Vector2D { X = 1, Y = 2 };

            var actual = -vector;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = -1, Y = -2 }));
        }

        #endregion

        #region MultipleOperator

        [Test]
        public void MultipleOperator_Int_ReturnsThatAllAxesAreMultipleValue()
        {
            var vector = new Vector2D { X = 1, Y = 2 };
            var multiplier = 2;

            var actual = vector * multiplier;

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 2, Y = 4 }));
        }

        #endregion

        #region ExtendTo

        [Test]
        public void ExtendTo_Values_ReturnsThatExtendedVector()
        {
            var vector = new Vector2D { X = 1, Y = 2 };
            var direction = Direction2D.XPositive;
            var distance = new Distance(3);

            var actual = vector.ExtendTo(direction, distance);

            Assert.That(actual, Is.EqualTo(new Vector2D { X = 4, Y = 2 }));
        }

        #endregion
    }
}