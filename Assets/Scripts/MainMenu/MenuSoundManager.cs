using UnityEngine;

namespace DefaultNamespace
{
    public class MenuSoundManager : MonoBehaviour
    {
        [SerializeField] public AudioSource _soundManager;
        [SerializeField] public AudioClip _buttonHover;
        [SerializeField] public AudioClip _buttonClick;
        [SerializeField] public AudioClip _startButton;
        [SerializeField] public AudioClip _nextShipButton;

        public void HoverSound()
        {
            _soundManager.PlayOneShot(_buttonHover);
        }
        
        public void ClickSound()
        {
            _soundManager.PlayOneShot(_buttonClick);
        }
        
        public void StartSound()
        {
            _soundManager.PlayOneShot(_startButton);
        }
        
        public void NextShipSound()
        {
            _soundManager.PlayOneShot(_nextShipButton);
        }
    }
}