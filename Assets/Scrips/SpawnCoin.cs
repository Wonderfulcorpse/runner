using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public Transform SpawnPos;
    public GameObject Coin;
    public float time;

    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    void Repeat()
    {
        StartCoroutine(SpawnCoins());
    }

    IEnumerator SpawnCoins()
    {
        yield return new WaitForSeconds(time);
        Instantiate(Coin, SpawnPos.position, Quaternion.identity);
        Repeat();
    }
}
