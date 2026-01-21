using UnityEngine;
using UnityEngine.InputSystem;
using Unity.AI.Navigation;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance;
    
    [SerializeField] private InputActionReference confirmBuild;
    [SerializeField] private InputActionReference cancelBuild;
    [SerializeField] private LayerMask buildableLayer;
    [SerializeField] private float tileRayHeight = 2f;
    
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
        _ghost = FindFirstObjectByType<RoomGhostPreview>();
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

        BuildTile tile = FindTileUnderRoom(room.transform);

        if (tile == null || tile.IsOccupied)
        {
            Destroy(room);
            return;
        }
        
        tile.Consume();

        if (_selectedRoom.roomType == RoomType.Quarry)
        {
            QuarryManager.Instance.RegisterNewQuarry(room, _currentConnector);
        }
        
        ResourceManager.Instance.Spend(_selectedRoom);
        _currentConnector.Consume();
        
        RuntimeNavMeshManager.Instance?.UpdateAllSurfaces();
        
        CancelPlacement();
    }

    private BuildTile FindTileUnderRoom(Transform room)
    {
        var footprint = room.transform.Find("BuildFootprint");
        
        if (footprint == null) return null;
        
        var rayStart = footprint.position + Vector3.up * tileRayHeight;
        
        if (Physics.Raycast(rayStart + Vector3.up, Vector3.down, out var hit, tileRayHeight * 2f, buildableLayer))
        {
            return hit.collider.GetComponent<BuildTile>();
        }

        return null;
    }
}