using NUnit.Framework;

namespace RineaR.MadeHighlow.Engine.Subjects.Geometry
{
    public class HeightTest
    {
        #region AddOperator

        [Test]
        public void AddOperator_Int_ReturnsThatValueIsAddedValue()
        {
            var height = new Height() + 1;

            var actual = height.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion

        #region SubtractOperator

        [Test]
        public void SubtractOperator_Int_ReturnsThatValueIsSubtractedValue()
        {
            var height = new Height(2) - 1;

            var actual = height.Value;

            Assert.That(actual, Is.EqualTo(1));
        }

        #endregion

        #region Constructor

        [Test]
        [TestCase(Height.MinValue)]
        [TestCase(Height.MinValue + 1)]
        [TestCase(Height.MaxValue)]
        public void Constructor_InRange_ReturnsThatValueIsSame(int value)
        {
            var height = new Height(value);

            var actual = height.Value;

            Assert.That(actual, Is.EqualTo(value));
        }

        [Test]
        public void Constructor_LessThanMin_ReturnsThatValueIsMin()
        {
            var height = new Height(Height.MinValue - 1);

            var actual = height.Value;

            Assert.That(actual, Is.EqualTo(Height.MinValue));
        }

        [Test]
        public void Constructor_GreaterThanMax_ReturnsThatValueIsMax()
        {
            var height = new Height(Height.MaxValue + 1);

            var actual = height.Value;

            Assert.That(actual, Is.EqualTo(Height.MaxValue));
        }

        #endregion
    }
}