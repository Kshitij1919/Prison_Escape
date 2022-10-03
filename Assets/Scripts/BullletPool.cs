using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletPool : MonoBehaviour
{
    [SerializeField] short poolSize;
    [SerializeField]private GameObject bullet;
    private List<GameObject> bulletPool;


    private void Awake()
    {
        Init();
    }

    void Init()
    {
        bulletPool = new List<GameObject>();    
        GameObject temp = null;

        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(bullet, transform);
            temp.SetActive(false);
            bulletPool.Add(temp);
            
        }
    }

    public void spawn(Transform parent)
    {
        GameObject temp = null;

        for (int i = 0; i < poolSize; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                temp = bulletPool[i];
                temp.transform.position = parent.transform.position;
                temp.SetActive(true);
                return;
            }
        }
    }

    
}
