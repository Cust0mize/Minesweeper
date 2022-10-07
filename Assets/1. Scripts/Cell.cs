using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshPro _numberBombText;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _howeredMaterital;
    [SerializeField] private Material _defaultMaterital;
    [SerializeField] private Material _openedMaterital;
    [SerializeField] private Color[] _textColor;
    private BoxCollider _cellCollider;

    //попробовать убрать этот паблик, перенести метод в этот класс
    public int NeighbourBombs { get; private set; }
    public bool _isBomb { get; set; }
    public bool IsOpened { get; private set; }
    private bool _hasFlag;
    public Vector2Int Position { get; private set; }

    private void Awake()
    {
        _numberBombText.gameObject.SetActive(false);
        _bombPrefab.SetActive(false);
        _cellCollider = GetComponent<BoxCollider>();
    }

    public void MakeBomb()
    {
        _isBomb = true;
    }

    public void AddOneNeighbour()
    {
        if (_isBomb) return;
        NeighbourBombs++;
        _numberBombText.color = _textColor[NeighbourBombs - 1];
        _numberBombText.text = NeighbourBombs.ToString();
    }

    public void AddPosition(int x, int y)
    {
        Position = new Vector2Int(x, y);
    }

    public void Open()
    {
        _hasFlag = false;
        _flagPrefab.SetActive(false);
        Unselected();
        IsOpened = true;
        _cellCollider.enabled = false;
        _renderer.material = _openedMaterital;

        if (_isBomb)
        {
            _bombPrefab.SetActive(true);
            EventManager.OnLose();
        }
        else
        {
            if (NeighbourBombs != 0)
                _numberBombText.gameObject.SetActive(true);
        }
    }

    public void SetFlag()
    {
        _hasFlag = !_hasFlag;
        _flagPrefab.SetActive(!_flagPrefab.activeSelf);
    }

    public void Selected()
    {
        _renderer.material = _howeredMaterital;
    }

    public void Unselected()
    {
        if (!IsOpened)
            _renderer.material = _defaultMaterital;
    }
}