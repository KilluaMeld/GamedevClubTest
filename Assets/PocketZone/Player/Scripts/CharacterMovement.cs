using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform _body;
    private Vector3 moveDirection;
    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("GameController").GetComponent<Joystick>();
    }
    private void Move(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.fixedDeltaTime);

        if (direction.x != 0 || direction.y != 0)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);

        FlipSide();
    }
    void FlipSide()
    {
        if (joystick.Horizontal >0 )
            _body.rotation = Quaternion.Euler(0, 0, 0);
        else if(joystick.Horizontal < 0)
            _body.rotation = Quaternion.Euler(0, 180, 0);
    }
    private void FixedUpdate()
    {
        Move(moveDirection);
    }
    void Update()
    {
        moveDirection = new Vector3(joystick.Horizontal, joystick.Vertical, 0f);
    }
}
