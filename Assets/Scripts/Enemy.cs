using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private Rigidbody _rigidbody;
    private GameObject _player;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(lookDirection * speed);
        
        if (transform.position.y < -10)
        {
            Destroy(GameObject.Find("Enemy"));
        }
    }
}
