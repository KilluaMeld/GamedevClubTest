using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> where T : MonoBehaviour
{
    public T prefab { get; set; }
    public bool autoExpand { get; set; }

    public Transform container { get; set; }
    private List<T> pool;

    public ObjectsPool(T prefab, int count)
    {
        this .prefab = prefab;  
        this.container = null;
        CreatePool(count);
    }
    public ObjectsPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
      
    }
    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = GameObject.Instantiate(this.prefab, this.container);
        createObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createObject);
        return createObject;
    }
    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
        {
            return element;
        }

        if (this.autoExpand)
        {
            return this.CreateObject(true);
        }

        throw new Exception($"There is no free Element in pool of type {typeof(T)}");
    }
}
