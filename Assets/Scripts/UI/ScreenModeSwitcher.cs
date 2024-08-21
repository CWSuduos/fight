using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreenModeSwitcher : MonoBehaviour
{
    public Dropdown fpsDropdown; // Reference to the FPS Dropdown
    public Button fullscreenButton; // Button for fullscreen mode
    public Button windowedButton;   // Button for windowed mode
    public Button borderlessButton; // Button for borderless mode
    public Text fpsDisplay;         // Reference to the Text component for displaying FPS

    private float fpsUpdateInterval = 0.40f; // Interval in seconds between FPS updates
    private float lastUpdateTime; // Time of the last FPS update

    private void Start()
    {
        // Setup FPS options for Dropdown
        List<string> fpsOptions = new List<string> { "Unlimited", "30", "60", "120", "144", "240" };
        fpsDropdown.ClearOptions();
        fpsDropdown.AddOptions(fpsOptions);
        fpsDropdown.onValueChanged.AddListener(SetFPS);

        // Initialize current FPS value in Dropdown
        string currentFPS = Application.targetFrameRate > 0 ? Application.targetFrameRate.ToString() : "Unlimited";
        fpsDropdown.value = fpsOptions.IndexOf(currentFPS);
        fpsDropdown.RefreshShownValue();

        // Bind button functions
        fullscreenButton.onClick.AddListener(SetFullscreen);
        windowedButton.onClick.AddListener(SetWindowed);
        borderlessButton.onClick.AddListener(SetBorderless);

        // Initialize the last update time
        lastUpdateTime = Time.time;
    }

    private void Update()
    {
        // Update FPS display based on the defined interval
        if (Time.time - lastUpdateTime >= fpsUpdateInterval)
        {
            UpdateFPSDisplay();
            lastUpdateTime = Time.time;
        }
    }

    public void SetFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        Screen.fullScreen = true;
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.fullScreen = false;
    }

    public void SetBorderless()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        Screen.fullScreen = true;
    }

    public void SetFPS(int fpsIndex)
    {
        string fpsOption = fpsDropdown.options[fpsIndex].text;
        if (fpsOption == "Unlimited")
        {
            // Set FPS to unlimited
            Application.targetFrameRate = -1;
        }
        else
        {
            // Set FPS to the selected value
            int fps = int.Parse(fpsOption);
            Application.targetFrameRate = fps;
        }
    }

    private void UpdateFPSDisplay()
    {
        // Display the current FPS value
        int fps = Mathf.RoundToInt(1.0f / Time.deltaTime);
        fpsDisplay.text = "FPS: " + fps.ToString();
    }
}
