using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject goodItem;
    public GameObject badItem;

    public int goodItemCount = 30;
    public int badItemCount = 10;

    private List<GameObject> goodItemList = new List<GameObject>();
    private List<GameObject> badItemList = new List<GameObject>();

    public void SpawnItems()
    {
        foreach(GameObject obj in goodItemList)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in badItemList)
        {
            Destroy(obj);
        }

        goodItemList.Clear();
        badItemList.Clear();

        // GoodItem(슬라임)을 30개 스폰
        for(int i = 0; i < goodItemCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-23f, 23f), 0.05f, Random.Range(-23f, 23f));
            Quaternion rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
            goodItemList.Add(Instantiate(goodItem, transform.position + position, rotation, transform));
        }

        // BadItem(터틀)을 10개 스폰
        for(int i = 0; i < badItemCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-23f, 23f), 0.05f, Random.Range(-23f, 23f));
            Quaternion rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
            badItemList.Add(Instantiate(badItem, transform.position + position, rotation, transform));
        }
    }
}
