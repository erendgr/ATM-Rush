using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        private byte _currentIncomeLevel;
        private byte _currentStackLevel;
        
        private void OnEnable()
        {
            SubscribeEvents();

            OpenStartPanel();

            _currentIncomeLevel = GetIncomeLevel();
            _currentStackLevel = GetStackLevel();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetIncomeLevel += () => _currentIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel += () => _currentStackLevel;
        }
        
        private byte GetIncomeLevel()
        {
            if (!ES3.FileExists()) return 0;
            return (byte)(ES3.KeyExists("IncomeLevel") ? ES3.Load<int>("IncomeLevel") : 1);
        }
        
        private byte GetStackLevel()
        {
            if (!ES3.FileExists()) return 0;
            return (byte)(ES3.KeyExists("StackLevel") ? ES3.Load<int>("StackLevel") : 1);
        }

        private void OpenStartPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 0);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 1);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Shop, 2);
        }

        private void OnLevelInitialize(byte levelValue)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 0);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 1);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Shop, 2);
            UISignals.Instance.onSetNewLevelValue?.Invoke(levelValue);
        }

        public void OnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(0);
            CoreUISignals.Instance.onClosePanel?.Invoke(2);
            // CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Follow);
            CameraSignals.Instance.onSetCameraTarget?.Invoke();
        }

        private void OnOpenWinPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 2);
        }

        private void OnOpenFailPanel()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 2);
        }

        public void OnNextLevel()
        {
            CoreGameSignals.Instance.onNextLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void OnRestartLevel()
        {
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        private void OnLevelFailed()
        {
            OnOpenFailPanel();
        }

        private void OnLevelSuccessful()
        {
            OnOpenWinPanel();
        }

        public void OnIncomeUpdate()
        {
            _currentIncomeLevel++;
            UISignals.Instance.onClickIncome?.Invoke();
            UISignals.Instance.onSetIncomeLvlText?.Invoke();
        }

        public void OnStackUpdate()
        {
            _currentStackLevel++;
            UISignals.Instance.onClickStack?.Invoke();
            UISignals.Instance.onSetStackLvlText?.Invoke();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetIncomeLevel -= () => _currentIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel -= () => _currentStackLevel;
        }


        private void OnReset()
        {
            //CoreUISignals.Instance.onCloseAllPanels?.Invoke();
        }
    }
}