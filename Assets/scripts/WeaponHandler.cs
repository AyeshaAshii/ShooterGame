using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using StarterAssets;
using UnityEngine.Audio;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{
    [Header("references")]
    private ThirdPersonController controller;
    private Animator animator;
    private AudioSource source;
    [SerializeField] CinemachineThirdPersonFollow follow;
    public Camera mainCam;
    public Transform barrelEnd;

    [Header("Shooting")]
    public float firerate = 0.09f;
    public float blendTime = 0.07f;
    public string stateName = "Fire_Rifle";
    public AudioClip fireClip;
    public ParticleSystem muzzleflash;
    public bool canShoot = true;
    public float shootingRange = 100f;
    public float bulletDamage = 10f;

    [Header("Bullets")]
    public int currentBullets;
    public int bulletsInMag = 30;
    public int totalBullets = 100;
    public float reloadTime = 1f;
    public GameObject impact;

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
    public bool isReloading = false;

    [Header("UI")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private Text bulletsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();

        defaultverticalArmLength = follow.VerticalArmLength;
        defaultcameraside = follow.CameraSide;
        defaultcameraDistance = follow.CameraDistance;
        source = GetComponent<AudioSource>();

        currentBullets = bulletsInMag;
        bulletsText.text = currentBullets.ToString() + " / " + totalBullets.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // INPUT
        Aiming = Input.GetButton("Fire2");
        bool shootInp = Input.GetButton("Fire1");
        bool reloadInp = Input.GetKeyDown(KeyCode.R);
        

        // ANIMATIONS
        animator.SetBool("Aiming", Aiming);
        controller.strafe = Aiming;

        

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

        //RELOAD
        if(reloadInp && !isReloading && totalBullets > 0)
        {
            StartCoroutine(Reload());
        }

        // SHOOT
        if (shootInp && Aiming && currentBullets > 0 && !isReloading)
            Shoot();

        bulletsText.text = currentBullets.ToString() + " / " + totalBullets.ToString();

    }

    private void Shoot() 
    {
        if (!canShoot)
            return;
        StartCoroutine("ResetFireRate");
        source.PlayOneShot(fireClip);
        muzzleflash.Play();
        animator.CrossFadeInFixedTime(stateName, blendTime);

        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        Vector3 target;
        if(Physics.Raycast(ray, out hit, shootingRange))
        {
            target = hit.point;

            var health = hit.collider.GetComponent<TargetHealth>();

            if (health != null)
            {
                health.TakeDamage(bulletDamage);
            }

            GameObject bulletImpact = GameObject.Instantiate(impact, target, Quaternion.LookRotation(hit.normal));
            Destroy(bulletImpact, 5f);
        }
        else
        {
            target = ray.GetPoint(shootingRange);
        }

        currentBullets--;

        Debug.DrawRay(barrelEnd.position, target, Color.red, 1f);
        
    }


    private IEnumerator ResetFireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(firerate);
        canShoot =true;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        int bulletsToLoad = bulletsInMag - currentBullets;
        currentBullets += bulletsToLoad;
        totalBullets -= bulletsToLoad;
        isReloading=false;
    }
}
