using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ReadOnlyCollection<Command> ReservedCommands => new(_commands);

        public void Push(Command command)
        {
            _commands.Add(command);
            command.transform.parent = transform;
        }

        private void ClearReservations()
        {
            _commands.Clear();
        }
    }
}