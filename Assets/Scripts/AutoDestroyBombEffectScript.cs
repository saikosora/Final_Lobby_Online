using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AutoDestroyBombEffectScript : NetworkBehaviour
{
    public float delayTime = 1.0f;
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        if (ps && !ps.IsAlive()) { DestoryEffect(); }
    }

    void DestoryEffect()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject, delayTime);
    }
}
