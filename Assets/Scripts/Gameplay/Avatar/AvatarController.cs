using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AvatarMovementController), typeof(AvatarShooter), typeof(InputController))]
public class AvatarController : MonoBehaviour
{
    private InputController _input;
    private AvatarShooter _shooter;
    private AvatarMovementController _movement;

    public static bool Inverted = false;

    [SerializeField]
    private float recoilRatio = 1.0f;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Invert()
    {
        Inverted = !Inverted;
    }
    private void Awake()
    {
        _movement = GetComponent<AvatarMovementController>();
        _input = GetComponent<InputController>();
        _shooter = GetComponent<AvatarShooter>();
    }

    private void Start()
    {
        _shooter.OnShoot += OnShoot;
    }
    
    private void OnDestroy()
    {
        _shooter.OnShoot -= OnShoot;
    }

    private void Update()
    {
        UpdateAimDirection();
        UpdateShooter();
        Move();
    }

    private void UpdateAimDirection()
    {
        if (_input.IsDirectionZero)
        {
            return;
        }

        transform.forward = _input.Direction;
    }

    private void UpdateShooter()
    {
        _shooter.IsShooting = !_input.IsDirectionZero;
    }

    private void Move()
    {
        _movement.Move();
    }

    private void OnShoot(ShotData shotData)
    {
        _movement.AddVelocity(-shotData.Velocity * recoilRatio);
    }
}
