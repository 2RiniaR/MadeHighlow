﻿using RineaR.MadeHighlow.GameModel;
using RineaR.MadeHighlow.GameModel.Interfaces;
using UnityEngine;

namespace RineaR.MadeHighlow.GameData.EntityEffects
{
    public class EveryTurnDamage : EntityEffect, ITurnUpdater
    {
        #region Serialized Fields

        [SerializeField] private int damage;

        #endregion

        public int Damage => damage;

        public void Create(Entity entity)
        {
            Instantiate(this, entity.transform);
        }

        public void Delete()
        {
            Destroy(this);
        }

        #region ITurnUpdater Members

        public int UpdateTurnPriority => 1;

        public void UpdateTurn()
        {
            if (Entity.Life == null) return;
            Entity.Life.Damage(Damage);
        }

        #endregion
    }
}