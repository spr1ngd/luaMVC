using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject
{
    [TaskCategory("Basic/GameObject")]
    [TaskDescription("Returns Success if the layermasks match, otherwise Failure.")]
    public class CompareLayerMask : Conditional
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The layermask to compare against")]
        public LayerMask layermask;

        public override TaskStatus OnUpdate()
        {
            return (GetDefaultGameObject(targetGameObject.Value).layer & layermask.value) != 0 ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}