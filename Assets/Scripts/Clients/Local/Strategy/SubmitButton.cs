using UnityEngine;

namespace RineaR.MadeHighlow.Clients.Local.Strategy
{
    public class SubmitButton : MonoBehaviour
    {
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}