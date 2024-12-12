namespace ComponentStateMachine
{
    /// <summary>
    /// blank states doesnt do anything but manage the its child states
    /// </summary>
    public class BlankState : State
    {
        public override void Do()
        {
            //noop
        }

        public override void Enter()
        {
           //noop
        }

        public override void Exit()
        {
           //noop
        }

        public override void FixedDo()
        {
           //noop
        }
    }
}

