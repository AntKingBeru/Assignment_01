using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionReference harvestAction;

    private IHarvestable _currentHarvestable;

    private void Start()
    {
        harvestAction.action.performed += OnHarvest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHarvestable>(out var harvestable))
        {
            _currentHarvestable = harvestable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IHarvestable>(out var harvestable) &&
            harvestable == _currentHarvestable)
        {
            _currentHarvestable = null;
        }
    }

    private void OnHarvest(InputAction.CallbackContext context)
    {
        if (_currentHarvestable == null) return;
        if (_currentHarvestable.CanHarvest())
        {
            _currentHarvestable.Harvest();
            _currentHarvestable = null;
        }
    }
}