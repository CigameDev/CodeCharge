using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ChargeNow
{
    public class UIMgr : MonoBehaviour
    {

        [SerializeField] private List<Sprite> _listNumber;
        [SerializeField] private List<Image> _listCoin;
        [SerializeField] private List<Image> _listLevel;
        [SerializeField] private List<Image> _listReward;
        [SerializeField] private Animator _animComplete;
        [SerializeField] private Animator _animMainMenu;
        [SerializeField] private UIButton _btnClaimX5;
        [SerializeField] private UIButton _btnContinue;
        [SerializeField] private UIButton _btnStart;
        [SerializeField] private ParticleSystem _confetiLeft;
        [SerializeField] private ParticleSystem _confetiRight;
        [SerializeField] private Transform _coin;
        [SerializeField] private List<Color> _colorBG;
        [SerializeField] private Camera _mainCam;

        private int _totalCoin;
        private int _currentLevel;
        private GameObject _objLevel,_nextLV;

        // Start is called before the first frame update
        private void Awake()
        {
        //    PlayerPrefs.DeleteAll();
            _currentLevel = PlayerPrefs.GetInt("current-level",1);
        }
        void Start()
        {
            _totalCoin = PlayerPrefs.GetInt("total-coin");
            this.FormatNumber(_listCoin, _totalCoin);
            this.FormatNumber(_listLevel, _currentLevel);

            int n = _currentLevel % 50;
            _objLevel = Instantiate(Resources.Load<GameObject>("Levels/Level" + n));
            _btnStart?.onClick.AddListener(this.OnClickStart);
            _btnContinue?.onClick.AddListener(this.OnClickNext);
            _btnClaimX5?.onClick.AddListener(this.OnClickRewardX5);
            _mainCam.backgroundColor = _colorBG[Random.Range(0, _colorBG.Count)];
        }


        private void FormatNumber(List<Image> display,int number)
        {
            string str = number.ToString();
            for(int i = 0; i < display.Count; i++)
            {
                if(i < str.Length)
                {
                    int alpha = int.Parse(str[i].ToString());
                    display[i].gameObject.SetActive(true);
                    display[i].sprite = _listNumber[alpha];
                    display[i].SetNativeSize();
                }
                else
                {
                    display[i].gameObject.SetActive(false);
                }
            }
        }

        public void AddCoin(int coin)
        {
            _totalCoin += coin;
            PlayerPrefs.SetInt("total-coin", _totalCoin);
        }

        public void SetLevel(int level)
        {
            this.FormatNumber(_listLevel, level);
        }

        public void ShowComplete()
        {
            _animComplete.gameObject.SetActive(true);
            _animComplete.Play("panelcomplete-show");
            _confetiLeft.Play();
            _confetiRight.Play();
            _currentLevel++;
            PlayerPrefs.SetInt("current-level", _currentLevel);

            StartCoroutine(IECountCoin(60, 0.5f));
            SoundMgr.Instance?.OnPlaySound(SoundType.Win);
        }

        private IEnumerator IECountCoin(int coinReward, float time)
        {
            int target = _totalCoin + coinReward;
            float timeCount = 0f;
            while(timeCount < time)
            {
                timeCount += Time.deltaTime;
                float percent = timeCount / time;
                int perCoin = Mathf.RoundToInt(percent * coinReward);
                int curCoin = _totalCoin + perCoin;
                this.FormatNumber(_listCoin, curCoin);

                yield return null;
            }

            this.AddCoin(coinReward);
            this.FormatNumber(_listCoin, _totalCoin);
        }

        public Vector2 CoinPosition()
        {
            return _coin.position;
        }

        public void AddCoin(int coin,float time)
        {
            StartCoroutine(IECountCoin(coin, time));
        }

        private void OnClickStart()
        {
            _animMainMenu.Play("Menu-hide");
            StartCoroutine(IEStart());
        }

        private void OnClickNext()
        {
            int n = _currentLevel % 50;
            _nextLV = Instantiate(Resources.Load<GameObject>("Levels/Level" + n));
            _nextLV.transform.position = Vector3.up * 10f;
            _objLevel.transform.DOMoveY(-10f, 1f);
            _nextLV.transform.DOMoveY(0f, 1f).OnComplete(this.OnLevelDone);
            _animComplete.Play("panelcomplete-hide");
            StartCoroutine(IEHideComplete());
            this.FormatNumber(_listLevel, _currentLevel);
            _mainCam.backgroundColor = _colorBG[Random.Range(0, _colorBG.Count)];
            AdsManager.Instance?.ShowInterAds();
        }

        private void OnClickRewardX5()
        {
            AdsManager.Instance?.ShowAds(() =>
            {
                this.AddCoin(300, 0.5f);
            });
        }

        private void OnLevelDone()
        {
            Destroy(_objLevel);
            _objLevel = _nextLV;
            GameMgr.Instance?.SetReady(true);
        }

        private IEnumerator IEStart()
        {
            yield return new WaitForSeconds(0.5f);
            _animMainMenu.gameObject.SetActive(false);
            GameMgr.Instance?.SetReady(true);
        }

        private IEnumerator IEHideComplete()
        {
            yield return new WaitForSeconds(0.5f);
            _animComplete.gameObject.SetActive(false);
        }
    }
}
