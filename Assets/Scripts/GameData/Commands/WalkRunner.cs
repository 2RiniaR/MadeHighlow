using System;
using System.Collections.Generic;
using RineaR.MadeHighlow.GameData.CardEffects;
using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Geometry;
using UnityEngine;

namespace RineaR.MadeHighlow.GameData.Commands
{
    [RequireComponent(typeof(Command))]
    public class WalkRunner : MonoBehaviour, ICommandRunner
    {
        public Command command;
        public WalkRoute Route { get; set; }

        private void Reset()
        {
            RefreshReferences();
        }

        private void Start()
        {
            RefreshReferences();
            command.RegisterRunner(this);
        }

        public ICommandResult RunCommand()
        {
            // 状態を更新する
            var movedDirections = new List<FieldDirection2>();
            foreach (var direction in Route.Directions)
            {
                command.figure.fieldTransform.position += direction.ToVector().To3D(0);
                movedDirections.Add(direction);
            }

            // 起こったことを出力する
            return new WalkResult
            {
                Route = new WalkRoute(movedDirections),
                Walker = command.figure,
            };
        }

        private void RefreshReferences()
        {
            command ??= GetComponent<Command>() ?? throw new NullReferenceException();
        }

        [ContextMenu("Print Route")]
        private void PrintRoute()
        {
            Debug.Log(Route);
        }
    }
}