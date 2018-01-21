using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Add this MonoBehaviour to an object and add a UI image object to the object field 'fadingImage'.
/// <para/>
/// Ideally, the image object should be a single colour pane that hides everything else when fully opaque.
/// <para/>
/// When your scene begins, that image object will fade from opaque to transparent.
/// <para/>
/// When you wish to switch scenes, call this behaviour's 'FadeTo(insert scene name as string here)' method from another class.
/// <para/>
/// That method will fade the UI image from transparent to opaque, and the scene will change to the scene in the parameters.
/// </summary>
public class SceneFader : MonoBehaviour
{
    /// <summary>
    /// The image that will fade from opaque to transparent (and vice versa) when the methods 'FadeOut(scene name string)' and 'FadeIn()' are called. 
    /// </summary>
    [SerializeField]
    private Image fadingImage;

    /// <summary>
    /// The curve that controls the way in which the scene will fade. Adjustable in the Inspector. 
    /// </summary>
    [SerializeField]
    private AnimationCurve fadeCurve;

    /// <summary>
    /// The total time needed for the UI image to fade from opaque to transparent.
    /// </summary>
    [SerializeField]
    private float duration = 1F;

    /// <summary>
    /// When the scene is first loaded, this starts the coroutine 'FadeIn()'.
    /// </summary>
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Call this from another script when you want to switch scenes. 
    /// </summary>
    /// <param name="scene"></param>
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    
    ///<summary>
    ///Starts a timer at 1 second, and decreases it over a period of seconds of length 'duration'.
    ///The alpha level of the UI image becomes that of the fade curve evaluated at the current time. 
    ///</summary>
    private IEnumerator FadeIn ()
    {
        ///Despite the fact that this coroutine is started and run only once (in 'Start()'),
        ///the 'while' loop below will run continuously until the 'while' condition (_time > 0) evaluates to false.
        ///This is owing to the fact that this is a coroutine, and not a method. 
        ///It works independently of 'Update()', and thus it isn't bound by it. 

        ///Declares a local variable, '_time', of value 1.
        float _time = 1F;

        /// While '_time' is greater than 0, 
        while (_time > 0)
        {
            ///it reduces over the period of time specified in 'duration'.
            _time -= Time.deltaTime * (1 / duration);

            ///During that time, a new local variable named '_alpha' is declared,
            ///with its value set to that of its position on the fade curve at the time '_time'.
            float _alpha = fadeCurve.Evaluate(_time);

            /// Finally, the alpha of the UI image object becomes that of the new local '_alpha'. 
            fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.g, fadingImage.color.b, _alpha);

            yield return 0; ///Iterates through the loop again immediately. 
            
            ///The 'yield' statement is added at the end of a loop within a coroutine, 
            ///to wait for the designated amount of time (in this case, nothing) before iterating through the loop once more. 
            ///If you wanted to wait for 1 second at the end of every iteration of the coroutine's loop,
            ///you would use 'yield return new WaitForSeconds(1F);' instead. 
        }        
    }

    ///<summary>
    ///Starts a timer at 0, and increases it over a period of seconds of length 'duration'.
    ///The alpha level of the UI image becomes that of the fade curve evaluated at the current time. 
    ///</summary>
    private IEnumerator FadeOut(string scene)
    {
        float _time = 0F;

        while (_time < 1F)
        {
            _time += Time.deltaTime * (1 / duration);

            float _alpha = fadeCurve.Evaluate(_time);

            fadingImage.color = new Color(fadingImage.color.r, fadingImage.color.g, fadingImage.color.b, _alpha);

            yield return 0; ///Wait for one frame.

                            ///Since this is a 'return' statement within a loop in a coroutine, where the loop's condition is being met,
                            ///execution doesn't go beyond this point, meaning that the method 'SceneManager.LoadScene(scene);' isn't called.
                            ///Instead, the loop is iterated through once more, and over and over until the loop condition is no longer met.
                            ///Once that stage is reached, the loop's contents are skipped entirely and the method below is called.
        }

        SceneManager.LoadScene(scene);
    }
}