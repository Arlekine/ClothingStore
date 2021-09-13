using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WishView : MonoBehaviour
{
    public GameObject menu;
    public Image image;
    
    public void ShowWish(ClothPack clothPack)
    {
        menu.SetActive(true);
        menu.transform.DOScale(0.001f, 0.5f);
    }

    public void HideWith()
    {
        menu.transform.DOScale(0f, 0.5f);
    }
}