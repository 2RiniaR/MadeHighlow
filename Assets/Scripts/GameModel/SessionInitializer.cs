using System;
using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class SessionInitializer : ScriptableObject
    {
        public Field field;
        public List<PlayerOption> playerOptions;

        public void Initialize(Session session)
        {
            var playerConnector = GeneratePlayerConnector(session);
            foreach (var playerOption in playerOptions)
            {
                JoinPlayer(playerConnector, playerOption);
            }

            // すべてのプレイヤーの開始位置を決定する

            // フィールド上にフィギュアを生成する

            // すべてのプレイヤーに初期カードを配布する
        }

        private Player JoinPlayer(PlayerConnector connector, PlayerOption option)
        {
            var instance = new GameObject(option.name);
            instance.transform.SetParent(connector.transform);
            var player = instance.AddComponent<Player>();
            connector.Join(player);
            return player;
        }

        private PlayerConnector GeneratePlayerConnector(Session session)
        {
            var instance = new GameObject("Player Connector");
            instance.transform.SetParent(session.transform);
            return instance.AddComponent<PlayerConnector>();
        }

        [Serializable]
        public class PlayerOption
        {
            public string name;
            public List<Figure> figures;
            public LandingPoint requestedLandingPoint;
        }
    }
}