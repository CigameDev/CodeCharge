using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class SwitchMacbook : BasePlug
    {
        [SerializeField] private SpriteRenderer _switch;
        [SerializeField] private SpriteRenderer _sprLogo;
        [SerializeField] private Sprite _tgOff;
        [SerializeField] private Sprite _sprLogoLight;


        protected override void OnMouseDown()
        {

        }

        protected override void OnMouseDrag()
        {

        }

        protected override void OnMouseUp()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Truck")
            {
                _switch.sprite = _tgOff;
                _sprLogo.sprite = _sprLogoLight;
                _pluged = true;
                base._baseCtr?.OnCheckDone();
            }
        }

        public override bool IsCharge()
        {
            return _pluged;
        }
    }
}