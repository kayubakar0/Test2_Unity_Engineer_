using System.Collections;
using UnityEngine;

namespace TestResources.Test3
{
    public class ChaseState : IBaseState
    {
        public void EnterState(DeerBehaviour deer)
        {
            
        }
        
        public void UpdateState(DeerBehaviour deer)
        {
            if (deer.mushroom.activeSelf)
            {
                deer.NavMeshAgent.destination = deer.mushroom.transform.position;
                if (Vector3.Distance(deer.transform.position, deer.mushroom.transform.position) < deer.chaseDistance)
                {
                    deer.EatMushroom();
                }
            }
        }
        
        public void ExitState(DeerBehaviour deer)
        {
            
        }

        
    }
}
