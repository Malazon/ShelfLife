using UnityEngine;

public class GrabableObject : MonoBehaviour
{
        [SerializeField] public int Priority = 0;
        [SerializeField] public bool triggersButtons = false;
        [SerializeField] public bool isKeyCard = false;
        [SerializeField] public bool isImportant = false;
        [SerializeField] public Vector3 spawnedPosition;


    private void Awake()
    {
        spawnedPosition = this.transform.position;
    }

    private void Update()
    {
        if (transform.position.y < -5f && isImportant)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        this.transform.position = spawnedPosition;
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}