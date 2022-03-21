using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class HorizontalTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Int_ReturnsThatValueIsAddedValue()
        {
            var horizontal = new Horizontal() + 1;

            var actual = horizontal.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Int_ReturnsThatValueIsSubtractedValue()
        {
            var horizontal = new Horizontal(2) - 1;

            var actual = horizontal.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion
    }
}