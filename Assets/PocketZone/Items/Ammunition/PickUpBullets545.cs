using UnityEngine;

public class PickUpBullets545 : MonoBehaviour
{
    private LoadItemList loadItemList;

    private void Start()
    {
        loadItemList = GameObject.FindObjectOfType<LoadItemList>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            loadItemList.CreateNewBullets545(39);
            Destroy(this.gameObject);
        }
    }
}
