using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class SunPC : SunPin
    {

        [SerializeField] private SpriteRenderer _screen;

        // Update is called once per frame
        void Update()
        {

        }

        public override void ChargeBySun(bool isCharge)
        {
            if (isCharge)
            {
                _screen.sprite = _sprPluged;
                _pluged = true;
                base._baseCtr?.OnCheckDone();
                SoundMgr.Instance?.OnPlaySound(SoundType.OpenTiVi);
            }
            else
            {
                _pluged = false;
                _screen.sprite = _sprNormal;
            }
        }

        public override bool IsCharge()
        {
            return _pluged;
        }
    }
}
