using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject prefabDragged, prefabTower;
    private GameObject towerDragged;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        towerDragged = Instantiate(prefabDragged, mainCamera.ScreenToWorldPoint(eventData.position), Quaternion.identity);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(eventData.position);
        towerDragged.transform.position = new Vector3((int)Mathf.Floor(mousePos.x + 0.5f), (int)Mathf.Floor(mousePos.y + 0.5f), 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Instantiate(prefabTower, towerDragged.transform.position, Quaternion.identity);
        Destroy(towerDragged);
        Destroy(transform.parent.gameObject);
    }

}
