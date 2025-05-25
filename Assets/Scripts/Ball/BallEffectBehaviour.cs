using UnityEngine;

/// <summary>
/// �{�[���̃G�t�F�N�g��ێ�����N���X�B
/// </summary>
public class BallEffectBehaviour : MonoBehaviour
{
    private BallEffect _effect;
    public BallEffect Effect { get => _effect; }

    [SerializeField] private GameObject _fireEffectPrefab;
    public void SetBallEffect(BallEffect ballEffect)
    {
        if (_effect != ballEffect)
        {
            _effect = ballEffect;
            if (ballEffect == BallEffect.Fire)
            {
                Instantiate(_fireEffectPrefab, transform);
            }
        }
    }
}
public enum BallEffect
{
    None,
    Fire,
}