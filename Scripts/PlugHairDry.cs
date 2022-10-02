using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChargeNow
{
    public class PlugHairDry : BasePlug
    {
        [SerializeField] private List<Transform> _listPaper;
        [SerializeField] private Transform _directFly;
        [SerializeField] private List<GameObject> _listPlugActive;

        private bool _isPluged;

        protected override void Init()
        {
            base.Init();

           

            for (int i = 0; i < _listPlugActive.Count; i++)
            {
                _listPlugActive[i].SetActive(false);
            }
        }

        protected override void ActionUp()
        {
            if (_isPluged) return;

            for(int i = 0; i < _listPaper.Count; i++)
            {
                Vector3 endPos = _listPaper[i].position + _directFly.up.normalized * 10f;
                _listPaper[i].DOMove(endPos, 2.5f).SetEase(Ease.InQuad).SetDelay(Random.Range(0.2f, 1f));
            }

            for(int i = 0; i < _listPlugActive.Count; i++)
            {
                _listPlugActive[i].SetActive(true);
            }

            SoundMgr.Instance?.OnPlaySound(SoundType.MaySay);

            _isPluged = true;
        }

        public override bool IsCharge()
        {
            return true;
        }
    }
}