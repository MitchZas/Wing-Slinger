using UnityEngine;
using System.Collections;

public class WingScript : MonoBehaviour
{
    public GameObject wing;
    public bool isActive;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WingMaker());
        wing.SetActive(false);
        isActive = false;
    }

    private IEnumerator WingMaker()
    {
        yield return new WaitForSeconds(3f);
        wing.SetActive(true);
        isActive = true;
    }
}
