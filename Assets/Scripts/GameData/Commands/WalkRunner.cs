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
        [Min(0)]
        public int costLimit;

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
            var cost = 0;
            var directions = new List<FieldDirection2>();
            foreach (var direction in Route.Directions)
            {
                var start = command.figure.fieldTransform.position;
                var end2D = start.To2D() + direction.ToVector();

                var tile = command.session.field.FindTileWithPosition(end2D);

                // 移動先にタイルが存在しない場合、移動を中断する。
                if (tile == null)
                {
                    break;
                }

                var climbHeight = tile.elevation - start.height;
                cost += climbHeight switch
                {
                    2 => 15,
                    1 => 5,
                    0 => 1,
                    -1 => 1,
                    -2 => 1,
                    _ => int.MaxValue,
                };

                // 使用可能なコストを超えた場合、移動を中断する。
                if (cost > costLimit)
                {
                    break;
                }

                var end = end2D.To3D(tile.elevation);
                command.figure.MoveTo(end);
                directions.Add(direction);
            }

            return new WalkResult
            {
                Route = new WalkRoute(directions),
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
            this.LogInfo(Route.ToString());
        }
    }
}