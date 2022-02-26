using Cysharp.Threading.Tasks;
using Game.Characters;
using Game.Entities;
using Game.Maps;
using Game.Primitives;
using Game.Sessions;
using UnityEngine;

namespace Scenes
{
    public class DebugGameRunner : MonoBehaviour
    {
        public OfflineGameSession Session { get; private set; }

        private static readonly PlayerSetting PlayerSetting = new PlayerSetting
        {
            SelectedUnits = new[]
            {
                new SampleCharacter(CharacterID.FromIdentity(1)),
                new SampleCharacter(CharacterID.FromIdentity(2)),
                new SampleCharacter(CharacterID.FromIdentity(3)),
                new SampleCharacter(CharacterID.FromIdentity(4))
            }
        };

        private static readonly GlobalSetting GlobalSetting = new GlobalSetting
        {
            Players = new[]
            {
                PlayerSetting,
                PlayerSetting,
                PlayerSetting,
                PlayerSetting
            },
            SelectedMap = new SampleMap()
        };

        private void Start()
        {
            Session = new OfflineGameSession
            {
                Setting = GlobalSetting
            };

            Session.Run(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
}