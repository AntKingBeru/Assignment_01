using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomChoiceUI : MonoBehaviour
{
    [Header("UI")]
    // [SerializeField] private Texture roomIcon;
    [SerializeField] private Image woodIcon;
    [SerializeField] private Image stoneIcon;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;

    [Header("Visual States")]
    [SerializeField] private Color affordableColor = Color.white;
    [SerializeField] private Color unaffordableColor = new Color(1, 1, 1, 0.4f);

    private RoomDefinition _room;
    private bool _affordable;
    
    public void Set(RoomDefinition roomDef)
    {
        _room = roomDef;

        woodText.text = _room.woodCost.ToString();
        stoneText.text = _room.stoneCost.ToString();

        _affordable = ResourceManager.Instance.CanAfford(_room);
        UpdateVisualState();
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