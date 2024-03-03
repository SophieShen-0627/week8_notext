using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject Particle; // 需要池化的预制体
    private Queue<GameObject> objects = new Queue<GameObject>(); // 存储池化对象的队列

    public int initialSize = 20; // 初始池大小

    void Start()
    {
        // 初始化对象池
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = CreateNewObject();
            ReturnObject(obj);
        }
    }

    private GameObject CreateNewObject()
    {
        var obj = Instantiate(Particle);
        obj.SetActive(false); // 默认创建时不激活
        obj.transform.SetParent(transform); // 可选：设置为当前对象的子对象，以便于管理
        return obj;
    }

    public GameObject GetObject()
    {
        // 如果对象池为空，创建一个新对象
        if (objects.Count == 0)
        {
            return CreateNewObject();
        }

        // 从池中取出一个对象并返回
        var obj = objects.Dequeue();
        Debug.Log(obj.name);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        // 将对象返回池中
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}
