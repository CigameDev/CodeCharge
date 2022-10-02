using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugPhone : BasePlug
    {
        [SerializeField] private GameObject _emptyPin;
        [SerializeField] private GameObject _fullPin;
        [SerializeField] private SpriteRenderer _batery;

        private float _minBatery, _maxBatery;
        private bool _isPluged, _isFull;



        protected override void Init()
        {
            base.Init();

            _minBatery = 0f;
            _maxBatery = 0.6f;
        }

        
        void Update()
        {
            if (_isPluged)
            {
                if (!_isFull)
                {
                    _batery.color = Color.green;
                    Vector2 size = _batery.size;
                    size.y += Time.deltaTime / 2f;

                    if (size.y >= _maxBatery)
                    {
                        size.y = _maxBatery;
                        _isFull = true;
                        _fullPin.SetActive(true);
                        _baseCtr?.OnCheckDone();
                        SoundMgr.Instance?.OnPlaySound(SoundType.OpenIphone);
                    }

                    _batery.size = size;

                }
            }
            else
            {
                if (!_isFull)
                {
                    _batery.color = Color.red;
                    Vector2 size = _batery.size;
                    size.y -= Time.deltaTime / 2f;

                    if (size.y <= _minBatery)
                    {
                        size.y = _minBatery;
                        _isFull = true;
                        _emptyPin.SetActive(true);
                    }

                    _batery.size = size;
                }
            }
        }

        protected override void ActionDown()
        {
            _isFull = _isPluged = false;
            _fullPin.SetActive(false);
        }

        protected override void ActionUp()
        {
            _emptyPin.SetActive(false);
            _isPluged = true;
            _isFull = _batery.size.y >= _maxBatery;
        }

        public override bool IsCharge()
        {
            return _fullPin.activeInHierarchy;
        }

        public virtual void ChargeBySun(bool isCharge)
        {
            if (isCharge) this.ActionUp();
            else this.ActionDown();
        }
    }
}