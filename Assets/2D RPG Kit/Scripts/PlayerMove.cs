using System;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    private Vector3 _nextMovePos;
    private Camera _mainCameraCache;
    public GameObject player;
    public float _moveSpeed = 10;
    private Animator _animitor;
    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    private void Start()
    {
        _nextMovePos = player.gameObject.transform.position;
        _rigidbody = player.GetComponent<Rigidbody2D>();
        _animitor = gameObject.GetComponent<Animator>();
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    private float inputMoveX = 0;
    private float inputMoveY = 0;
    private Vector2 dir;

    // Update is called once per frame
    void Update()
    {
        if (_mainCameraCache == null)
        {
            _mainCameraCache = Camera.main;
        }

        if (_mainCameraCache == null)
        {
            return;
        }

        if (player == null)
        {
            return;
        }

        if (!_playerController.canMove)
        {
            return;
        }

        inputMoveX = Input.GetAxisRaw("Horizontal");
        inputMoveY = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(inputMoveX) > 0 || Mathf.Abs(inputMoveY) > 0)
        {
            _nextMovePos = player.transform.position;
            dir = Vector2.zero;
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _nextMovePos = _mainCameraCache.ScreenToWorldPoint(mousePos);
            _nextMovePos.z = 0;
            dir = _nextMovePos - player.transform.position;
        }

        Vector3 offset = (_nextMovePos - player.transform.position).normalized;
        offset.z = 0;
        float distance = Vector3.Distance(_nextMovePos, player.transform.position);
        _playerController.canMove = false;
        
        
        if (distance > 0.5)
        {
            if (Math.Abs(offset.x) > Math.Abs(offset.y))
            {
                _animitor.SetFloat("moveX", offset.x * 10);
            }
            else
            {
                _animitor.SetFloat("moveY", offset.y * 10);
            }
            _rigidbody.velocity = offset * _moveSpeed;
            //player.transform.position = player.transform.position + dir * _moveSpeed * Time.deltaTime;
        }
        else
        {
            StopMove();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StopMove();
    }

    private void StopMove()
    {
        _nextMovePos = player.transform.position;
        _rigidbody.velocity = Vector2.zero;
        _animitor.SetFloat("moveX", 0);
        _animitor.SetFloat("moveY", 0);
        if (Math.Abs(dir.x) > Math.Abs(dir.y))
        {
            _animitor.SetFloat("lastMoveX", dir.x < 0 ? -1 : 1);
            _animitor.SetFloat("lastMoveY", 0);
        }
        else if((Math.Abs(dir.y) - Math.Abs(dir.x) > 0.1f))
        {
            _animitor.SetFloat("lastMoveY", dir.y < 0 ? -1 : 1);
            _animitor.SetFloat("lastMoveX", 0);
        }
        
        _playerController.canMove = true; 
    }
}