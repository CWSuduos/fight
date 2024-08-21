using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // ����� �������� ����� ������������� ����� (� ��������)
    public float loadingTime = 6.0f;

    // ��� �����, �� ������� ����� ������������� ����� ��������
    public string nextSceneName;

    // ��������� ���� ��� ����������� �������� ��������
    public Text loadingText;

    // �������� ��� ����������� ��������� ��������
    public Slider loadingSlider;

    // ������ ��� ������� �������
    private float timer;

    // ������� �������� ��������
    private float loadSpeed;

    // ������������ � ����������� �������� ��� ��������� �������� ��������
    public float minLoadSpeed = 0.5f;
    public float maxLoadSpeed = 2.0f;

    void Start()
    {
        // ������������� �������
        timer = 0f;

        // ��������� ��������� �������� ��������
        loadSpeed = Random.Range(minLoadSpeed, maxLoadSpeed);

        // ���������, ��� ��������� ���� �� ������
        if (loadingText != null)
        {
            loadingText.text = "Loading... 0%";
        }

        // ������������� �������� ��������, ���� �� ����������
        if (loadingSlider != null)
        {
            loadingSlider.value = 0f;
        }
    }

    void Update()
    {
        // ���������� ������� � ����������� �� ���������� ������� � ��������� �������� ��������
        timer += Time.deltaTime * loadSpeed;

        // ������������ �������� �������� �������� ��� ���������� �����������
        if (Random.Range(0f, 1f) < 0.1f) // 10% ���� ��������� �������� �� ������ �����
        {
            loadSpeed = Random.Range(minLoadSpeed, maxLoadSpeed);
        }

        // ���������� �������� �������� ��������
        float progress = Mathf.Clamp01(timer / loadingTime);

        // ���������� ���������� ����
        if (loadingText != null)
        {
            loadingText.text = $"Loading... {progress * 100f:F0}%";
        }

        // ���������� �������� ��������
        if (loadingSlider != null)
        {
            loadingSlider.value = progress;
        }

        // ��������, ���� ������ ������ ������� ��������
        if (timer >= loadingTime)
        {
            // ������������ �� ��������� �����
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
