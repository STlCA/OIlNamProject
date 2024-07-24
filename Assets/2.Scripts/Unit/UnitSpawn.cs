using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    private Transform[] spawnArray;
    private List<Transform> spawnPoints;

    private List<int> spawnUnitID = new();

    private void Start()
    { 
        PointsInit();
    }

    private void PointsInit()
    {
        spawnArray = GetComponentsInChildren<Transform>();
        spawnPoints  = new List<Transform>(spawnArray);
        spawnPoints.Remove(spawnPoints[0]);
    }

    public Vector3 RandomUnitSpawn()
    {
        if (spawnPoints.Count == 0)
        {
            //TODO : 동료를 더이상 소환할수없습니다 팝업 혹은 글귀 올라가면서 투명해지는

            Debug.Log("동료를 더이상 소환할수없습니다.");
            return Vector3.zero;
        }

        return RandomSpawnPoint().position;
    }

    private Transform RandomSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Count);

        Transform transform = spawnPoints[index];
        spawnPoints.Remove(spawnPoints[index]);

        return transform;
    }



/*    private void DropItem(Vector3 target, int count, int ID)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject go = Instantiate(dropItemPrefab);
            go.GetComponentInChildren<SpriteRenderer>().sprite = itemDatabase.GetItemByKey(ID).SpriteList[0];
            go.GetComponent<DropItem>().id = ID;
            go.transform.position = new Vector3(target.x, target.y + 0.5f);
        }
    }
    private void DropItem(Vector3 target, int count, DropItemType type)
    {
        for (int i = 0; i < count; ++i)
        {
            ItemInfo item = RandomItem(type);

            GameObject go = Instantiate(dropItemPrefab);
            go.GetComponentInChildren<SpriteRenderer>().sprite = item.SpriteList[0];
            go.GetComponent<DropItem>().id = item.ID;
            go.transform.position = new Vector3(target.x, target.y + 0.5f);
        }
    }
    private ItemInfo RandomItem(DropItemType type)
    {
        ItemInfo[] spawnItem;

        switch (type)
        {
            case DropItemType.Stone:
            default:
                spawnItem = new ItemInfo[5];//돌추가되면 갯수바까야함
                for (int i = 0; i < spawnItem.Length; ++i)
                {
                    spawnItem[i] = itemDatabase.GetItemByKey(int.Parse("401" + (i + 1).ToString()));
                }
                break;
        }

        int dropIndex = 0;
        float total = 0;
        float[] itemPercent = new float[spawnItem.Length];

        for (int i = 0; i < spawnItem.Length; i++)
        {
            float percent = spawnItem[i].DropPercent;
            itemPercent[i] = percent;
            total += percent;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < itemPercent.Length; i++)
        {
            if (randomPoint <= itemPercent[i])
            {
                dropIndex = i;
                break;
            }
            else
                randomPoint -= itemPercent[i];
        }

        return spawnItem[dropIndex];
    }*/
}
