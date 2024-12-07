using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int _movingKey = Animator.StringToHash("IsMoving");
    private readonly int _idlingKey = Animator.StringToHash("IsIdling");

    private Animator _animator;

    private Character _character;

    public bool IsPaused { get; private set; }

    private void OnDisable()
    {
        _character.Mover.Moved -= OnMoved;
        _character.Mover.Stopped -= OnStopped;
    }

    public void Initialize(Character character)
    {
        _animator = GetComponent<Animator>();

        _character = character;
        _character.Mover.Moved += OnMoved;
        _character.Mover.Stopped += OnStopped;
    }

    private void OnStopped()
    {
        StopMoving();
        StartIdling();
    }

    private void OnMoved()
    {
        StopIdling();
        StartMoving();
    }

    private void StartMoving() => _animator.SetBool(_movingKey, true);

    private void StopMoving() => _animator.SetBool(_movingKey, false);

    private void StartIdling() => _animator.SetBool(_idlingKey, true);

    private void StopIdling() => _animator.SetBool(_idlingKey, false);
}
