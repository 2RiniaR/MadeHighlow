using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class Position2DTest
    {
        #region Zero

        [Test]
        public void Zero_Get_ReturnsThatAllAxesAreZero()
        {
            var actual = Position2D.Zero;

            Assert.That(actual, Is.EqualTo(new Position2D { X = new Horizontal(), Y = new Vertical() }));
        }

        #endregion

        #region AddOperator

        [Test]
        public void AddOperator_Vector2D_ReturnsThatAllAxesAreAddedValue()
        {
            var position = new Position2D { X = new Horizontal(1), Y = new Vertical(2) };
            var vector = new Vector2D { X = 3, Y = 4 };

            var actual = position + vector;

            Assert.That(actual, Is.EqualTo(new Position2D { X = new Horizontal(4), Y = new Vertical(6) }));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Vector2D_ReturnsThatAllAxesAreSubtractedValue()
        {
            var position = new Position2D { X = new Horizontal(1), Y = new Vertical(2) };
            var vector = new Vector2D { X = 3, Y = 4 };

            var actual = position - vector;

            Assert.That(actual, Is.EqualTo(new Position2D { X = new Horizontal(-2), Y = new Vertical(-2) }));
        }

        #endregion

        #region MoveTo

        [Test]
        public void MoveTo_Values_ReturnsThatMovedPosition()
        {
            var position = new Position2D { X = new Horizontal(1), Y = new Vertical(2) };
            var direction = Direction2D.XPositive;
            var distance = new Distance(3);

            var actual = position.MoveTo(direction, distance);

            Assert.That(actual, Is.EqualTo(new Position2D { X = new Horizontal(4), Y = new Vertical(2) }));
        }

        #endregion
    }
}