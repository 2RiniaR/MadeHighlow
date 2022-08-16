using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class EventLogger : MonoBehaviour
    {
        private readonly List<IEventLog> _results = new();
        public ReadOnlyCollection<IEventLog> EventLogs => new(_results);

        public void Append(IEventLog result)
        {
            _results.Add(result);
        }
    }
}