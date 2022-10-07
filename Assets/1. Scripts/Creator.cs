using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Creator : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    private int _gridSize = 10;
    private int _numberOfBombs = 10;
    private Dictionary<Vector2Int, Cell> _cellsDictionary = new Dictionary<Vector2Int, Cell>();

    void Start()
    {
        CreateCell();
        CreateBomb();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Regenerate();
    }

    private void CreateCell()
    {
        for (int x = 0; x < _gridSize; x++)
        {
            for (int y = 0; y < _gridSize; y++)
            {
                Cell newCell = Instantiate(_cellPrefab, new Vector3(y, x, 0), transform.rotation, transform);
                newCell.AddPosition(x, y);
                _cellsDictionary.Add(new Vector2Int(x, y), newCell);
            }
        }
    }

    private void CreateBomb()
    {
        int[] array = ShuffleCell(_numberOfBombs);

        for (int i = 0; i < _numberOfBombs; i++)
        {
            Cell bombCell = _cellsDictionary.ElementAt(array[i]).Value;
            bombCell.MakeBomb();
            NeighbotSearch(bombCell);
        }
    }

    private int[] ShuffleCell(int number)
    {
        int NumberCell = number * number;
        int[] intArray = new int[NumberCell];

        for (int i = 0; i < NumberCell; i++)
            intArray[i] = i;

        for (int i = intArray.Length - 1; i > 0; i--)
        {
            int temp = intArray[i];
            int randomIndex = Random.Range(0, NumberCell);
            intArray[i] = intArray[randomIndex];
            intArray[randomIndex] = temp;
        }
        return intArray;
    }

    private void NeighbotSearch(Cell cell)
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int neighborPosition = cell.Position + new Vector2Int(x, y);
                if (_cellsDictionary.ContainsKey(neighborPosition))
                    _cellsDictionary[neighborPosition].AddOneNeighbour();
            }
        }
    }

    public void NeighbourCheck(Vector2Int position)
    {
    Cell cell = _cellsDictionary[position];
        if (cell.IsOpened) return;
        cell.Open();
        if (cell.NeighbourBombs == 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2Int key = position + new Vector2Int(x, y);
                    if (_cellsDictionary.ContainsKey(key))
                        NeighbourCheck(key);
                }
            }
        }
    }

    private void Regenerate()
    {
        foreach (Cell cell in _cellsDictionary.Values)
            Destroy(cell.gameObject);

        _cellsDictionary.Clear();
        CreateCell();
        CreateBomb();
    }
}