using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    public Slider sensitivitySlider; // ������ �� Slider ��� ����������� ����������������
    public Text sensitivityValueText; // ������ �� Text ��� ����������� �������� ����������������
 //public CameraController cameraController; // ������ �� ��� ������ CameraController

    private float sensitivity; // ���������� ��� �������� �������� �������� ����������������

    private void Start()
    {
        // ������������� ��������� �������� � �������� ��������
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 100f;
 //sensitivitySlider.value = cameraController.sensitivity; // ���������� ��������� ��������, ���� ���������

        // �������������� ������� �������� ����������������
        sensitivity = sensitivitySlider.value;
        UpdateSensitivityText();

        // ��������� ���������� ��������� �������� ��������
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float value)
    {
        // ��������� �������� ����������������
        sensitivity = value;
        UpdateSensitivityText();

        // ��������� ����� �������� ����������������
        ApplySensitivity(value);
    }

    private void UpdateSensitivityText()
    {
        // ��������� ��������� ���� � ������� ��������� ����������������
        sensitivityValueText.text = "Sensitivity: " + sensitivity.ToString("F2"); // ����������� �� ���� ������ ����� �������
    }

    private void ApplySensitivity(float value)
    {
        // ���������, ��� cameraController �� ����� null
       // if (cameraController != null)
        {
       //     cameraController.SetSensitivity(value);
        }
      //  else
        {
       //     Debug.LogWarning("CameraController is not assigned in the SensitivityManager.");
        }
    }
}
