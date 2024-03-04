using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Box : MonoBehaviour
{
    public bool IsDestroyed = false;
    private bool IsFirstTime = true;

    public CinemachineTargetGroup targetGroup;
    [SerializeField] ParticleSystem DestroyParticle;
    [SerializeField] Transform PositionIndicator;
    [SerializeField] AudioClip Explosion;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0) PositionIndicator.transform.position = new Vector3(0, transform.position.y, 0);
        else PositionIndicator.transform.position = Vector3.zero;

        if (transform.position.y <= -6) IsDestroyed = true;

        if (IsDestroyed && IsFirstTime)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            RemoveFromTargetGroup();
            DestroyParticle.Play();
            IsFirstTime = false;
            GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
            if (GetComponentInChildren<Collider2D>()) GetComponentInChildren<Collider2D>().enabled = false;
        }
    }

    private void OnEnable()
    {
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
        // ��GameObject����ʱ��������ӵ�Cinemachine Target Group��
        FindObjectOfType<ReachTarget>().boxes.Add(this);
        AddToTargetGroup();
    }


    void AddToTargetGroup()
    {
        if (targetGroup != null)
        {
            // ������transform�Ƿ��Ѿ���targetGroup��
            bool alreadyInGroup = false;
            foreach (var member in targetGroup.m_Targets)
            {
                if (member.target == PositionIndicator)
                {
                    alreadyInGroup = true;
                    break;
                }
            }

            // �������group�У������
            if (!alreadyInGroup)
            {
                targetGroup.AddMember(PositionIndicator, 1, 1); // �����Ȩ�غͰ뾶���Ը�����Ҫ����
            }
        }
        else
        {
            Debug.LogError("CinemachineTargetGroup is not assigned.");
        }
    }

    void RemoveFromTargetGroup()
    {
        if (targetGroup != null)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            GetComponent<AudioSource>().PlayOneShot(Explosion);
            FindObjectOfType<ReachTarget>().boxes.Remove(this);
            targetGroup.RemoveMember(PositionIndicator);
            Debug.Log("removed");
        }
    }

}
