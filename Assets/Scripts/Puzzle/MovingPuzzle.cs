using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPuzzle : MonoBehaviour
{
    private bool move;
    private Vector2 mousePos;
    private float startPosX;
    private float startPosY;

    [SerializeField] private GameObject form;
    [SerializeField] private bool finish = false;

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
            mousePos = Input.mousePosition;

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
        }
    }

    void OnMouseUp()
    {
        move = false;

        if (Mathf.Abs(this.transform.localPosition.x - form.transform.localPosition.x) <= 10f &&
            Mathf.Abs(this.transform.localPosition.y - form.transform.localPosition.y) <= 10f && !finish)
        {
            this.transform.localPosition = new Vector2(form.transform.localPosition.x, form.transform.localPosition.y);
            finish = true;
            WinScrpte.AddElement();
        }
    }

    void Update()
    {
        if (move && !finish)
        {
            mousePos = Input.mousePosition;

            this.gameObject.transform.localPosition = new Vector2(mousePos.x - startPosX, mousePos.y - startPosY);
        }
        if (finish)
        {
            // Перемещаем объект вниз по оси Z
            Vector3 currentPos = transform.localPosition;
            currentPos.z = 2;
            transform.localPosition = currentPos;
        }
    }
}
