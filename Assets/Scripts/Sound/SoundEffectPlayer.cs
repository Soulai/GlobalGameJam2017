using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class SoundEffectPlayer : MonoBehaviour
    {
        private static SoundEffectPlayer _instance;

        public static void PlayPositionedSound(string name, Vector3 origin, float pitch = 1.0f)
        {
            _instance.PlayPositionedSoundEffect(name, origin, pitch);
        }

        private Transform _playerOneTransform;
        private Transform _playerTwoTransform;
        private List<AudioSource> _voices;
        private Dictionary<string, AudioClip> _effects;

        public GameObject PlayerOne;
        public GameObject PlayerTwo;
        public int VoiceCount;
        public List<AudioClip> Effects;
        public float DistanceThreshold;

        private void Start()
        {
            _playerOneTransform = PlayerOne.transform;
            _playerTwoTransform = PlayerTwo.transform;

            _effects = new Dictionary<string, AudioClip>();
            foreach (AudioClip clip in Effects) { _effects.Add(clip.name, clip); }

            _voices = new List<AudioSource>();
            for (int i = 0; i < VoiceCount; i++) { _voices.Add(gameObject.AddComponent<AudioSource>()); }

            _instance = this;
        }

        private void PlayPositionedSoundEffect(string name, Vector3 origin, float pitch)
        {
            if (_effects.ContainsKey(name))
            {
                AudioSource voice = GetFirstAvailableVoice();

                if (voice != null)
                {
                    SetVoiceForPositionedPlay(voice, origin);

                    voice.pitch = pitch;
                    voice.clip = _effects[name];
                    voice.Play();
                }
            }
        }

        private AudioSource GetFirstAvailableVoice()
        {
            AudioSource voice = null;
            for (int i = 0; ((i < _voices.Count) && (voice == null)); i++)
            {
                if (!_voices[i].isPlaying) { voice = _voices[i]; }
            }
            return voice;
        }

        private void SetVoiceForPositionedPlay(AudioSource voice, Vector3 origin)
        {
            float playerOneDistance = Vector3.Distance(origin, _playerOneTransform.position);
            float playerTwoDistance = Vector3.Distance(origin, _playerTwoTransform.position);
            float totalDistance = playerOneDistance + playerTwoDistance;

            if ((playerOneDistance > DistanceThreshold) && (playerTwoDistance > DistanceThreshold))
            {
                voice.volume = 0.0f;
                Debug.Log("Both out of hearing range: " + playerOneDistance + " | " + playerTwoDistance);
            }
            else if (playerOneDistance > DistanceThreshold)
            {
                voice.panStereo = 1.0f;
                voice.volume = 1.0f - (playerTwoDistance / DistanceThreshold);
                Debug.Log("P2 only: " + voice.volume);
            }
            else if (playerTwoDistance > DistanceThreshold)
            {
                voice.panStereo = -1.0f;
                voice.volume = 1.0f - (playerOneDistance / DistanceThreshold);
                Debug.Log("P1 only: " + voice.volume);
            }
            else
            {
                voice.panStereo = (playerOneDistance / totalDistance) - (playerTwoDistance / totalDistance);
                voice.volume = totalDistance / (DistanceThreshold * 2.0f);
                Debug.Log("Both: " + voice.volume + " at pan " + voice.panStereo);
            }        
        }
    }
}
