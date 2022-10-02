using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace ChargeNow
{
    public class PluginCtrl : MonoBehaviour
    {
        [SerializeField] private Sprite _sprNormal;
        [SerializeField] private Sprite _sprPluged;
        [SerializeField] private GameObject _emptyPin;
        [SerializeField] private GameObject _fullPin;
        [SerializeField] private SpriteRenderer _batery;
        [SerializeField] private GameObject _limitPoint;
        [SerializeField] private GameObject _iphone;


        private Vector3 screenPoint;
        private Vector3 offset;
        private Rigidbody2D _myRigid;
        private GameObject _ocam;
        private SpriteRenderer _myRender;
        private Transform _checkPlug;
        private float _minBatery, _maxBatery, _maxDistance;
        private bool _isPluged, _isFull;
        private BaseCtr _baseCtr;

        // Start is called before the first frame update
        void Start()
        {
            _myRigid = GetComponent<Rigidbody2D>();
            _myRender = this.GetComponent<SpriteRenderer>();
            _myRigid.bodyType = RigidbodyType2D.Kinematic;
            _checkPlug = this.transform.GetChild(0);
            _minBatery = 0f;
            _maxBatery = 0.6f;
            _maxDistance = Vector2.Distance(_limitPoint.transform.position, _iphone.transform.position);
        }

        private void Update()
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


        void OnMouseDown()
        {
            if (!_baseCtr.IsReady()) return;

            screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = screenPoint - this.transform.position;
            _myRigid.bodyType = RigidbodyType2D.Dynamic;
            _myRender.sprite = _sprNormal;
            _isFull = _isPluged = false;
            _fullPin.SetActive(false);
        }

        void OnMouseDrag()
        {
            if (!_baseCtr.IsReady()) return;

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            curPosition.z = 0f;

            float distance = Vector2.Distance(_iphone.transform.position, curPosition);
            if (distance < _maxDistance)
                _myRigid.MovePosition(curPosition);
        }

        private void OnMouseUp()
        {
            if (!_baseCtr.IsReady()) return;

            _myRigid.bodyType = RigidbodyType2D.Kinematic;

            Collider2D hit = Physics2D.OverlapCircle(this._checkPlug.position, 0.2f, 1 << 8);
            if (hit)
            {
                this.transform.position = hit.transform.position;
                _myRender.sprite = _sprPluged;
                _emptyPin.SetActive(false);
                _isPluged = true;
                _isFull = _batery.size.y >= _maxBatery;
                //    Debug.Log("Mouse Uppp");
            }

        }

        public void SetDelegate(BaseCtr checkDone)
        {
            _baseCtr = checkDone;
        }

        public bool IsCharge()
        {
            return _fullPin.activeInHierarchy;
        }


    }
}
