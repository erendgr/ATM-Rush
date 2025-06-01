using Runtime.Enums;
using Runtime.Signals;
using Unity.Cinemachine;
using UnityEngine;

namespace Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]private CinemachineCamera virtualCamera;

        [SerializeField]private GameObject fakePlayer;

        #endregion

        #region Private Variables

        private CameraStates CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.InitializeCamera;
        private Animator _camAnimator;

        private readonly string _initializeCamera = "InitializeCamera";
        private readonly string _playerCamera = "PlayerCamera";
        private readonly string _miniGameCamera = "MiniGameCamera";
        
        #endregion
        #endregion

        private void Awake()
        {
            virtualCamera = transform.GetChild(1).GetComponent<CinemachineCamera>();
            _camAnimator = GetComponent<Animator>();
            GetInitialPosition();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onMiniGameStart += OnMiniGame;
            CoreGameSignals.Instance.onPlay += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onMiniGameStart -= OnMiniGame;
            CoreGameSignals.Instance.onPlay -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void SetCameraStates()
        {
            if (CameraStateController == CameraStates.InitializeCamera)
            {
                _camAnimator.Play(_initializeCamera);
            }
            else if (CameraStateController == CameraStates.PlayerCamera)
            {
                _camAnimator.Play(_playerCamera);
            }
            else if (CameraStateController == CameraStates.MiniGameCamera)
            {
                virtualCamera = transform.GetChild(2).GetComponent<CinemachineCamera>();
                virtualCamera.Follow = fakePlayer.transform;
                _camAnimator.Play(_miniGameCamera);
            }
        }

        private void GetInitialPosition()
        {
            _initialPosition = virtualCamera.transform.localPosition;
        }

        private void OnMoveToInitialPosition()
        {
            virtualCamera.transform.localPosition = _initialPosition;
        }

        private void OnSetCameraTarget()
        {
            var playerManager = FindAnyObjectByType<PlayerManager>().transform;
            virtualCamera.Follow = playerManager;
            CameraStateController = CameraStates.PlayerCamera;
        }

        private void OnMiniGame()
        {
            CameraStateController = CameraStates.MiniGameCamera;
        }

        private void OnNextLevel()
        {
            CameraStateController = CameraStates.InitializeCamera;
        }

        private void OnReset()
        {
            CameraStateController = CameraStates.InitializeCamera;
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            virtualCamera = transform.GetChild(1).GetComponent<CinemachineCamera>();
            OnMoveToInitialPosition();
        }
    }
}