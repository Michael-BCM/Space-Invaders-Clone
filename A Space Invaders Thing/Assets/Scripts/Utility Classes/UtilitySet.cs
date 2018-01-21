using UnityEngine;

public abstract class UtilitySet : MonoBehaviour
{
    public static void MoveOnKeyboardPress(Transform t, KeyCode key, Vector3 direction)
    {
        if (Input.GetKey(key))
            t.Translate(direction * Time.deltaTime);
    }

    public static void MoveOnKeyboardPress(Transform t, KeyCode[] keys, Vector3 direction)
    {
        foreach (KeyCode key in keys)
        {
            if(Input.GetKey(key))
                t.Translate(direction * Time.deltaTime);
        }
    }

    public static void moveIfOutOfBounds(Transform t, float Position, float Bound1, float Bound2, Vector3 direction)
    {
        if (Position < Bound1 || Position > Bound2)
            t.Translate(direction * Time.deltaTime);
    }

    public static bool CheckIfOutOfBounds(float Position, float Bound1, float Bound2)
    {
        if (Position < Bound1 || Position > Bound2)
            return true;

        return false;
    }

    public static void moveIfOnALine(Transform t, float line, float positionOnAxis, Vector3 direction)
    {
        if (positionOnAxis == line)
            t.Translate(direction * Time.deltaTime);
    }

    public static void moveIfOnALine(Transform t, float[] FEArray, float positionCoOrd, Vector3 direction)
    {
        foreach (float i in FEArray)
            if (positionCoOrd == i)
                t.Translate(direction * Time.deltaTime);
    }    

    public static void DestroyAndSpawn(GameObject objForDestruction, InstantiationObject InstObj)
    {
        Instantiate(InstObj.Obj, InstObj.Pos, InstObj.Rot);
        Destroy(objForDestruction);
    }      

    public static void DestroyAndSpawn(InstantiationObject[] InstObjs, GameObject objForDestruction)
    {
        for(int i = 0; i < InstObjs.Length; i++)
        {
            Instantiate(InstObjs[i].Obj, InstObjs[i].Pos, InstObjs[i].Rot);
            if(i == InstObjs.Length)
                Destroy(objForDestruction);
        }
    }

    public static void SpawnObject(bool s, GameObject obj, Vector3 pos, Quaternion rot)
    {
        if(s)
        {
            s = false;
            Instantiate(obj, pos, rot);
        }
    }

    public static void SpawnObjectRandomTime(ref bool doOnce, float LowestRandNum, float HighestRandNum, float Threshold, GameObject obj, Vector3 pos, Quaternion rot)
    {
        if(doOnce)
        {
            doOnce = false;
            float randNum = Random.Range(LowestRandNum, HighestRandNum);
            if(randNum > Threshold)
                Instantiate(obj, pos, rot);
        }
    }

    public static void toggle2Nums(string button, KeyCode key, ref int mode, int n0, int n1)
    {
        if (Input.GetButtonDown(button) || Input.GetKeyDown(key))
        {
            if (mode == n0)
                mode = n1;

            else if (mode == n1)
                mode = n0;
        }
    }

    public static void toggle2Enums(string button, KeyCode key, ref ControlType _controlType, ControlType CT1, ControlType CT2)
    {
        if (Input.GetButtonDown(button) || Input.GetKeyDown(key))
        {
            if (_controlType == CT1)
                _controlType = CT2;

            else if (_controlType == CT2)
                _controlType = CT1;
        }
    }

    public static void toggle2Enums(string button, KeyCode key, ref FiringMode _firingMode, FiringMode FM1, FiringMode FM2)
    {
        if (Input.GetButtonDown(button) || Input.GetKeyDown(key))
        {
            if (_firingMode == FM1)
                _firingMode = FM2;

            else if (_firingMode == FM2)
                _firingMode = FM1;
        }
    }

    public static bool CheckSceneFor1Obj(string Object, ref bool Check)
    {
        if (GameObject.FindGameObjectWithTag(Object) == null)
            Check = true;

        else if (GameObject.FindGameObjectWithTag(Object) != null)
            Check = false;

        return Check;
    }

    public static bool IsOnCooldown(float time, ref bool Check)
    {
        if (time == 0)
            Check = false;

        else if (time > 0)
            Check = true;

        return Check;
    }

    public static void Cooldown(ref bool countCheck, ref float timer, float timeInterval, float cooldownLength)
    {
        if (countCheck)
            timer += timeInterval * Time.deltaTime;

        if (timer >= cooldownLength) { countCheck = false; timer = 0; }
    }

    public static void ColourChangeOnNumber(Renderer[] rArray, float num, float botLim, float topLim, float r, float g, float b, float a)
    {
        foreach (Renderer rend in rArray)
            if (num > botLim && num <= topLim)
                rend.material.color = new Color(r, g, b, a);
    }

    public static bool Set1BoolTrueInArray(ref bool[] bools, int i)
    {
        bools[i] = true;
        return (bools[i]);
    }

    public static bool Set1BoolFalseInArray(bool[] bools, int i)
    {
        bools[i] = false;
        return (bools[i]);
    }

    public static bool JoyAxisIsInUse() { if (Input.GetAxis("Joystick Horizontal") != 0 || Input.GetAxis("Joystick Vertical") != 0) { return true; } return false; }

    public static bool KeyAxisIsInUse() { if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) { return true; } return false; }
        
}