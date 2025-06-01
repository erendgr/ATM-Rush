using Runtime.Commands.Feature;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class FeatureManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public FeatureManager()
        {
            _onClickIncomeCommand = new OnClickIncomeCommand(this);
            _onClickStackCommand = new OnClickStackCommand(this);
        }

        #endregion

        #region Private Variables

        private readonly OnClickIncomeCommand _onClickIncomeCommand;
        private readonly OnClickStackCommand _onClickStackCommand;

        #endregion

        #endregion

        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            UISignals.Instance.onClickIncome += _onClickIncomeCommand.Execute;
            UISignals.Instance.onClickStack += _onClickStackCommand.Execute;
        }

        private void UnSubscription()
        {
            UISignals.Instance.onClickIncome -= _onClickIncomeCommand.Execute;
            UISignals.Instance.onClickStack -= _onClickStackCommand.Execute;
        }

        private void OnDisable()
        {
            UnSubscription();
        }

        internal void SaveFeatureData()
        {
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }
    }
}