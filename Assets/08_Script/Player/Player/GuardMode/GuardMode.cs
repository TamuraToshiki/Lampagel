//======================================================================
// GuardMode.cs
//======================================================================
// 開発履歴
//
// 2022/03/01 author：田村敏基 ガード状態を管理するスクリプト
//                             止まる機能実装
// 2022/03/03 author：田村敏基 ガードゲージなどのガードに必要な機能実装
// 2022/03/11 author：田村敏基 UI機能実装(時間がなかっため、作り直したい)
//                             爆発実装(敵に効果なし...)
// 2022/03/27 author：田村敏基 爆発の威力を蓄える機能実装
// 2022/03/28 author：竹尾　応急 エフェクト発生組み込み
//
//======================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 判定コンポーネントアタッチ
[RequireComponent(typeof(Stop))]
[RequireComponent(typeof(GuardBurst))]

public class GuardMode : MonoBehaviour
{
    // 停止
    private Stop stop;

    // バースト
    private GuardBurst burst;

    // リジッドボディ
    private Rigidbody rb;

    // ステート
    private PlayerState state;

    // ガードゲージ
    private PlayerStatus status;
    [SerializeField] private int nRecovery = 2;
    [SerializeField] private int nCost = 1;

    // 爆発威力を収納
    private float fStockBurst = 0.0f;

    // 硬化中にローテするための変数
    bool bGuardStart = false;

    //*応急* エフェクトスクリプト
    [SerializeField] AID_PlayerEffect effect;

    // ガードモデルとデフォルトモデル
    [SerializeField] private GameObject DefaultModel;
    [SerializeField] private GameObject GuardModel;

    // ガード中に向き変えるための変数
    private Vector3 vStartPos = Vector3.zero;
    private Vector3 vCurrentForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        stop = GetComponent<Stop>();
        burst = GetComponent<GuardBurst>();
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!state.IsHard)
        {
            // ゲージ回復
            RecoveryGauge();
            DefaultModel.SetActive(true);
            GuardModel.SetActive(false);
        }
        // ハードモードなら
        if (state.IsHard)
        {
            // キーボード
            if(!bGuardStart)
            {
                vStartPos = GetMousePosition();
                bGuardStart = true;
            }
            // パッド
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            if (Mathf.Abs(x) >= 0.5f || Mathf.Abs(y) >= 0.5f)
            {
                // 入力方向を逆にして受け取る
                vCurrentForce = new Vector3(-x * 1000, 0, -y * 1000);

                // 動く方向を見る
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }


            // ゲージ消費
            SubtractGauge();
            DefaultModel.SetActive(false);
            GuardModel.SetActive(true);

            // 動かしたマウス座標の位置を取得
            var position = GetMousePosition();
            // マウスの初期座標と動かした座標の差分を取得
            vCurrentForce = vStartPos - position;

            // 動く方向を見る
            if (vCurrentForce != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.LookRotation(vCurrentForce);
            }
        }
        // バーストモードなら
        if (state.IsBurst)
        {
            //*応急*
            effect.StartEffect(1, this.gameObject, 1.0f);

            // 爆発
            burst.Explode(fStockBurst);
            // 瞬間的に力を加えてはじく
            rb.AddForce(transform.forward * fStockBurst, ForceMode.Impulse);
            status.bArmor = true;
            status.fBreakTime = 0.0f;
            state.GotoNormalState();
            fStockBurst = 0.0f;
            bGuardStart = false;
        }

        // ハードモードかつゲージ残量があるなら停止
        if (state.IsHard && status.Stamina > 0)
        {
            stop.DoStop(rb);
        }
        else
        {
            state.GotoNormalState();
            state.bGuard = false;
            bGuardStart = false;
        }
    }

    // ゲージ回復
    private void RecoveryGauge()
    {
        // ゲージ量回復
        status.Stamina += nRecovery;
        if (status.Stamina >= status.MaxStamina)
        {
            status.Stamina = status.MaxStamina;
        }
    }

    // ゲージ消費
    private void SubtractGauge()
    {
        status.Stamina -= nCost;
        if (status.Stamina < 0)
        {
            status.Stamina = 0;
        }
    }

    public void AddStockExplode(float damage)
    {
        fStockBurst += damage;
    }

    private Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
    }

}
