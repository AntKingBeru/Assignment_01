using UnityEngine;
using UnityEngine.InputSystem;

public class BuildUIController : MonoBehaviour
{
    public static BuildUIController Instance;
    
    [SerializeField] private GameObject buildImage;
    [SerializeField] private RoomGhostPreview ghostPreview;
    // [SerializeField] private InputActionReference confirmAction;
    [SerializeField] private RoomDefinition[] availableRooms;
    [SerializeField] private RoomChoiceUI[] slots;
    
    private RoomDefinition _selectedRoom;
    private RoomConnector _activeConnector;

    public bool IsPlacing { get; private set; }

    private void Awake()
    {
        Instance = this;
        buildImage.SetActive(false);
        
        foreach (var slot in slots) 
            slot.Initialize(this);
    }
    
    // private void OnEnable()
    // {
    //     confirmAction.action.performed += ConfirmBuild;
    //     confirmAction.action.Enable();
    // }
    //
    // private void OnDisable()
    // {
    //     confirmAction.action.performed -= ConfirmBuild;
    // }
    
    public void Show(RoomConnector connector)
    {
        buildImage.SetActive(true);
        
        _activeConnector = connector;
        _selectedRoom = null;
        IsPlacing = false;

        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].Set(availableRooms[i]);
        }
    }
    
    public void Hide()
    {
        _activeConnector = null;
        _selectedRoom = null;
        IsPlacing = false;
        
        ghostPreview.Clear();
        buildImage.SetActive(false);
    }

    public void SelectRoom(RoomDefinition room)
    {
        _selectedRoom = room;
        IsPlacing = true;
        
        ghostPreview.Show(_selectedRoom, _activeConnector);
    }

    public RoomDefinition GetSelectedRoom()
    {
        return _selectedRoom;
    }
}