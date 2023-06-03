using UnityEngine;
using UnityEngine.UI;

public class OpenCloseWindows : MonoBehaviour
{
    [SerializeField] GameObject[] forOpenWindows;
    [SerializeField] GameObject[] forCloseWindows;

    private void OnEnable()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OpenAndCloseWindows);
    }
    void OpenAndCloseWindows()
    {
        foreach (var item in forOpenWindows)
        {
            item.SetActive(true);
        }
        foreach (var item in forCloseWindows)
        {
            item.SetActive(false);
        }
    }
}
