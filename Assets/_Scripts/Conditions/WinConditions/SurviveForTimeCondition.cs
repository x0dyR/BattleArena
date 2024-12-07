using System;
using System.Collections;
using UnityEngine;

public class SurviveForTimeCondition : IWinCondition
{
    public event Action Won;

    private MonoBehaviour _context;

    private float _timeToSurvive;
    private Character _character;

    private bool _isWon = true;

    public SurviveForTimeCondition(MonoBehaviour context, float timeToSurvive, Character character)
    {
        _context = context;

        _timeToSurvive = timeToSurvive;
        _character = character;

        _character.Died += OnDied;
    }

    public void Start() => _context.StartCoroutine(Timer(_timeToSurvive));

    public void End() => _character.Died -= OnDied;

    private void OnDied(IDamageable obj) => _isWon = false;

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);

        if (_isWon)
            Won?.Invoke();
    }
}
