using UnityEngine;

namespace RineaR.MadeHighlow.GameData.Figures
{
    public class Shadow : MonoBehaviour, IFigure
    {
        #region IFigure Members

        public float GetVision()
        {
            return 0;
        }

        #endregion
    }
}