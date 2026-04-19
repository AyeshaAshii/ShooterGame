using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using Unity.Cinemachine;
using StarterAssets;

public class WeaponHandler : MonoBehaviour
{
    [Header("References")]
    private ThirdPersonController controller;
    private Animator animator;
    private AudioSource source;
    [SerializeField] private CinemachineThirdPersonFollow followCamera;
    public Camera mainCam;
    public Transform barrelEnd;

    [Header("Shooting")]
    public float fireRate = 0.1f;
    public float shootRange = 100f;
    public float damage = 10f;
    public string fireAnim = "Fire_Rifle";
    public AudioClip fireClip;
    public ParticleSystem muzzleFlash;

    private bool canShoot = true;

    [Header("Ammo")]
    public int currentAmmo = 30;
    public int maxAmmo = 30;
    public int reserveAmmo = 90;

    [Header("Aiming")]
    public bool Aiming { get; private set; }
    [SerializeField] private MultiAimConstraint aimIK;

    [SerializeField] private float aimFOV = 40f;
    private float defaultFOV;

    [Header("UI")]
    [SerializeField] private GameObject crosshair;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<ThirdPersonController>();
        source = GetComponent<AudioSource>();

        defaultFOV = mainCam.fieldOfView;
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        Aiming = Input.GetButton("Fire2");
        bool shootInput = Input.GetButton("Fire1");

        animator.SetBool("Aiming", Aiming);
        controller.strafe = Aiming;

        crosshair.SetActive(Aiming);

        // Camera FOV
        mainCam.fieldOfView = Mathf.Lerp(
            mainCam.fieldOfView,
            Aiming ? aimFOV : defaultFOV,
            10f * Time.deltaTime
        );

        // Shoot
        if (shootInput && Aiming && canShoot && currentAmmo > 0)
        {
            Shoot();
        }

        // IK weight
        aimIK.weight = Mathf.Lerp(aimIK.weight, Aiming ? 1 : 0, 10f * Time.deltaTime);
    }

    void Shoot()
    {
        canShoot = false;

        source.PlayOneShot(fireClip);
        muzzleFlash.Play();
        animator.CrossFadeInFixedTime(fireAnim, 0.05f);

        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange))
        {
            Debug.Log("Hit: " + hit.collider.name);

            var health = hit.collider.GetComponentInParent<TargetHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            GameObject impact = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            impact.transform.position = hit.point;
            Destroy(impact, 0.5f);
        }

        currentAmmo--;

        StartCoroutine(FireRateReset());
    }

    IEnumerator FireRateReset()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}