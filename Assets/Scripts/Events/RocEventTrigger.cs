using UnityEngine;
using UnityEngine.Events;

public class RocEventTrigger : MonoBehaviour
{
    [SerializeField] private Transform startPoint, endPoint, triggerPoint;
    [SerializeField] private float travelTime;
    [SerializeField] private GameObject bird;
    // [SerializeField] private UnityEvent onMidPointReached;
    // [SerializeField] private AudioClip rocCaw;
    
    private bool _started = false, _midPointTriggered = false; 
    private float _elapsedTime = 0f;

    private void Start()
    {
        bird.transform.position = startPoint.position;
        bird.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_started) return;
        if (!other.CompareTag("Player")) return;
        
        StartMovement();
    }

    private void StartMovement()
    {
        _started = true;
        bird.SetActive(true);
        _elapsedTime = 0f;

        bird.transform.LookAt(endPoint.position);
    }

    private void Update()
    {
        if (!_started) return;

        // PlayCaw();
        
        _elapsedTime += Time.deltaTime;
        var time = Mathf.Clamp01(_elapsedTime / travelTime);
        
        bird.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, time);

        CheckMidPoint();
        
        if (time >= 1f)
        {
            _started = false;
            bird.SetActive(false);
        }
    }

    private void CheckMidPoint()
    {
        if (_midPointTriggered) return;
        
        var distanceToMid = Vector3.Distance(bird.transform.position, triggerPoint.position);

        if (distanceToMid < 0.1f)
        {
            _midPointTriggered = true;
            // onMidPointReached.Invoke();
            // PlayCaw();
        }
    }

    // private void PlayCaw()
    // {
    //     TODO: add caw sound logic
    // }
}