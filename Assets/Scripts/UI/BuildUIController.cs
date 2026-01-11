using UnityEngine;
using UnityEngine.InputSystem;

public class BuildUIController : MonoBehaviour
{
    public static BuildUIController Instance;
    
    [SerializeField] private GameObject buildImage;
    [SerializeField] private InputActionReference confirmAction;

    [SerializeField] private RoomDefinition[] availableRooms;
    [SerializeField] private RoomChoiceUI[] slots;
    
    private RoomConnector _activeConnector;
    private int _selectedIndex;
    
    private void Awake()
    {
        Instance = this;
        buildImage.SetActive(false);
    }
    
    private void OnEnable()
    {
        confirmAction.action.performed += ConfirmBuild;
        confirmAction.action.Enable();
    }

    private void OnDisable()
    {
        confirmAction.action.performed -= ConfirmBuild;
    }
    
    public void Show(RoomConnector connector)
    {
        buildImage.SetActive(true);
        
        _activeConnector = connector;
        _selectedIndex = 0;

        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].Set(availableRooms[i]);
        }

        buildImage.SetActive(true);
    }
    
    public void Hide()
    {
        _activeConnector = null;
        buildImage.SetActive(false);
    }

    private void ConfirmBuild(InputAction.CallbackContext ctx)
    {
        var slot = slots[_selectedIndex];

        if (!slot.IsAffordable())
        {
            Debug.Log("Room not affordable");
            return;
        }

        var room = slot.GetRoom();

        ResourceManager.Instance.Spend(room);
        RoomPlacer.Instance.PlaceRoom(room, _activeConnector);

        _activeConnector.Disable();
        Hide();
    }
}