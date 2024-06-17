using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private float _moveSpeed = 250f;

        private Rigidbody2D _rigidbody;

        private Vector2 _velocity;
        public Vector2 Velocity { get { return _velocity; } }
        public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

        public void Construct(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        //TODO: ��������� ������� �������� ����
        private void Start()
        {
            IGameLoopListener.Register(this);
        }

        //TODO: ����� ����� ����������� ������, ��� Awake � GameLoopManager, ������� �� ��� ������� ����� ������� � ��� �������������
        //��������������, ������������ �������� �� �����
        //������� - ��������� ������ �� ������� ����� ������������� ���� ��������� �������
        //private void OnEnable()
        //{
        //    IGameLoopListener.Register(this);
        //}

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void Move(Vector2 moveVector)
        {
            _velocity = moveVector;
        }

        public void OnFixedUpdate(float delta)
        {
            _rigidbody.velocity = _velocity * delta * _moveSpeed;
        }
    }
}