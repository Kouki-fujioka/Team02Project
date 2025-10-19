using UnityEngine;

public class Baton : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _batonSpeed;

    /// <summary>
    /// バトンパスの合計回数
    /// </summary>
    public int BattonPassTotalCounter;

    /// <summary>
    ///  バトンパスのコンボ回数
    /// </summary>
    public int BattonPassComboCounter;
    
    private bool _isMoving;
    private float _totalDistance;
    private Vector3 _playerFront;
    private Vector3 _diffVector;
    private float _diff;
    private GameObject _parent;
    private bool _isBatonControl;
    private Rigidbody _rb;

    void Start()
    {
        _isMoving = false;        
        _totalDistance = 0;
        transform.localPosition = new Vector3(0, _height, 0);
        _parent = transform.parent.gameObject;
        _isBatonControl = false;
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.angularVelocity = new Vector3(0, 0, 0);
        BattonPassTotalCounter = 0;
        BattonPassComboCounter = 0;
    }

    void Update()
    {
        //Debug.Log($"Total:{BattonPassTotalCounter}, Combo:{BattonPassComboCounter}");
        // バトン発射処理
        if (Input.GetKeyDown(KeyCode.Space) && _isMoving == false && _parent != null)
        {
            Debug.Log("バトン発射");
            _isMoving = true;
            _playerFront = transform.parent.gameObject.transform.forward;
            _diff = Time.deltaTime * _parent.gameObject.GetComponent<Player>()._moveSpeed;
            _diffVector = new Vector3(_playerFront.x * _diff, 0, _playerFront.z * _diff);
            transform.parent = null;
            _parent.gameObject.GetComponent<Player>().m_isControllerCharcter = false;
            _rb.useGravity = true;
            AudioManager.Instance.PlaySE("Throw", -1.0f);
        }
        // バトン発射による移動中の動作処理
        else if (_isMoving)
        {
            var position = transform.position;
            _totalDistance += _diff;
            transform.position = new Vector3(position.x + _diffVector.x, position.y, position.z + _diffVector.z);

            if (_totalDistance > _parent.gameObject.GetComponent<Player>()._maxDistance)
            {
                Debug.Log("バトン移動終了");
                _isMoving = false;
                _totalDistance = 0;
                _parent = null;
                _diff = 0;
                _diffVector = Vector3.zero;
                // バトンの操作を可能にする
                _isBatonControl = true;
            }
        }
        // バトンの操作
        else if (_isBatonControl)
        {
            Vector2 vector = Vector2.zero;

            if (Input.GetKey(KeyCode.A)) vector.x--;
            if (Input.GetKey(KeyCode.D)) vector.x++;
            if (Input.GetKey(KeyCode.W)) vector.y++;
            if (Input.GetKey(KeyCode.S)) vector.y--;

            if (vector != Vector2.zero)
            {
                BattonPassComboCounter = 0;
            }

            Vector3 velocity = Camera.main.transform.right * vector.x + Camera.main.transform.forward * vector.y;
            velocity.y = 0.0f;
            transform.Translate(velocity.normalized * _batonSpeed * Time.deltaTime, Space.World);
        }
        else
        {   
            transform.localPosition = new Vector3(0, _height, 0);
        }
    }

    // バトンの衝突処理
    private void OnCollisionEnter(Collision collision)
    {
        // 地面と衝突したときの処理
        if (collision.gameObject.name == "Plane")
        {
            AudioManager.Instance.PlaySE("Drop", 0.0f);
            EffectManager.Instance.PlayEffect(transform.position, "BoingText", 0.0f);
            _isBatonControl = true;
        }
        // 落ちているバトンを拾ったときの処理
        else if (_parent == null && collision.transform.GetComponent<Player>())
        {
            Debug.Log("落下バトン取得");
            EffectManager.Instance.PlayEffect(transform.position, "Smoke", 0.0f);
            AudioManager.Instance.PlaySE("Pick", 0.0f);
            transform.parent = collision.transform;
            collision.gameObject.GetComponent<Player>().m_isControllerCharcter = true;
            transform.localPosition = new Vector3(0, _height, 0);
            _parent = collision.gameObject;
            _rb.useGravity = false;
            _rb.angularVelocity = new Vector3(0, 0, 0);
            _isBatonControl = false;
        }
        // 他のプレイヤーからバトンを受け取ったときの処理
        else if (_parent != null && collision.gameObject != _parent.gameObject && collision.transform.GetComponent<Player>())
        {
            Debug.Log("バトン受け取り");
            EffectManager.Instance.PlayEffect(transform.position, "CatchStylish", 0.0f);
            AudioManager.Instance.PlaySE("Pick", 0.0f);
            _isMoving = false;
            _parent.gameObject.GetComponent<Player>().m_isControllerCharcter = false;
            transform.parent = collision.transform;
            collision.gameObject.GetComponent<Player>().m_isControllerCharcter = true;
            transform.localPosition = new Vector3(0, _height, 0);
            _rb.useGravity = false;
            _rb.angularVelocity = new Vector3(0, 0, 0);
            _isBatonControl = false;
            _parent = transform.parent.gameObject;
            _totalDistance = 0;
            _diff = 0;
            _diffVector = Vector3.zero;
            BattonPassTotalCounter++;
            BattonPassComboCounter++;
        }
    }
}
