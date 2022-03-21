using NUnit.Framework;
using RineaR.MadeHighlow.Engine.Exceptions;

namespace RineaR.MadeHighlow.Engine
{
    public class IDTest
    {
        #region None

        [Test]
        public void None_Get_ReturnsThatInternalValueIsNoneInternalValue()
        {
            var id = ID<Sample>.None;

            var actual = id.InternalValue;

            Assert.That(actual, Is.EqualTo(ID<Sample>.NoneInternalValue));
        }

        #endregion

        private class Sample
        {
        }

        private class Another
        {
        }

        #region From

        [Test]
        [TestCase(0)]
        public void From_CallWithN_ReturnsThatInternalValueIsN(int n)
        {
            var id = ID<Sample>.From(n);

            var actual = id.InternalValue;

            Assert.That(actual, Is.EqualTo(n));
        }

        [Test]
        public void From_NoneInternalValue_ThrowsInvalidIDException()
        {
            Assert.That(
                () => ID<Sample>.From(ID<Sample>.NoneInternalValue),
                Throws.TypeOf<InvalidIDException>()
            );
        }

        #endregion

        #region Equals

        [Test]
        public void Equals_NoneAndNone_ReturnsFalse()
        {
            var id1 = ID<Sample>.None;
            var id2 = ID<Sample>.None;

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_NoneAndValue_ReturnsFalse()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Sample>.None;

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Sample>.From(1);

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_SameValue_ReturnsTrue()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Sample>.From(0);

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Another>.From(0);

            // ReSharper disable once SuspiciousTypeConversion.Global
            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        #endregion

        #region CompareTo

        [Test]
        public void CompareTo_GreaterInternalValue_ReturnsTrue()
        {
            var id1 = ID<Sample>.From(1);
            var id2 = ID<Sample>.From(0);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Positive);
        }

        [Test]
        public void CompareTo_SameInternalValue_ReturnsZero()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Sample>.From(0);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Zero);
        }

        [Test]
        public void CompareTo_LessInternalValue_ReturnsFalse()
        {
            var id1 = ID<Sample>.From(0);
            var id2 = ID<Sample>.From(1);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Negative);
        }

        #endregion
    }
}