using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject Particle; // ��Ҫ�ػ���Ԥ����
    private Queue<GameObject> objects = new Queue<GameObject>(); // �洢�ػ�����Ķ���

    public int initialSize = 20; // ��ʼ�ش�С

    void Start()
    {
        // ��ʼ�������
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNewObject();
            ReturnObject(obj);
        }
    }

    private GameObject CreateNewObject()
    {
        var obj = Instantiate(Particle);
        obj.SetActive(false); // Ĭ�ϴ���ʱ������
        obj.transform.SetParent(transform); // ��ѡ������Ϊ��ǰ������Ӷ����Ա��ڹ���
        return obj;
    }

    public GameObject GetObject()
    {
        // ��������Ϊ�գ�����һ���¶���
        if (objects.Count == 0)
        {
            return CreateNewObject();
        }

        // �ӳ���ȡ��һ�����󲢷���
        var obj = objects.Dequeue();
        Debug.Log(obj.name);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        // �����󷵻س���
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}
