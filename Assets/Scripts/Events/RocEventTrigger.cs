using UnityEngine;
using UnityEngine.Events;

public class RocEventTrigger : MonoBehaviour
{
    [SerializeField] private Transform startPoint, endPoint, triggerPoint;
    [SerializeField] private float travelTime;
    [SerializeField, Range(0f, 1f)] private float midPointTime = 0.33f;
    [SerializeField] private GameObject bird;
    [SerializeField] private UnityEvent onMidPointReached;
    // [SerializeField] private AudioClip rocCaw;
    
    private bool _started = false, _midPointTriggered = false; 
    private float _elapsedTime = 0f;

    private void Start()
    {
        if (bird)
        {
            bird.SetActive(false);
            bird.transform.position = startPoint.position;
        }
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
        _elapsedTime = 0f;
        _midPointTriggered = false;
        
        bird.SetActive(true);

        bird.transform.LookAt(endPoint.position);
    }

    private void Update()
    {
        if (!_started || !bird) return;

        // PlayCaw();
        
        _elapsedTime += Time.deltaTime;
        var time = Mathf.Clamp01(_elapsedTime / travelTime);
        
        bird.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, time);

        CheckMidPoint(time);
        
        if (time >= 1f)
        {
            FinishMovement();
        }
    }

    private void FinishMovement()
    {
        bird.SetActive(false);
    }

    private void CheckMidPoint(float time)
    {
        if (_midPointTriggered)
            return;

        if (time >= midPointTime)
        {
            _midPointTriggered = true;
            onMidPointReached.Invoke();
        }
    }

    // private void PlayCaw()
    // {
    //     TODO: add caw sound logic
    // }
}