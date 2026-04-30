using UnityEngine;

public class GameWin : MonoBehaviour
{

    private Collider mCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mCollider = GetComponent<Collider>();
        mCollider.isTrigger = true;
    }

    public void Awake()
    {
        GameManager.instance.endGoals.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

        //if the collider of this object has the tag "Player", go to win screen.
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GameWin();
        }

    }

    public void OnDestroy()
    {
        GameManager.instance.endGoals.Remove(this);
    }
}
