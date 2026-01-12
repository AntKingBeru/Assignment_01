using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomChoiceUI : MonoBehaviour
{
    [Header("UI")]
    // [SerializeField] private Image roomIcon;
    [SerializeField] private Button button;
    [SerializeField] private Image woodIcon;
    [SerializeField] private Image stoneIcon;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;

    [Header("Visual States")]
    [SerializeField] private Color affordableColor = Color.white;
    [SerializeField] private Color unaffordableColor = new Color(1, 1, 1, 0.4f);

    private RoomDefinition _room;
    private BuildUIController _buildUI;
    private bool _affordable;
    
    public void Set(RoomDefinition roomDef)
    {
        _room = roomDef;

        // roomIcon.sprite = _room.icon;
        roomName.text = _room.name;
        woodText.text = _room.woodCost.ToString();
        stoneText.text = _room.stoneCost.ToString();

        _affordable = ResourceManager.Instance.CanAfford(_room);
        button.interactable = _affordable;
        UpdateVisualState();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        BuildController.Instance.BeginPlacement(_room, BuildUIController.Instance.ActiveConnector);
    }
    
    private void UpdateVisualState()
    {
        var color = _affordable ? affordableColor : unaffordableColor;
        
        // roomIcon.color = color;
        woodIcon.color = color;
        stoneIcon.color = color;

        woodText.color = _affordable ? Color.white : Color.red;
        stoneText.color = _affordable ? Color.white : Color.red;
    }
    
    public bool IsAffordable()
    {
        return _affordable;
    }

    public RoomDefinition GetRoom()
    {
        return _room;
    }
}