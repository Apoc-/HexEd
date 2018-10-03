using System.Collections.Generic;
using System.Linq;

namespace HexEd.Actions
{
    public class ActionManager : Singleton<ActionManager>
    {
        // Contains the history of actions
        private readonly List<List<Action>> _actionHistory = new List<List<Action>>();

        // Contains the future of actions. Will be filled on revert and deleted on any change.
        private readonly List<List<Action>> _actionFuture = new List<List<Action>>();

        // The current action group that contains any changes made in the current step
        private List<Action> _currentActionGroup;

        public void StartNewActionGroup()
        {
            if (_currentActionGroup != null && _currentActionGroup.Count > 0)
            {
                _actionHistory.Add(new List<Action>(_currentActionGroup));
                _currentActionGroup = null;
            }

            _currentActionGroup = new List<Action>();
        }

        public void AddAndExecuteAction(Action action)
        {
            // Adding ANY action removes the whole action future!
            _actionFuture.Clear();

            _currentActionGroup.Add(action);
            action.Execute();
        }


        public void RevertLastStep()
        {
            StartNewActionGroup();

            if (_actionHistory.Count == 0)
                return;

            // Get the last ActionGroup, revert it and add it to the action future.
            var findLast = _actionHistory.Last();
            if (findLast == null)
                return;

            for (var i = findLast.Count - 1; i >= 0; i--)
            {
                findLast[i].Revert();
            }

            _actionFuture.Add(findLast);
            _actionHistory.Remove(findLast);
        }

        public void RedoNextStep()
        {
            StartNewActionGroup();

            if (_actionFuture.Count == 0)
                return;

            var actions = _actionFuture.Last();
            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.Execute();
            }

            _actionHistory.Add(actions);
            _actionFuture.Remove(actions);
        }
    }
}