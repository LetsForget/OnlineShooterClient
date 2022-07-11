using RiptideNetworking;
using UnityEngine;

namespace GameLogic
{
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
    }
}