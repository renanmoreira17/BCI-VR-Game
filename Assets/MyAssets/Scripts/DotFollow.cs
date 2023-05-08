using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DotFollow : MonoBehaviour
{
    public float selectionTime = 1f; // The time required to select a button in seconds
    public float rayLength = 10f;
    private float timer; // The timer used to track the selection time
    private Button selectedButton; // The currently selected button

    public Canvas canvas;


    void Update()
    {
        // Get the forward direction of the camera
        Vector3 cameraDirection = canvas.transform.forward;


        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        // Cast a ray from the camera to the camera point
        // Ray ray = Camera.main.ScreenPointToRay(cameraDirection);
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        // debug Ray
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            print(hit.collider.name);
            Button hitButton = hit.collider.gameObject.GetComponent<Button>();
            if (hitButton != null)
            {
                print("notnull");
                // Check if the same button is still being pointed at
                if (hitButton == selectedButton)
                {
                    // Update the timer and check if the selection time has been reached
                    // print(hitButton.name+"timer: "+timer);
                    print("timer finished? "+(timer >= selectionTime));
                    timer += Time.deltaTime;
                    if (timer >= selectionTime)
                    {
                        print("select "+hitButton.name);
                        print(hitButton);
                        // Select the button
                        // hitButton.Select();
                        ExecuteEvents.Execute(hitButton.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
                    }
                }
                else
                {
                    // Reset the timer and select the new button
                    timer = 0f;
                    selectedButton = hitButton;
                    print("selected "+hitButton.name);
                }
            }
            else
            {
                // Reset the timer if the ray hit something that is not a button
                timer = 0f;
                selectedButton = null;
            }
        }
        else
        {
            // Reset the timer if the ray hit nothing
            timer = 0f;
            selectedButton = null;
        }
    }
}
