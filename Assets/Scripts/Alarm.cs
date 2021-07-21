using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currStrength;
    [SerializeField] private float _maxStrength;
    [SerializeField] private float _soundRate;

    private float _step;
    private bool _isEnter = false;
    private float _startVolume;
    private float _endVolume = 1;

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
            _isEnter = true;
            StartCoroutine(VolumeChanger());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _audioSource.loop = false;
        _isEnter = false;
        StartCoroutine(VolumeChanger());
    }

    private IEnumerator VolumeChanger()
    {
        if (_isEnter)
        {
            _step = _soundRate * Time.deltaTime;
        }
        else
        {
            _step = -_soundRate * Time.deltaTime;
        }

        for (_startVolume = 0; _startVolume < _endVolume; _startVolume += _step)
        {
            _audioSource.volume += Mathf.MoveTowards(_currStrength, _maxStrength, _step);

            yield return null;
        }
    }
}
