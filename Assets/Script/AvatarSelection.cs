using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    public Image avatarPreview;
    public Sprite maleAvatar;
    public Sprite femaleAvatar;

    public void SelectMaleAvatar()
    {
        avatarPreview.sprite = maleAvatar;
    }

    public void SelectFemaleAvatar()
    {
        avatarPreview.sprite = femaleAvatar;
    }
}
