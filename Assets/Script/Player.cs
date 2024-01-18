using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _moveDir;
    [SerializeField] private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Activating the InputManager and Controls
        InputManager.InitM(this, this);
        InputManager.EnableInGame();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(_moveSpeed * Time.fixedDeltaTime * _moveDir);

    }

    public void SetMoveDirection(Vector2 newDir)
    {
        _moveDir = newDir;
    }
}
