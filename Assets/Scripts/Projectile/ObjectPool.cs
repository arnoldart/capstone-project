using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab; // Prefab projectile
    public int poolSize = 10; // Jumlah awal dalam pool

    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        // Inisialisasi pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform); // Jadikan child dari PoolObject
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        // Cari object yang tidak aktif
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // Jika tidak ada, buat object baru dan tambahkan ke pool
        GameObject newObj = Instantiate(prefab, transform); // Jadikan child dari PoolObject
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }
}
