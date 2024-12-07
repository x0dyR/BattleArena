using System;

public class CharacterDiedCondition : ILooseCondition
{
    public event Action Lost;

    private Character _character;

    public CharacterDiedCondition(Character character)
    {
        _character = character;
    }

    public void Start() => _character.Died += OnDied;

    public void End() => _character.Died -= OnDied;

    private void OnDied(IDamageable damageable) => Lost?.Invoke();
}
