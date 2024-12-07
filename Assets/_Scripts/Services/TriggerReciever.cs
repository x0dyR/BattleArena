using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class TriggerReciever : MonoBehaviour
{
    public event Action<Collider> Triggered;
    public CapsuleCollider Collider { get; private set; }

    public void Initiazlie(float triggerRadius)
    {
        Collider = GetComponent<CapsuleCollider>();
        Collider.radius = triggerRadius * 2; //при radius * 2, норм работает
    }

    private void OnTriggerEnter(Collider other) => Triggered?.Invoke(other);
}
