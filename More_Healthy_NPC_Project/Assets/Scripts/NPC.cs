using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private int startingHp = 100;
    [SerializeField] private UnityEngine.UI.Slider hpBarSlider = null;

    internal void TakeDamage(int amount)
    {
        GetComponent<IHealth>().TakeDamage(amount);
    }

    internal void GainHealth(int amount)
    {
        GetComponent<IHealth>().GainHealth(amount);
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);
        hpBarSlider.transform.LookAt(Camera.main.transform);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(startingHp / 10);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            GainHealth(startingHp / 10);
        }
    }
}