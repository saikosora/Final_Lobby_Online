using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BombScript : NetworkBehaviour
{
    public BombSpawnerScript bombSpawner;
    public GameObject bombEffectPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;

        if (collision.gameObject.tag == "Player")
        {
            ulong networkObjectId = GetComponent<NetworkObject>().NetworkObjectId;
            SpawnBombEffect();
            bombSpawner.DestroyServerRpc(networkObjectId);
        }
    }
    private void SpawnBombEffect()
    {
        GameObject bombEffect = Instantiate(bombEffectPrefab,
            transform.position, Quaternion.identity);
        bombEffect.GetComponent<NetworkObject>().Spawn();
    }
    //ParticleSystem ps;
    //ps.isAlive();
}
