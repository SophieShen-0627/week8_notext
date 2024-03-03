using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool IsDestroyed = false;
    private bool IsFirstTime = true;
    [SerializeField] ParticleSystem DestroyParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed && IsFirstTime)
        {
            DestroyParticle.Play();
            IsFirstTime = false;
            GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        }
    }
}
