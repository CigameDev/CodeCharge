using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class DummyPhone : SunPin
    {
        protected override void Init()
        {
           
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Chain")
            {
                GameMgr.Instance?.ShowComplete();
            }
        }

        private void Update()
        {
            
        }

        public override void ChargeBySun(bool isCharge)
        {
            
        }
    }
}