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
    public TowerData data;
    private void Start()
    {
        if (prefabDragged == null)
        {
            Debug.LogWarning("Var prefabDragged is null! Assign in inspector!");
            prefabDragged = new GameObject();
        }
        if (prefabTower == null)
        {
            Debug.LogWarning("Var prefabTower is null! Assign in inspector!");
            prefabTower = new GameObject();
        }
        mainCamera = Camera.main;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        towerDragged = Instantiate(prefabDragged, mainCamera.ScreenToWorldPoint(eventData.position), Quaternion.identity);
        towerDragged.GetComponent<SpriteRenderer>().sprite = data.sprite;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(eventData.position);
        towerDragged.transform.position = new Vector3((int)Mathf.Floor(mousePos.x + 0.5f), (int)Mathf.Floor(mousePos.y + 0.5f), 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject tower;
        tower = Instantiate(prefabTower, towerDragged.transform.position, Quaternion.identity);
        if (data.isRanged)
        {
            tower.AddComponent<TowerRanged>().towerData = data;
        }
        else
        {
            tower.AddComponent<TowerMelee>().towerData = data;
        }
        Destroy(towerDragged);
        Destroy(transform.parent.gameObject);
    }
}
