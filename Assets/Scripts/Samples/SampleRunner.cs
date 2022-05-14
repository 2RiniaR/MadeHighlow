using System.Threading;
using Cysharp.Threading.Tasks;
using RineaR.MadeHighlow.Directors;
using RineaR.MadeHighlow.Directors.Environments;
using UnityEngine;

namespace RineaR.MadeHighlow.Samples
{
    public class SampleRunner
    {
        private CancellationTokenSource CancellationTokenSource { get; } = new();

        private Director Director { get; } = new(new Environment(new SampleRandomGenerator()));

        private void Initialize()
        {
        }

        public async UniTask Run()
        {
            await Director.Run(CancellationTokenSource.Token);
        }

        public class SampleRandomGenerator : IRandomGenerator
        {
            public float Range(float minInclusive, float maxExclusive)
            {
                return Random.Range(minInclusive, maxExclusive);
            }
        }
    }
}
