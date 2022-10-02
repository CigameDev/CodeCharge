using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChargeNow
{
    public class CleanerCtrl : MonoBehaviour
    {
        private bool _goAhead;
        private Vector2 _direct;

        // Start is called before the first frame update
        void Start()
        {
            _direct = this.transform.localScale.y < 0 ? Vector2.down : Vector2.up;
        }

        // Update is called once per frame
        void Update()
        {
            if (_goAhead)
            {
                if (Mathf.Abs( this.transform.position.y) > 10f) return;

                this.transform.Translate(_direct * Time.deltaTime * 2f);
            }
        }

        public void GoAhead()
        {
            _goAhead = true;
            SoundMgr.Instance?.OnPlaySound(SoundType.HomeCleaner); 
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Brick")
            {
                _goAhead = false;
            }
            else if(collision.tag == "Coin")
            {
                Vector2 pos = GameMgr.Instance.CoinPosition();
                collision.transform.DOMove(pos, 1f);
                collision.transform.DOScale(Vector2.zero, 1.2f);
                GameMgr.Instance?.AddCoin(10, 0.5f);
            }

            if(collision.name == "Goal")
            {
                GameMgr.Instance?.ShowComplete();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Brick")
            {
                _goAhead = true;
            }
        }
    }
}