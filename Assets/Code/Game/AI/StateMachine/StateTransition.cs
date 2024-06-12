using ArenaShooter.Utils;

namespace ArenaShooter.AI
{
    public class StateTransition
    {
        private BaseState _nextState;
        private CompositeCondition _condition;

        public BaseState NextState { get { return _nextState; } }
        public CompositeCondition Condition { get { return _condition; } }

        public StateTransition(BaseState nextState)
        {
            _nextState = nextState;
            _condition = new CompositeCondition();
        }

        public bool IsConditionMet()
        {
            return _condition.IsTrue();
        }

    }
}