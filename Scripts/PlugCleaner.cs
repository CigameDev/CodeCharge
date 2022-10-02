using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugCleaner : BasePlug
    {

        [SerializeField] private CleanerCtrl _cleaner;
        
        // Update is called once per frame
        void Update()
        {

        }

        protected override void ActionUp()
        {
            _cleaner.GoAhead();
        }


    }
}