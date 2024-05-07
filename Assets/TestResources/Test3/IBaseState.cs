using UnityEngine;

namespace TestResources.Test3
{
    public interface IBaseState
    {
        public void EnterState(DeerBehaviour deer);
        public void UpdateState(DeerBehaviour deer);
        public void ExitState(DeerBehaviour deer);
    }
}
