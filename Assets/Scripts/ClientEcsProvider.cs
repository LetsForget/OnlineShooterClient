﻿using GameLogic;
using RiptideNetworking;
using UnityEngine;

public class ClientEcsProvider : BaseEcsProvider
{
    public Client Client
    {
        set
        {
            if (inited)
            {
                return;
            }

            inited = true;
            systems.Inject(value);
            systems.Init();
        }
    }

    private bool inited = false;

    protected override void AddOneFrames()
    {
        base.AddOneFrames();

        systems.OneFrame<CharacterMovementUpdate>();
    }

    protected override void AddSystems()
    {
        base.AddSystems();

        systems.Add(new UpdateSendSystem());
        systems.Add(new NetworkSendSystem());
    }
    
    public void SpawnPlayer(ushort clientId)
    {
        SpawnSystem.Spawn(clientId, Vector3.zero);
    }

    public void DestroyPlayer(ushort clientId)
    {
        SpawnSystem.Destroy(clientId);
    }
}