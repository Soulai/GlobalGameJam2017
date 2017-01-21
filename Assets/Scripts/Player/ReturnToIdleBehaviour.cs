using UnityEngine;

namespace Player
{
    public class ReturnToIdleBehaviour : StateMachineBehaviour
    {
        private PlayerActions _playerActions = null;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_playerActions == null)
            {
                _playerActions = animator.transform.parent.GetComponent<PlayerActions>();
            }

            _playerActions.PunchInProgress = false;
        }
    }
}