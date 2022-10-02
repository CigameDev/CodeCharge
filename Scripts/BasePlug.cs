using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class BasePlug : MonoBehaviour
    {
        [SerializeField] protected Sprite _sprNormal;//tat ca de protected de PlugPhone co the ke thua
        [SerializeField] protected Sprite _sprPluged;
        [SerializeField] protected GameObject _limitPoint;
        [SerializeField] protected GameObject _iphone;


        protected Vector3 screenPoint;//lay toa do cua chuot
        protected Vector3 offset;
        protected Rigidbody2D _myRigid;
        protected GameObject _ocam;
        protected SpriteRenderer _myRender;
        protected Transform _checkPlug;
        protected BaseCtr _baseCtr;
        protected float _maxDistance;
        protected bool _pluged;//trang thai cam hoac khong cam

        // Start is called before the first frame update
        void Start()
        {
            _myRigid = GetComponent<Rigidbody2D>();
            _myRender = this.GetComponent<SpriteRenderer>();
            _myRigid.bodyType = RigidbodyType2D.Kinematic;//khong cho tac dong vat ly
            _checkPlug = this.transform.GetChild(0);
            _maxDistance = Vector2.Distance(_limitPoint.transform.position, _iphone.transform.position);
            //khoang cach toi da cua day
            GameMgr.Instance?.AddPlug(this);
            this.Init();
            
        }

        protected virtual void Init()
        {
            this.OnCheckPlug();
        }

        protected virtual void ActionDown()
        {

        }

        protected virtual void ActionDrag()
        {

        }

        protected virtual void ActionUp()
        {

        }

        protected virtual void OnMouseDown()
        {
            if (!_baseCtr.IsReady()) return;

            screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = screenPoint - this.transform.position;
            _myRigid.bodyType = RigidbodyType2D.Dynamic;
            _myRender.sprite = _sprNormal;

            this.ActionDown();
        }

        protected virtual void OnMouseDrag()
        {
            if (!_baseCtr.IsReady()) return;

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
            curPosition.z = 0f;

            float distance = Vector2.Distance(_iphone.transform.position, curPosition);
            if (distance < _maxDistance)
                _myRigid.MovePosition(curPosition);

            this.ActionDrag();
        }

        protected virtual void OnMouseUp()
        {
            if (!_baseCtr.IsReady()) return;

            _myRigid.bodyType = RigidbodyType2D.Kinematic;
            this.OnCheckPlug();
        }

        private void OnCheckPlug()
        {
            Collider2D hit = Physics2D.OverlapCircle(this.transform.position, 0.2f, 1 << 8);//chi lay layer so 8
            
            if (hit)
            {
                this.transform.position = hit.transform.position;
                _myRender.sprite = _sprPluged;
                SoundMgr.Instance?.OnPlaySound(SoundType.Pluged);//neu instance !=null

                this.ActionUp();
            }
        }

        public virtual void SetDelegate(BaseCtr checkDone)
        {
            _baseCtr = checkDone;
        }

        public virtual bool IsCharge()
        {
            return true;
        }

    }
}
