using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class PlugDoor : BasePlug
    {
        [SerializeField] private Transform _doorLeft;
        [SerializeField] private Transform _doorRight;

        private float _minDoorLeft, _maxDoorLeft, _minDoorRight, _maxDoorRight,_speed;

        protected override void Init()
        {
            base.Init();

            _minDoorLeft = _doorLeft.position.x;
            _minDoorRight = _doorRight.position.x;
            _maxDoorLeft = _minDoorLeft - 2f;
            _maxDoorRight = _minDoorRight + 2f;
            _speed = 1.5f;
        }

        protected override void ActionDown()
        {
            _pluged = false;
        }

        protected override void ActionUp()
        {
            _pluged = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_pluged)
            {
                if (_doorLeft.position.x > _maxDoorLeft)
                    _doorLeft.Translate(Vector3.left * _speed * Time.deltaTime);
                if (_doorRight.position.x < _maxDoorRight)
                    _doorRight.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else
            {
                if (_doorLeft.position.x < _minDoorLeft)
                    _doorLeft.Translate(Vector3.right * _speed * Time.deltaTime);
                if (_doorRight.position.x > _minDoorRight)
                    _doorRight.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }

        public override bool IsCharge()
        {
            return true;
        }

    }
}