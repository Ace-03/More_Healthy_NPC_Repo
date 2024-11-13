public interface IHealth
{
    event System.Action<float> OnHPPctChanged;
    event System.Action OnDied;
    void TakeDamage(int amount);
    void GainHealth(int amount);
    void GetPoisoned(int amount);
}
