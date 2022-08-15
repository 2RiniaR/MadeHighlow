using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    /// <summary>
    ///     コマンドを格納するコンポーネント。
    /// </summary>
    /// <remarks>
    ///     コマンドは子のGameObjectに配置される。
    /// </remarks>
    public class CommandStack : MonoBehaviour
    {
        private readonly List<Command> _commands = new();
        private CommandOrderer _orderer;

        private void Awake()
        {
            _orderer = new CommandOrderer();
        }

        public void Push(Command command)
        {
            _commands.Add(command);
            command.transform.parent = transform;
        }

        private void ClearReservations()
        {
            _commands.Clear();
        }

        private void ResolveOrder()
        {
            _orderer.Resolve(_commands);
            foreach (var command in _commands) command.transform.SetSiblingIndex(_commands.Count - 1);
        }

        public async UniTask RunAll(CancellationToken token)
        {
            foreach (var command in _commands) await command.Run(token);
            ClearReservations();
        }
    }
}