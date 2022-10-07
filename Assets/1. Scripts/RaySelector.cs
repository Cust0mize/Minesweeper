using UnityEngine;

public class RaySelector : MonoBehaviour
{
    private Cell _currentCell;

    private void Update()
    {
        FlashCell();
        if (Input.GetMouseButtonDown(0))
            _currentCell.Click();
    }

    private void FlashCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.TryGetComponent(out Cell cell))
            {
                _currentCell?.Unselected();
                _currentCell = cell;
                _currentCell.Selected();
            }
        }
        else
            _currentCell?.Unselected();
    }
}
