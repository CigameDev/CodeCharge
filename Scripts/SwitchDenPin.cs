using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChargeNow
{
    public class SwitchDenPin : BasePlug
    {
        [SerializeField] private SpriteRenderer _switch;
        [SerializeField] private Sprite _tgOff;
        [SerializeField] private Transform _light;

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
                RaycastHit2D coli = Physics2D.Raycast(_light.position, _light.up, 15f);
                if (coli.collider != null)
                {
                    PlugPhone phone = coli.collider.GetComponent<PlugPhone>();
                    float distance = Mathf.Abs(phone.transform.position.y - _light.transform.position.y);
                    _light.DOScaleY(distance, 0.5f).OnComplete(() => this.ChargeSun(phone));
                    Debug.Log(phone.name);
                }
            }
        }

        private void ChargeSun(PlugPhone phone)
        {
            if (_pluged) phone?.ChargeBySun(true);
        }


    }
}