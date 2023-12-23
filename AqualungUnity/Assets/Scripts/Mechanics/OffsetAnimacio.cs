using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetAnimacio : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        float randomOffset = Random.Range(0f, 1f);
        _animator.SetFloat("Offset", randomOffset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
