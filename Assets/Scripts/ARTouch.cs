using UnityEngine;

public class ARTouch : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    void Update()
    {
        if (cam is null) return;
        
        Vector2? pos = null;
				
        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                if (touch.phase != TouchPhase.Ended) continue;
						
                pos = touch.position;
                break;
            }
        }
				
        if (Input.GetMouseButtonDown(0))
        {
            pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (!pos.HasValue || cam is null) return;
        
        var ray = cam.ScreenPointToRay(pos.Value);

        if (!Physics.Raycast(ray, out var hit, 100)) return;
        
        if (hit.transform.CompareTag("Button"))
        {
            Debug.Log($"Button '{hit.transform.name}' was hit with tag '{hit.transform.tag}'");
                
            var player = hit.transform.GetComponent<Player>();
            player.StartPlay();
        }
    }
}
