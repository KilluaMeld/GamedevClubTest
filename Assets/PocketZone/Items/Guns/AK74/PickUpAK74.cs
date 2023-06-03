using UnityEngine;

public class PickUpAK74 : MonoBehaviour
{
    private LoadItemList loadItemList;

    private void Start()
    {
        loadItemList = GameObject.FindObjectOfType<LoadItemList>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            loadItemList.CreateNewAK74();
            Destroy(this.gameObject);
        }
    }
}
