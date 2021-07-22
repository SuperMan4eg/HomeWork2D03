using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currStrength;
    [SerializeField] private float _maxStrength;
    [SerializeField] private float _soundRate;

    private float _startVolume;
    private float _endVolume = 1;
    private Coroutine _VolumeChangerJob;

    private void Start()
    {
        _audioSource.volume = _currStrength;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _audioSource.loop = true;
            _audioSource.Play();
            StartVolumeChanger(_soundRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _audioSource.loop = false;
        StartVolumeChanger(-_soundRate);
    }

    private void StartVolumeChanger(float soundRate)
    {
        if (_VolumeChangerJob != null)
        {
            StopCoroutine(_VolumeChangerJob);
        }
        _VolumeChangerJob = StartCoroutine(VolumeChanger(soundRate));
    }

    private IEnumerator VolumeChanger(float soundRate)
    {
        for (_startVolume = 0; _startVolume < _endVolume; _startVolume += soundRate * Time.deltaTime)
        {
            _audioSource.volume += Mathf.MoveTowards(_currStrength, _maxStrength, soundRate * Time.deltaTime);

            yield return null;
        }
    }
}
