using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawn
{
    public bool canSpawn;
    public Vector3 pos;
}

public class UnitSpawn : MonoBehaviour
{
    private Transform[] spawnArray;
    private List<Vector3> spawnPoints;

    private List<int> spawnUnitID = new();

    private void Start()
    {
        PointsInit();
    }

    private void PointsInit()
    {
        spawnArray = GetComponentsInChildren<Transform>();
        spawnPoints = new List<Vector3>();

        foreach (Transform t in spawnArray)
        {
            spawnPoints.Add(t.position);
        }

        spawnPoints.Remove(spawnPoints[0]);
    }

    public CanSpawn RandomUnitSpawn()
    {
        CanSpawn canSpawn = new CanSpawn();

        Debug.Log("스폰포인트 : " + spawnPoints.Count);

        if (spawnPoints.Count == 0)
        {
            canSpawn.canSpawn = false;
            canSpawn.pos = Vector3.zero;

            return canSpawn;
        }

        canSpawn.canSpawn = true;
        canSpawn.pos = RandomSpawnPoint();

        return canSpawn;
    }

    private Vector3 RandomSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Count);

        Vector3 pos = new();
        pos = spawnPoints[index];
        spawnPoints.Remove(spawnPoints[index]);

        return pos;
    }

    public void PlusSpawnPoint(Vector3 pos)
    {
        spawnPoints.Add(pos);
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
