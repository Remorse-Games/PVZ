using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSpell : MonoBehaviour
{
    public float cooldown = 10f;
    public Image icon;
    public Button button;
    private bool canCast = true;
    public void ButtonClicked()
    {
        if (!canCast)
        {
            return;
        }
        DisableButton();
        Spell();
        StartCoroutine(Cooldown());
    }
    private void DisableButton()
    {
        canCast = false;
        button.interactable = false;
        icon.fillAmount = 0f;
    }
    private void EnableButton()
    {
        canCast = true;
        button.interactable = true;
    }
    protected virtual void Spell() { }
    private IEnumerator Cooldown()
    {
        while (icon.fillAmount < 1f)
        {
            icon.fillAmount += Time.deltaTime / cooldown;
            yield return null;
        }
        EnableButton();
    }
}
