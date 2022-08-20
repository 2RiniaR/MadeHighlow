using System.Collections.Generic;
using RineaR.MadeHighlow.GameModel.Events;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class EventProcessor : MonoBehaviour
    {
        public EventLogger logger;
        private readonly CommandOrderer _orderer = new();

        public void RunCommandsByOrder(IEnumerable<Command> commands)
        {
            var orderedCommands = _orderer.Resolve(commands);

            foreach (var command in orderedCommands)
            {
                RunCommandSingle(command);
            }
        }

        public void RunCommandSingle(Command command)
        {
            var result = command.Run();
            var log = new RunCommandLog { CommandResult = result };
            logger.Append(log);
        }

        public void UpdateTurn()
        {
        }
    }
}