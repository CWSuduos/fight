using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown;   // ������ �� ��� Dropdown
    public Toggle fullscreenToggle;       // ������ �� Toggle ��� �������������� ������
    private List<Resolution> availableResolutions; // ������ ��������� ����������

    void Start()
    {
        // �������� ��� ��������� ���������� ��� ������
        availableResolutions = new List<Resolution>(Screen.resolutions);

        // ������� ��� ������� ����� � Dropdown
        resolutionDropdown.ClearOptions();

        // ������� ������ ����� ��� ����������� � Dropdown
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Count; i++)
        {
            Resolution resolution = availableResolutions[i];
            string option = resolution.width + " x " + resolution.height;
            options.Add(option);

            // ��������� ������� ���������� ������ � ��������� ��� ������
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // ��������� ����� � Dropdown
        resolutionDropdown.AddOptions(options);
        // ������������� ������� ���������� ��� ���������
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // ����������� ������� �� ��������� �������� � Dropdown � Toggle
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(fullscreenToggle.isOn); });

        // �������������� ��������� Toggle � ����������� �� �������� �������������� ������
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        // ������������� ��������� ����������
        Resolution resolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        // ������������ �������������� ������
        Screen.fullScreen = isFullScreen;
    }
}
