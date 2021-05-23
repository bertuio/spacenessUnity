using UnityEngine;

public class MineralResearchBoulder : MonoBehaviour
{
    [SerializeField] private Material _rareMaterial;
    private Vector3 _rotation;
    private bool _rare = false;
    public event System.Action<bool> OnBoulderGrabbed;
    private const int _power = 1;
    public string Id { get; private set; }
    public bool Rare { get => _rare; set => _rare = value; }

    private static string[] _prefixes = { "HK", "UN", "EPQ", "DRM", "ABR" };

    public void MakeRare() 
    {
        if (TryGetComponent(out MeshRenderer renderer)) 
        {
            renderer.material = _rareMaterial;
        }
        Rare = true;
    }
    private void Awake()
    {
        Id = GenerateId();
        _rotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _power;
        Debug.Log(Id);
    }

    private string GenerateId()
    {
        return _prefixes[Random.Range(0, _prefixes.Length)]+'-'+(Random.Range(0, 1000).ToString().PadLeft(4,'0'));
    }

    public void Grab() 
    {
        EventLogDisplay.display.AddEvent(Id + "_demolished");
        OnBoulderGrabbed?.Invoke(Rare);
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        transform.Rotate(_rotation, UnityEngine.Space.World);
    }
}
