using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshPro _numberBombText;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _howeredMaterital;
    [SerializeField] private Material _defaultMaterital;
    [SerializeField] private Material _openedMaterital;
    private BoxCollider _cellCollider;

    private int _neighbourBombs;
    public bool _isBomb { get; set; }
    private int _group = -1;
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
        _bombPrefab.SetActive(true);
        _numberBombText.gameObject.SetActive(false);
    }

    public void AddOneNeighbour()
    {
        if (_isBomb) return;
        _numberBombText.gameObject.SetActive(true);
        _neighbourBombs++;
        _numberBombText.text = _neighbourBombs.ToString();
    }

    public void AddPosition(int x, int y)
    {
        Position = new Vector2Int(x, y);
    }

    public void Click()
    {
        _cellCollider.enabled = false;
        _renderer.material = _openedMaterital;
        
    }

    public void Selected()
    {
        _renderer.material = _howeredMaterital;
    }

    public void Unselected()
    {
        _renderer.material = _defaultMaterital;
    }
}
