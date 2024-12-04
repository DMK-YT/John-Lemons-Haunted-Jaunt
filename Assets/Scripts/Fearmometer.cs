using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fearmometer : MonoBehaviour
{
    Slider slider;
    [SerializeField] float speedOfChange;

    bool paralyzed;
    [SerializeField] GameObject paralyzedCanvas;
    [SerializeField] float paralyzedStepDown;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (paralyzed) { UpdateWhenScared(); } else { UpdateWhenMoving(); }
    }

    void UpdateWhenMoving() 
    {
        if (!GetAnyMovementKey())
        {
            slider.value -= Time.deltaTime * speedOfChange;
        }
        else
        {
            float speedMultiplier = 1f;
            if (Input.GetKey(KeyCode.LeftShift)) { speedMultiplier = 2f; }

            slider.value += Time.deltaTime * speedOfChange * speedMultiplier;
        }


        if (slider.value >= 1) 
        {
            paralyzed = true;
            PlayerMovement.SetCanMove(false);
            paralyzedCanvas.SetActive(true);
        }
    }

    void UpdateWhenScared()
    {
        if (GetAnyMovementKeyDown()) 
        {
            slider.value -= paralyzedStepDown;
        }

        if (slider.value <= 0)
        {
            paralyzed = false;
            PlayerMovement.SetCanMove(true);
            paralyzedCanvas.SetActive(false);
        }
    } 

    bool GetAnyMovementKey()
    {
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
    }
    bool GetAnyMovementKeyDown()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S);
    }
}
