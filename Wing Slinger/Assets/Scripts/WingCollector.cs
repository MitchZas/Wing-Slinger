using UnityEngine;

public class WingCollector : MonoBehaviour
{
    [SerializeField] private GameObject wingPrefabItem;
    public GameObject wingHolder;
    [SerializeField] private SpriteRenderer wingPrefab;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wingPrefab.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Picked up");
            
            wingPrefab.sortingOrder = 2;
            wingPrefabItem.transform.position = wingHolder.transform.position;
        }
    }
}
