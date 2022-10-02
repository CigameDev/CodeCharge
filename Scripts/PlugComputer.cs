using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugComputer : BasePlug
    {
        [SerializeField] private SpriteRenderer _screen;
        [SerializeField] private Sprite _sprScreenOn;
        [SerializeField] private Sprite _sprScreenOff;

        private float _timePlug;
        private bool _isFull;

        // Update is called once per frame
        void Update()
        {
            if (_pluged && !_isFull)
            {
                _timePlug += Time.deltaTime;
                if(_timePlug >= 1f)
                {
                    _screen.sprite = _sprScreenOn;
                    _isFull = true;
                    _baseCtr.OnCheckDone();
                    SoundMgr.Instance?.OnPlaySound(SoundType.OpenTiVi);
                }
            }
        }

        protected override void ActionDown()
        {
            _screen.sprite = _sprScreenOff;
            _pluged = _isFull = false;
        }

        protected override void ActionUp()
        {
            _timePlug = 0f;
            _pluged = true;
        }

        public override bool IsCharge()
        {
            return _isFull;
        }

    }
}