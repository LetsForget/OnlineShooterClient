using GameLogic;
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
    
    protected override void AddOneFrames()
    {
        base.AddOneFrames();

        systems.OneFrame<CharacterMovementUpdate>();
    }

    protected override void AddSystems()
    {
        base.AddSystems();

        systems.Add(new UpdateSendSystem());
    }
    
    public void SpawnPlayer(ushort incomeClientId, int thisClientId)
    {
        SpawnSystem.Spawn(incomeClientId, Vector3.zero, thisClientId);
    }

    public void DestroyPlayer(ushort clientId)
    {
        SpawnSystem.Destroy(clientId);
    }

    public void AddUpdate(CharacterMovementUpdate update)
    {
        UpdateReceiveSystem.AddUpdate(update);
    }
}