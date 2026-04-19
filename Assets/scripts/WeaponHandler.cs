using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using StarterAssets;
using UnityEngine.Audio;

public class WeaponHandler : MonoBehaviour
{
    [Header("references")]
    private ThirdPersonController controller;
    private Animator animator;
    private AudioSource source;
    [SerializeField] CinemachineThirdPersonFollow follow;

    [Header("Shooting")]
    public float firerate = 0.09f;
    public float blendTime = 0.07f;
    public string stateName = "Fire_Rifle";
    public AudioClip fireClip;
    public ParticleSystem muzzleflash;
    public bool canShoot = true;

    [Header("Aiming")]
    [SerializeField] private float cameraTransitionSpeed = 7f;
    [SerializeField] private float ikTransitionSpeed = 10f;
    [SerializeField] private MultiAimConstraint aimIk;
    [Space(10)]
    [SerializeField] private float aimverticalArmLength = 0.2f;
    [SerializeField] private float aimCameraSide = 0.75f;
    [SerializeField] private float aimCameraDistance = 0.85f;
    private float defaultverticalArmLength;
    private float defaultcameraside;
    private float defaultcameraDistance;

    public bool Aiming { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject crosshair;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();

        defaultverticalArmLength = follow.VerticalArmLength;
        defaultcameraside = follow.CameraSide;
        defaultcameraDistance = follow.CameraDistance;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // INPUT
        Aiming = Input.GetButton("Fire2");
        bool shootInp = Input.GetButton("Fire1");

        

        // ANIMATIONS
        animator.SetBool("Aiming", Aiming);
        controller.strafe = Aiming;

        if (shootInp && Aiming)
            Shoot();

        // ADJUST CAMERA
        float targetverticalArmLength = Aiming ? aimverticalArmLength : defaultverticalArmLength;
        float targetside = Aiming ? aimCameraSide : defaultcameraside;
        float targetDistance = Aiming ? aimCameraDistance : defaultcameraDistance;

        follow.VerticalArmLength = Mathf.Lerp(follow.VerticalArmLength, targetverticalArmLength, cameraTransitionSpeed * Time.deltaTime);
        follow.CameraSide = Mathf.Lerp(follow.CameraSide, cameraTransitionSpeed, cameraTransitionSpeed * Time.deltaTime);
        follow.CameraDistance = Mathf.Lerp(follow.CameraDistance, targetDistance, cameraTransitionSpeed* Time.deltaTime);

        
        crosshair.SetActive(Aiming);


        float targetweight = Aiming ? 1 : 0;
        aimIk.weight = Mathf.Lerp(aimIk.weight, targetweight, ikTransitionSpeed * Time.deltaTime);

        // SHOOT
        
    }

    private void Shoot() 
    {
        if (!canShoot)
            return;
        StartCoroutine("ResetFireRate");
        source.PlayOneShot(fireClip);
        muzzleflash.Play();
        animator.CrossFadeInFixedTime(stateName, blendTime);
        
    }


    private IEnumerator ResetFireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(firerate);
        canShoot =true;
    }
}
