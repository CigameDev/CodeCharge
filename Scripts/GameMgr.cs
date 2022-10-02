using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class GameMgr : MonoBehaviour, BaseCtr
    {
        private static GameMgr _instance;
        public static GameMgr Instance => _instance;

        [SerializeField] private UIMgr _uiManager;

        private List<BasePlug> _listPlug;
        private bool _isReady;

        // Start is called before the first frame update
        private void Awake()
        {
            _instance = this;
            _listPlug = new List<BasePlug>();
        }

        void Start()
        {
           
        }


        public void AddPlug(BasePlug ctr)
        {
            if (!_listPlug.Contains(ctr))
            {
                ctr.SetDelegate(this);
                _listPlug.Add(ctr);
            }
        }

        public void ShowComplete()
        {
            _uiManager.ShowComplete();
        }

        public void AddCoin(int coin,float time)
        {
            _uiManager.AddCoin(coin, time);
        }

        public Vector2 CoinPosition()
        {
            return _uiManager.CoinPosition();
        }

        void BaseCtr.OnCheckDone()
        {
            for(int i = 0; i < _listPlug.Count; i++)
            {
                if (!_listPlug[i].IsCharge()) return;
            }
            Debug.Log("Level Complete");
            _uiManager.ShowComplete();
            _isReady = false;
            _listPlug.Clear();
            
        }

        bool BaseCtr.IsReady()
        {
            return _isReady;
        }

        public void SetReady(bool ready)
        {
            _isReady = ready;
        }
    }

    public interface BaseCtr
    {
        void OnCheckDone();
        bool IsReady();
    }
}
