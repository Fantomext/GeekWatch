using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectTransform
{
    public Transform target;
    public Vector3 position;
    public Vector3 rotation;
}

public class MinecraftDoorScript : MonoBehaviour
{
    public float interactionDistance = 5f; // Расстояние взаимодействия
    public List<ObjectTransform> otherObjects; // Список других объектов для взаимодействия

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверка нажатия кнопки мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactionDistance)) // Проверка попадания луча в объект
            {
                if (hit.transform == this.transform) // Проверка, является ли объект целью взаимодействия
                {
                    this.transform.Rotate(Vector3.up, 90); // Поворот объекта

                    foreach (ObjectTransform obj in otherObjects) // Изменение позиций и поворотов других объектов
                    {
                        obj.target.position = obj.position;
                        obj.target.rotation = Quaternion.Euler(obj.rotation);
                    }
                }
            }
        }
    }
}
