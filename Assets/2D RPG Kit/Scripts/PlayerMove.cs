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

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _nextMovePos = _mainCameraCache.ScreenToWorldPoint(mousePos);
            _nextMovePos.z = 0;
            dir = _nextMovePos - player.transform.position;
        }
        else if (Mathf.Abs(inputMoveX) > 0 || Mathf.Abs(inputMoveY) > 0)
        {
            _nextMovePos = player.transform.position;
            return;
        }

        Vector3 offset = (_nextMovePos - player.transform.position).normalized;
        offset.z = 0;
        float distance = Vector3.Distance(_nextMovePos, player.transform.position);
        _playerController.canMove = false;
        if (distance > 0.1)
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
        else if (distance > 0.01)
        {
            if (Math.Abs(dir.x) > Math.Abs(dir.y))
            {
                _animitor.SetFloat("lastMoveX", dir.x < 0 ? -1 : 1);
                _animitor.SetFloat("lastMoveY", 0);
            }
            else
            {
                _animitor.SetFloat("lastMoveY", dir.y < 0 ? -1 : 1);
                _animitor.SetFloat("lastMoveX", 0);
            }
            player.transform.position = _nextMovePos;
        }
        else
        {
            _playerController.canMove = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _nextMovePos = player.transform.position;
        _animitor.SetFloat("moveX", 0);
        _animitor.SetFloat("moveY", 0);
    }
}