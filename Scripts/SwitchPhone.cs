using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class SwitchPhone : PlugPhone
    {
        [SerializeField] private SpriteRenderer _switch;
        [SerializeField] private Sprite _tgOn;
        [SerializeField] private Sprite _tgOff;


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
            if(collision.tag == "Truck")
            {
                _switch.sprite = _tgOff;
                base.ActionUp();
            }
        }

    }
}