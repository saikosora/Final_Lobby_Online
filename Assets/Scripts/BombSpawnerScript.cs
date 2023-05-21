using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class BombSpawnerScript : NetworkBehaviour
{
    public GameObject bombPrefab;
    private List<GameObject> spawnedBomb = new List<GameObject>();
    private OwnerNetworkAnimator ownerNetworkAnimator;

    private void Start()
    {
        ownerNetworkAnimator = GetComponent<OwnerNetworkAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ownerNetworkAnimator.SetTrigger("PuttingDown");
            SpawnBombServerRpc();
        }
    }

    [ServerRpc]
    void SpawnBombServerRpc()
    {
        Vector3 spawnPos = transform.position
            + (transform.forward * 0.6f) + (transform.up * 0.8f);
        Quaternion spawnRot = transform.rotation;
        GameObject bomb = Instantiate(bombPrefab, spawnPos, spawnRot);
        spawnedBomb.Add(bomb);
        bomb.GetComponent<BombScript>().bombSpawner = this;
        bomb.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc (RequireOwnership = false)]
    public void DestroyServerRpc(ulong networkObjectId)
    {
        GameObject toDestroy = findBombFromNetworkId(networkObjectId);
        if (toDestroy == null) return;
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedBomb.Remove(toDestroy);
        Destroy(toDestroy);
    }

    private GameObject findBombFromNetworkId(ulong networkObjectId)
    {
        foreach (GameObject bomb in spawnedBomb)
        {
            ulong bombId = bomb.GetComponent<NetworkObject>().NetworkObjectId;
            if (bombId == networkObjectId) { return bomb; }
        }
        return null;
    }
}
