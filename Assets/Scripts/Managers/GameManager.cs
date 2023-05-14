using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool PlayerStopMoving = false;
    public bool PlayerInSellArea = false;

    [SerializeField]
    private GameObject m_Player;

    [SerializeField]
    private InputManager m_InputManager;

    [SerializeField]
    private InventoryController m_InventoryController;

    [SerializeField]
    private ShopController m_ShopController;

    [SerializeField]
    private TextManager m_TextManager;


    public static InputManager InputManager => Instance.m_InputManager;
    public static InventoryController InventoryController => Instance.m_InventoryController;
    public static ShopController ShopController => Instance.m_ShopController;
    public static TextManager TextManager => Instance.m_TextManager;
    public static GameObject Player => Instance.m_Player;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
