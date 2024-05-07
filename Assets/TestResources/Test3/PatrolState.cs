using UnityEngine;

namespace TestResources.Test3
{
    public class PatrolState : IBaseState
    {
        private bool _isMoving;
        private Vector3 _destination;
        
        public void EnterState(DeerBehaviour deer)
        {
            _isMoving = false;
            deer.animatorDeer.SetBool("isWalking", true);
        }
        
        public void UpdateState(DeerBehaviour deer)
        {
            if (deer.mushroom.activeSelf)
            {
                if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown(KeyCode.A))
                {
                    deer.SwictchState(deer.chaseState);
                }
            }

            if (!_isMoving)
            {
                _isMoving = true;
                int index = Random.Range(0, deer.waypoints.Count);
                _destination = deer.waypoints[index].position;
                deer.NavMeshAgent.destination = _destination;
            }
            else
            {
                if (Vector3.Distance(_destination, deer.transform.position) <= 1)
                {
                    _isMoving = false;
                }
            }
        }
        
        public void ExitState(DeerBehaviour deer)
        {
            
        }
    }
}
