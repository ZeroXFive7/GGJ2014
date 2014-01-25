using UnityEngine;

namespace UnityFSM
{
    class NullState : State
    {
        #region Public Methods

        public NullState(FSM fsm)
            : base(fsm) { }

        public override void Enter()
        {
            Debug.LogWarning("Entering " + Name);
        }

        public override void Exit()
        {
            Debug.LogWarning("Exiting " + Name);
        }

        #endregion
    }
}
