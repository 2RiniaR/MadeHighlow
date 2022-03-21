using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class Position3DTest
    {
        #region Zero

        [Test]
        public void Zero_Get_ReturnsThatAllAxesAreZero()
        {
            var actual = Position3D.Zero;

            Assert.That(
                actual,
                Is.EqualTo(new Position3D { X = new Horizontal(), Y = new Vertical(), Z = new Height() })
            );
        }

        #endregion

        #region AddOperator

        [Test]
        public void AddOperator_Vector3D_ReturnsThatAllAxesAreAddedValue()
        {
            var position = new Position3D { X = new Horizontal(1), Y = new Vertical(2), Z = new Height(1) };
            var vector = new Vector3D { X = 3, Y = 4, Z = 1 };

            var actual = position + vector;

            Assert.That(
                actual,
                Is.EqualTo(new Position3D { X = new Horizontal(4), Y = new Vertical(6), Z = new Height(2) })
            );
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Vector3D_ReturnsThatAllAxesAreSubtractedValue()
        {
            var position = new Position3D { X = new Horizontal(1), Y = new Vertical(2), Z = new Height(1) };
            var vector = new Vector3D { X = 3, Y = 4, Z = 1 };

            var actual = position - vector;

            Assert.That(
                actual,
                Is.EqualTo(new Position3D { X = new Horizontal(-2), Y = new Vertical(-2), Z = new Height() })
            );
        }

        #endregion

        #region MoveTo

        [Test]
        public void MoveTo_Values_ReturnsThatMovedPosition()
        {
            var position = new Position3D { X = new Horizontal(1), Y = new Vertical(2), Z = new Height(1) };
            var direction = Direction3D.XPositive;
            var distance = new Distance(3);

            var actual = position.MoveTo(direction, distance);

            Assert.That(
                actual,
                Is.EqualTo(new Position3D { X = new Horizontal(4), Y = new Vertical(2), Z = new Height(1) })
            );
        }

        #endregion

        #region To2D

        [Test]
        public void To2D_Get_ReturnsThatAllAxesAreSameValue()
        {
            var position = new Position3D { X = new Horizontal(1), Y = new Vertical(2), Z = new Height(1) };

            var actual = position.To2D();

            Assert.That(
                actual,
                Is.EqualTo(new Position2D { X = new Horizontal(1), Y = new Vertical(2) })
            );
        }

        #endregion
    }
}