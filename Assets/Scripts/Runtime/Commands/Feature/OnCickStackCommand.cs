using Runtime.Managers;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Commands.Feature
{
    public class OnClickStackCommand
    {
        private readonly FeatureManager _featureManager;
        private int _newPriceTag;
        private byte _stackLevel;

        public OnClickStackCommand(FeatureManager featureManager)
        {
            _featureManager = featureManager;
            // _newPriceTag = newPriceTag;
            // _stackLevel = stackLevel;
        }

        internal void Execute()
        {
            _stackLevel = CoreGameSignals.Instance.onGetStackLevel();
            _newPriceTag = (int)(ScoreSignals.Instance.onGetMoney() -
                                 ((Mathf.Pow(2, Mathf.Clamp(--_stackLevel, 0, 10)) * 100)));
            _stackLevel += 1;
            ScoreSignals.Instance.onSendMoney?.Invoke(_newPriceTag);
            UISignals.Instance.onSetMoneyValue?.Invoke(_newPriceTag);
            _featureManager.SaveFeatureData();
        }
    }
}