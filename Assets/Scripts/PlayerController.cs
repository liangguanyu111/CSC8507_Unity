using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PlayerController : MonoBehaviour
{
    float currentVelocity;
    public float smoothTime = 0.2f;
    public float moveSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce;
    public float shootDelay;

    public Transform groundCheck;

    private Animator ac;
    private Transform cameraTransform;
    private Rigidbody rb;

    public bool isGround;
    private CapsuleCollider capsuleCollider;

    private int weaponLevel = 1;
   

    public Weapon[] weaponList;

    bool aimMode = false;
    private void Start()
    {
        ac = this.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        shootDelay = 0.3f;
        Cursor.visible = false;
    }


    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 inputDir = input.normalized;

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        if(Input.GetMouseButton(1))
        {
            aimMode = true;
            transform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, 0);
        }
        else
        {
            aimMode = false;
        }

        if (inputDir!=Vector2.zero)
        {

            Vector3 moveDir = inputDir.x * transform.right + input.y * transform.forward;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);

            if (!aimMode)
            {
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * walkSpeed);
            }
            else
            {
                rb.MovePosition(transform.position + moveDir * Time.deltaTime * walkSpeed);
            }

            ac.SetBool("Move", true);
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        Vector3 startPoint = groundCheck.transform.position + new Vector3(0, 0.25f, 0);
        Vector3 endPoint = groundCheck.transform.position + new Vector3(0, 1.4f, 0);
        Collider[] colliders = Physics.OverlapCapsule(startPoint, endPoint, 0.5f, LayerMask.GetMask("Ground"));

        if (colliders.Length != 0)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * jumpForce);
        }

        PressWeapon PW;

        if (weaponList[weaponLevel - 1].TryGetComponent<PressWeapon>(out PW))
        {
            if (Input.GetMouseButton(0))
            {
                PW.fire = true;
               
            }
            else if (Input.GetMouseButtonUp(0))
            {
                PW.fire = false;
            }

        }

        if (Input.GetMouseButton(0) && shootDelay < 0)
        {
             weaponList[weaponLevel - 1].Fire();
             shootDelay = 0.3f;
        }

        //pixel check


        if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, Vector3.down);
            if(Physics.Raycast(ray.origin, ray.direction,  out hit,0.5f))
            {
                Paintable pa;
                if (hit.collider.gameObject.TryGetComponent<Paintable>(out pa))
                {
                    RenderTexture mask = pa.getMask();
                    if (mask!=null)
                    {
                        float uvX = hit.textureCoord.x;
                        float uvY = hit.textureCoord.y;

                        int x = Mathf.RoundToInt(uvX * mask.width);
                        int y = Mathf.RoundToInt(uvY * mask.height);

                        AsyncGPUReadback.Request(mask, 0, x, 1, y, 1, 0, 1, TextureFormat.RGBA32, (req) =>
                        {
                           var colorArray = req.GetData<Color32>();
                           Debug.Log("The Color of this pixel is " + colorArray[0]);
                        });
                    }


                }


          
  
            }
        }
    }

    void Fire()
    {

    }
    private void FixedUpdate()
    {
        shootDelay = shootDelay - 10 * Time.deltaTime;
       
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 inputDir = input.normalized;

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

        transform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, 0);

        

        if (inputDir != Vector2.zero)
        {

            Vector3 moveDir = inputDir.x * transform.right + input.y * transform.forward;
            //transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);

            rb.MovePosition(transform.position + moveDir.normalized * Time.deltaTime * moveSpeed);

            ac.SetBool("Move", true);
        }
        else
        {
            ac.SetBool("Move", false);
        }

    }
    
    public void WeaonUp()
    {
        weaponList[weaponLevel - 1].gameObject.SetActive(false);
        weaponLevel++;
        weaponList[weaponLevel - 1].gameObject.SetActive(true);
        ac.SetFloat("Blend", weaponLevel * 0.33f);
       
    }
}
