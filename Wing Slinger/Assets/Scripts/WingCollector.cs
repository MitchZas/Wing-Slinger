using System.Collections;
using UnityEngine;

public class WingCollector : MonoBehaviour
{
    private bool isTriggered = false;
    private bool isCreated = false;

    [SerializeField] private GameObject wingBasket1;
    [SerializeField] private GameObject wingBasket2;
    [SerializeField] private GameObject wingBasket3;
    [SerializeField] private GameObject wingBasket4;

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            if (wingBasket1.tag == "WB1" && wingBasket1.activeSelf == true)
            {
               wingBasket1.SetActive(false);
            }
        }

        if (wingBasket1.activeSelf == false)
        {
            StartCoroutine(WingSpawn());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    IEnumerator WingSpawn()
    {
        if (wingBasket1.activeSelf == false)
        {
            yield return new WaitForSeconds(3); //wait 3 seconds

            if (!isCreated)
            {
                Instantiate(wingBasket1, new Vector2(-3.528f, -0.115f), Quaternion.identity); //instaitnate wing basket 1
                isCreated = true;
                wingBasket1.SetActive(true);
            }
        }
    }
}
