using GameView.Strategy;
using GameView.Structures;
using UnityEngine;

namespace GameView
{
    public class GameViewRunner : MonoBehaviour
    {
        [Header("Components")]
        public StrategyWindow strategy;

        [Header("States"), Space]
        public CharacterData character1;
        public CharacterData character2;
        public CharacterData character3;
        public CharacterData character4;
        public UnitEffectData effect1;
        public UnitEffectData effect2;
        public UnitEffectData effect3;

        private void Start()
        {
            var unit1 = strategy.units.Create(0);
            unit1.character = character1;
            unit1.Strength = 14;
            unit1.Health = 60;
            unit1.InitialHealth = 60;
            var unitEffect11 = unit1.effectIcons.Create(0);
            unitEffect11.effect = effect1;
            unitEffect11.duration = 3;
            var unitEffect12 = unit1.effectIcons.Create(1);
            unitEffect12.effect = effect2;
            unitEffect12.duration = 2;


            var unit2 = strategy.units.Create(1);
            unit2.character = character2;
            unit2.Strength = 8;
            unit2.Health = 40;
            unit2.InitialHealth = 40;


            var unit3 = strategy.units.Create(2);
            unit3.character = character3;
            unit3.Strength = 10;
            unit3.Health = 50;
            unit3.InitialHealth = 50;
            var unitEffect31 = unit3.effectIcons.Create(0);
            unitEffect31.effect = effect3;
            unitEffect31.duration = 1;


            var unit4 = strategy.units.Create(3);
            unit4.character = character4;
            unit4.Strength = 12;
            unit4.Health = 1;
            unit4.InitialHealth = 50;
            var unitEffect41 = unit4.effectIcons.Create(0);
            unitEffect41.effect = effect1;
            unitEffect41.duration = 3;
            var unitEffect42 = unit4.effectIcons.Create(1);
            unitEffect42.effect = effect2;
            unitEffect42.duration = 2;
            var unitEffect43 = unit4.effectIcons.Create(2);
            unitEffect43.effect = effect3;
            unitEffect43.duration = 3;


            var player1 = strategy.players.Create(0);
            player1.playerName = "せっかち";
            var pu11 = player1.units.Create(0);
            pu11.health = 60;
            pu11.character = character1;
            var pu12 = player1.units.Create(1);
            pu12.health = 40;
            pu12.character = character2;
            var pu13 = player1.units.Create(2);
            pu13.health = 50;
            pu13.character = character3;
            var pu14 = player1.units.Create(3);
            pu14.health = 1;
            pu14.character = character4;


            var player2 = strategy.players.Create(1);
            player2.playerName = "れいせい";
            var pu21 = player2.units.Create(0);
            pu21.health = 3;
            pu21.character = character4;
            var pu22 = player2.units.Create(1);
            pu22.health = 1;
            pu22.character = character4;
            var pu23 = player2.units.Create(2);
            pu23.health = 6;
            pu23.character = character4;
            var pu24 = player2.units.Create(3);
            pu24.health = 6;
            pu24.character = character4;


            var player3 = strategy.players.Create(2);
            player3.playerName = "うっかり";
            var pu31 = player3.units.Create(0);
            pu31.health = 25;
            pu31.character = character2;
            var pu32 = player3.units.Create(1);
            pu32.health = 13;
            pu32.character = character4;
            var pu33 = player3.units.Create(2);
            pu33.health = 44;
            pu33.character = character3;
            var pu34 = player3.units.Create(3);
            pu34.health = 42;
            pu34.character = character4;


            var player4 = strategy.players.Create(3);
            player4.playerName = "かしこい";
            var pu41 = player4.units.Create(0);
            pu41.health = 999;
            pu41.character = character1;
            var pu42 = player4.units.Create(1);
            pu42.health = 999;
            pu42.character = character1;
            var pu43 = player4.units.Create(2);
            pu43.health = 999;
            pu43.character = character3;
            var pu44 = player4.units.Create(3);
            pu44.health = 999;
            pu44.character = character3;
        }
    }
}