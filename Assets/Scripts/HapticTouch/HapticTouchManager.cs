using UnityEngine;
using MoreMountains.NiceVibrations;

public class HapticTouchManager
{
    public static bool IsHaptic
    {
        get
        {
            return PlayerPrefs.GetInt("IsHaptic", 1) == 0 ? false : true;
        }
        set
        {
            PlayerPrefs.SetInt("IsHaptic", value == true ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool IsHapticAvailable()
    {
#if UNITY_EDITOR
        return false;
#else
        return MMVibrationManager.HapticsSupported();
#endif
    }

    public static void HapticButton()
    {

        if (IsHaptic)
            MMVibrationManager.Haptic(HapticTypes.Selection);

    }

    public static void HapticSuccess()
    {

        if (IsHaptic)
            MMVibrationManager.Haptic(HapticTypes.Success);

    }

    public static void LightHapticTouch()
    {
        if (IsHaptic)
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }

    public static void HeavyHapticTouch()
    {
        //Debug.LogError("Heavy Haptic");

        if (IsHaptic)
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public static void MediumHapticTouch()
    {
        //Debug.LogError("Medium Haptic");

        if (IsHaptic)
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    public void PlayButton() 
    {
        HapticButton();
    }
}
