using UnityEngine;


public class MyAdvancedGUI : MonoBehaviour
{
    [SerializeField][Header("Отладочная переменная")][Range(0, 100)]
    [Tooltip("Значение находится в диапазоне от 0 до 100")]
    private float FLOAT = 1.0f; // Положение пользовательского слайдера

    [SerializeField] [Header("Поле для текста")] [TextArea(5,6)]
    private string TEXT;

    [SerializeField] [Range(0, 50)] [Tooltip("Значение находится в диапазоне от 0 до 50")] 
    private int my3Slider = 1;

    private string[] textMass = { "100hp", "100mp" };

    public Color myColor;         // Градиент цвета
    public MeshRenderer GO;      // Ссылка на рендер объекта

    void OnGUI()
    {
        myColor = RGBSlider(new Rect(10, 30, 200, 20), myColor);  // Отрисовка пользовательского набора слайдеров для получения градиента цвета
        GO.material.color = myColor; // Покраска объекта
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

        GUI.Box(new Rect(10, 5, 45, 25), textMass[0]);

        return rgb; // возвращаем цвет
    } 
}
