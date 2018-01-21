using UnityEngine;
using System.Collections;

public class YieldingStuff : MonoBehaviour
{
    //Coroutines require a method to work, but not one with a 'void' return type, or indeed any usual return type: they require 'IENumerator' methods.

    public bool check;
    public bool check0 = false;

    void Start ()
    {
        check = false;
        StartCoroutine(method0(5, "hello", "goodbye", check));
    }

    IEnumerator method0 (float t, string word0, string word1, bool c)
    {
        print(word0);
        yield return new WaitForSeconds(t);
        print(word1);
    }

    /*void Start ()
    {
        StartCoroutine(Go(5F, "Michael, ", "SomeCompany."));
    }
     
    IEnumerator Go(float wait, string text0, string text1)
    {
        print(Time.time + text0 + text1);
        yield return new WaitForSeconds(wait);
        print(Time.time);
    }*/


}