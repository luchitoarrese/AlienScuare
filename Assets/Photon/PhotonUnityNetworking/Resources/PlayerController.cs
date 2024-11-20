using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject Rocketprefab;
    [SerializeField] private Transform trgt;
    [SerializeField] public Joystick stick = null;
    [SerializeField, Range(0, 10)] private int _speedBullet=0;
    private Transform _transform = null;
    private Vector3 _direction = Vector3.zero;
    [SerializeField] private float PlayerSpeed = 5f;
    private float axisX;
    private float axisZ;
    private int _actionAnimation=0;
    private Animator _animator;
    [SerializeField] private bool _isState=true;
    [SerializeField, Range(0, 10)] private float range=0;
    public Transform origin; 
    [Range(0, 10)]public float radius = 5f; 
    public LayerMask detectionLayer;
    [SerializeField] private bool _isinRange=false;
   

    private PhotonView photonView;

    private void Awake()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        


       

       
    }

    private Joystick FindJoystickInCanvas(Canvas canvas)
    {
        
        Joystick foundJoystick = canvas.GetComponentInChildren<Joystick>();

        if (foundJoystick == null)
        {
            Debug.LogError($"No se encontró un joystick dentro del Canvas '{canvas.name}'.");
        }

        return foundJoystick;
    }



    private Canvas FindCanvasByName(string name)
    {
       
        Canvas[] allCanvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.name == name)
            {
                return canvas;
            }
        }

        return null; 
    }

    
    private void Update()
    {
        if(GetComponent<PhotonView>().IsMine == true)
        {

 
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin.position, radius, detectionLayer);

        foreach (Collider2D hit in hits)
        {
            Debug.Log("ENTRO: " + hit.gameObject.name);
        }



        switch (_actionAnimation)
        {
            case 0:
                _animator.SetInteger("Action", 0);
                break;
                case 1:
                _animator.SetInteger("Action", 1);
                break;
            case 2:
                _animator.SetInteger("Action", 2);
                break;
            case 3:
                _animator.SetInteger("Action", 3);
                break;
            case 4:
                _animator.SetInteger("Action", 4);
                break;
            case 5:
                _animator.SetInteger("Action", 5);
                break;
            case 6:
                _animator.SetInteger("Action", 6);
                break;
        }
#if UNITY_ANDROID
        if(stick != null)
        {
            _direction.x = stick.Horizontal;
            _direction.y = stick.Vertical;
        }
     else
        {
            _direction.y = Input.GetAxis("Vertical");
        }
#endif
            _transform.position += PlayerSpeed * Time.deltaTime * _direction;

        if(_direction.x < 0)
        {
            _transform.localScale = new Vector2(-1, 1);
            _actionAnimation = 1;
        }
        else if(_direction.x > 0)
        {
            _transform.localScale = new Vector2(1, 1);
            _actionAnimation = 1;
        }
           
        else
        {
          if(_isState)
            {
                _actionAnimation = 0;
            }
        }


#if UNITY_EDITOR

        _direction.x = Input.GetAxis("Horizontal");
        _direction.y = Input.GetAxis("Vertical");

        _transform.position += PlayerSpeed * Time.deltaTime * _direction;

#endif
        }
       
    }


    public void AnimatedShoot()
    {
        if(_isinRange)
        {
            _isState = false;
            _actionAnimation = 2;
        }
       
    }
    public void IdleState()
    {
        _actionAnimation = 0;
        _isState = true;
    }

    public void Shoot()
    {
        
        Rocketprefab = Instantiate(Rocketprefab, trgt.position, Quaternion.identity);
        Rigidbody2D rb2D = Rocketprefab.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = trgt.right.normalized;
        
       
        if (rb2D != null)
        {

            if (transform.localScale.x < 0)
            {
                Rocketprefab.transform.localScale = new Vector2(-1, 1);
                rb2D.velocity = -shootDirection * _speedBullet;
            }
            if (transform.localScale.x > 0)
            {
                Rocketprefab.transform.localScale = new Vector2( 1, 1);
                rb2D.velocity = shootDirection * _speedBullet;
            }


           
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawWireCircle(transform.position, range);
        if (origin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(origin.position, radius);
        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawWireCircle(transform.position, range);
    }

    void DrawWireCircle(Vector3 position, float radius)
    {
        const int segments = 64; 
        float angleStep = 360f / segments;

        Vector3 prevPoint = position + new Vector3(Mathf.Cos(0) * radius, Mathf.Sin(0) * radius, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector3 newPoint = position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}
