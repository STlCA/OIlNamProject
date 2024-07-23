using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    private Transform[] spawnPoints;
    private List<Transform> spawnList;

    private void Start()
    {
        PointsInit();
    }

    private void PointsInit()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnList  = new List<Transform>(spawnPoints);
        spawnList.Remove(spawnList[0]);
        spawnPoints = spawnList.ToArray();

        spawnList.Clear();
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
