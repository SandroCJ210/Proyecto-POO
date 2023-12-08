using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player player;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void Start()
    {
        CreateEmptyHearts();
        DrawHearts();
    }

    public void DrawHearts()
    {
        for(int i=0; i<hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(player.GetHealth() - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHearts()
    {
        float maxHealthRemainder = player.GetMaxHealth() % 2;
        int heartsToMake = (int)((player.GetMaxHealth() / 2) + maxHealthRemainder);

        for (int i = 0; i < heartsToMake; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab);
            newHeart.transform.SetParent(transform);

            HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
            heartComponent.SetHeartImage(HeartStatus.Empty);
            hearts.Add(heartComponent);
        }
    }
}
