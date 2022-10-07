using UnityEngine;

public class RaySelector : MonoBehaviour
{
    private Cell _currentCell;
    [SerializeField] private Creator _creator;

    private void Update()
    {
        FlashCell();
        if (Input.GetMouseButtonDown(0))
            _creator.NeighbourCheck(_currentCell.Position);
        if (Input.GetMouseButtonDown(1))
            _currentCell.SetFlag();
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
