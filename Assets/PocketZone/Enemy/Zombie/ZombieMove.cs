using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private float _speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = null;
        }
    }
    private void Update()
    {
        if (target != null)
            animator.SetBool("IsRunning", true);
        else
            animator.SetBool("IsRunning", false);

        if (target != null)
        {
            var step = _speed * Time.deltaTime;
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, target.position, step);

            if (target.transform.position.x > transform.parent.position.x) {
                transform.parent.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
                transform.parent.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
