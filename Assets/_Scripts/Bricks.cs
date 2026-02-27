using UnityEngine;

public class Bricks : MonoBehaviour
{

    [SerializeField] private float _movespeed = 3f;
     [SerializeField] private float _lifecycle = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, _lifecycle); // destroy the brick after _lifecycle seconds
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * _movespeed * Time.deltaTime); // move left
    }
}
