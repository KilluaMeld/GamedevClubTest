using UnityEngine;

public class PickUpMakarov : MonoBehaviour
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
            loadItemList.CreateNewMakarov();
            Destroy(this.gameObject);
        }
    }
}
