using System.Collections.Generic;
using UnityEngine;

namespace RineaR.MadeHighlow.GameModel
{
    public class Command : MonoBehaviour
    {
        public CommandQuickness quickness;
        public Figure figure;
        public Session session;
        public List<Card> payCards;
        private ICommandRunner _runner;

        public void RegisterRunner(ICommandRunner runner)
        {
            _runner = runner;
        }

        public ICommandResult Run()
        {
            return _runner.RunCommand();
        }
    }
}