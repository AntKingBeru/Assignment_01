using UnityEngine;

public class BuildUIController : MonoBehaviour
{
    public static BuildUIController Instance;
    
    [SerializeField] private GameObject buildImage;
    [SerializeField] private RoomGhostPreview ghostPreview;
    [SerializeField] private RoomDefinition[] availableRooms;
    [SerializeField] private RoomChoiceUI[] slots;

    private RoomDefinition _selectedRoom;
   
    public RoomConnector ActiveConnector { get; private set; }
    public bool IsPlacing { get; private set; }

    private void Awake()
    {
        Instance = this;
        buildImage.SetActive(false);
    }
    
    public void Show(RoomConnector connector)
    {
        buildImage.SetActive(true);
        
        ActiveConnector = connector;
        _selectedRoom = null;
        IsPlacing = false;

        for (var i = 0; i < slots.Length; i++)
        {
            if (i < availableRooms.Length)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Set(availableRooms[i]);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void Hide()
    {
        buildImage.SetActive(false);
    }

    public void SelectRoom(RoomDefinition room)
    {
        _selectedRoom = room;
        IsPlacing = true;
        
        ghostPreview.Show(_selectedRoom, ActiveConnector);
    }

    public RoomDefinition GetSelectedRoom()
    {
        return _selectedRoom;
    }
}