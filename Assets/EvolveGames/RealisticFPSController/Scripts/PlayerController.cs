//by EvolveGames
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
        [SerializeField] public ItemChange Items;
        [SerializeField, Range(1, 10)] float walkingSpeed = 3.0f;
        [Range(0.1f, 5)] public float CroughSpeed = 1.0f;
        [SerializeField, Range(2, 20)] float RuningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;
        [Space(20)]
        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float CroughHeight = 1.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]
        [Header("Climbing")]
        [SerializeField] bool CanClimbing = true;
        [SerializeField, Range(1, 25)] float Speed = 2f;
        bool isClimbing = false;

        [Space(20)]
        [Header("HandsHide")]
        [SerializeField] bool CanHideDistanceWall = true;
        [SerializeField, Range(0.1f, 5)] float HideDistance = 1.5f;
        [SerializeField] int LayerMaskInt = 1;

        [Space(20)]
        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;
        [SerializeField] private LayerMask _obstructionMask;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _standIcon;
        [SerializeField] private Sprite _croughIcon;
        [SerializeField] private Sprite _runIcon;
        [SerializeField] private AudioClip[] _sends;
        [SerializeField] private AudioClip _runAudioClip;
        [SerializeField] private AudioClip _croughAudioClip;
        private AudioSource _audioSource;
        private bool _isMovePressed;

        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        bool isCrough = false;
        float InstallCroughHeight;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        Vector3 InstallCameraMovement;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        float installGravity;
        bool WallDistance;
        [HideInInspector] public float WalkingValue;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            if (Items == null && GetComponent<ItemChange>()) Items = GetComponent<ItemChange>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = Camera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = RuningSpeed;
            installGravity = gravity;
            WalkingValue = walkingSpeed;
        }

        void Update()
        {
            RaycastHit CroughCheck;
            RaycastHit ObjectCheck;

            if (!characterController.isGrounded && !isClimbing)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            isRunning = !isCrough ? CanRunning ? Input.GetKey(KeyCode.LeftShift) : false : false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;
            _isMovePressed = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
            if (isRunning)
            {
                RunningValue = Mathf.Lerp(RunningValue, RuningSpeed, timeToRunning * Time.deltaTime);
                PlaySoundRun();
                ChangeIcon(_runIcon);
            }
            else
            {
                RunningValue = WalkingValue;
                if (_isMovePressed && !characterController.isGrounded)
                {
                    if (isCrough)
                        PlaySoundCrough();
                    else
                        PlaySoundSens();
                }
                ChangeIcon(_standIcon);
            }

            if (_isMovePressed == false)
                _audioSource.Stop();

            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * vertical) + (right * horizontal);

            if (Input.GetKeyDown(_player.PlayerInput.JumpKey) && canMove && characterController.isGrounded && !isClimbing)
            {
                Debug.Log("jump");
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (Cursor.lockState == CursorLockMode.Locked && canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

                if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
                else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
            }

            if (Input.GetKeyDown(_player.PlayerInput.CroughKey))
            {
                if (isCrough == false)
                {
                    isCrough = true;
                }
                else
                {
                    if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, _obstructionMask))
                    {
                        if (characterController.height != InstallCroughHeight)
                        {
                            isCrough = false;
                        }
                    }
                }
            }

            if (isCrough)
            {
                float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, CroughSpeed, 6 * Time.deltaTime);
                ChangeIcon(_croughIcon);
            }
            else
            {
                float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
            }



            //if(WallDistance != Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt) && CanHideDistanceWall)
            //{
            //    WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, HideDistance, LayerMaskInt);
            //    Items.ani.SetBool("Hide", WallDistance);
            //    Items.DefiniteHide = WallDistance;
            //}
        }

        private void PlaySoundRun()
        {
            if (_audioSource.isPlaying == false)
                _audioSource.PlayOneShot(_runAudioClip);
        }

        private void PlaySoundCrough()
        {
            if (_audioSource.isPlaying == false)
                _audioSource.PlayOneShot(_croughAudioClip);
        }

        private void PlaySoundSens()
        {
            int random = Random.Range(0, _sends.Length);

            if (_audioSource.isPlaying == false)
                _audioSource.PlayOneShot(_sends[random]);
        }

        private void ChangeIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Ladder" && CanClimbing)
        //    { 
        //        CanRunning = false;
        //        isClimbing = true;
        //        WalkingValue /= 2;
        //        Items.Hide(true);
        //    }
        //}
        //private void OnTriggerStay(Collider other)
        //{
        //    if (other.tag == "Ladder" && CanClimbing)
        //    {
        //        moveDirection = new Vector3(0, Input.GetAxis("Vertical") * Speed * (-Camera.localRotation.x / 1.7f), 0);
        //    }
        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.tag == "Ladder" && CanClimbing)
        //    {
        //        CanRunning = true;
        //        isClimbing = false;
        //        WalkingValue *= 2;
        //        Items.ani.SetBool("Hide", false);
        //        Items.Hide(false);
        //    }
        //}

    }
}