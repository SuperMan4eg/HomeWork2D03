using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _currStrength;
    [SerializeField] private float _maxStrength;
    [SerializeField] private float _soundRate;

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
            StartCoroutine(VolumeUp());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _audioSource.loop = false;
        StartCoroutine(VolumeDown());
    }

    private IEnumerator VolumeUp()
    {
        while (_audioSource.volume < _maxStrength)
        {
            _audioSource.volume += Mathf.MoveTowards(_currStrength, _maxStrength, _soundRate * Time.deltaTime);

            yield return null;
        }

        StopCoroutine(VolumeUp());
    }

    private IEnumerator VolumeDown()
    {
        while (_audioSource.volume > _currStrength)
        {
            _audioSource.volume -= Mathf.MoveTowards(_currStrength, _maxStrength, _soundRate * Time.deltaTime);

            yield return null;
        }

        StopCoroutine(VolumeDown());
    }
}
