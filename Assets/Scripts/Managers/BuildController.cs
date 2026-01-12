using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI.Navigation;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance;
    
    [SerializeField] private InputActionReference confirmBuild;
    [SerializeField] private InputActionReference cancelBuild;
    
    private RoomGhostPreview _ghost;
    private RoomDefinition _selectedRoom;
    private RoomConnector _currentConnector;
    
    public bool IsPlacing => _selectedRoom != null;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        _ghost = FindObjectOfType<RoomGhostPreview>();
    }

    private void OnEnable()
    {
        confirmBuild.action.performed += OnConfirm;
        cancelBuild.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        confirmBuild.action.performed -= OnConfirm;
        cancelBuild.action.performed -= OnCancel;
    }
    
    public void BeginPlacement(RoomDefinition room, RoomConnector connector)
    {
        CancelPlacement();
        
        _selectedRoom = room;
        _currentConnector = connector;
        
        _ghost.Show(room, connector);
    }

    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (_ghost == null || _selectedRoom == null) return;

        if (!_ghost.CheckPlacementValid(_selectedRoom, _currentConnector)) return;
        
        PlaceRoom();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (!IsPlacing) return;
        CancelPlacement();
    }

    public void CancelPlacement()
    {
        _selectedRoom = null;
        _currentConnector = null;
        
        if (_ghost != null) _ghost.Clear();
        
        BuildUIController.Instance.Hide();
    }

    private void PlaceRoom()
    {
        var room = Instantiate(_selectedRoom.roomPrefab);
        
        var attach = room.transform.Find("AttachPoint");
        
        RoomGhostPreview.AlignRoomToConnector(room.transform, attach, _currentConnector);

        if (_selectedRoom.roomType == RoomType.Quarry)
        {
            QuarryManager.Instance.RegisterNewQuarry(room, _currentConnector);
        }
        
        ResourceManager.Instance.Spend(_selectedRoom);
        _currentConnector.Consume();

        if (RuntimeNavMeshManager.Instance != null)
        {
            RuntimeNavMeshManager.Instance.UpdateAllSurfaces();
        }
        else
        {
            Debug.LogError("RuntimeNavMeshManager missing from scene!");
        }
        
        CancelPlacement();
    }
}