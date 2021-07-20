using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnMoving : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _animator.SetBool("IsRunning", true);

            if (Input.GetKey(KeyCode.A))
            {
                _renderer.flipX = true;
            }
            else
                _renderer.flipX = false;
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }
}
