using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Card : MonoBehaviour
    {
        public CardGenre genre;
        public CardSetting setting;
        public CommandQuickness quickness;
        public string optionText;
        public string displayName;

        public IEnumerable<Component> GetActivators()
        {
            return GetComponents<Component>();
        }
    }
}