using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    public Image healthImage;
    public BasicController basicController;

    private void OnEnable()
    {
        if (basicController != null)
        {
            basicController.onDamage += UpdateUI;
            basicController.onHeal += UpdateUI;
        }
    }

    private void OnDestroy()
    {
        if (basicController != null)
        {
            basicController.onDamage -= UpdateUI;
            basicController.onHeal -= UpdateUI;
        }
    }


    private void UpdateUI(float amount)
    {
        if (basicController != null)
        {
            var fill = basicController.hp / basicController.maxHp;
            healthImage.fillAmount = fill;
        }
    }


}
