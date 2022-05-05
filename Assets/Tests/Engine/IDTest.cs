using System;
using NUnit.Framework;

namespace RineaR.MadeHighlow
{
    public class IDTest
    {
        #region From

        [Test]
        public void From_0_ThrowsArgumentException()
        {
            Assert.That(() => ID.From(0), Throws.TypeOf<ArgumentException>());
        }

        #endregion

        #region Equals

        [Test]
        public void Equals_NoneAndNone_ReturnsFalse()
        {
            var id1 = ID.None;
            var id2 = ID.None;

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_NoneAndValue_ReturnsFalse()
        {
            var id1 = ID.From(1);
            var id2 = ID.None;

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var id1 = ID.From(1);
            var id2 = ID.From(2);

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Equals_SameValue_ReturnsTrue()
        {
            var id1 = ID.From(1);
            var id2 = ID.From(1);

            var actual = id1.Equals(id2);

            Assert.That(actual, Is.True);
        }

        #endregion

        #region CompareTo

        [Test]
        public void CompareTo_Greater_ReturnsTrue()
        {
            var id1 = ID.From(2);
            var id2 = ID.From(1);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Positive);
        }

        [Test]
        public void CompareTo_Same_ReturnsZero()
        {
            var id1 = ID.From(1);
            var id2 = ID.From(1);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Zero);
        }

        [Test]
        public void CompareTo_Less_ReturnsFalse()
        {
            var id1 = ID.From(1);
            var id2 = ID.From(2);

            var actual = id1.CompareTo(id2);

            Assert.That(actual, Is.Negative);
        }

        #endregion
    }
}