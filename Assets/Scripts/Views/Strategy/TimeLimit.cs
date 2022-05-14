using UnityEngine;
using UnityEngine.UI;

namespace Views.Strategy
{
    public class TimeLimit : MonoBehaviour
    {
        [Header("Components")] public Image timerImage;

        [Header("States")] [Space] [Min(0)] public float leftTime;
        [Min(float.Epsilon)] public float fullTime;

        private void Update()
        {
            UpdateTimerView();
        }

        private void UpdateTimerView()
        {
            var percent = leftTime / Mathf.Max(fullTime, float.Epsilon);
            timerImage.fillAmount = percent;
        }
    }
}
