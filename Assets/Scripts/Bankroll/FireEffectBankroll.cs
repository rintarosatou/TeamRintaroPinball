using UnityEngine;

/// <summary>
/// �Փ˂����{�[���ɉΑ����̃G�t�F�N�g��t�^����o���N���[���B
/// </summary>
public class FireEffectBankroll : BankrollBase
{
    public override void OnBankrollEffect(GameObject ballObject)
    {
        if (ballObject.TryGetComponent<BallEffectBehaviour>(out var component))
        {
            component.SetBallEffect(BallEffect.Fire);
        }
    }
}
