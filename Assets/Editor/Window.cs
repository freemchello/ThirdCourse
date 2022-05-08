using UnityEngine;
using UnityEditor;

public class Window : EditorWindow
{

    public Color myColor;         // Градиент цвета
    public MeshRenderer GO;      // Ссылка на рендер объекта

    public Material newMaterial;
    private Transform MainCam;

    [MenuItem("Инструменты /Окна/ Генератор префабов")]

    public static void ShowMyWindow()
    {
        GetWindow(typeof(Window), false, "Генератор префабов");
    }
    void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("Мэш объекта", GO, typeof(MeshRenderer), true) as MeshRenderer;
        newMaterial = EditorGUILayout.ObjectField("Материал объекта", newMaterial, typeof(Material), true) as Material;

        if (GO)
        {
            myColor = RGBSlider(new Rect(10, 50, 200, 20), myColor);  // Отрисовка пользовательского набора слайдеров для получения градиента цвета
            GO.sharedMaterial.color = myColor; // Покраска объекта
        }
        else
        {
            if(GUI.Button(new Rect(10, 60, 100, 30),"Создать"))
            {
                MainCam = Camera.main.transform;

                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                MeshRenderer GORenderer = temp.GetComponent<MeshRenderer>();
                GORenderer.sharedMaterial = newMaterial;
                temp.transform.position = new Vector3(MainCam.position.x-10f, MainCam.position.y, MainCam.position.z);
                GO = GORenderer;
            }
        }
        if(GUI.Button(new Rect(10, 160, 100, 30), "Удалить"))
        {
            DestroyImmediate(GO.gameObject);
            GO = null;
        }
    }

    // Отрисовка пользовательского слайдера
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) // ДЗ добавить MinValue
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }

    // Отрисовка тройной слайдер группы, каждый слайдер отвечает за свой цвет
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // Используя пользовательский слайдер, создаём его
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, "Red");

        // делаем промежуток
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, "alpha");

        return rgb; // возвращаем цвет
    }
}