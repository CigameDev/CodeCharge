using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChargeNow
{
    public class SoundMgr : MonoBehaviour
    {
        private static SoundMgr _instance;
        public static SoundMgr Instance => _instance;

        public List<Sound> AllSound;
        public AudioSource AudioCenter;


        private void Awake()
        {
            _instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        public void OnPlaySound(SoundType type)
        {
            Sound sound = this.GetSound(type);
            AudioCenter.clip = sound.Audio;
            AudioCenter.Play();
        }

        private Sound GetSound(SoundType type)
        {
            return AllSound.Find(x => x.Type == type);
        }

    }

    public enum SoundType
    {
        HomeCleaner,MaySay,Win,OpenTiVi,OpenIphone,Pluged,Coin,DenPin
    }

    [System.Serializable]
    public class Sound
    {
        public string Name;
        public SoundType Type;
        public AudioClip Audio;
    }
}