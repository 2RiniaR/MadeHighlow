using GameView.Strategy;
using GameView.Structures;
using UnityEngine;

namespace GameView
{
    public class GameViewRunner : MonoBehaviour
    {
        [Header("Components")]
        public StrategyView strategy;

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
        }
    }
}