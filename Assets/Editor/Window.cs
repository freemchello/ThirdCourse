using UnityEngine;
using UnityEditor;

public class Window : EditorWindow
{

    public Color myColor;         // �������� �����
    public MeshRenderer GO;      // ������ �� ������ �������

    public Material newMaterial;
    private Transform MainCam;

    [MenuItem("����������� /����/ ��������� ��������")]

    public static void ShowMyWindow()
    {
        GetWindow(typeof(Window), false, "��������� ��������");
    }
    void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("��� �������", GO, typeof(MeshRenderer), true) as MeshRenderer;
        newMaterial = EditorGUILayout.ObjectField("�������� �������", newMaterial, typeof(Material), true) as Material;

        if (GO)
        {
            myColor = RGBSlider(new Rect(10, 50, 200, 20), myColor);  // ��������� ����������������� ������ ��������� ��� ��������� ��������� �����
            GO.sharedMaterial.color = myColor; // �������� �������
        }
        else
        {
            if(GUI.Button(new Rect(10, 60, 100, 30),"�������"))
            {
                MainCam = Camera.main.transform;

                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                MeshRenderer GORenderer = temp.GetComponent<MeshRenderer>();
                GORenderer.sharedMaterial = newMaterial;
                temp.transform.position = new Vector3(MainCam.position.x-10f, MainCam.position.y, MainCam.position.z);
                GO = GORenderer;
            }
        }
        if(GUI.Button(new Rect(10, 160, 100, 30), "�������"))
        {
            DestroyImmediate(GO.gameObject);
            GO = null;
        }
    }

    // ��������� ����������������� ��������
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) // �� �������� MinValue
    {
        // ������ ������������� � ������������ � ������������ � ������� ������� � ������� 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // ������ Label �� ������

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // ����� ������� ��������
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // ������������ ������� � ��������� ��� ��������
        return sliderValue; // ���������� �������� ��������
    }

    // ��������� ������� ������� ������, ������ ������� �������� �� ���� ����
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // ��������� ���������������� �������, ������ ���
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, "Red");

        // ������ ����������
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, "alpha");

        return rgb; // ���������� ����
    }
}