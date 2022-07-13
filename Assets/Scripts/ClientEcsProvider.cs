using GameLogic;
using RiptideNetworking;
using UnityEngine;

public class ClientEcsProvider : BaseEcsProvider
{
    private PlayerUpdateSystem<ClientMovementUpdate, ClientPlayerComponent> playerUpdatePositionSystem = null;

    private SpawnSystem<ClientPlayerComponent> spawnSystem = null;

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
    
    public void SpawnPlayer(ushort incomeClientId, int thisClientId)
    {
        spawnSystem.Spawn(incomeClientId, Vector3.zero, thisClientId);
    }

    public void DestroyPlayer(ushort clientId)
    {
        spawnSystem.Destroy(clientId);
        playerUpdatePositionSystem.OnDestroyPlayer(clientId);
    }

    public void AddUpdate(ClientMovementUpdate update)
    {
        playerUpdatePositionSystem.AddUpdate(update);
    }
    
    
    protected override void AddSystems()
    {
        base.AddSystems();
        systems.Add(spawnSystem = new SpawnSystem<ClientPlayerComponent>())
            .Add(new InputSystem())
            .Add(new PlayerObserveSystem())
            .Add(playerUpdatePositionSystem = new PlayerUpdateSystem<ClientMovementUpdate, ClientPlayerComponent>())
            .Add(new ClientMovementSystem())
            .Add(new ClientSendSystem())
            .Add(new ClientIdSetterSystem<ClientPlayerComponent>());
    }
}