using Game;
using Game.Entities;
using Game.Primitives;
using Game.Sessions;
using UnityEngine;

namespace Scenes
{
    public class GameSceneRunner : MonoBehaviour
    {
        public OfflineGameSession Session { get; private set; }

        private void Start()
        {
            Session = new OfflineGameSession
            {
                Setting = new GlobalSetting()
            };

            StartCoroutine(Session.Run());
        }
    }
}