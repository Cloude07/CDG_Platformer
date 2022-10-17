using CDG.Core.Player;
using System;
using UnityEngine;

namespace CDG.Components
{
    public class Heath : MonoBehaviour, IDamageable
    {
        public int MaxHealth;
        private int currentHeath;

        public event Action<int, int> OnHealthChange;

        private void OnValidate()
        {
            if (MaxHealth < 0)
                MaxHealth = 0;
        }
        private void Start()
        {
            currentHeath = MaxHealth;
            if ((TryGetComponent(out PlayerMove player)))
                OnHealthChange?.Invoke(currentHeath, MaxHealth);

        }
        public void ApplyDamage(int damage)
        {
            currentHeath -= damage;

            if (currentHeath <= 0)
            {
                this.gameObject.SetActive(false);
            }
            if ((TryGetComponent(out PlayerMove player)))
            {
                player.DontMove = true;
                player.Damageable();
                OnHealthChange?.Invoke(currentHeath, MaxHealth);
                player.DontMove = false;
            }

        }

        public void SetHealth(int addHealth)
        {
            currentHeath += addHealth;

            if ((TryGetComponent(out PlayerMove player)))
                OnHealthChange?.Invoke(currentHeath, MaxHealth);
        }
    }
}
