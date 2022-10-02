using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugTruck : BasePlug
    {
        [SerializeField] private Transform _truck;



        // Update is called once per frame
        void Update()
        {
            if (_pluged)
            {
                _truck.Translate(_truck.up.normalized * Time.deltaTime * 3f);

                if (Mathf.Abs(_truck.position.y) > 10f) _pluged = false;
            }
        }

        protected override void ActionDown()
        {
            _pluged = false;
        }

        protected override void ActionUp()
        {
            _pluged = true;
        }

        public override bool IsCharge()
        {
            return true;
        }
    }
}