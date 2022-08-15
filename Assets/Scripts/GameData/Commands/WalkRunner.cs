using System;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using UnityEngine;

namespace RineaR.MadeHighlow.GameData.Commands
{
    [RequireComponent(typeof(Command))]
    public class WalkRunner : MonoBehaviour
    {
        public bool enableClimb;
        public bool enableDown;
        public Command command;
        public WalkRoute Route { get; set; }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
        }

        private void RefreshReferences()
        {
            command = GetComponent<Command>() ?? throw new NullReferenceException();
        }

        [ContextMenu("Print Route")]
        private void PrintRoute()
        {
            Debug.Log(Route);
        }
    }
}