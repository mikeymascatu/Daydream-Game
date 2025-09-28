using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour
{
    public static bool InventoryIsActive = false;

    public GameObject InventoryMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryIsActive)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Hide()
    {
        InventoryMenuUI.SetActive(false);
        Time.timeScale = 1f;
        InventoryIsActive = false;
    }
    void Show()
    {
        InventoryMenuUI.SetActive(true);
        Time.timeScale = 0f;
        InventoryIsActive = true;
    }
}

