using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private class CharacterStateMachine
    {
        [SerializeField]
        private float maxClickDelay;
        private float lastClickTime;
        private bool isWalking;

        private delegate void State();
        private State activeState;

        public CharacterStateMachine()
        {
            setActiveState(idle);
            maxClickDelay = 0.85f;
            lastClickTime = Time.time;
            isWalking = false;
        }
        void idle()
        {
            isWalking = false;
            if (Input.GetMouseButtonDown(0))
            {
                lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
        }
        void walk_leftClick()
        {
            isWalking = true;
            if (Input.GetMouseButtonDown(0)) {
                lastClickTime = Time.time;
                setActiveState(walk_rightClick);
            }
            if (Time.time - lastClickTime > maxClickDelay)
            {
                setActiveState(idle);
            }
        }
        void walk_rightClick()
        {
            isWalking = true;
            if (Input.GetMouseButtonDown(1))
            {
                lastClickTime = Time.time;
                setActiveState(walk_leftClick);
            }
            if (Time.time - lastClickTime > maxClickDelay)
            {
                setActiveState(idle);
            }

        }
        void setActiveState(State state)
        {
            activeState = state;
        }
        public bool IsWalking 
        {
            get { return isWalking; }
        }
        public void Update()
        {
            activeState();
        }
    }

    [SerializeField]
    private GameObject mainCam;
    private Vector3 cameraLinearOffset;
    private Quaternion cameraAngularOffset;
    private CharacterController cc;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float linearSmoothRate;
    [SerializeField]
    private float angularSmoothRate;
    private float nodAngle;
    private CharacterStateMachine CSM;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        cameraLinearOffset = transform.InverseTransformPoint(mainCam.transform.position);
        cameraAngularOffset = Quaternion.Inverse(transform.rotation)*mainCam.transform.rotation;
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CSM = new CharacterStateMachine();
    }

    void Update() 
    {
        ComputeCameraTransform();
        PerformCharacterMoevement();
        CSM.Update();
    }
    void PerformCharacterMoevement()
    {
        cc.transform.rotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Input.GetAxis("Mouse X") * Time.deltaTime) * cc.transform.rotation;
        anim.SetBool("Walking", CSM.IsWalking);
        if (CSM.IsWalking)
        {
            cc.Move(transform.forward * speed * Time.deltaTime);
        }
    }

    void ComputeCameraTransform() 
    {
        Vector3 newCameraPosition = Vector3.Lerp(mainCam.transform.position, transform.TransformPoint(cameraLinearOffset), linearSmoothRate);
        mainCam.transform.position = newCameraPosition;
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, transform.rotation * cameraAngularOffset, angularSmoothRate);
        nodAngle += Input.GetAxis("Mouse Y") * rotationSpeed;
        //TODO: add nod rotation
    }
}