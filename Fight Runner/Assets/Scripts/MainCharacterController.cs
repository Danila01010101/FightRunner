using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _startCamPos;

    public bool _isMovesOnXAxis = true;
    public bool _canRun = false;
    public bool _canControll = true;
    private bool _isAlive = true;

    private bool _isFighting = false;
    private bool _canEnterBercerkMode = true;
    private bool _isGameStarted = false;
    private bool _canEnterBersercMode => _straightLevel > _bersercModeEnterValue;
    public bool _isInBersercMode = false;

    private string _isInBersercModeAnim = "IsInBersercMode";
    private string _isGameStartedAnimName = "IsGameStarted";
    private string _isFightingAnimationName = "IsFighting";
    private string _combatStartedAnim = "CombatStarted";
    private string _canRunAnimationName = "CanRun";
    private string _isAliveAnim = "IsAlive";
    private string _enemyTag = "Enemy";

    private Vector3 _usualCharacterSize;
    private Vector3 _usualColliderSize;
    [SerializeField]
    private GameObject _startGameText;
    [SerializeField]
    private StraightBar _straightBar;
    [SerializeField]
    private int _bersercModeEnterValue = 180;
    [SerializeField]
    private int _healValue = 2;
    [SerializeField]
    private ParticleSystem _explosionEffect;
    [SerializeField]
    public int _straightLevel = 15;
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _turnSpeed = 1f;
    [SerializeField]
    private float _bersercModeDuration = 7f;
    private string _damageSaveName = "Damage";
    private int _damage = 3;
    private Vector3 _rotationOfPlayer = Vector3.zero;
    public Vector3 _directionToMove;
    private Vector3 _directionToRun = Vector3.right;
    private Vector3 _cameraRotation;
    public float _endOfLevelPos;

    [SerializeField]
    public Vector3 forward = new Vector3(-2, 4, -5);

    [SerializeField]
    public Vector3 right = new Vector3(-9, 12, -0.5f);

    public Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _canRun = true;
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _animator.SetBool(_canRunAnimationName, _canRun);
        _straightBar = _straightBar.GetComponent<StraightBar>();
        _straightBar.SetMaxBarValue(_bersercModeEnterValue);
        _straightBar.SetStraight(_straightLevel);
        Camera.main.GetComponent<TrackTarget>().ChangeTargetToTrack(gameObject);
        _startGameText = Instantiate(_startGameText, new Vector3 (540, 810),_startGameText.transform.rotation, _straightBar.GetComponentInParent<Transform>());
        if (PlayerPrefs.GetInt(_damageSaveName) == 0)
        {
            PlayerPrefs.SetInt(_damageSaveName, 3);
        }
        _damage = PlayerPrefs.GetInt(_damageSaveName);

        Vector3 _cameraRotation = right;
        ChangeRotationAndDirection(Vector3.up * 90, Vector3.right, forward, _startCamPos);
    }

    private void FixedUpdate()
    {   
        if (_isGameStarted)
        {
            TryMove();
            if (_canEnterBersercMode && !_isInBersercMode && _canEnterBercerkMode)
            {
                EnterBersercMode();
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).position.y < 1300)
            {
                _isGameStarted = true;
                _animator.SetBool(_isGameStartedAnimName,_isGameStarted);
                Destroy(_startGameText);
            }
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_rotationOfPlayer), 100);
    }

    public void Heal()
    {
        if (_straightLevel < _bersercModeEnterValue - 1 || _canEnterBercerkMode)
            _straightLevel += _healValue;
        _straightBar.SetStraight(_straightLevel);
    }

    private void TryMove()
    {
        if (Input.touchCount > 0 && _canControll)
        {
            Touch touch = Input.GetTouch(0);
            if (_isMovesOnXAxis)
            {
                Vector3 touchpos = new Vector3(transform.position.x + touch.deltaPosition.x * _turnSpeed, transform.position.y, transform.position.z);

                var direction = touchpos - gameObject.transform.position;
                _directionToMove = new Vector3(direction.x, 0, 0) * _turnSpeed;
            }
            else
            {
                Vector3 touchpos = new Vector3(transform.position.x, transform.position.y, transform.position.z - touch.deltaPosition.x * _turnSpeed);

                var direction = touchpos - gameObject.transform.position;
                _directionToMove = new Vector3(0, 0, direction.z) * _turnSpeed;
            }
        }
        else if (_canControll)
        {
            _directionToMove = Vector3.zero;
        }
        else
        {
            _directionToMove = new Vector3 (0 - gameObject.transform.position.x,0,0) * 3;
        }

        if (_canRun)
        {
            _rigidbody.velocity = (_directionToRun * _moveSpeed) + _directionToMove;
        }
        else
        {
            _rigidbody.velocity = _directionToMove;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_enemyTag))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if (!_isInBersercMode)
            {
                _canRun = false;
                _animator.SetBool(_canRunAnimationName, _canRun);
                _isFighting = true;
                _animator.SetBool(_isFightingAnimationName, _isFighting);
                StartCoroutine("StartFight", enemy);
            }
            else
            {
                _canRun = false;
                StartCoroutine(WinFightAfterSeconds(0.4f, enemy));
                StartCoroutine(StartRunForSeconds(0.5f));
                _animator.SetTrigger(_combatStartedAnim);
                StartCoroutine(ExplodeAfterSeconds(0.39f));
            }
        }
    }

    private void EnterBersercMode()
    {
        StartCoroutine("ExitBersercMode", _bersercModeDuration);
        _canEnterBercerkMode = false;

        _isInBersercMode = true;
        _animator.SetBool(_isInBersercModeAnim, _isInBersercMode);
        _usualCharacterSize = transform.localScale;
        transform.localScale = new Vector3(2.4f,1.4f,1.4f);
        _usualColliderSize = gameObject.GetComponent<BoxCollider>().size;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(1.2f,1.6f,1.2f);
    }

    IEnumerator ExitBersercMode(float bersercModeDuration)
    {
        yield return new WaitForSeconds(bersercModeDuration);
        _isInBersercMode = false;
        _animator.SetBool(_isInBersercModeAnim, _isInBersercMode);
        gameObject.GetComponent<BoxCollider>().size = _usualColliderSize;
        transform.localScale = _usualCharacterSize;
        _straightLevel = _bersercModeEnterValue - 1;
        _straightBar.SetStraight(_straightLevel);
    }

    private void WinFight(EnemyBehavior enemy)
    {
        
        _animator.SetBool(_canRunAnimationName, true);
        _canRun = true;
        enemy._animator.SetBool(_isAliveAnim, enemy._isAlive);
        enemy.LoseFight();
        _isFighting = false;
        _animator.SetBool(_isFightingAnimationName, _isFighting);
    }

    private void LoseFight(EnemyBehavior enemy)
    {
        StartCoroutine("RestartLevel");
        _isFighting = false;
        _animator.SetBool(_isFightingAnimationName, _isFighting);
        _isAlive = false;
        _animator.SetBool(_isAliveAnim, _isAlive);
    }

    private void TakeHit(EnemyBehavior enemy)
    {
        if (enemy._isFighting)
        {
            _straightLevel -= 3;
            if (_straightLevel <= 0)
            {
                _straightLevel = 0;
                LoseFight(enemy);
            }
                
            _straightBar.SetStraight(_straightLevel);
        }
    }


    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    IEnumerator StartFight(EnemyBehavior enemy)
    {
        while (_isFighting)
        {
            TakeHit(enemy);
            enemy.TakeHit();
            //if (enemy.straightLevel == 1)
            //    StartCoroutine(SlowTimeForSeconds(0.3f));
            if (enemy.straightLevel <= 0)
            {
                StartCoroutine("WinFight", enemy);
                break;
            }
            else if (_straightLevel < 0)
            {
                StartCoroutine("LoseFight", enemy);
                break;
            }
            yield return new WaitForSeconds(0.17f);
        }
    }

    IEnumerator StartRunForSeconds(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        _canRun = true;
    }

    IEnumerator ExplodeAfterSeconds(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        Instantiate(_explosionEffect,
         new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z + 2.2f),
         _explosionEffect.transform.rotation);
    }

    IEnumerator WinFightAfterSeconds(float _seconds, EnemyBehavior enemy)
    {
        yield return new WaitForSeconds(_seconds);
        StartCoroutine(SlowTimeForSeconds(0.24f));
        WinFight(enemy);
    }

    IEnumerator SlowTimeForSeconds(float _seconds)
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(_seconds);
        Time.timeScale = 1;
    }
    public void ChangeRotationAndDirection(Vector3 newRotation, Vector3 newDirection, Vector3 cameraRotation, Vector3 newPos)
    {
        _directionToRun = newDirection;
        _rotationOfPlayer = newRotation;
        _cameraRotation = cameraRotation;
        Camera.main.GetComponent<TrackTarget>().ChangeCameraPosition(newPos);
        _isMovesOnXAxis = !_isMovesOnXAxis;
    }

}

