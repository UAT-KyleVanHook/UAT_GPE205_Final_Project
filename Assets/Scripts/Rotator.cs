using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 objectRotation;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(objectRotation * speed * Time.deltaTime);
    }
}
