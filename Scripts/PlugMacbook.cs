using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugMacbook : BasePlug
    {
        [SerializeField] private SpriteRenderer _appleLogo;
        [SerializeField] private Sprite _sprLogonOn;
        [SerializeField] private Sprite _sprLogoOff;


        protected override void ActionDown()
        {
            _pluged = false;
            _appleLogo.sprite = _sprLogoOff;
        }

        protected override void ActionUp()
        {
            _pluged = true;
            _appleLogo.sprite = _sprLogonOn;
            _baseCtr?.OnCheckDone();
            SoundMgr.Instance?.OnPlaySound(SoundType.OpenTiVi);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool IsCharge()
        {
            return _pluged;
        }

    }
}