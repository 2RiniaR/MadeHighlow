using System;
using UnityEngine;

namespace Experiments
{
    public class SortTest : MonoBehaviour
    {
        public interface ISample : IComparable<ISample>
        {
        }

        public class SampleA : ISample
        {
            public int CompareTo(ISample other)
            {
                return -1;
            }

            public override string ToString()
            {
                return "A";
            }
        }

        public class SampleB : ISample
        {
            public int CompareTo(ISample other)
            {
                return 0;
            }

            public override string ToString()
            {
                return "B";
            }
        }

        public class SampleC : ISample
        {
            public int CompareTo(ISample other)
            {
                return 1;
            }

            public override string ToString()
            {
                return "C";
            }
        }

        private void Start()
        {
            var samples = ValueList.Create<ISample>(
                new SampleB(),
                new SampleA(),
                new SampleC(),
                new SampleA(),
                new SampleC(),
                new SampleB(),
                new SampleA(),
                new SampleB(),
                new SampleA(),
                new SampleC(),
                new SampleB(),
                new SampleC(),
                new SampleB(),
                new SampleB(),
                new SampleC(),
                new SampleC(),
                new SampleC(),
                new SampleB(),
                new SampleC(),
                new SampleA()
            );
            foreach (var sample in samples)
            {
                Debug.Log(sample);
            }

            var sortedSamples = samples.Sort();
            foreach (var sample in sortedSamples)
            {
                Debug.Log(sample);
            }
        }
    }
}
