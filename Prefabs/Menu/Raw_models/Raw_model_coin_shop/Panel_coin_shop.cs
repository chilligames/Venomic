using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BazaarPlugin;
using UnityEngine.UI;

public class Panel_coin_shop : MonoBehaviour
{
    public Button BTN_buy_800;

    void Start()
    {
        BazaarIAB.init("MIHNMA0GCSqGSIb3DQEBAQUAA4G7ADCBtwKBrwCsKayLopsdltsho45vsaVhWeamm89xS62xwub2QU8DF9AOndmaLr3yP+lP53tdwNc5V4wVEyb6/EIZWMEZdWAYH2oNOhLNkK2MBSaQ0fHWnXnVTnAoJUnzVJxzCVPOpXAtOK5SVZiDMjlx3q16eYZe6y1ams6+mIcTDpCogHBeQlKT3jWzhTIdyGsz+d7MhwYa5rNU/CzRN09L70XNWctFdF0VCXZkCCFhIgszExUCAwEAAQ==");
        BTN_buy_800.onClick.AddListener(() =>
        {
            BazaarIAB.purchaseProduct("CH-V-C");
            print("hi");
        });

    }


    private void OnDestroy()
    {
        print("Code close bazar herere");
    }
}
