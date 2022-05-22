using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.General.BigBang
{
    public record BigBangResult([NotNull] BigBangAction Action, [NotNull] BigBangProcess Process) : ValidResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
