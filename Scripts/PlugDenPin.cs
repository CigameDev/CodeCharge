using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChargeNow
{
    public class PlugDenPin : BasePlug
    {
        [SerializeField] private Transform _light;
        [SerializeField] private PlugPhone _sunPhone;

        protected override void ActionDown()
        {
            _light.localScale = new Vector3(1f, 0f, 1f);
            _pluged = false;

            if (_sunPhone)
            {
                _sunPhone.ChargeBySun(false);
            }
            else
            {
                RaycastHit2D coli = Physics2D.Raycast(_light.position, _light.up, 15f);
                if (coli.collider != null)
                {
                    PlugPhone phone = coli.collider.GetComponent<PlugPhone>();
                    phone?.ChargeBySun(false);
                }
            }
        }

        protected override void ActionUp()
        {
            _pluged = true;
            if (_sunPhone)
            {
                float distance = Mathf.Abs(_sunPhone.transform.position.y - _light.transform.position.y);
                _light.DOScaleY(distance + 5f, 0.5f).OnComplete(() => this.ChargeSun(_sunPhone));
            }
            else
            {
                RaycastHit2D coli = Physics2D.Raycast(_light.position, _light.up, 15f);
                if (coli.collider != null)
                {
                    PlugPhone phone = coli.collider.GetComponent<PlugPhone>();
                    if (phone)
                    {
                        float distance = Mathf.Abs(phone.transform.position.y - _light.transform.position.y);
                        _light.DOScaleY(distance, 0.5f).OnComplete(() => this.ChargeSun(phone));
                        Debug.Log(phone.name);
                    }
                }
            }
        }

        private void ChargeSun(PlugPhone phone)
        {
            if (_pluged) phone?.ChargeBySun(true);
            SoundMgr.Instance?.OnPlaySound(SoundType.DenPin);
        }

    }
}
