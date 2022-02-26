using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Entities;
using Game.Players;
using JetBrains.Annotations;

namespace Game.Directors
{
    /// <summary>
    /// ゲームの状態を初期化するクラス
    /// </summary>
    public class StateInitializer
    {
        [NotNull] private readonly IGameSession _session;

        public StateInitializer(IGameSession session)
        {
            _session = session;
        }

        /// <summary>
        /// 設定に従って、ゲームの状態を初期化する。
        /// </summary>
        public void Initialize()
        {
            SetStartTurn();
            GeneratePlayers();
            GenerateField();
        }

        private void SetStartTurn()
        {
            _session.CurrentTurn = 1;
        }

        private void GeneratePlayers()
        {
            var playerSettings = _session.Setting?.Players;
            if (playerSettings == null)
            {
                throw new InvalidOperationException("`GlobalSetting.Players` must not be null.");
            }

            var players = playerSettings.Select(setting => (IPlayer)new SamplePlayer
            {
                Units = new ReadOnlyCollection<IUnit>(GenerateUnits(setting).ToList())
            });
            _session.Players = new ReadOnlyCollection<IPlayer>(players.ToList());
        }

        private static IEnumerable<IUnit> GenerateUnits(PlayerSetting setting)
        {
            if (setting?.SelectedUnits == null)
            {
                throw new InvalidOperationException("`PlayerSetting.SelectedUnits` must not be null.");
            }

            return setting.SelectedUnits.Select(selectedUnit => selectedUnit.GenerateUnit());
        }

        private void GenerateField()
        {
            if (_session.Setting?.SelectedMap == null)
            {
                throw new InvalidOperationException("`GlobalSetting.SelectedMap` must not be null.");
            }

            _session.Field = _session.Setting.SelectedMap.GenerateField();
        }
    }
}