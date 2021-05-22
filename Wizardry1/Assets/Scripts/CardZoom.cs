using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class CardZoom : NetworkBehaviour
{
    public GameObject Canvas;
    public GameObject ZoomCard;

    private GameObject zoomCard;
    private Sprite zoomSprite;


    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
        zoomSprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverEnter()
    {
        //if (!hasAuthority)
        //{
            
        //}
        zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 190),Quaternion.identity);
        zoomCard.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        zoomCard.transform.SetParent(Canvas.transform, true);
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(126, 176);
    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
