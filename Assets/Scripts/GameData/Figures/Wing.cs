using UnityEngine;

namespace RineaR.MadeHighlow.GameData.Figures
{
    public class Wing : MonoBehaviour, IFigure
    {
        #region IFigure Members

        public float GetVision()
        {
            return 0;
        }

        #endregion
    }
}