using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ChargeNow
{
    public class LightCtrl : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Coin")
            {
                Vector2 pos = GameMgr.Instance.CoinPosition();
                collision.transform.DOMove(pos, 1f);
                collision.transform.DOScale(Vector2.zero, 1.2f);
                GameMgr.Instance?.AddCoin(10, 0.5f);
                SoundMgr.Instance?.OnPlaySound(SoundType.Coin);
            }
        }
    }
}