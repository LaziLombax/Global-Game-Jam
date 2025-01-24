//using UnityEngine;
//using Cinemachine;

//public class CinemachinePOVExtension : CinemachineExtension
//{
//    [SerializeField]
//    public float horizontalSpeed = 10f;
//    [SerializeField]
//    public float verticalSpeed = 10f; 
//    [SerializeField]
//    public float pan = 0f;
//    [SerializeField]
//    public float dutch = 0f;
//    [SerializeField]
//    private float clampAngle = 80f;
//    private InputHandler inputHandler;
//    public Vector3 startingRotation;
//    private CinemachineRecomposer recomposer;
//    public float timeChangeFactor = 1f;

//    protected override void Awake()
//    {
//        startingRotation = new Vector3(90,0,0);
//        inputHandler = InputHandler.Instance;
//        base.Awake();
//        Cursor.visible = false;
//        recomposer = GetComponent<CinemachineRecomposer>();
//        recomposer.m_Pan = pan;
//        recomposer.m_Dutch = dutch;
//    }
//    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
//    {
//        if (vcam.Follow)
//        {
//            if (stage == CinemachineCore.Stage.Aim)
//            {
//                if (startingRotation == null) startingRotation = transform.rotation.eulerAngles;
//                if (inputHandler == null) return;
//                Vector2 deltaInput = inputHandler.GetMouseDelta();
//                recomposer.m_Pan = pan;
//                recomposer.m_Dutch = dutch;
//                startingRotation.x += deltaInput.x * verticalSpeed * timeChangeFactor * Time.deltaTime;
//                startingRotation.y += deltaInput.y * horizontalSpeed * timeChangeFactor * Time.deltaTime;
//                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
//                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
//            }
//        }
//    }
//}
