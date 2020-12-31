using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDropTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject prefabDragged, prefabTower;
    private GameObject towerDragged;
    private Camera mainCamera;
    public TowerData data; 
    float cooldown = 10f;
    private Image icon;
    private bool canCast = true;
    private void DisableButton()
    {
        canCast = false;
        icon.fillAmount = 0f;
    }
    private void EnableButton()
    {
        canCast = true;
    }
    private IEnumerator Cooldown()
    {
        while (icon.fillAmount < 1f)
        {
            icon.fillAmount += Time.deltaTime / cooldown;
            yield return null;
        }
        EnableButton();
    }
    private void Start()
    {
        icon = GetComponent<Image>();
        cooldown = data.cooldown;
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
        if (!canCast)
        {
            return;
        }
        towerDragged = Instantiate(prefabDragged, mainCamera.ScreenToWorldPoint(eventData.position), Quaternion.identity);
        towerDragged.GetComponent<SpriteRenderer>().sprite = data.sprite;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (towerDragged == null) return;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(eventData.position);
        towerDragged.transform.position = new Vector3((int)Mathf.Floor(mousePos.x + 0.5f), (int)Mathf.Floor(mousePos.y + 0.5f), 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (towerDragged == null) return;
        Vector3 towerPos = towerDragged.transform.position;
        Destroy(towerDragged);
        if (!CoinManager.instance.CanPurchase(data.price)) return;
        if (!FixedPositionManager.instance.CanPlace(new Vector2Int((int)towerPos.x, (int)towerPos.y))) return;
        CoinManager.instance.Purchase(data.price);
        GameObject tower;
        tower = Instantiate(prefabTower, towerPos, Quaternion.identity);
        tower.GetComponent<AudioSource>().clip = data.spawnSfx;
        tower.GetComponent<AudioSource>().Play();
        if (data.isRanged)
        {
            tower.AddComponent<TowerRanged>().towerData = data;
        }
        else
        {
            tower.AddComponent<TowerMelee>().towerData = data;
        }
        FixedPositionManager.instance.Place(new Vector2Int((int)towerPos.x, (int)towerPos.y));
        DisableButton();
        StartCoroutine(Cooldown());
        //Destroy(transform.parent.gameObject);
    }
}
