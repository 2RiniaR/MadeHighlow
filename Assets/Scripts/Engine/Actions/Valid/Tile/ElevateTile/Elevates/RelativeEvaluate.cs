namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record RelativeElevate(int Level) : Elevate
    {
        public override Elevation Caused(Elevation original)
        {
            if (original is not GroundElevation ground)
            {
                return original;
            }

            return new GroundElevation(ground.Height + Level, original.Placeable);
        }
    }
}
