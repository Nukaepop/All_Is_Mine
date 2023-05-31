using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCharacter : Character
{

    public WeaponParent playerWeaponScript;
    public List<Item> lootItems;
    public Animator animator;

    public GameObject floatingTextPrefab;
    [SerializeField] FloatingHelathBar healthBar;


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHelathBar>();
    }

    private void Update()
    {
        if (health >= maxHealth)
        {
            healthBar.HideHealthBar();
        }
        else
        {
            healthBar.ShowHealthBar();
        }
    }

    public override void TakeDamage(int damage)
    {
        // Logique spécifique pour les personnages ennemis lorsqu'ils subissent des dégâts
        base.TakeDamage(damage); // Appel à la méthode de la classe de base pour gérer les points de vie
        // Autres actions spécifiques aux ennemis
        animator.SetTrigger("DamageReceived");
        ShowDamage(damage.ToString());

        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <=0)
        {
            DropLoot(); // methode pour le butin
            Destroy(gameObject);
            Debug.Log("Enemy Died");

        }
    }

    public override void Heal(int amount)
    {
        // Les personnages ennemis ne peuvent généralement pas être soignés, cette méthode peut être laissée vide
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            int damage = playerWeaponScript.AttackDamage;
            TakeDamage(damage);

            Debug.Log("Enemy Hit");
        }
    }

    private void DropLoot()
    {
        foreach (Item lootItem in lootItems)
        {
            if (Random.value >= lootItem.dropRate)
            {
             /*   Vector2 randomOffset = Random.insideUnitCircle * 1.5f;*/
                Vector3 spawnPosition = transform.position /*+ new Vector3(randomOffset.x, randomOffset.y, 0f)*/; 
                Instantiate(lootItem.Object, spawnPosition, Quaternion.identity);
            }
        }
    }

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }

}