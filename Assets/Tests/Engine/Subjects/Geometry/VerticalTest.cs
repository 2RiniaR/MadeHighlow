using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class VerticalTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Int_ReturnsThatValueIsAddedValue()
        {
            var vertical = new Vertical() + 1;

            var actual = vertical.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Int_ReturnsThatValueIsSubtractedValue()
        {
            var vertical = new Vertical(2) - 1;

            var actual = vertical.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion
    }
}