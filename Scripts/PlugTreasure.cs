using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugTreasure : BasePlug
    {
        [SerializeField] private SpriteRenderer _treasure;
        [SerializeField] private Sprite _sprTreasureOpened;

        protected override void ActionUp()
        {
            if (!_pluged)
            {
                _treasure.sprite = _sprTreasureOpened;
                _pluged = true;
                GameMgr.Instance?.AddCoin(25, 0.5f);
                SoundMgr.Instance?.OnPlaySound(SoundType.Coin);
            }
        }

        public override bool IsCharge()
        {
            return true;
        }


    }
}